
namespace TibiantisHelper.Tabs
{
    partial class Tab_Spells
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
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.splitContainer9 = new System.Windows.Forms.SplitContainer();
            this.spells_comboBox_premium = new System.Windows.Forms.ComboBox();
            this.spells_comboBox_type = new System.Windows.Forms.ComboBox();
            this.spells_comboBox_group = new System.Windows.Forms.ComboBox();
            this.spells_comboBox_vocation = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.spells_label = new System.Windows.Forms.Label();
            this.listView_npcs = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn33 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn34 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.listView_spells = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn25 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn26 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn30 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn31 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn27 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn36 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn28 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn29 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn35 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn32 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).BeginInit();
            this.splitContainer9.Panel1.SuspendLayout();
            this.splitContainer9.Panel2.SuspendLayout();
            this.splitContainer9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listView_npcs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listView_spells)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer8
            // 
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.splitContainer9);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.listView_spells);
            this.splitContainer8.Size = new System.Drawing.Size(898, 397);
            this.splitContainer8.SplitterDistance = 212;
            this.splitContainer8.SplitterWidth = 5;
            this.splitContainer8.TabIndex = 1;
            // 
            // splitContainer9
            // 
            this.splitContainer9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer9.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer9.IsSplitterFixed = true;
            this.splitContainer9.Location = new System.Drawing.Point(0, 0);
            this.splitContainer9.Name = "splitContainer9";
            this.splitContainer9.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer9.Panel1
            // 
            this.splitContainer9.Panel1.Controls.Add(this.spells_comboBox_premium);
            this.splitContainer9.Panel1.Controls.Add(this.spells_comboBox_type);
            this.splitContainer9.Panel1.Controls.Add(this.spells_comboBox_group);
            this.splitContainer9.Panel1.Controls.Add(this.spells_comboBox_vocation);
            this.splitContainer9.Panel1.Controls.Add(this.label11);
            this.splitContainer9.Panel1.Controls.Add(this.label10);
            this.splitContainer9.Panel1.Controls.Add(this.label9);
            this.splitContainer9.Panel1.Controls.Add(this.label8);
            this.splitContainer9.Panel1.Controls.Add(this.spells_label);
            // 
            // splitContainer9.Panel2
            // 
            this.splitContainer9.Panel2.Controls.Add(this.listView_npcs);
            this.splitContainer9.Size = new System.Drawing.Size(212, 397);
            this.splitContainer9.SplitterDistance = 170;
            this.splitContainer9.TabIndex = 0;
            // 
            // spells_comboBox_premium
            // 
            this.spells_comboBox_premium.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spells_comboBox_premium.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spells_comboBox_premium.FormattingEnabled = true;
            this.spells_comboBox_premium.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.spells_comboBox_premium.Location = new System.Drawing.Point(79, 139);
            this.spells_comboBox_premium.Name = "spells_comboBox_premium";
            this.spells_comboBox_premium.Size = new System.Drawing.Size(129, 21);
            this.spells_comboBox_premium.TabIndex = 8;
            this.spells_comboBox_premium.SelectedIndexChanged += new System.EventHandler(this.comboBox_filters_SelectedIndexChanged);
            // 
            // spells_comboBox_type
            // 
            this.spells_comboBox_type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spells_comboBox_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spells_comboBox_type.FormattingEnabled = true;
            this.spells_comboBox_type.Items.AddRange(new object[] {
            "All",
            "Instant",
            "Rune"});
            this.spells_comboBox_type.Location = new System.Drawing.Point(79, 112);
            this.spells_comboBox_type.Name = "spells_comboBox_type";
            this.spells_comboBox_type.Size = new System.Drawing.Size(129, 21);
            this.spells_comboBox_type.TabIndex = 7;
            this.spells_comboBox_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_filters_SelectedIndexChanged);
            // 
            // spells_comboBox_group
            // 
            this.spells_comboBox_group.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spells_comboBox_group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spells_comboBox_group.FormattingEnabled = true;
            this.spells_comboBox_group.Items.AddRange(new object[] {
            "All",
            "Healing",
            "Support",
            "Attack"});
            this.spells_comboBox_group.Location = new System.Drawing.Point(79, 85);
            this.spells_comboBox_group.Name = "spells_comboBox_group";
            this.spells_comboBox_group.Size = new System.Drawing.Size(129, 21);
            this.spells_comboBox_group.TabIndex = 6;
            this.spells_comboBox_group.SelectedIndexChanged += new System.EventHandler(this.comboBox_filters_SelectedIndexChanged);
            // 
            // spells_comboBox_vocation
            // 
            this.spells_comboBox_vocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spells_comboBox_vocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spells_comboBox_vocation.FormattingEnabled = true;
            this.spells_comboBox_vocation.Items.AddRange(new object[] {
            "All",
            "Knight",
            "Paladin",
            "Sorcerer",
            "Druid"});
            this.spells_comboBox_vocation.Location = new System.Drawing.Point(79, 58);
            this.spells_comboBox_vocation.Name = "spells_comboBox_vocation";
            this.spells_comboBox_vocation.Size = new System.Drawing.Size(129, 21);
            this.spells_comboBox_vocation.TabIndex = 5;
            this.spells_comboBox_vocation.SelectedIndexChanged += new System.EventHandler(this.comboBox_filters_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 142);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Premium";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Type";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Group";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Vocation";
            // 
            // spells_label
            // 
            this.spells_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spells_label.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spells_label.Location = new System.Drawing.Point(0, 0);
            this.spells_label.Name = "spells_label";
            this.spells_label.Size = new System.Drawing.Size(212, 52);
            this.spells_label.TabIndex = 0;
            this.spells_label.Text = "No spell selected";
            this.spells_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView_npcs
            // 
            this.listView_npcs.AllColumns.Add(this.olvColumn33);
            this.listView_npcs.AllColumns.Add(this.olvColumn34);
            this.listView_npcs.CellEditUseWholeCell = false;
            this.listView_npcs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn33,
            this.olvColumn34});
            this.listView_npcs.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView_npcs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_npcs.FullRowSelect = true;
            this.listView_npcs.GridLines = true;
            this.listView_npcs.HideSelection = false;
            this.listView_npcs.Location = new System.Drawing.Point(0, 0);
            this.listView_npcs.MultiSelect = false;
            this.listView_npcs.Name = "listView_npcs";
            this.listView_npcs.ShowGroups = false;
            this.listView_npcs.Size = new System.Drawing.Size(212, 223);
            this.listView_npcs.TabIndex = 0;
            this.listView_npcs.UseCompatibleStateImageBehavior = false;
            this.listView_npcs.View = System.Windows.Forms.View.Details;
            this.listView_npcs.ItemActivate += new System.EventHandler(this.listView_npcs_ItemActivate);
            // 
            // olvColumn33
            // 
            this.olvColumn33.AspectName = "Name";
            this.olvColumn33.FillsFreeSpace = true;
            this.olvColumn33.Text = "Name";
            // 
            // olvColumn34
            // 
            this.olvColumn34.AspectName = "Location.Name";
            this.olvColumn34.Text = "Location";
            this.olvColumn34.Width = 111;
            // 
            // listView_spells
            // 
            this.listView_spells.AllColumns.Add(this.olvColumn25);
            this.listView_spells.AllColumns.Add(this.olvColumn26);
            this.listView_spells.AllColumns.Add(this.olvColumn30);
            this.listView_spells.AllColumns.Add(this.olvColumn31);
            this.listView_spells.AllColumns.Add(this.olvColumn27);
            this.listView_spells.AllColumns.Add(this.olvColumn36);
            this.listView_spells.AllColumns.Add(this.olvColumn28);
            this.listView_spells.AllColumns.Add(this.olvColumn29);
            this.listView_spells.AllColumns.Add(this.olvColumn35);
            this.listView_spells.AllColumns.Add(this.olvColumn32);
            this.listView_spells.CellEditUseWholeCell = false;
            this.listView_spells.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn25,
            this.olvColumn26,
            this.olvColumn30,
            this.olvColumn31,
            this.olvColumn27,
            this.olvColumn36,
            this.olvColumn28,
            this.olvColumn29,
            this.olvColumn35,
            this.olvColumn32});
            this.listView_spells.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView_spells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_spells.FullRowSelect = true;
            this.listView_spells.GridLines = true;
            this.listView_spells.HideSelection = false;
            this.listView_spells.Location = new System.Drawing.Point(0, 0);
            this.listView_spells.Name = "listView_spells";
            this.listView_spells.ShowGroups = false;
            this.listView_spells.Size = new System.Drawing.Size(681, 397);
            this.listView_spells.TabIndex = 0;
            this.listView_spells.UseCompatibleStateImageBehavior = false;
            this.listView_spells.View = System.Windows.Forms.View.Details;
            this.listView_spells.SelectionChanged += new System.EventHandler(this.listView_spells_SelectionChanged);
            // 
            // olvColumn25
            // 
            this.olvColumn25.AspectName = "Name";
            this.olvColumn25.FillsFreeSpace = true;
            this.olvColumn25.Text = "Name";
            this.olvColumn25.Width = 125;
            // 
            // olvColumn26
            // 
            this.olvColumn26.AspectName = "Words";
            this.olvColumn26.Text = "Words";
            this.olvColumn26.Width = 98;
            // 
            // olvColumn30
            // 
            this.olvColumn30.AspectName = "Price";
            this.olvColumn30.DisplayIndex = 7;
            this.olvColumn30.Text = "Price";
            this.olvColumn30.Width = 55;
            // 
            // olvColumn31
            // 
            this.olvColumn31.AspectName = "Mana";
            this.olvColumn31.DisplayIndex = 8;
            this.olvColumn31.Text = "Mana";
            this.olvColumn31.Width = 55;
            // 
            // olvColumn27
            // 
            this.olvColumn27.AspectName = "Type";
            this.olvColumn27.DisplayIndex = 2;
            this.olvColumn27.Text = "Type";
            // 
            // olvColumn36
            // 
            this.olvColumn36.AspectName = "Group";
            this.olvColumn36.DisplayIndex = 3;
            this.olvColumn36.Text = "Group";
            // 
            // olvColumn28
            // 
            this.olvColumn28.AspectName = "Premium";
            this.olvColumn28.DisplayIndex = 4;
            this.olvColumn28.Text = "Premium";
            this.olvColumn28.Width = 67;
            // 
            // olvColumn29
            // 
            this.olvColumn29.AspectName = "LvlCast";
            this.olvColumn29.DisplayIndex = 5;
            this.olvColumn29.Text = "ML";
            this.olvColumn29.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn29.ToolTipText = "Magic Level";
            this.olvColumn29.Width = 27;
            // 
            // olvColumn35
            // 
            this.olvColumn35.AspectName = "LvlUse";
            this.olvColumn35.DisplayIndex = 6;
            this.olvColumn35.Text = "ML Use";
            this.olvColumn35.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn35.Width = 53;
            // 
            // olvColumn32
            // 
            this.olvColumn32.AspectName = "";
            this.olvColumn32.Sortable = false;
            this.olvColumn32.Text = "Class";
            this.olvColumn32.Width = 75;
            // 
            // Tab_Spells
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer8);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Tab_Spells";
            this.Size = new System.Drawing.Size(898, 397);
            this.Load += new System.EventHandler(this.Tab_Spells_Load);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
            this.splitContainer8.ResumeLayout(false);
            this.splitContainer9.Panel1.ResumeLayout(false);
            this.splitContainer9.Panel1.PerformLayout();
            this.splitContainer9.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).EndInit();
            this.splitContainer9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listView_npcs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listView_spells)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer8;
        private System.Windows.Forms.SplitContainer splitContainer9;
        private System.Windows.Forms.ComboBox spells_comboBox_premium;
        private System.Windows.Forms.ComboBox spells_comboBox_type;
        private System.Windows.Forms.ComboBox spells_comboBox_group;
        private System.Windows.Forms.ComboBox spells_comboBox_vocation;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label spells_label;
        private BrightIdeasSoftware.ObjectListView listView_npcs;
        private BrightIdeasSoftware.OLVColumn olvColumn33;
        private BrightIdeasSoftware.OLVColumn olvColumn34;
        private BrightIdeasSoftware.OLVColumn olvColumn25;
        private BrightIdeasSoftware.OLVColumn olvColumn26;
        private BrightIdeasSoftware.OLVColumn olvColumn30;
        private BrightIdeasSoftware.OLVColumn olvColumn31;
        private BrightIdeasSoftware.OLVColumn olvColumn27;
        private BrightIdeasSoftware.OLVColumn olvColumn36;
        private BrightIdeasSoftware.OLVColumn olvColumn28;
        private BrightIdeasSoftware.OLVColumn olvColumn29;
        private BrightIdeasSoftware.OLVColumn olvColumn35;
        private BrightIdeasSoftware.OLVColumn olvColumn32;
        public BrightIdeasSoftware.ObjectListView listView_spells;
    }
}
