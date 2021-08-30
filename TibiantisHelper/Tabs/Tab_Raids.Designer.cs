
namespace TibiantisHelper.Tabs
{
    partial class Tab_Raids
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
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label_remaining = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_raidName = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Interval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_LastSeen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_NextTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader_Empty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Race = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Delay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Spread = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_ItemDrops = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Lifetime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Position = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label_lastSeen = new System.Windows.Forms.Label();
            this.label_nextRaid = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.control_MinimapViewer1 = new TibiantisHelper.Control_MinimapViewer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(718, 424);
            this.splitContainer1.SplitterDistance = 239;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.control_MinimapViewer1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.textBox1);
            this.splitContainer3.Panel2.Controls.Add(this.label_nextRaid);
            this.splitContainer3.Panel2.Controls.Add(this.label_lastSeen);
            this.splitContainer3.Panel2.Controls.Add(this.label_remaining);
            this.splitContainer3.Panel2.Controls.Add(this.label3);
            this.splitContainer3.Panel2.Controls.Add(this.label2);
            this.splitContainer3.Panel2.Controls.Add(this.label_raidName);
            this.splitContainer3.Size = new System.Drawing.Size(239, 424);
            this.splitContainer3.SplitterDistance = 168;
            this.splitContainer3.TabIndex = 0;
            // 
            // label_remaining
            // 
            this.label_remaining.Location = new System.Drawing.Point(3, 60);
            this.label_remaining.Name = "label_remaining";
            this.label_remaining.Size = new System.Drawing.Size(185, 14);
            this.label_remaining.TabIndex = 3;
            this.label_remaining.Text = "Remaining: xx-xx-xx";
            this.label_remaining.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Next raid:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Last seen:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_raidName
            // 
            this.label_raidName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_raidName.Location = new System.Drawing.Point(0, 0);
            this.label_raidName.Name = "label_raidName";
            this.label_raidName.Size = new System.Drawing.Size(239, 20);
            this.label_raidName.TabIndex = 0;
            this.label_raidName.Text = "Raid Name";
            this.label_raidName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listView1);
            this.splitContainer2.Size = new System.Drawing.Size(475, 424);
            this.splitContainer2.SplitterDistance = 283;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Name,
            this.Column_Interval,
            this.Column_LastSeen,
            this.Column_NextTime});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(475, 283);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Column_Name
            // 
            this.Column_Name.HeaderText = "Name";
            this.Column_Name.Name = "Column_Name";
            // 
            // Column_Interval
            // 
            this.Column_Interval.HeaderText = "Interval";
            this.Column_Interval.Name = "Column_Interval";
            // 
            // Column_LastSeen
            // 
            this.Column_LastSeen.HeaderText = "Last Seen";
            this.Column_LastSeen.Name = "Column_LastSeen";
            // 
            // Column_NextTime
            // 
            this.Column_NextTime.HeaderText = "Predicted Time";
            this.Column_NextTime.Name = "Column_NextTime";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Empty,
            this.columnHeader_Race,
            this.columnHeader_Count,
            this.columnHeader_Delay,
            this.columnHeader_Spread,
            this.columnHeader_ItemDrops,
            this.columnHeader_Lifetime,
            this.columnHeader_Position,
            this.columnHeader_Message});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(475, 137);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // columnHeader_Empty
            // 
            this.columnHeader_Empty.Width = 0;
            // 
            // columnHeader_Race
            // 
            this.columnHeader_Race.Text = "Race";
            this.columnHeader_Race.Width = 41;
            // 
            // columnHeader_Count
            // 
            this.columnHeader_Count.Text = "Count";
            this.columnHeader_Count.Width = 45;
            // 
            // columnHeader_Delay
            // 
            this.columnHeader_Delay.Text = "Delay";
            this.columnHeader_Delay.Width = 40;
            // 
            // columnHeader_Spread
            // 
            this.columnHeader_Spread.Text = "Spread";
            this.columnHeader_Spread.Width = 48;
            // 
            // columnHeader_ItemDrops
            // 
            this.columnHeader_ItemDrops.Text = "Items";
            // 
            // columnHeader_Lifetime
            // 
            this.columnHeader_Lifetime.Text = "Lifetime";
            this.columnHeader_Lifetime.Width = 50;
            // 
            // columnHeader_Position
            // 
            this.columnHeader_Position.Text = "Position";
            this.columnHeader_Position.Width = 67;
            // 
            // columnHeader_Message
            // 
            this.columnHeader_Message.Text = "Message";
            this.columnHeader_Message.Width = 59;
            // 
            // label_lastSeen
            // 
            this.label_lastSeen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_lastSeen.Location = new System.Drawing.Point(82, 25);
            this.label_lastSeen.Name = "label_lastSeen";
            this.label_lastSeen.Size = new System.Drawing.Size(157, 14);
            this.label_lastSeen.TabIndex = 4;
            this.label_lastSeen.Text = "xx-xx-xx";
            this.label_lastSeen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_nextRaid
            // 
            this.label_nextRaid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_nextRaid.Location = new System.Drawing.Point(82, 39);
            this.label_nextRaid.Name = "label_nextRaid";
            this.label_nextRaid.Size = new System.Drawing.Size(157, 14);
            this.label_nextRaid.TabIndex = 5;
            this.label_nextRaid.Text = "xx-xx-xx";
            this.label_nextRaid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(0, 82);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(239, 170);
            this.textBox1.TabIndex = 6;
            this.textBox1.WordWrap = false;
            // 
            // control_MinimapViewer1
            // 
            this.control_MinimapViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.control_MinimapViewer1.Framerate = ((byte)(90));
            this.control_MinimapViewer1.Location = new System.Drawing.Point(0, 0);
            this.control_MinimapViewer1.Name = "control_MinimapViewer1";
            this.control_MinimapViewer1.Size = new System.Drawing.Size(239, 168);
            this.control_MinimapViewer1.TabIndex = 0;
            // 
            // Tab_Raids
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "Tab_Raids";
            this.Size = new System.Drawing.Size(718, 424);
            this.Load += new System.EventHandler(this.Tab_Raids_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Interval;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_LastSeen;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_NextTime;
        private System.Windows.Forms.ColumnHeader columnHeader_Delay;
        private System.Windows.Forms.ColumnHeader columnHeader_Position;
        private System.Windows.Forms.ColumnHeader columnHeader_Spread;
        private System.Windows.Forms.ColumnHeader columnHeader_Race;
        private System.Windows.Forms.ColumnHeader columnHeader_Count;
        private System.Windows.Forms.ColumnHeader columnHeader_Lifetime;
        private System.Windows.Forms.ColumnHeader columnHeader_Message;
        private System.Windows.Forms.ColumnHeader columnHeader_ItemDrops;
        private System.Windows.Forms.ColumnHeader columnHeader_Empty;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label_remaining;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_raidName;
        private Control_MinimapViewer control_MinimapViewer1;
        private System.Windows.Forms.Label label_nextRaid;
        private System.Windows.Forms.Label label_lastSeen;
        private System.Windows.Forms.TextBox textBox1;
    }
}
