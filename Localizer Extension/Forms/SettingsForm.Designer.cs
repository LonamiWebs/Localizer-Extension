namespace Localizer_Extension
{
    partial class SettingsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.selectLocaleStartCB = new System.Windows.Forms.CheckBox();
            this.warningP = new System.Windows.Forms.Panel();
            this.warningB = new System.Windows.Forms.Button();
            this.warningL = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.useDynamicXamlCB = new System.Windows.Forms.CheckBox();
            this.resFolderNameTB = new System.Windows.Forms.TextBox();
            this.resManNameTB = new System.Windows.Forms.TextBox();
            this.acceptB = new System.Windows.Forms.Button();
            this.resetB = new System.Windows.Forms.Button();
            this.warningP.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectLocaleStartCB
            // 
            this.selectLocaleStartCB.AutoSize = true;
            this.selectLocaleStartCB.Location = new System.Drawing.Point(12, 13);
            this.selectLocaleStartCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.selectLocaleStartCB.Name = "selectLocaleStartCB";
            this.selectLocaleStartCB.Size = new System.Drawing.Size(231, 21);
            this.selectLocaleStartCB.TabIndex = 2;
            this.selectLocaleStartCB.Text = "Select locale on application startup";
            this.toolTip.SetToolTip(this.selectLocaleStartCB, "Determines whether code should be generated or not for the\r\napplication locale to" +
        " be automatically set on the app startup");
            this.selectLocaleStartCB.UseVisualStyleBackColor = true;
            // 
            // warningP
            // 
            this.warningP.Controls.Add(this.warningB);
            this.warningP.Controls.Add(this.warningL);
            this.warningP.Dock = System.Windows.Forms.DockStyle.Top;
            this.warningP.Location = new System.Drawing.Point(0, 0);
            this.warningP.Name = "warningP";
            this.warningP.Size = new System.Drawing.Size(390, 24);
            this.warningP.TabIndex = 4;
            this.warningP.Visible = false;
            // 
            // warningB
            // 
            this.warningB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.warningB.BackColor = System.Drawing.Color.Khaki;
            this.warningB.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.warningB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.warningB.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.warningB.Location = new System.Drawing.Point(367, 2);
            this.warningB.Name = "warningB";
            this.warningB.Size = new System.Drawing.Size(20, 20);
            this.warningB.TabIndex = 5;
            this.warningB.UseVisualStyleBackColor = false;
            this.warningB.Click += new System.EventHandler(this.warningB_Click);
            // 
            // warningL
            // 
            this.warningL.BackColor = System.Drawing.Color.Khaki;
            this.warningL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.warningL.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.warningL.Location = new System.Drawing.Point(0, 0);
            this.warningL.Margin = new System.Windows.Forms.Padding(0);
            this.warningL.Name = "warningL";
            this.warningL.Size = new System.Drawing.Size(390, 24);
            this.warningL.TabIndex = 4;
            this.warningL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 3600000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Resources folder name:";
            this.toolTip.SetToolTip(this.label1, "The name of the folder where the resources will be located");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Static resource manager class name:";
            this.toolTip.SetToolTip(this.label2, "The class name for the resource manager");
            // 
            // useDynamicXamlCB
            // 
            this.useDynamicXamlCB.AutoSize = true;
            this.useDynamicXamlCB.Location = new System.Drawing.Point(12, 103);
            this.useDynamicXamlCB.Name = "useDynamicXamlCB";
            this.useDynamicXamlCB.Size = new System.Drawing.Size(208, 21);
            this.useDynamicXamlCB.TabIndex = 10;
            this.useDynamicXamlCB.Text = "Use DynamicResource in XAML";
            this.toolTip.SetToolTip(this.useDynamicXamlCB, resources.GetString("useDynamicXamlCB.ToolTip"));
            this.useDynamicXamlCB.UseVisualStyleBackColor = true;
            // 
            // resFolderNameTB
            // 
            this.resFolderNameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resFolderNameTB.Location = new System.Drawing.Point(164, 41);
            this.resFolderNameTB.Name = "resFolderNameTB";
            this.resFolderNameTB.Size = new System.Drawing.Size(214, 25);
            this.resFolderNameTB.TabIndex = 7;
            // 
            // resManNameTB
            // 
            this.resManNameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resManNameTB.Location = new System.Drawing.Point(239, 72);
            this.resManNameTB.Name = "resManNameTB";
            this.resManNameTB.Size = new System.Drawing.Size(139, 25);
            this.resManNameTB.TabIndex = 8;
            // 
            // acceptB
            // 
            this.acceptB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptB.Location = new System.Drawing.Point(248, 136);
            this.acceptB.Name = "acceptB";
            this.acceptB.Size = new System.Drawing.Size(130, 32);
            this.acceptB.TabIndex = 9;
            this.acceptB.Text = "Save and exit";
            this.acceptB.UseVisualStyleBackColor = true;
            this.acceptB.Click += new System.EventHandler(this.acceptB_Click);
            // 
            // resetB
            // 
            this.resetB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetB.Location = new System.Drawing.Point(12, 136);
            this.resetB.Name = "resetB";
            this.resetB.Size = new System.Drawing.Size(130, 32);
            this.resetB.TabIndex = 11;
            this.resetB.Text = "Reset settings";
            this.resetB.UseVisualStyleBackColor = true;
            this.resetB.Click += new System.EventHandler(this.resetB_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.acceptB;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 180);
            this.Controls.Add(this.resetB);
            this.Controls.Add(this.useDynamicXamlCB);
            this.Controls.Add(this.acceptB);
            this.Controls.Add(this.resManNameTB);
            this.Controls.Add(this.resFolderNameTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.warningP);
            this.Controls.Add(this.selectLocaleStartCB);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SettingsForm";
            this.Text = "Localizer Extension settings";
            this.warningP.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox selectLocaleStartCB;
        private System.Windows.Forms.Panel warningP;
        private System.Windows.Forms.Label warningL;
        private System.Windows.Forms.Button warningB;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox resFolderNameTB;
        private System.Windows.Forms.TextBox resManNameTB;
        private System.Windows.Forms.Button acceptB;
        private System.Windows.Forms.CheckBox useDynamicXamlCB;
        private System.Windows.Forms.Button resetB;
    }
}