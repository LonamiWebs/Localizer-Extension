namespace Localizer_Extension
{
    partial class ManageStringsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageStringsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.customResLL = new System.Windows.Forms.LinkLabel();
            this.settingsLL = new System.Windows.Forms.LinkLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusCSL = new Localizer_Extension.ColoredStatusLabel();
            this.addTranslationLL = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.localeCB = new System.Windows.Forms.ComboBox();
            this.delTranslationLL = new System.Windows.Forms.LinkLabel();
            this.stringsResDGV = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.keyTB = new System.Windows.Forms.TextBox();
            this.defaultValueTB = new System.Windows.Forms.TextBox();
            this.translationTB = new System.Windows.Forms.TextBox();
            this.defaultValueB = new System.Windows.Forms.Button();
            this.translationB = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.filterTB = new System.Windows.Forms.TextBox();
            this.filterTypeCB = new System.Windows.Forms.ComboBox();
            this.manageStringsFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stringsResDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.manageStringsFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manage other translations here or";
            // 
            // customResLL
            // 
            this.customResLL.AutoSize = true;
            this.customResLL.Location = new System.Drawing.Point(218, 9);
            this.customResLL.Name = "customResLL";
            this.customResLL.Size = new System.Drawing.Size(170, 17);
            this.customResLL.TabIndex = 2;
            this.customResLL.TabStop = true;
            this.customResLL.Text = "add a custom resources file";
            this.customResLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.customResLL_LinkClicked);
            // 
            // settingsLL
            // 
            this.settingsLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsLL.AutoSize = true;
            this.settingsLL.Location = new System.Drawing.Point(538, 9);
            this.settingsLL.Name = "settingsLL";
            this.settingsLL.Size = new System.Drawing.Size(54, 17);
            this.settingsLL.TabIndex = 3;
            this.settingsLL.TabStop = true;
            this.settingsLL.Text = "Settings";
            this.settingsLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.settingsLL_LinkClicked);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusCSL});
            this.statusStrip.Location = new System.Drawing.Point(0, 350);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(604, 22);
            this.statusStrip.TabIndex = 5;
            // 
            // statusCSL
            // 
            this.statusCSL.Name = "statusCSL";
            this.statusCSL.Size = new System.Drawing.Size(0, 17);
            // 
            // addTranslationLL
            // 
            this.addTranslationLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addTranslationLL.AutoSize = true;
            this.addTranslationLL.Location = new System.Drawing.Point(200, 317);
            this.addTranslationLL.Name = "addTranslationLL";
            this.addTranslationLL.Size = new System.Drawing.Size(135, 17);
            this.addTranslationLL.TabIndex = 6;
            this.addTranslationLL.TabStop = true;
            this.addTranslationLL.Text = "Add a new translation";
            this.addTranslationLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.addTranslationLL_LinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 317);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Translation to edit:";
            // 
            // localeCB
            // 
            this.localeCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.localeCB.FormattingEnabled = true;
            this.localeCB.Location = new System.Drawing.Point(134, 314);
            this.localeCB.Name = "localeCB";
            this.localeCB.Size = new System.Drawing.Size(60, 25);
            this.localeCB.TabIndex = 8;
            this.localeCB.TextChanged += new System.EventHandler(this.localeCB_TextChanged);
            // 
            // delTranslationLL
            // 
            this.delTranslationLL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.delTranslationLL.AutoSize = true;
            this.delTranslationLL.Location = new System.Drawing.Point(341, 317);
            this.delTranslationLL.Name = "delTranslationLL";
            this.delTranslationLL.Size = new System.Drawing.Size(110, 17);
            this.delTranslationLL.TabIndex = 9;
            this.delTranslationLL.TabStop = true;
            this.delTranslationLL.Text = "Delete translation";
            this.delTranslationLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.delTranslationLL_LinkClicked);
            // 
            // stringsResDGV
            // 
            this.stringsResDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stringsResDGV.BackgroundColor = System.Drawing.Color.White;
            this.stringsResDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.stringsResDGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.stringsResDGV.Location = new System.Drawing.Point(15, 41);
            this.stringsResDGV.Name = "stringsResDGV";
            this.stringsResDGV.Size = new System.Drawing.Size(577, 111);
            this.stringsResDGV.TabIndex = 10;
            this.stringsResDGV.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.stringsResDGV_EditingControlShowing);
            this.stringsResDGV.SelectionChanged += new System.EventHandler(this.stringsResDGV_SelectionChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Key:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Default value:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Translation:";
            // 
            // keyTB
            // 
            this.keyTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyTB.Location = new System.Drawing.Point(107, 158);
            this.keyTB.Name = "keyTB";
            this.keyTB.Size = new System.Drawing.Size(187, 25);
            this.keyTB.TabIndex = 14;
            this.keyTB.TextChanged += new System.EventHandler(this.keyTB_TextChanged);
            this.keyTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyBox_KeyDown);
            this.keyTB.Leave += new System.EventHandler(this.valuesBox_Leave);
            // 
            // defaultValueTB
            // 
            this.defaultValueTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultValueTB.Location = new System.Drawing.Point(107, 189);
            this.defaultValueTB.Multiline = true;
            this.defaultValueTB.Name = "defaultValueTB";
            this.defaultValueTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.defaultValueTB.Size = new System.Drawing.Size(454, 50);
            this.defaultValueTB.TabIndex = 15;
            this.defaultValueTB.TextChanged += new System.EventHandler(this.defaultValueTB_TextChanged);
            this.defaultValueTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.translationBox_KeyDown);
            this.defaultValueTB.Leave += new System.EventHandler(this.valuesBox_Leave);
            // 
            // translationTB
            // 
            this.translationTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.translationTB.Location = new System.Drawing.Point(107, 245);
            this.translationTB.Multiline = true;
            this.translationTB.Name = "translationTB";
            this.translationTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.translationTB.Size = new System.Drawing.Size(454, 50);
            this.translationTB.TabIndex = 16;
            this.translationTB.TextChanged += new System.EventHandler(this.translationTB_TextChanged);
            this.translationTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.translationBox_KeyDown);
            this.translationTB.Leave += new System.EventHandler(this.valuesBox_Leave);
            // 
            // defaultValueB
            // 
            this.defaultValueB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultValueB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("defaultValueB.BackgroundImage")));
            this.defaultValueB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.defaultValueB.Location = new System.Drawing.Point(567, 219);
            this.defaultValueB.Name = "defaultValueB";
            this.defaultValueB.Size = new System.Drawing.Size(25, 25);
            this.defaultValueB.TabIndex = 17;
            this.defaultValueB.UseVisualStyleBackColor = true;
            this.defaultValueB.Click += new System.EventHandler(this.defaultValueB_Click);
            // 
            // translationB
            // 
            this.translationB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.translationB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("translationB.BackgroundImage")));
            this.translationB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.translationB.Location = new System.Drawing.Point(567, 270);
            this.translationB.Name = "translationB";
            this.translationB.Size = new System.Drawing.Size(25, 25);
            this.translationB.TabIndex = 18;
            this.translationB.UseVisualStyleBackColor = true;
            this.translationB.Click += new System.EventHandler(this.translationB_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(12, 305);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(580, 2);
            this.label6.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(300, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 17);
            this.label7.TabIndex = 20;
            this.label7.Text = "Filter items:";
            // 
            // filterTB
            // 
            this.filterTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.filterTB.Location = new System.Drawing.Point(380, 158);
            this.filterTB.Name = "filterTB";
            this.filterTB.Size = new System.Drawing.Size(125, 25);
            this.filterTB.TabIndex = 21;
            this.filterTB.TextChanged += new System.EventHandler(this.filterTB_TextChanged);
            // 
            // filterTypeCB
            // 
            this.filterTypeCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.filterTypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterTypeCB.FormattingEnabled = true;
            this.filterTypeCB.Items.AddRange(new object[] {
            "In name",
            "In value",
            "In translation"});
            this.filterTypeCB.Location = new System.Drawing.Point(511, 158);
            this.filterTypeCB.Name = "filterTypeCB";
            this.filterTypeCB.Size = new System.Drawing.Size(81, 25);
            this.filterTypeCB.TabIndex = 22;
            this.filterTypeCB.SelectedIndexChanged += new System.EventHandler(this.filterTB_TextChanged);
            // 
            // manageStringsFormBindingSource
            // 
            this.manageStringsFormBindingSource.DataSource = typeof(Localizer_Extension.ManageStringsForm);
            // 
            // ManageStringsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 372);
            this.Controls.Add(this.filterTypeCB);
            this.Controls.Add(this.filterTB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.translationB);
            this.Controls.Add(this.defaultValueB);
            this.Controls.Add(this.translationTB);
            this.Controls.Add(this.defaultValueTB);
            this.Controls.Add(this.keyTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stringsResDGV);
            this.Controls.Add(this.delTranslationLL);
            this.Controls.Add(this.addTranslationLL);
            this.Controls.Add(this.localeCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.settingsLL);
            this.Controls.Add(this.customResLL);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(480, 260);
            this.Name = "ManageStringsForm";
            this.Text = "Manage strings";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stringsResDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.manageStringsFormBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel customResLL;
        private System.Windows.Forms.LinkLabel settingsLL;
        private System.Windows.Forms.StatusStrip statusStrip;
        private Localizer_Extension.ColoredStatusLabel statusCSL;
        private System.Windows.Forms.LinkLabel addTranslationLL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox localeCB;
        private System.Windows.Forms.LinkLabel delTranslationLL;
        private System.Windows.Forms.BindingSource manageStringsFormBindingSource;
        private System.Windows.Forms.DataGridView stringsResDGV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox keyTB;
        private System.Windows.Forms.TextBox defaultValueTB;
        private System.Windows.Forms.TextBox translationTB;
        private System.Windows.Forms.Button defaultValueB;
        private System.Windows.Forms.Button translationB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox filterTB;
        private System.Windows.Forms.ComboBox filterTypeCB;
    }
}