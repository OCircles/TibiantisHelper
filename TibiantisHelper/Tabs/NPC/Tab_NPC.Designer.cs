
namespace TibiantisHelper.Tabs
{
    partial class Tab_NPC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label_npcName = new System.Windows.Forms.Label();
            this.listView_npcs = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.listView_npcItems = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listView_npcs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listView_npcItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listView_npcs);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listView_npcItems);
            this.splitContainer2.Size = new System.Drawing.Size(546, 444);
            this.splitContainer2.SplitterDistance = 321;
            this.splitContainer2.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(0, 55);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(233, 389);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "This will have useful information one day!\r\n\r\n- Topic counts\r\n- Total interaction" +
    "s\r\n- Associated questlines\r\n\r\nNot because anyone actually wants it, but because " +
    "I can";
            // 
            // label_npcName
            // 
            this.label_npcName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_npcName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_npcName.Location = new System.Drawing.Point(3, 0);
            this.label_npcName.Name = "label_npcName";
            this.label_npcName.Size = new System.Drawing.Size(230, 52);
            this.label_npcName.TabIndex = 1;
            this.label_npcName.Text = "No NPC selected";
            this.label_npcName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView_npcs
            // 
            this.listView_npcs.AllColumns.Add(this.olvColumn1);
            this.listView_npcs.AllColumns.Add(this.olvColumn2);
            this.listView_npcs.AllColumns.Add(this.olvColumn3);
            this.listView_npcs.AllColumns.Add(this.olvColumn4);
            this.listView_npcs.CellEditUseWholeCell = false;
            this.listView_npcs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.listView_npcs.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView_npcs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_npcs.FullRowSelect = true;
            this.listView_npcs.GridLines = true;
            this.listView_npcs.HideSelection = false;
            this.listView_npcs.Location = new System.Drawing.Point(0, 0);
            this.listView_npcs.MultiSelect = false;
            this.listView_npcs.Name = "listView_npcs";
            this.listView_npcs.ShowGroups = false;
            this.listView_npcs.Size = new System.Drawing.Size(321, 444);
            this.listView_npcs.TabIndex = 0;
            this.listView_npcs.UseCompatibleStateImageBehavior = false;
            this.listView_npcs.View = System.Windows.Forms.View.Details;
            this.listView_npcs.SelectionChanged += new System.EventHandler(this.listView_npcs_SelectionChanged);
            this.listView_npcs.ItemActivate += new System.EventHandler(this.listView_npcs_ItemActivate);
            this.listView_npcs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_npcs_MouseClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 144;
            // 
            // olvColumn2
            // 
            this.olvColumn2.Text = "Location";
            this.olvColumn2.Width = 126;
            // 
            // olvColumn3
            // 
            this.olvColumn3.Text = "Vendor";
            // 
            // olvColumn4
            // 
            this.olvColumn4.Text = "Spells";
            // 
            // listView_npcItems
            // 
            this.listView_npcItems.AllColumns.Add(this.olvColumn5);
            this.listView_npcItems.AllColumns.Add(this.olvColumn6);
            this.listView_npcItems.CellEditUseWholeCell = false;
            this.listView_npcItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn5,
            this.olvColumn6});
            this.listView_npcItems.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView_npcItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_npcItems.FullRowSelect = true;
            this.listView_npcItems.GridLines = true;
            this.listView_npcItems.HideSelection = false;
            this.listView_npcItems.Location = new System.Drawing.Point(0, 0);
            this.listView_npcItems.Name = "listView_npcItems";
            this.listView_npcItems.Size = new System.Drawing.Size(221, 444);
            this.listView_npcItems.TabIndex = 0;
            this.listView_npcItems.UseCompatibleStateImageBehavior = false;
            this.listView_npcItems.View = System.Windows.Forms.View.Details;
            this.listView_npcItems.ItemActivate += new System.EventHandler(this.listView_npcItems_ItemActivate);
            // 
            // olvColumn5
            // 
            this.olvColumn5.FillsFreeSpace = true;
            this.olvColumn5.Text = "Item";
            this.olvColumn5.Width = 145;
            // 
            // olvColumn6
            // 
            this.olvColumn6.Text = "Gold";
            this.olvColumn6.Width = 65;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label_npcName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(785, 444);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 7;
            // 
            // Tab_Npcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Tab_Npcs";
            this.Size = new System.Drawing.Size(785, 444);
            this.Load += new System.EventHandler(this.Tab_Npcs_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listView_npcs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listView_npcItems)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer2;
        private BrightIdeasSoftware.ObjectListView listView_npcs;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.ObjectListView listView_npcItems;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label_npcName;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
