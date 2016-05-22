namespace Localizer_Extension
{
    partial class ExtractStringCmdForm
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
            this.resNameTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cancelB = new System.Windows.Forms.Button();
            this.acceptB = new System.Windows.Forms.Button();
            this.resValueTB = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusCSL = new Localizer_Extension.ColoredStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Resource name:";
            // 
            // resNameTB
            // 
            this.resNameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resNameTB.Location = new System.Drawing.Point(120, 16);
            this.resNameTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.resNameTB.Name = "resNameTB";
            this.resNameTB.Size = new System.Drawing.Size(266, 25);
            this.resNameTB.TabIndex = 1;
            this.resNameTB.TextChanged += new System.EventHandler(this.resNameTB_TextChanged);
            this.resNameTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.resNameTB_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Resource value:";
            // 
            // cancelB
            // 
            this.cancelB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelB.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelB.Location = new System.Drawing.Point(14, 148);
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
            this.acceptB.Enabled = false;
            this.acceptB.Location = new System.Drawing.Point(241, 148);
            this.acceptB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.acceptB.Name = "acceptB";
            this.acceptB.Size = new System.Drawing.Size(146, 30);
            this.acceptB.TabIndex = 4;
            this.acceptB.Text = "Accept and extract";
            this.acceptB.UseVisualStyleBackColor = true;
            this.acceptB.Click += new System.EventHandler(this.acceptB_Click);
            // 
            // resValueTB
            // 
            this.resValueTB.AcceptsReturn = true;
            this.resValueTB.AcceptsTab = true;
            this.resValueTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resValueTB.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resValueTB.Location = new System.Drawing.Point(120, 50);
            this.resValueTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.resValueTB.Multiline = true;
            this.resValueTB.Name = "resValueTB";
            this.resValueTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resValueTB.Size = new System.Drawing.Size(266, 85);
            this.resValueTB.TabIndex = 5;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusCSL});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 192);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(401, 19);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusCSL
            // 
            this.statusCSL.Name = "statusCSL";
            this.statusCSL.Size = new System.Drawing.Size(0, 0);
            this.statusCSL.Spring = true;
            // 
            // ExtractStringCmdForm
            // 
            this.AcceptButton = this.acceptB;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelB;
            this.ClientSize = new System.Drawing.Size(401, 211);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.resValueTB);
            this.Controls.Add(this.acceptB);
            this.Controls.Add(this.cancelB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resNameTB);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(289, 210);
            this.Name = "ExtractStringCmdForm";
            this.Text = "Extract string";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox resNameTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cancelB;
        private System.Windows.Forms.Button acceptB;
        private System.Windows.Forms.TextBox resValueTB;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private Localizer_Extension.ColoredStatusLabel statusCSL;
    }
}