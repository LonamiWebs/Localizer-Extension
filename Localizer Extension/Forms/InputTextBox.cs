//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Drawing;
using System.Windows.Forms;

namespace Localizer_Extension
{
    public partial class InputTextBox : Form
    {
        #region Show methods

        public static string Show(string text) { return Show(text, null); }
        public static string Show(string text, string caption) { return Show(text, caption, null, -1); }
        public static string Show(string text, string caption, string defaultText) { return Show(text, caption, defaultText, -1); }
        public static string Show(string text, string caption, string defaultText, int maxLength)
        {
            using (var itb = new InputTextBox(text, caption, defaultText, maxLength))
            {
                itb.ShowDialog();
                return itb.InputText;
            }
        }

        #endregion

        #region Properties

        string InputText => DialogResult == DialogResult.OK ? inputTB.Text : null;

        #endregion

        #region Constructor

        InputTextBox(string text, string caption, string defaultText, int maxLength)
        {
            InitializeComponent();

            textL.Text = text;

            if (!string.IsNullOrEmpty(caption))
                Text = caption;

            if (!string.IsNullOrEmpty(defaultText))
                inputTB.Text = defaultText;

            if (maxLength > -1)
                inputTB.MaxLength = maxLength;
        }

        #endregion

        #region Loading

        void InputTextBox_Load(object sender, System.EventArgs e)
        {
            Size = parentTable.GetPreferredSize(Size.Empty);
        }

        #endregion

        #region Accept and cancel

        void acceptB_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        void cancelB_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region Events

        void inputTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                acceptB_Click(null, null);
            }
        }

        void inputTB_TextChanged(object sender, System.EventArgs e)
        {
            acceptB.Enabled = inputTB.Text.Length > 0;
        }

        void InputTextBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                inputTB.Text = string.Empty;
        }

        #endregion
    }
}
