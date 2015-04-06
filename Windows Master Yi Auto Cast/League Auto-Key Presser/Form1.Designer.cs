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
            this.active4On = new System.Windows.Forms.CheckBox();
            this.active3On = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Designed for Master Yi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 46);
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
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Q - 10 millisecond delay";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "W - 100 millisecond delay";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "E - 130 millisecond delay";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 245);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "R - 120 millisecond delay";
            // 
            // autoKeyOn
            // 
            this.autoKeyOn.AutoSize = true;
            this.autoKeyOn.Checked = true;
            this.autoKeyOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoKeyOn.Location = new System.Drawing.Point(15, 295);
            this.autoKeyOn.Name = "autoKeyOn";
            this.autoKeyOn.Size = new System.Drawing.Size(133, 17);
            this.autoKeyOn.TabIndex = 6;
            this.autoKeyOn.Text = "Master Yi Auto-Key On";
            this.autoKeyOn.UseVisualStyleBackColor = true;
            this.autoKeyOn.CheckedChanged += new System.EventHandler(this.autoKeyOn_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(71, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(209, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Actives - 500 millisecond delay, bound to E";
            // 
            // active1On
            // 
            this.active1On.AutoSize = true;
            this.active1On.Location = new System.Drawing.Point(74, 204);
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
            this.active2On.Location = new System.Drawing.Point(191, 204);
            this.active2On.Name = "active2On";
            this.active2On.Size = new System.Drawing.Size(89, 17);
            this.active2On.TabIndex = 9;
            this.active2On.Text = "Do #2 Active";
            this.active2On.UseVisualStyleBackColor = true;
            this.active2On.CheckedChanged += new System.EventHandler(this.active2On_CheckedChanged);
            // 
            // active4On
            // 
            this.active4On.AutoSize = true;
            this.active4On.Location = new System.Drawing.Point(191, 225);
            this.active4On.Name = "active4On";
            this.active4On.Size = new System.Drawing.Size(89, 17);
            this.active4On.TabIndex = 11;
            this.active4On.Text = "Do #4 Active";
            this.active4On.UseVisualStyleBackColor = true;
            this.active4On.CheckedChanged += new System.EventHandler(this.active4On_CheckedChanged);
            // 
            // active3On
            // 
            this.active3On.AutoSize = true;
            this.active3On.Location = new System.Drawing.Point(74, 225);
            this.active3On.Name = "active3On";
            this.active3On.Size = new System.Drawing.Size(89, 17);
            this.active3On.TabIndex = 10;
            this.active3On.Text = "Do #3 Active";
            this.active3On.UseVisualStyleBackColor = true;
            this.active3On.CheckedChanged += new System.EventHandler(this.active3On_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 324);
            this.Controls.Add(this.active4On);
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
            this.Text = "Master Yi Auto-Key Presser";
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
        private System.Windows.Forms.CheckBox active4On;
        private System.Windows.Forms.CheckBox active3On;
    }
}

