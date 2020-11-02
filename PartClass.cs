using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_PartitionUpdater
{
    public class PartClass
    {
        public PartClass()
        { }

        public string _id { set; get; }
        [DisplayName("Part Number")]
        public string partNumber { set; get; }

        [DisplayName("Target Partition")]
        public string targetPartition { set; get; }

        [DisplayName("Current Partition")]
        public string currentPartition { set; get; }

        
        Status _stat = Status.Unknown;

        public Status status { set { _stat = value; } get { return _stat; } }

        public enum Status
        {
            Unknown = 0,
            Ready = 1,
            Completed = 2,
            Target_Partition_Not_Found = 4,
            Working = 8,
            Resolving = 16,
            Part_Not_Found = 32
        }
    }
    public class PartsClass
    {
        public PartsClass() { }

        private List<PartClass> _parts = new List<PartClass>();
        public List<PartClass> Parts { set { _parts = value; } get { return _parts; } }

        public EventHandler RefreshDataGrid;
        public EventHandler ResolveCompleted;
        public EventHandler ReplaceCompleted;


        public void ResolveParts(LibraryManager.LibraryManagerApp _libApp)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += delegate
            {
                MGCPCBPartsEditor.PartsEditorDlg _dlg = (MGCPCBPartsEditor.PartsEditorDlg)_libApp.ActiveLibrary.PartEditor;
                try
                {
                    _parts.ForEach(_part =>
                    {
                        _part.status = PartClass.Status.Resolving;
                        RefreshDataGrid(null, null);

                        if (_dlg.ActiveDatabaseEx.get_Partitions(_part.targetPartition).Count == 0)
                        {
                            _part.status = PartClass.Status.Target_Partition_Not_Found;
                            RefreshDataGrid(null, null);
                        }
                    });


                    foreach (MGCPCBPartsEditor.Partition pt in _dlg.ActiveDatabaseEx.get_Partitions("*"))
                    {
                        _parts.FindAll(x => x.status == PartClass.Status.Resolving).ForEach(_part => {
                            var parts = pt.get_Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll, _part.partNumber, "*", "*");
                            Console.WriteLine(pt.Name + "\t" + parts.Count);

                            if(parts.Count == 1)
                            {
                                _part.currentPartition = pt.Name;
                                if (pt.Name == _part.targetPartition)
                                {
                                    _part.status = PartClass.Status.Completed;
                                }
                                else
                                {
                                    _part.status = PartClass.Status.Ready;
                                }
                                RefreshDataGrid(null, null);
                            }
                        });
                        if (_parts.Count(x => x.status == PartClass.Status.Resolving) == 0)
                        {
                            Console.WriteLine("Breaking");
                            break;
                        }
                    }
                    _parts.FindAll(x => x.status == PartClass.Status.Resolving).ForEach(x => x.status = PartClass.Status.Part_Not_Found);
                    RefreshDataGrid(null, null);
                }
                catch (Exception m)
                {
                    Console.WriteLine(m.Message);
                    Console.WriteLine(m.Source);
                    Console.WriteLine(m.StackTrace);
                }
                finally
                {
                    try
                    {
                        _dlg.UnlockServer();
                    }
                    catch { }
                    _dlg.Quit();
                }
            };
            bw.RunWorkerCompleted += delegate
            {
                ResolveCompleted(null, null);
            };
            bw.RunWorkerAsync();
        }
        public void ReplacePartitionNames(LibraryManager.LibraryManagerApp _libApp)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += delegate
            {
                MGCPCBPartsEditor.PartsEditorDlg _dlg = (MGCPCBPartsEditor.PartsEditorDlg)_libApp.ActiveLibrary.PartEditor;
                try
                {
                    foreach (var _part in Parts.FindAll(x=>x.status == PartClass.Status.Ready))
                    {
                        MGCPCBPartsEditor.Partition target_partition = _dlg.ActiveDatabaseEx.get_Partitions(_part.targetPartition)[1];
                        MGCPCBPartsEditor.Partition current_partition = _dlg.ActiveDatabaseEx.get_Partitions(_part.currentPartition)[1];
                        try
                        {
                            _dlg.LockServer();
                        }
                        catch { }

                        _part.status = PartClass.Status.Working;
                        this.RefreshDataGrid(null, null);
                        Console.WriteLine(_part.currentPartition + "->" + _part.targetPartition);
                        var _pparts = current_partition.get_Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll, _part.partNumber);
                        MGCPCBPartsEditor.Part __part = _pparts[1];

                        string _temporaryPartNumber = __part.Number + "_OBS_" + new Random().Next(1000,9999);

                        var newPart = target_partition.NewPart();
                        newPart.Name = __part.Name;
                        newPart.Number = _temporaryPartNumber;
                        newPart.Label = __part.Label;
                        newPart.Description = __part.Description;
                        newPart.RefDesPrefix = __part.RefDesPrefix;
                        newPart.Type = __part.Type;

                        newPart.Commit();

                        foreach (MGCPCBPartsEditor.Property prop in __part.Properties)
                            newPart.PutPropertyEx(prop.Name, prop.Value);

                        foreach (MGCPCBPartsEditor.SymbolReference symRef in __part.SymbolReferences)
                            newPart.PinMapping.PutSymbolReference(symRef.Name);

                        foreach (MGCPCBPartsEditor.CellReference cellRef in __part.CellReferences)
                            newPart.PinMapping.PutCellReference(cellRef.Name, cellRef.Type);

                        foreach (MGCPCBPartsEditor.Gate gate in __part.PinMapping.Gates) {
                            var _gate = newPart.PinMapping.PutGate(gate.Name, gate.PinDefinitions.Count, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeLogical);

                            foreach (MGCPCBPartsEditor.PinDefinition def in gate.PinDefinitions)
                                _gate.PutPinDefinition(def.Index, def.SwapIdentifier, def.PinPropertyType, def.PinValueType);

                            foreach(MGCPCBPartsEditor.Slot slot in gate.Slots)
                            {
                                var newSlot = newPart.PinMapping.PutSlot(_gate, slot.SymbolReference);
                                foreach (MGCPCBPartsEditor.PinInstance pin in slot.Pins)
                                    newSlot.PutPin(pin.Index, pin.Number, pin.Name);
                            }
                        }

                        foreach(MGCPCBPartsEditor.Gate gate in __part.PinMapping.Supply)
                        {
                            var _gate = newPart.PinMapping.PutGate(gate.Name, gate.PinDefinitions.Count, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeSupply);

                            foreach (MGCPCBPartsEditor.PinDefinition def in gate.PinDefinitions)
                                _gate.PutPinDefinition(def.Index, def.SwapIdentifier, def.PinPropertyType, def.PinValueType);

                            foreach (MGCPCBPartsEditor.Slot slot in gate.Slots)
                            {
                                var newSlot = newPart.PinMapping.PutSlot(_gate, slot.SymbolReference);
                                foreach (MGCPCBPartsEditor.PinInstance pin in slot.Pins)
                                    newSlot.PutPin(pin.Index, pin.Number, pin.Name);
                            }
                        }

                        foreach (MGCPCBPartsEditor.Gate gate in __part.PinMapping.NoConnect)
                        {
                            var _gate = newPart.PinMapping.PutGate(gate.Name, gate.PinDefinitions.Count, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeNoConnect);

                            foreach (MGCPCBPartsEditor.PinDefinition def in gate.PinDefinitions)
                                _gate.PutPinDefinition(def.Index, def.SwapIdentifier, def.PinPropertyType, def.PinValueType);

                            foreach (MGCPCBPartsEditor.Slot slot in gate.Slots)
                            {
                                var newSlot = newPart.PinMapping.PutSlot(_gate, slot.SymbolReference);
                                foreach (MGCPCBPartsEditor.PinInstance pin in slot.Pins)
                                    newSlot.PutPin(pin.Index, pin.Number, pin.Name);
                            }
                        }

                        foreach (MGCPCBPartsEditor.Gate gate in __part.PinMapping.NoRoute)
                        {
                            var _gate = newPart.PinMapping.PutGate(gate.Name, gate.PinDefinitions.Count, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeNoRoute);

                            foreach (MGCPCBPartsEditor.PinDefinition def in gate.PinDefinitions)
                                _gate.PutPinDefinition(def.Index, def.SwapIdentifier, def.PinPropertyType, def.PinValueType);

                            foreach (MGCPCBPartsEditor.Slot slot in gate.Slots)
                            {
                                var newSlot = newPart.PinMapping.PutSlot(_gate, slot.SymbolReference);
                                foreach (MGCPCBPartsEditor.PinInstance pin in slot.Pins)
                                    newSlot.PutPin(pin.Index, pin.Number, pin.Name);
                            }
                        }
                        newPart.PinMapping.Commit();

                        Console.WriteLine("Old part pin count: " + __part.PinMapping.PinCount);
                        Console.WriteLine("New part pin count: " + newPart.PinMapping.PinCount);

                        _dlg.SaveActiveDatabase();

                        if (newPart.PinMapping.PinCount == __part.PinMapping.PinCount)
                        {
                            if (!newPart.Incomplete)
                            {
                                string pn = __part.Number;
                                __part.Delete();
                                newPart.Number = pn;
                            }
                        }

                        _dlg.SaveActiveDatabase();
                        _part.status = PartClass.Status.Completed;
                        RefreshDataGrid(null, null);
                    }
                }
                catch (Exception m) { System.Windows.Forms.MessageBox.Show(m.Message + "\r\n" + m.StackTrace + "\r\n" + m.Source); }
                finally
                {
                    try
                    {
                        _dlg.UnlockServer();
                    }
                    catch { }
                    _dlg.Quit();
                }

                Parallel.ForEach(_parts.FindAll(x => x.status == PartClass.Status.Working), _part =>
                {
                    _part.status = PartClass.Status.Target_Partition_Not_Found;
                });

            };

            bw.RunWorkerCompleted += delegate
            {
                ReplaceCompleted(null, null);
            };

            bw.RunWorkerAsync();
        }
    }


}
