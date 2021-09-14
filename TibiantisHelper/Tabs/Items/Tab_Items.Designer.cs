
namespace TibiantisHelper.Tabs
{
    partial class Tab_Items
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.items_checkBox_hideExpiring = new System.Windows.Forms.CheckBox();
            this.items_comboBox_itemCategory = new System.Windows.Forms.ComboBox();
            this.itemName_label = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.itemIcon = new System.Windows.Forms.PictureBox();
            this.listView_itemDrops = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn10 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn12 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn11 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.listView_items = new BrightIdeasSoftware.ObjectListView();
            this.listView_trades = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listView_itemDrops)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listView_items)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listView_trades)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer1.Size = new System.Drawing.Size(882, 418);
            this.splitContainer1.SplitterDistance = 237;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.items_checkBox_hideExpiring);
            this.splitContainer4.Panel1.Controls.Add(this.items_comboBox_itemCategory);
            this.splitContainer4.Panel1.Controls.Add(this.itemName_label);
            this.splitContainer4.Panel1.Controls.Add(this.textBox1);
            this.splitContainer4.Panel1.Controls.Add(this.itemIcon);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.listView_itemDrops);
            this.splitContainer4.Size = new System.Drawing.Size(237, 418);
            this.splitContainer4.SplitterDistance = 228;
            this.splitContainer4.TabIndex = 0;
            // 
            // items_checkBox_hideExpiring
            // 
            this.items_checkBox_hideExpiring.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.items_checkBox_hideExpiring.AutoSize = true;
            this.items_checkBox_hideExpiring.Location = new System.Drawing.Point(3, 209);
            this.items_checkBox_hideExpiring.Name = "items_checkBox_hideExpiring";
            this.items_checkBox_hideExpiring.Size = new System.Drawing.Size(138, 17);
            this.items_checkBox_hideExpiring.TabIndex = 4;
            this.items_checkBox_hideExpiring.Text = "Hide Expiring Items";
            this.items_checkBox_hideExpiring.UseVisualStyleBackColor = true;
            this.items_checkBox_hideExpiring.CheckedChanged += new System.EventHandler(this.items_checkBox_hideExpiring_CheckedChanged);
            // 
            // items_comboBox_itemCategory
            // 
            this.items_comboBox_itemCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.items_comboBox_itemCategory.FormattingEnabled = true;
            this.items_comboBox_itemCategory.Items.AddRange(new object[] {
            "--- Armors ---",
            "Helmet",
            "Shield",
            "Armor",
            "Legs",
            "Boots",
            "Necklace",
            "Ring",
            "",
            "--- Weapons ---",
            "Axe",
            "Club",
            "Sword",
            "Distance",
            "Ammo",
            "",
            "--- Other ---",
            "Runes",
            "Light",
            "Food",
            "Equippable Container",
            "",
            "",
            "All"});
            this.items_comboBox_itemCategory.Location = new System.Drawing.Point(3, 3);
            this.items_comboBox_itemCategory.Name = "items_comboBox_itemCategory";
            this.items_comboBox_itemCategory.Size = new System.Drawing.Size(230, 21);
            this.items_comboBox_itemCategory.TabIndex = 0;
            this.items_comboBox_itemCategory.SelectedIndexChanged += new System.EventHandler(this.items_comboBox_itemCategory_SelectedIndexChanged);
            // 
            // itemName_label
            // 
            this.itemName_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemName_label.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemName_label.Location = new System.Drawing.Point(48, 27);
            this.itemName_label.Name = "itemName_label";
            this.itemName_label.Size = new System.Drawing.Size(186, 35);
            this.itemName_label.TabIndex = 2;
            this.itemName_label.Text = "No item selected";
            this.itemName_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 65);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(230, 138);
            this.textBox1.TabIndex = 3;
            // 
            // itemIcon
            // 
            this.itemIcon.Location = new System.Drawing.Point(3, 30);
            this.itemIcon.Name = "itemIcon";
            this.itemIcon.Size = new System.Drawing.Size(37, 32);
            this.itemIcon.TabIndex = 1;
            this.itemIcon.TabStop = false;
            // 
            // listView_itemDrops
            // 
            this.listView_itemDrops.AllColumns.Add(this.olvColumn10);
            this.listView_itemDrops.AllColumns.Add(this.olvColumn12);
            this.listView_itemDrops.AllColumns.Add(this.olvColumn11);
            this.listView_itemDrops.CellEditUseWholeCell = false;
            this.listView_itemDrops.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn10,
            this.olvColumn12,
            this.olvColumn11});
            this.listView_itemDrops.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView_itemDrops.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_itemDrops.FullRowSelect = true;
            this.listView_itemDrops.GridLines = true;
            this.listView_itemDrops.HideSelection = false;
            this.listView_itemDrops.Location = new System.Drawing.Point(0, 0);
            this.listView_itemDrops.MultiSelect = false;
            this.listView_itemDrops.Name = "listView_itemDrops";
            this.listView_itemDrops.ShowGroups = false;
            this.listView_itemDrops.Size = new System.Drawing.Size(237, 186);
            this.listView_itemDrops.TabIndex = 0;
            this.listView_itemDrops.UseCompatibleStateImageBehavior = false;
            this.listView_itemDrops.View = System.Windows.Forms.View.Details;
            this.listView_itemDrops.ItemActivate += new System.EventHandler(this.listView_itemDrops_ItemActivate);
            // 
            // olvColumn10
            // 
            this.olvColumn10.FillsFreeSpace = true;
            this.olvColumn10.Text = "Monster";
            // 
            // olvColumn12
            // 
            this.olvColumn12.Text = "Amount";
            this.olvColumn12.Width = 61;
            // 
            // olvColumn11
            // 
            this.olvColumn11.Text = "Drop%";
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.listView_items);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.listView_trades);
            this.splitContainer5.Size = new System.Drawing.Size(640, 418);
            this.splitContainer5.SplitterDistance = 418;
            this.splitContainer5.SplitterWidth = 5;
            this.splitContainer5.TabIndex = 0;
            // 
            // listView_items
            // 
            this.listView_items.AllowColumnReorder = true;
            this.listView_items.CellEditUseWholeCell = false;
            this.listView_items.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView_items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_items.FullRowSelect = true;
            this.listView_items.GridLines = true;
            this.listView_items.HideSelection = false;
            this.listView_items.Location = new System.Drawing.Point(0, 0);
            this.listView_items.MultiSelect = false;
            this.listView_items.Name = "listView_items";
            this.listView_items.ShowGroups = false;
            this.listView_items.Size = new System.Drawing.Size(418, 418);
            this.listView_items.TabIndex = 0;
            this.listView_items.UseCompatibleStateImageBehavior = false;
            this.listView_items.View = System.Windows.Forms.View.Details;
            this.listView_items.SelectionChanged += new System.EventHandler(this.listView_items_SelectionChanged);
            // 
            // listView_trades
            // 
            this.listView_trades.AllColumns.Add(this.olvColumn7);
            this.listView_trades.AllColumns.Add(this.olvColumn8);
            this.listView_trades.AllColumns.Add(this.olvColumn9);
            this.listView_trades.CellEditUseWholeCell = false;
            this.listView_trades.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn7,
            this.olvColumn8,
            this.olvColumn9});
            this.listView_trades.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView_trades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_trades.FullRowSelect = true;
            this.listView_trades.GridLines = true;
            this.listView_trades.HideSelection = false;
            this.listView_trades.Location = new System.Drawing.Point(0, 0);
            this.listView_trades.Name = "listView_trades";
            this.listView_trades.Size = new System.Drawing.Size(217, 418);
            this.listView_trades.TabIndex = 0;
            this.listView_trades.UseCompatibleStateImageBehavior = false;
            this.listView_trades.View = System.Windows.Forms.View.Details;
            this.listView_trades.ItemActivate += new System.EventHandler(this.listView_trades_ItemActivate);
            // 
            // olvColumn7
            // 
            this.olvColumn7.FillsFreeSpace = true;
            this.olvColumn7.Text = "Name";
            // 
            // olvColumn8
            // 
            this.olvColumn8.Text = "Price";
            // 
            // olvColumn9
            // 
            this.olvColumn9.Text = "Location";
            // 
            // Tab_Items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Tab_Items";
            this.Size = new System.Drawing.Size(882, 418);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.itemIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listView_itemDrops)).EndInit();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listView_items)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listView_trades)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.CheckBox items_checkBox_hideExpiring;
        private System.Windows.Forms.ComboBox items_comboBox_itemCategory;
        private System.Windows.Forms.Label itemName_label;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox itemIcon;
        private BrightIdeasSoftware.ObjectListView listView_itemDrops;
        private BrightIdeasSoftware.OLVColumn olvColumn10;
        private BrightIdeasSoftware.OLVColumn olvColumn12;
        private BrightIdeasSoftware.OLVColumn olvColumn11;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private BrightIdeasSoftware.ObjectListView listView_items;
        private BrightIdeasSoftware.ObjectListView listView_trades;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private BrightIdeasSoftware.OLVColumn olvColumn9;
    }
}
