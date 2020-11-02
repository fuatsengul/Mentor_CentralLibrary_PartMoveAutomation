using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CL_PartitionUpdater
{
    public partial class Form1 : Form
    {
        PartsClass _parts;

        public Form1()
        {
            InitializeComponent();
        }

        LibraryManager.LibraryManagerApp _libApp;
        private void Form1_Load(object sender, EventArgs e)
        {
            _parts = new PartsClass();
            xDM_LibraryManager_Connector.Connector _conn = new xDM_LibraryManager_Connector.Connector();
            _libApp = _conn.Connect2xDMLibraryManager(string.Empty);
            this.Text += string.Format(" [connected to {0}]", _libApp.ActiveLibrary.FullName);
            RefreshData();
            _parts.RefreshDataGrid = new EventHandler(BGRefreshData);
            _parts.ResolveCompleted = new EventHandler(ResolveCompleted);
            _parts.ReplaceCompleted = new EventHandler(ReplaceCompleted);
        }


        void BGRefreshData(object sender, EventArgs e)
        {
            RefreshData();
        }

        void ResolveCompleted(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            label1.Visible = false;

            RefreshData();
        }


        void ReplaceCompleted(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            label1.Visible = false;
            RefreshData();
        }

        private void pinsGrid_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.Control && e.KeyCode == Keys.C)
            {
                DataObject d = dgv.GetClipboardContent();
                Clipboard.SetDataObject(d);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                try
                {
                    string s = Clipboard.GetText();
                    s = s.Substring(0, s.LastIndexOf("\r\n"));
                    s = s.Replace("\r", "");
                    string[] lines = s.Split('\n');

                    if (lines.Count() != PartsGrid.SelectedRows.Count)
                    {
                        int pin2Add = lines.Count() - PartsGrid.SelectedRows.Count;
                        for (int i = 0; i < pin2Add; i++)
                        {
                            _parts.Parts.Add(new PartClass() { _id = Guid.NewGuid().ToString() });

                        }
                        RefreshData();
                    }

                    if (dgv.CurrentCell == null)
                        dgv.CurrentCell = dgv.Rows[0].Cells[1];

                    int row = dgv.CurrentCell.RowIndex;
                    int col = dgv.CurrentCell.ColumnIndex;
                    foreach (string line in lines)
                    {
                        if (row < dgv.RowCount && line.Length > 0)
                        {
                            string[] cells = line.Split('\t');
                            if(cells.Length > 0)
                            {
                                dgv[1, row].Value = Convert.ChangeType(cells[0].Trim(), dgv[1, row].ValueType);
                                dgv[2, row].Value = Convert.ChangeType(cells[1].Trim(), dgv[2, row].ValueType);
                            }
                            row++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                catch (Exception m)
                {
                    MessageBox.Show("Illegal Paste Operation\r\n" + m.Message + "\r\n" + m.Source);
                }
            }
        }

        private void clearDocumentButton_Click(object sender, EventArgs e)
        {
            _parts.Parts.Clear();
            RefreshData();
        }

        private void sortByPinNumber_Click(object sender, EventArgs e)
        {
            var firstPart = _parts.Parts.Where(x => !String.IsNullOrEmpty(x.targetPartition) && x.targetPartition.All(char.IsDigit)).OrderBy(x => Convert.ToInt32(x.targetPartition));
            var secondPart = _parts.Parts.Where(x => !String.IsNullOrEmpty(x.targetPartition) && !x.targetPartition.All(char.IsDigit)).OrderBy(x => x.targetPartition);
            var thirdPart = _parts.Parts.Where(x => String.IsNullOrEmpty(x.targetPartition)).ToList();

            _parts.Parts = firstPart.Union(secondPart).Union(thirdPart).ToList();

            RefreshData();
        }

        private void addRowButton_Click(object sender, EventArgs e)
        {
            _parts.Parts.Add(new PartClass() { _id = Guid.NewGuid().ToString() });
            RefreshData();
        }

        private void RefreshData()
        {

            PartsGrid.Invoke(new MethodInvoker(delegate
            {
                PartsGrid.DataSource = _parts.Parts.ToList();
                PartsGrid.Columns["_id"].Visible = false;

                Parallel.ForEach(PartsGrid.Rows.OfType<DataGridViewRow>(), row =>
                {
                    var _part = _parts.Parts.Find(x => x._id == row.Cells["_id"].Value);
                    if (_part != null)
                    {
                        switch (_part.status)
                        {
                            case PartClass.Status.Completed: row.DefaultCellStyle.BackColor = Color.LightGreen; break;
                            case PartClass.Status.Unknown: row.DefaultCellStyle.BackColor = Color.LightGray; break;
                            case PartClass.Status.Ready: row.DefaultCellStyle.BackColor = Color.LightYellow; break;
                            case PartClass.Status.Resolving: row.DefaultCellStyle.BackColor = Color.LightBlue; break;
                            case PartClass.Status.Target_Partition_Not_Found: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                            case PartClass.Status.Part_Not_Found: row.DefaultCellStyle.BackColor = Color.LightPink; break;
                        }
                    }
                });

                PartsGrid.ClearSelection();
            }));

            RunButton.Invoke(new MethodInvoker(delegate
            {
                if (_parts.Parts.FindAll(x => x.status == PartClass.Status.Ready).Count > 0)
                {
                    RunButton.Enabled = true;
                }
                else
                {
                    RunButton.Enabled = false;
                }
            }));

        }

        private void removeSelectedRowBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow _row in PartsGrid.SelectedRows)
            {
                string _id = _row.Cells["_id"].Value.ToString();

                _parts.Parts.Remove(_parts.Parts.Find(x => x._id == _id));
            }

            RefreshData();
        }

        private void ResolveParts(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            label1.Visible = true;
            label1.Text = "Resolving";
            _parts.ResolveParts(_libApp);
        }


        private void cacheRecallBtn_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            label1.Visible = true;
            label1.Text = "Running";
            _parts.ReplacePartitionNames(_libApp);
        }
    }
}
