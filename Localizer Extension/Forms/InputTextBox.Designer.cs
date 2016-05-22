namespace Localizer_Extension
{
    partial class InputTextBox
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
            this.textL = new System.Windows.Forms.Label();
            this.inputTB = new System.Windows.Forms.TextBox();
            this.parentTable = new System.Windows.Forms.TableLayoutPanel();
            this.bottomPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cancelB = new System.Windows.Forms.Button();
            this.acceptB = new System.Windows.Forms.Button();
            this.parentTable.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // textL
            // 
            this.textL.AutoSize = true;
            this.textL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textL.Location = new System.Drawing.Point(16, 16);
            this.textL.Margin = new System.Windows.Forms.Padding(16);
            this.textL.MaximumSize = new System.Drawing.Size(460, 800);
            this.textL.Name = "textL";
            this.textL.Size = new System.Drawing.Size(335, 17);
            this.textL.TabIndex = 0;
            // 
            // inputTB
            // 
            this.inputTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputTB.Location = new System.Drawing.Point(16, 53);
            this.inputTB.Margin = new System.Windows.Forms.Padding(16, 4, 16, 4);
            this.inputTB.Name = "inputTB";
            this.inputTB.Size = new System.Drawing.Size(335, 25);
            this.inputTB.TabIndex = 1;
            this.inputTB.TextChanged += new System.EventHandler(this.inputTB_TextChanged);
            this.inputTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTB_KeyDown);
            // 
            // parentTable
            // 
            this.parentTable.AutoSize = true;
            this.parentTable.BackColor = System.Drawing.Color.White;
            this.parentTable.ColumnCount = 1;
            this.parentTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.parentTable.Controls.Add(this.inputTB, 0, 1);
            this.parentTable.Controls.Add(this.textL, 0, 0);
            this.parentTable.Controls.Add(this.bottomPanel, 0, 2);
            this.parentTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parentTable.Location = new System.Drawing.Point(0, 0);
            this.parentTable.Name = "parentTable";
            this.parentTable.RowCount = 3;
            this.parentTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.parentTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.parentTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.parentTable.Size = new System.Drawing.Size(367, 173);
            this.parentTable.TabIndex = 5;
            // 
            // bottomPanel
            // 
            this.bottomPanel.AutoSize = true;
            this.bottomPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bottomPanel.BackColor = System.Drawing.SystemColors.Control;
            this.bottomPanel.Controls.Add(this.cancelB);
            this.bottomPanel.Controls.Add(this.acceptB);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.bottomPanel.Location = new System.Drawing.Point(3, 85);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(361, 85);
            this.bottomPanel.TabIndex = 2;
            // 
            // cancelB
            // 
            this.cancelB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelB.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelB.Location = new System.Drawing.Point(278, 16);
            this.cancelB.Margin = new System.Windows.Forms.Padding(3, 16, 3, 16);
            this.cancelB.Name = "cancelB";
            this.cancelB.Size = new System.Drawing.Size(80, 26);
            this.cancelB.TabIndex = 2;
            this.cancelB.Text = "Cancel";
            this.cancelB.UseVisualStyleBackColor = true;
            this.cancelB.Click += new System.EventHandler(this.cancelB_Click);
            // 
            // acceptB
            // 
            this.acceptB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptB.Enabled = false;
            this.acceptB.Location = new System.Drawing.Point(192, 16);
            this.acceptB.Margin = new System.Windows.Forms.Padding(3, 16, 3, 16);
            this.acceptB.Name = "acceptB";
            this.acceptB.Size = new System.Drawing.Size(80, 26);
            this.acceptB.TabIndex = 3;
            this.acceptB.Text = "Accept";
            this.acceptB.UseVisualStyleBackColor = true;
            this.acceptB.Click += new System.EventHandler(this.acceptB_Click);
            // 
            // InputTextBox
            // 
            this.AcceptButton = this.acceptB;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cancelB;
            this.ClientSize = new System.Drawing.Size(367, 173);
            this.Controls.Add(this.parentTable);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InputTextBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input text";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InputTextBox_FormClosed);
            this.Load += new System.EventHandler(this.InputTextBox_Load);
            this.parentTable.ResumeLayout(false);
            this.parentTable.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label textL;
        private System.Windows.Forms.TextBox inputTB;
        private System.Windows.Forms.TableLayoutPanel parentTable;
        private System.Windows.Forms.FlowLayoutPanel bottomPanel;
        private System.Windows.Forms.Button cancelB;
        private System.Windows.Forms.Button acceptB;
    }
}