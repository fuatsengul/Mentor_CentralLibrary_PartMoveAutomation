namespace CL_PartitionUpdater
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.PartsGrid = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ResolveButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RunButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addRowButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearDocumentButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.removeSelectedRowBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.sortByPinNumber = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.PartsGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PartsGrid
            // 
            this.PartsGrid.AllowUserToAddRows = false;
            this.PartsGrid.AllowUserToDeleteRows = false;
            this.PartsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PartsGrid.BackgroundColor = System.Drawing.Color.White;
            this.PartsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PartsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PartsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.PartsGrid.Location = new System.Drawing.Point(0, 25);
            this.PartsGrid.Name = "PartsGrid";
            this.PartsGrid.RowHeadersVisible = false;
            this.PartsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PartsGrid.Size = new System.Drawing.Size(779, 359);
            this.PartsGrid.TabIndex = 13;
            this.PartsGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pinsGrid_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.ResolveButton);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.RunButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 384);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20, 5, 10, 5);
            this.panel1.Size = new System.Drawing.Size(779, 35);
            this.panel1.TabIndex = 12;
            // 
            // ResolveButton
            // 
            this.ResolveButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ResolveButton.Location = new System.Drawing.Point(578, 5);
            this.ResolveButton.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.ResolveButton.Name = "ResolveButton";
            this.ResolveButton.Size = new System.Drawing.Size(81, 25);
            this.ResolveButton.TabIndex = 1;
            this.ResolveButton.Text = "Resolve Parts";
            this.ResolveButton.UseVisualStyleBackColor = true;
            this.ResolveButton.Click += new System.EventHandler(this.ResolveParts);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(659, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(20, 25);
            this.panel2.TabIndex = 2;
            // 
            // RunButton
            // 
            this.RunButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.RunButton.Location = new System.Drawing.Point(679, 5);
            this.RunButton.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(90, 25);
            this.RunButton.TabIndex = 0;
            this.RunButton.Text = "Start Operation";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.cacheRecallBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(411, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 8);
            this.progressBar1.MarqueeAnimationSpeed = 20;
            this.progressBar1.Maximum = 20;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(379, 19);
            this.progressBar1.Step = 50;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRowButton,
            this.toolStripSeparator1,
            this.clearDocumentButton,
            this.toolStripSeparator2,
            this.removeSelectedRowBtn,
            this.toolStripSeparator4,
            this.sortByPinNumber});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(779, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addRowButton
            // 
            this.addRowButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addRowButton.Image = ((System.Drawing.Image)(resources.GetObject("addRowButton.Image")));
            this.addRowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addRowButton.Name = "addRowButton";
            this.addRowButton.Size = new System.Drawing.Size(59, 22);
            this.addRowButton.Text = "Add Row";
            this.addRowButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.addRowButton.Click += new System.EventHandler(this.addRowButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // clearDocumentButton
            // 
            this.clearDocumentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearDocumentButton.Image = ((System.Drawing.Image)(resources.GetObject("clearDocumentButton.Image")));
            this.clearDocumentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearDocumentButton.Name = "clearDocumentButton";
            this.clearDocumentButton.Size = new System.Drawing.Size(97, 22);
            this.clearDocumentButton.Text = "&Clear Document";
            this.clearDocumentButton.Click += new System.EventHandler(this.clearDocumentButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // removeSelectedRowBtn
            // 
            this.removeSelectedRowBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.removeSelectedRowBtn.Image = ((System.Drawing.Image)(resources.GetObject("removeSelectedRowBtn.Image")));
            this.removeSelectedRowBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeSelectedRowBtn.Name = "removeSelectedRowBtn";
            this.removeSelectedRowBtn.Size = new System.Drawing.Size(132, 22);
            this.removeSelectedRowBtn.Text = "Remove Selected Rows";
            this.removeSelectedRowBtn.Click += new System.EventHandler(this.removeSelectedRowBtn_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // sortByPinNumber
            // 
            this.sortByPinNumber.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sortByPinNumber.Image = ((System.Drawing.Image)(resources.GetObject("sortByPinNumber.Image")));
            this.sortByPinNumber.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sortByPinNumber.Name = "sortByPinNumber";
            this.sortByPinNumber.Size = new System.Drawing.Size(119, 22);
            this.sortByPinNumber.Text = "Sort By Part Number";
            this.sortByPinNumber.Click += new System.EventHandler(this.sortByPinNumber_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 419);
            this.Controls.Add(this.PartsGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "CL Partition Update";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PartsGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView PartsGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ResolveButton;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addRowButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton clearDocumentButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton removeSelectedRowBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton sortByPinNumber;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
    }
}

