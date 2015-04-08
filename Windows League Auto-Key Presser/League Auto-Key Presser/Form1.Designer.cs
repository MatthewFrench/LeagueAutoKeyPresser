namespace League_Auto_Key_Presser
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.autoKeyOn = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.active1On = new System.Windows.Forms.CheckBox();
            this.active2On = new System.Windows.Forms.CheckBox();
            this.active5On = new System.Windows.Forms.CheckBox();
            this.active3On = new System.Windows.Forms.CheckBox();
            this.active6On = new System.Windows.Forms.CheckBox();
            this.active7On = new System.Windows.Forms.CheckBox();
            this.wardCheckbox = new System.Windows.Forms.CheckBox();
            this.qValueText = new System.Windows.Forms.TextBox();
            this.wValueText = new System.Windows.Forms.TextBox();
            this.eValueText = new System.Windows.Forms.TextBox();
            this.rValueText = new System.Windows.Forms.TextBox();
            this.activeValueText = new System.Windows.Forms.TextBox();
            this.activeKeyComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Designed for League of Legends";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Press QWER to activate FAST zxcv";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Q -                            millisecond delay";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "W -                           millisecond delay";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "E -                            millisecond delay";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "R -                            millisecond delay";
            // 
            // autoKeyOn
            // 
            this.autoKeyOn.AutoSize = true;
            this.autoKeyOn.Checked = true;
            this.autoKeyOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoKeyOn.Location = new System.Drawing.Point(15, 295);
            this.autoKeyOn.Name = "autoKeyOn";
            this.autoKeyOn.Size = new System.Drawing.Size(125, 17);
            this.autoKeyOn.TabIndex = 6;
            this.autoKeyOn.Text = "League Auto-Key On";
            this.autoKeyOn.UseVisualStyleBackColor = true;
            this.autoKeyOn.CheckedChanged += new System.EventHandler(this.autoKeyOn_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(282, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Actives -                            millisecond delay, bound to key:";
            // 
            // active1On
            // 
            this.active1On.AutoSize = true;
            this.active1On.Location = new System.Drawing.Point(39, 211);
            this.active1On.Name = "active1On";
            this.active1On.Size = new System.Drawing.Size(89, 17);
            this.active1On.TabIndex = 8;
            this.active1On.Text = "Do #1 Active";
            this.active1On.UseVisualStyleBackColor = true;
            this.active1On.CheckedChanged += new System.EventHandler(this.active1On_CheckedChanged);
            // 
            // active2On
            // 
            this.active2On.AutoSize = true;
            this.active2On.Location = new System.Drawing.Point(40, 235);
            this.active2On.Name = "active2On";
            this.active2On.Size = new System.Drawing.Size(89, 17);
            this.active2On.TabIndex = 9;
            this.active2On.Text = "Do #2 Active";
            this.active2On.UseVisualStyleBackColor = true;
            this.active2On.CheckedChanged += new System.EventHandler(this.active2On_CheckedChanged);
            // 
            // active5On
            // 
            this.active5On.AutoSize = true;
            this.active5On.Location = new System.Drawing.Point(156, 232);
            this.active5On.Name = "active5On";
            this.active5On.Size = new System.Drawing.Size(89, 17);
            this.active5On.TabIndex = 11;
            this.active5On.Text = "Do #5 Active";
            this.active5On.UseVisualStyleBackColor = true;
            this.active5On.CheckedChanged += new System.EventHandler(this.active5On_CheckedChanged);
            // 
            // active3On
            // 
            this.active3On.AutoSize = true;
            this.active3On.Location = new System.Drawing.Point(156, 211);
            this.active3On.Name = "active3On";
            this.active3On.Size = new System.Drawing.Size(89, 17);
            this.active3On.TabIndex = 10;
            this.active3On.Text = "Do #3 Active";
            this.active3On.UseVisualStyleBackColor = true;
            this.active3On.CheckedChanged += new System.EventHandler(this.active3On_CheckedChanged);
            // 
            // active6On
            // 
            this.active6On.AutoSize = true;
            this.active6On.Location = new System.Drawing.Point(251, 211);
            this.active6On.Name = "active6On";
            this.active6On.Size = new System.Drawing.Size(89, 17);
            this.active6On.TabIndex = 12;
            this.active6On.Text = "Do #6 Active";
            this.active6On.UseVisualStyleBackColor = true;
            this.active6On.CheckedChanged += new System.EventHandler(this.active6On_CheckedChanged);
            // 
            // active7On
            // 
            this.active7On.AutoSize = true;
            this.active7On.Location = new System.Drawing.Point(251, 232);
            this.active7On.Name = "active7On";
            this.active7On.Size = new System.Drawing.Size(89, 17);
            this.active7On.TabIndex = 13;
            this.active7On.Text = "Do #7 Active";
            this.active7On.UseVisualStyleBackColor = true;
            this.active7On.CheckedChanged += new System.EventHandler(this.active7On_CheckedChanged);
            // 
            // wardCheckbox
            // 
            this.wardCheckbox.AutoSize = true;
            this.wardCheckbox.Checked = true;
            this.wardCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.wardCheckbox.Location = new System.Drawing.Point(39, 260);
            this.wardCheckbox.Name = "wardCheckbox";
            this.wardCheckbox.Size = new System.Drawing.Size(160, 17);
            this.wardCheckbox.TabIndex = 14;
            this.wardCheckbox.Text = "Place ward every 6 seconds";
            this.wardCheckbox.UseVisualStyleBackColor = true;
            this.wardCheckbox.CheckedChanged += new System.EventHandler(this.wardCheckbox_CheckedChanged);
            // 
            // qValueText
            // 
            this.qValueText.Location = new System.Drawing.Point(34, 77);
            this.qValueText.Name = "qValueText";
            this.qValueText.Size = new System.Drawing.Size(73, 20);
            this.qValueText.TabIndex = 15;
            this.qValueText.Text = "10";
            this.qValueText.TextChanged += new System.EventHandler(this.qValueText_TextChanged);
            // 
            // wValueText
            // 
            this.wValueText.Location = new System.Drawing.Point(34, 102);
            this.wValueText.Name = "wValueText";
            this.wValueText.Size = new System.Drawing.Size(73, 20);
            this.wValueText.TabIndex = 16;
            this.wValueText.Text = "10";
            this.wValueText.TextChanged += new System.EventHandler(this.wValueText_TextChanged);
            // 
            // eValueText
            // 
            this.eValueText.Location = new System.Drawing.Point(34, 127);
            this.eValueText.Name = "eValueText";
            this.eValueText.Size = new System.Drawing.Size(73, 20);
            this.eValueText.TabIndex = 17;
            this.eValueText.Text = "10";
            this.eValueText.TextChanged += new System.EventHandler(this.eValueText_TextChanged);
            // 
            // rValueText
            // 
            this.rValueText.Location = new System.Drawing.Point(34, 150);
            this.rValueText.Name = "rValueText";
            this.rValueText.Size = new System.Drawing.Size(73, 20);
            this.rValueText.TabIndex = 18;
            this.rValueText.Text = "100";
            this.rValueText.TextChanged += new System.EventHandler(this.rValueText_TextChanged);
            // 
            // activeValueText
            // 
            this.activeValueText.Location = new System.Drawing.Point(61, 184);
            this.activeValueText.Name = "activeValueText";
            this.activeValueText.Size = new System.Drawing.Size(73, 20);
            this.activeValueText.TabIndex = 19;
            this.activeValueText.Text = "500";
            this.activeValueText.TextChanged += new System.EventHandler(this.activeValueText_TextChanged);
            // 
            // activeKeyComboBox
            // 
            this.activeKeyComboBox.DisplayMember = "E";
            this.activeKeyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.activeKeyComboBox.FormattingEnabled = true;
            this.activeKeyComboBox.Items.AddRange(new object[] {
            "Q",
            "W",
            "E",
            "R"});
            this.activeKeyComboBox.Location = new System.Drawing.Point(297, 184);
            this.activeKeyComboBox.MaxDropDownItems = 4;
            this.activeKeyComboBox.Name = "activeKeyComboBox";
            this.activeKeyComboBox.Size = new System.Drawing.Size(121, 21);
            this.activeKeyComboBox.TabIndex = 20;
            this.activeKeyComboBox.SelectedIndexChanged += new System.EventHandler(this.activeKeyComboBox_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 324);
            this.Controls.Add(this.activeKeyComboBox);
            this.Controls.Add(this.activeValueText);
            this.Controls.Add(this.rValueText);
            this.Controls.Add(this.eValueText);
            this.Controls.Add(this.wValueText);
            this.Controls.Add(this.qValueText);
            this.Controls.Add(this.wardCheckbox);
            this.Controls.Add(this.active7On);
            this.Controls.Add(this.active6On);
            this.Controls.Add(this.active5On);
            this.Controls.Add(this.active3On);
            this.Controls.Add(this.active2On);
            this.Controls.Add(this.active1On);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.autoKeyOn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "League Auto-Key Presser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox autoKeyOn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox active1On;
        private System.Windows.Forms.CheckBox active2On;
        private System.Windows.Forms.CheckBox active5On;
        private System.Windows.Forms.CheckBox active3On;
        private System.Windows.Forms.CheckBox active6On;
        private System.Windows.Forms.CheckBox active7On;
        private System.Windows.Forms.CheckBox wardCheckbox;
        private System.Windows.Forms.TextBox qValueText;
        private System.Windows.Forms.TextBox wValueText;
        private System.Windows.Forms.TextBox eValueText;
        private System.Windows.Forms.TextBox rValueText;
        private System.Windows.Forms.TextBox activeValueText;
        private System.Windows.Forms.ComboBox activeKeyComboBox;
    }
}

