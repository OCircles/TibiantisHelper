
namespace TibiantisHelper.Tabs
{
    partial class Tab_Monsters
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
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.checkBox_showSummonLevel = new System.Windows.Forms.CheckBox();
            this.checkBox_hideUniques = new System.Windows.Forms.CheckBox();
            this.monsters_textBox = new System.Windows.Forms.TextBox();
            this.monsters_labelName = new System.Windows.Forms.Label();
            this.listView_drops = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn21 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn22 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn23 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn24 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.listView_monsters = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn13 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn14 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn18 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn15 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn16 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn17 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn19 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn20 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listView_drops)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listView_monsters)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.splitContainer7);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.listView_monsters);
            this.splitContainer6.Size = new System.Drawing.Size(866, 466);
            this.splitContainer6.SplitterDistance = 335;
            this.splitContainer6.TabIndex = 2;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.checkBox_showSummonLevel);
            this.splitContainer7.Panel1.Controls.Add(this.checkBox_hideUniques);
            this.splitContainer7.Panel1.Controls.Add(this.monsters_textBox);
            this.splitContainer7.Panel1.Controls.Add(this.monsters_labelName);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.listView_drops);
            this.splitContainer7.Size = new System.Drawing.Size(335, 466);
            this.splitContainer7.SplitterDistance = 215;
            this.splitContainer7.TabIndex = 0;
            // 
            // checkBox_showSummonLevel
            // 
            this.checkBox_showSummonLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_showSummonLevel.AutoSize = true;
            this.checkBox_showSummonLevel.Location = new System.Drawing.Point(3, 172);
            this.checkBox_showSummonLevel.Name = "checkBox_showSummonLevel";
            this.checkBox_showSummonLevel.Size = new System.Drawing.Size(146, 17);
            this.checkBox_showSummonLevel.TabIndex = 3;
            this.checkBox_showSummonLevel.Text = "Show Summon Level";
            this.checkBox_showSummonLevel.UseVisualStyleBackColor = true;
            this.checkBox_showSummonLevel.CheckedChanged += new System.EventHandler(this.checkBox_showSummonLevel_CheckedChanged);
            // 
            // checkBox_hideUniques
            // 
            this.checkBox_hideUniques.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_hideUniques.AutoSize = true;
            this.checkBox_hideUniques.Location = new System.Drawing.Point(3, 195);
            this.checkBox_hideUniques.Name = "checkBox_hideUniques";
            this.checkBox_hideUniques.Size = new System.Drawing.Size(149, 17);
            this.checkBox_hideUniques.TabIndex = 2;
            this.checkBox_hideUniques.Text = "Hide Unique Monsters";
            this.checkBox_hideUniques.UseVisualStyleBackColor = true;
            this.checkBox_hideUniques.CheckedChanged += new System.EventHandler(this.checkBox_hideUniques_CheckedChanged);
            // 
            // monsters_textBox
            // 
            this.monsters_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monsters_textBox.Location = new System.Drawing.Point(3, 34);
            this.monsters_textBox.Multiline = true;
            this.monsters_textBox.Name = "monsters_textBox";
            this.monsters_textBox.ReadOnly = true;
            this.monsters_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.monsters_textBox.Size = new System.Drawing.Size(329, 132);
            this.monsters_textBox.TabIndex = 1;
            this.monsters_textBox.WordWrap = false;
            // 
            // monsters_labelName
            // 
            this.monsters_labelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monsters_labelName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monsters_labelName.Location = new System.Drawing.Point(3, 0);
            this.monsters_labelName.Name = "monsters_labelName";
            this.monsters_labelName.Size = new System.Drawing.Size(329, 31);
            this.monsters_labelName.TabIndex = 0;
            this.monsters_labelName.Text = "No monster selected";
            this.monsters_labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView_drops
            // 
            this.listView_drops.AllColumns.Add(this.olvColumn21);
            this.listView_drops.AllColumns.Add(this.olvColumn22);
            this.listView_drops.AllColumns.Add(this.olvColumn23);
            this.listView_drops.AllColumns.Add(this.olvColumn24);
            this.listView_drops.CellEditUseWholeCell = false;
            this.listView_drops.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn21,
            this.olvColumn22,
            this.olvColumn23,
            this.olvColumn24});
            this.listView_drops.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView_drops.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_drops.FullRowSelect = true;
            this.listView_drops.GridLines = true;
            this.listView_drops.HideSelection = false;
            this.listView_drops.Location = new System.Drawing.Point(0, 0);
            this.listView_drops.MultiSelect = false;
            this.listView_drops.Name = "listView_drops";
            this.listView_drops.ShowGroups = false;
            this.listView_drops.Size = new System.Drawing.Size(335, 247);
            this.listView_drops.TabIndex = 0;
            this.listView_drops.UseCompatibleStateImageBehavior = false;
            this.listView_drops.View = System.Windows.Forms.View.Details;
            this.listView_drops.ItemActivate += new System.EventHandler(this.listView_drops_ItemActivate);
            // 
            // olvColumn21
            // 
            this.olvColumn21.FillsFreeSpace = true;
            this.olvColumn21.Text = "Item";
            // 
            // olvColumn22
            // 
            this.olvColumn22.Text = "Amount";
            this.olvColumn22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn22.Width = 57;
            // 
            // olvColumn23
            // 
            this.olvColumn23.Text = "Drop%";
            this.olvColumn23.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn23.Width = 52;
            // 
            // olvColumn24
            // 
            this.olvColumn24.Text = "Sell";
            this.olvColumn24.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listView_monsters
            // 
            this.listView_monsters.AllColumns.Add(this.olvColumn13);
            this.listView_monsters.AllColumns.Add(this.olvColumn14);
            this.listView_monsters.AllColumns.Add(this.olvColumn18);
            this.listView_monsters.AllColumns.Add(this.olvColumn15);
            this.listView_monsters.AllColumns.Add(this.olvColumn16);
            this.listView_monsters.AllColumns.Add(this.olvColumn17);
            this.listView_monsters.AllColumns.Add(this.olvColumn19);
            this.listView_monsters.AllColumns.Add(this.olvColumn20);
            this.listView_monsters.CellEditUseWholeCell = false;
            this.listView_monsters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn13,
            this.olvColumn14,
            this.olvColumn18,
            this.olvColumn15,
            this.olvColumn16,
            this.olvColumn17,
            this.olvColumn19,
            this.olvColumn20});
            this.listView_monsters.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView_monsters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_monsters.FullRowSelect = true;
            this.listView_monsters.GridLines = true;
            this.listView_monsters.HideSelection = false;
            this.listView_monsters.Location = new System.Drawing.Point(0, 0);
            this.listView_monsters.MultiSelect = false;
            this.listView_monsters.Name = "listView_monsters";
            this.listView_monsters.ShowGroups = false;
            this.listView_monsters.Size = new System.Drawing.Size(527, 466);
            this.listView_monsters.TabIndex = 0;
            this.listView_monsters.UseCompatibleStateImageBehavior = false;
            this.listView_monsters.View = System.Windows.Forms.View.Details;
            this.listView_monsters.SelectionChanged += new System.EventHandler(this.listView_monsters_SelectionChanged);
            // 
            // olvColumn13
            // 
            this.olvColumn13.AspectName = "Name";
            this.olvColumn13.FillsFreeSpace = true;
            this.olvColumn13.Text = "Name";
            // 
            // olvColumn14
            // 
            this.olvColumn14.AspectName = "Experience";
            this.olvColumn14.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn14.Text = "Exp";
            this.olvColumn14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn14.Width = 50;
            // 
            // olvColumn18
            // 
            this.olvColumn18.AspectName = "SummonCost";
            this.olvColumn18.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn18.Text = "Summon";
            this.olvColumn18.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn18.Width = 64;
            // 
            // olvColumn15
            // 
            this.olvColumn15.AspectName = "Attack";
            this.olvColumn15.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn15.Text = "Attack";
            this.olvColumn15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn15.Width = 48;
            // 
            // olvColumn16
            // 
            this.olvColumn16.AspectName = "Defense";
            this.olvColumn16.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn16.Text = "Defense";
            this.olvColumn16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn16.Width = 61;
            // 
            // olvColumn17
            // 
            this.olvColumn17.AspectName = "Armor";
            this.olvColumn17.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn17.Text = "Armor";
            this.olvColumn17.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn17.Width = 48;
            // 
            // olvColumn19
            // 
            this.olvColumn19.AspectName = "Speed";
            this.olvColumn19.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn19.Text = "Speed";
            this.olvColumn19.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn19.Width = 49;
            // 
            // olvColumn20
            // 
            this.olvColumn20.AspectName = "Capacity";
            this.olvColumn20.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn20.Text = "Capacity";
            this.olvColumn20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn20.Width = 64;
            // 
            // Tab_Monsters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer6);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Tab_Monsters";
            this.Size = new System.Drawing.Size(866, 466);
            this.Load += new System.EventHandler(this.Tab_Monsters_Load);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel1.PerformLayout();
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listView_drops)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listView_monsters)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.CheckBox checkBox_showSummonLevel;
        private System.Windows.Forms.CheckBox checkBox_hideUniques;
        private System.Windows.Forms.TextBox monsters_textBox;
        private System.Windows.Forms.Label monsters_labelName;
        private BrightIdeasSoftware.ObjectListView listView_drops;
        private BrightIdeasSoftware.OLVColumn olvColumn21;
        private BrightIdeasSoftware.OLVColumn olvColumn22;
        private BrightIdeasSoftware.OLVColumn olvColumn23;
        private BrightIdeasSoftware.OLVColumn olvColumn24;
        public BrightIdeasSoftware.ObjectListView listView_monsters;
        private BrightIdeasSoftware.OLVColumn olvColumn13;
        private BrightIdeasSoftware.OLVColumn olvColumn14;
        private BrightIdeasSoftware.OLVColumn olvColumn18;
        private BrightIdeasSoftware.OLVColumn olvColumn15;
        private BrightIdeasSoftware.OLVColumn olvColumn16;
        private BrightIdeasSoftware.OLVColumn olvColumn17;
        private BrightIdeasSoftware.OLVColumn olvColumn19;
        private BrightIdeasSoftware.OLVColumn olvColumn20;
    }
}
