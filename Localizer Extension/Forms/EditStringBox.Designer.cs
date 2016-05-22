namespace Localizer_Extension
{
    partial class EditStringBox
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
        void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.keyTB = new System.Windows.Forms.TextBox();
            this.valueTB = new System.Windows.Forms.TextBox();
            this.cancelB = new System.Windows.Forms.Button();
            this.acceptB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key:";
            // 
            // keyTB
            // 
            this.keyTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyTB.Location = new System.Drawing.Point(50, 13);
            this.keyTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.keyTB.Name = "keyTB";
            this.keyTB.ReadOnly = true;
            this.keyTB.Size = new System.Drawing.Size(322, 25);
            this.keyTB.TabIndex = 1;
            // 
            // valueTB
            // 
            this.valueTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueTB.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.valueTB.Location = new System.Drawing.Point(12, 63);
            this.valueTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.valueTB.Multiline = true;
            this.valueTB.Name = "valueTB";
            this.valueTB.Size = new System.Drawing.Size(360, 107);
            this.valueTB.TabIndex = 2;
            // 
            // cancelB
            // 
            this.cancelB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelB.Location = new System.Drawing.Point(192, 178);
            this.cancelB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelB.Name = "cancelB";
            this.cancelB.Size = new System.Drawing.Size(87, 30);
            this.cancelB.TabIndex = 3;
            this.cancelB.Text = "Cancel";
            this.cancelB.UseVisualStyleBackColor = true;
            this.cancelB.Click += new System.EventHandler(this.cancelB_Click);
            // 
            // acceptB
            // 
            this.acceptB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptB.Location = new System.Drawing.Point(285, 178);
            this.acceptB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.acceptB.Name = "acceptB";
            this.acceptB.Size = new System.Drawing.Size(87, 30);
            this.acceptB.TabIndex = 4;
            this.acceptB.Text = "Accept";
            this.acceptB.UseVisualStyleBackColor = true;
            this.acceptB.Click += new System.EventHandler(this.acceptB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Value:";
            // 
            // EditStringBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 221);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.acceptB);
            this.Controls.Add(this.cancelB);
            this.Controls.Add(this.valueTB);
            this.Controls.Add(this.keyTB);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EditStringBox";
            this.Text = "Edit string";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EdiStringBox_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox keyTB;
        private System.Windows.Forms.TextBox valueTB;
        private System.Windows.Forms.Button cancelB;
        private System.Windows.Forms.Button acceptB;
        private System.Windows.Forms.Label label2;
    }
}