
namespace TibiantisHelper.Tabs
{
    partial class Tab_Accounts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tab_Accounts));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.checkBox_hideAcc = new System.Windows.Forms.CheckBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.accounts_label_selected = new System.Windows.Forms.Label();
            this.button_addAccount = new System.Windows.Forms.Button();
            this.button_removeAccount = new System.Windows.Forms.Button();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(783, 415);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.checkBox_hideAcc);
            this.splitContainer2.Panel1.Controls.Add(this.textBox10);
            this.splitContainer2.Panel1.Controls.Add(this.accounts_label_selected);
            this.splitContainer2.Panel1.Controls.Add(this.button_addAccount);
            this.splitContainer2.Panel1.Controls.Add(this.button_removeAccount);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBox12);
            this.splitContainer2.Size = new System.Drawing.Size(235, 415);
            this.splitContainer2.SplitterDistance = 177;
            this.splitContainer2.TabIndex = 3;
            // 
            // checkBox_hideAcc
            // 
            this.checkBox_hideAcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_hideAcc.AutoSize = true;
            this.checkBox_hideAcc.Checked = global::TibiantisHelper.Properties.Settings.Default.AccountsHideLogin;
            this.checkBox_hideAcc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hideAcc.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TibiantisHelper.Properties.Settings.Default, "AccountsHideLogin", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_hideAcc.Location = new System.Drawing.Point(4, 157);
            this.checkBox_hideAcc.Name = "checkBox_hideAcc";
            this.checkBox_hideAcc.Size = new System.Drawing.Size(131, 17);
            this.checkBox_hideAcc.TabIndex = 5;
            this.checkBox_hideAcc.Text = "Hide Account Number";
            this.checkBox_hideAcc.UseVisualStyleBackColor = true;
            this.checkBox_hideAcc.CheckedChanged += new System.EventHandler(this.checkBox_hideAcc_CheckedChanged);
            // 
            // textBox10
            // 
            this.textBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox10.Location = new System.Drawing.Point(0, 21);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox10.Size = new System.Drawing.Size(235, 105);
            this.textBox10.TabIndex = 4;
            this.textBox10.WordWrap = false;
            // 
            // accounts_label_selected
            // 
            this.accounts_label_selected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.accounts_label_selected.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accounts_label_selected.Location = new System.Drawing.Point(1, 0);
            this.accounts_label_selected.Name = "accounts_label_selected";
            this.accounts_label_selected.Size = new System.Drawing.Size(231, 18);
            this.accounts_label_selected.TabIndex = 3;
            this.accounts_label_selected.Text = "No account selected";
            this.accounts_label_selected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_addAccount
            // 
            this.button_addAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_addAccount.Location = new System.Drawing.Point(0, 132);
            this.button_addAccount.Name = "button_addAccount";
            this.button_addAccount.Size = new System.Drawing.Size(115, 23);
            this.button_addAccount.TabIndex = 0;
            this.button_addAccount.Text = "Add Account";
            this.button_addAccount.UseVisualStyleBackColor = true;
            this.button_addAccount.Click += new System.EventHandler(this.button_addAccount_Click);
            // 
            // button_removeAccount
            // 
            this.button_removeAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_removeAccount.Location = new System.Drawing.Point(121, 132);
            this.button_removeAccount.Name = "button_removeAccount";
            this.button_removeAccount.Size = new System.Drawing.Size(115, 23);
            this.button_removeAccount.TabIndex = 1;
            this.button_removeAccount.Text = "Remove Account";
            this.button_removeAccount.UseVisualStyleBackColor = true;
            this.button_removeAccount.Click += new System.EventHandler(this.button_removeAccount_Click);
            // 
            // textBox12
            // 
            this.textBox12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox12.Location = new System.Drawing.Point(0, 0);
            this.textBox12.Multiline = true;
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox12.Size = new System.Drawing.Size(235, 234);
            this.textBox12.TabIndex = 0;
            this.textBox12.Text = resources.GetString("textBox12.Text");
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(544, 415);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView_CellBeginEdit);
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
            this.dataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView_DataBindingComplete);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            this.dataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseDown);
            // 
            // Tab_Accounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "Tab_Accounts";
            this.Size = new System.Drawing.Size(783, 415);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox checkBox_hideAcc;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label accounts_label_selected;
        private System.Windows.Forms.Button button_addAccount;
        private System.Windows.Forms.Button button_removeAccount;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}
