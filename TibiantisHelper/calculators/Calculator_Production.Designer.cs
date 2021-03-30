
namespace TibiantisHelper
{
    partial class Calculator_Production
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
            this.calculator_label_title = new System.Windows.Forms.Label();
            this.calculator_production_label1 = new System.Windows.Forms.Label();
            this.calculator_production_comboBox_rune = new System.Windows.Forms.ComboBox();
            this.calculator_production_label2 = new System.Windows.Forms.Label();
            this.calculator_production_textBox_amountSingle = new System.Windows.Forms.TextBox();
            this.calculator_production_label5 = new System.Windows.Forms.Label();
            this.calculator_production_textBox_amountBackpack = new System.Windows.Forms.TextBox();
            this.calculator_production_label3 = new System.Windows.Forms.Label();
            this.calculator_production_label4 = new System.Windows.Forms.Label();
            this.calculator_production_button_addTimer = new System.Windows.Forms.Button();
            this.calculator_production_comboBox_food = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // calculator_label_title
            // 
            this.calculator_label_title.AutoSize = true;
            this.calculator_label_title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calculator_label_title.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calculator_label_title.Location = new System.Drawing.Point(3, 3);
            this.calculator_label_title.Name = "calculator_label_title";
            this.calculator_label_title.Size = new System.Drawing.Size(93, 17);
            this.calculator_label_title.TabIndex = 0;
            this.calculator_label_title.Text = "Production";
            // 
            // calculator_production_label1
            // 
            this.calculator_production_label1.AutoSize = true;
            this.calculator_production_label1.Location = new System.Drawing.Point(42, 43);
            this.calculator_production_label1.Name = "calculator_production_label1";
            this.calculator_production_label1.Size = new System.Drawing.Size(33, 13);
            this.calculator_production_label1.TabIndex = 1;
            this.calculator_production_label1.Text = "Rune";
            // 
            // calculator_production_comboBox_rune
            // 
            this.calculator_production_comboBox_rune.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.calculator_production_comboBox_rune.FormattingEnabled = true;
            this.calculator_production_comboBox_rune.Location = new System.Drawing.Point(99, 40);
            this.calculator_production_comboBox_rune.Name = "calculator_production_comboBox_rune";
            this.calculator_production_comboBox_rune.Size = new System.Drawing.Size(197, 21);
            this.calculator_production_comboBox_rune.TabIndex = 2;
            this.calculator_production_comboBox_rune.SelectedIndexChanged += new System.EventHandler(this.calculator_production_comboBox_rune_SelectedIndexChanged);
            // 
            // calculator_production_label2
            // 
            this.calculator_production_label2.AutoSize = true;
            this.calculator_production_label2.Location = new System.Drawing.Point(42, 75);
            this.calculator_production_label2.Name = "calculator_production_label2";
            this.calculator_production_label2.Size = new System.Drawing.Size(43, 13);
            this.calculator_production_label2.TabIndex = 3;
            this.calculator_production_label2.Text = "Amount";
            // 
            // calculator_production_textBox_amountSingle
            // 
            this.calculator_production_textBox_amountSingle.Location = new System.Drawing.Point(99, 72);
            this.calculator_production_textBox_amountSingle.Name = "calculator_production_textBox_amountSingle";
            this.calculator_production_textBox_amountSingle.Size = new System.Drawing.Size(51, 20);
            this.calculator_production_textBox_amountSingle.TabIndex = 4;
            this.calculator_production_textBox_amountSingle.Text = "0";
            this.calculator_production_textBox_amountSingle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.calculator_production_textBox_amountSingle.TextChanged += new System.EventHandler(this.calculator_production_textBox_TextChanged);
            this.calculator_production_textBox_amountSingle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.calculator_production_textBox_KeyPress);
            // 
            // calculator_production_label5
            // 
            this.calculator_production_label5.AutoSize = true;
            this.calculator_production_label5.Location = new System.Drawing.Point(153, 75);
            this.calculator_production_label5.Name = "calculator_production_label5";
            this.calculator_production_label5.Size = new System.Drawing.Size(13, 13);
            this.calculator_production_label5.TabIndex = 5;
            this.calculator_production_label5.Text = "+";
            // 
            // calculator_production_textBox_amountBackpack
            // 
            this.calculator_production_textBox_amountBackpack.Location = new System.Drawing.Point(169, 72);
            this.calculator_production_textBox_amountBackpack.Name = "calculator_production_textBox_amountBackpack";
            this.calculator_production_textBox_amountBackpack.Size = new System.Drawing.Size(51, 20);
            this.calculator_production_textBox_amountBackpack.TabIndex = 9;
            this.calculator_production_textBox_amountBackpack.Text = "0";
            this.calculator_production_textBox_amountBackpack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.calculator_production_textBox_amountBackpack.TextChanged += new System.EventHandler(this.calculator_production_textBox_TextChanged);
            this.calculator_production_textBox_amountBackpack.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.calculator_production_textBox_KeyPress);
            // 
            // calculator_production_label3
            // 
            this.calculator_production_label3.AutoSize = true;
            this.calculator_production_label3.Location = new System.Drawing.Point(226, 75);
            this.calculator_production_label3.Name = "calculator_production_label3";
            this.calculator_production_label3.Size = new System.Drawing.Size(60, 13);
            this.calculator_production_label3.TabIndex = 7;
            this.calculator_production_label3.Text = "backpacks";
            // 
            // calculator_production_label4
            // 
            this.calculator_production_label4.AutoSize = true;
            this.calculator_production_label4.Location = new System.Drawing.Point(42, 107);
            this.calculator_production_label4.Name = "calculator_production_label4";
            this.calculator_production_label4.Size = new System.Drawing.Size(31, 13);
            this.calculator_production_label4.TabIndex = 10;
            this.calculator_production_label4.Text = "Food";
            // 
            // calculator_production_button_addTimer
            // 
            this.calculator_production_button_addTimer.Location = new System.Drawing.Point(45, 145);
            this.calculator_production_button_addTimer.Name = "calculator_production_button_addTimer";
            this.calculator_production_button_addTimer.Size = new System.Drawing.Size(75, 23);
            this.calculator_production_button_addTimer.TabIndex = 10;
            this.calculator_production_button_addTimer.Text = "Add Timer";
            this.calculator_production_button_addTimer.UseVisualStyleBackColor = true;
            this.calculator_production_button_addTimer.Click += new System.EventHandler(this.calculator_production_button_addTimer_Click);
            // 
            // calculator_production_comboBox_food
            // 
            this.calculator_production_comboBox_food.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.calculator_production_comboBox_food.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.calculator_production_comboBox_food.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.calculator_production_comboBox_food.FormattingEnabled = true;
            this.calculator_production_comboBox_food.Location = new System.Drawing.Point(99, 104);
            this.calculator_production_comboBox_food.Name = "calculator_production_comboBox_food";
            this.calculator_production_comboBox_food.Size = new System.Drawing.Size(197, 21);
            this.calculator_production_comboBox_food.TabIndex = 10;
            this.calculator_production_comboBox_food.SelectedIndexChanged += new System.EventHandler(this.calculator_production_comboBox_food_SelectedIndexChanged);
            // 
            // Calculator_Production
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.calculator_label_title);
            this.Controls.Add(this.calculator_production_label1);
            this.Controls.Add(this.calculator_production_comboBox_rune);
            this.Controls.Add(this.calculator_production_comboBox_food);
            this.Controls.Add(this.calculator_production_label2);
            this.Controls.Add(this.calculator_production_button_addTimer);
            this.Controls.Add(this.calculator_production_textBox_amountSingle);
            this.Controls.Add(this.calculator_production_label4);
            this.Controls.Add(this.calculator_production_label5);
            this.Controls.Add(this.calculator_production_label3);
            this.Controls.Add(this.calculator_production_textBox_amountBackpack);
            this.Name = "Calculator_Production";
            this.Size = new System.Drawing.Size(316, 184);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label calculator_label_title;
        private System.Windows.Forms.Label calculator_production_label1;
        private System.Windows.Forms.ComboBox calculator_production_comboBox_rune;
        private System.Windows.Forms.Label calculator_production_label2;
        private System.Windows.Forms.TextBox calculator_production_textBox_amountSingle;
        private System.Windows.Forms.Label calculator_production_label5;
        private System.Windows.Forms.TextBox calculator_production_textBox_amountBackpack;
        private System.Windows.Forms.Label calculator_production_label3;
        private System.Windows.Forms.Label calculator_production_label4;
        private System.Windows.Forms.Button calculator_production_button_addTimer;
        private System.Windows.Forms.ComboBox calculator_production_comboBox_food;
    }
}
