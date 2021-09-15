
namespace TibiantisHelper.Tabs
{
    partial class Tab_Calculators
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
            this.splitContainer14 = new System.Windows.Forms.SplitContainer();
            this.listBox = new System.Windows.Forms.ListBox();
            this.splitContainer15 = new System.Windows.Forms.SplitContainer();
            this.calculator_tabControl = new System.Windows.Forms.TabControl();
            this.calculator_textBox_result = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer14)).BeginInit();
            this.splitContainer14.Panel1.SuspendLayout();
            this.splitContainer14.Panel2.SuspendLayout();
            this.splitContainer14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer15)).BeginInit();
            this.splitContainer15.Panel1.SuspendLayout();
            this.splitContainer15.Panel2.SuspendLayout();
            this.splitContainer15.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer14
            // 
            this.splitContainer14.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer14.Location = new System.Drawing.Point(0, 0);
            this.splitContainer14.Name = "splitContainer14";
            // 
            // splitContainer14.Panel1
            // 
            this.splitContainer14.Panel1.Controls.Add(this.listBox);
            // 
            // splitContainer14.Panel2
            // 
            this.splitContainer14.Panel2.Controls.Add(this.splitContainer15);
            this.splitContainer14.Size = new System.Drawing.Size(661, 420);
            this.splitContainer14.SplitterDistance = 124;
            this.splitContainer14.TabIndex = 14;
            // 
            // listBox
            // 
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(0, 0);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(124, 420);
            this.listBox.TabIndex = 0;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // splitContainer15
            // 
            this.splitContainer15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer15.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer15.Location = new System.Drawing.Point(0, 0);
            this.splitContainer15.Name = "splitContainer15";
            this.splitContainer15.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer15.Panel1
            // 
            this.splitContainer15.Panel1.Controls.Add(this.calculator_tabControl);
            // 
            // splitContainer15.Panel2
            // 
            this.splitContainer15.Panel2.Controls.Add(this.calculator_textBox_result);
            this.splitContainer15.Size = new System.Drawing.Size(533, 420);
            this.splitContainer15.SplitterDistance = 321;
            this.splitContainer15.TabIndex = 0;
            // 
            // calculator_tabControl
            // 
            this.calculator_tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.calculator_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calculator_tabControl.ItemSize = new System.Drawing.Size(0, 1);
            this.calculator_tabControl.Location = new System.Drawing.Point(0, 0);
            this.calculator_tabControl.Name = "calculator_tabControl";
            this.calculator_tabControl.SelectedIndex = 0;
            this.calculator_tabControl.Size = new System.Drawing.Size(533, 321);
            this.calculator_tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.calculator_tabControl.TabIndex = 12;
            // 
            // calculator_textBox_result
            // 
            this.calculator_textBox_result.BackColor = System.Drawing.SystemColors.ControlLight;
            this.calculator_textBox_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calculator_textBox_result.Location = new System.Drawing.Point(0, 0);
            this.calculator_textBox_result.Multiline = true;
            this.calculator_textBox_result.Name = "calculator_textBox_result";
            this.calculator_textBox_result.ReadOnly = true;
            this.calculator_textBox_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.calculator_textBox_result.Size = new System.Drawing.Size(533, 95);
            this.calculator_textBox_result.TabIndex = 8;
            // 
            // Tab_Calculators
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer14);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Tab_Calculators";
            this.Size = new System.Drawing.Size(661, 420);
            this.Load += new System.EventHandler(this.Tab_Calculators_Load);
            this.splitContainer14.Panel1.ResumeLayout(false);
            this.splitContainer14.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer14)).EndInit();
            this.splitContainer14.ResumeLayout(false);
            this.splitContainer15.Panel1.ResumeLayout(false);
            this.splitContainer15.Panel2.ResumeLayout(false);
            this.splitContainer15.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer15)).EndInit();
            this.splitContainer15.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer14;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.SplitContainer splitContainer15;
        private System.Windows.Forms.TabControl calculator_tabControl;
        public System.Windows.Forms.TextBox calculator_textBox_result;
    }
}
