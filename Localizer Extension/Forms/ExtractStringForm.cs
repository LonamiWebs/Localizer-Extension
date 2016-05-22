//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Localizer_Extension
{
    public partial class ExtractStringCmdForm : Form
    {
        #region Show

        public static KeyValuePair<string, string>? PromptDialog(string resFile, string defaultValue)
        {
            using (var esf = new ExtractStringCmdForm(resFile, defaultValue))
            {
                if (esf.ShowDialog() != DialogResult.OK)
                    return null;

                return new KeyValuePair<string, string>(esf.name, esf.value);
            }
        }

        #endregion

        #region Properties and fields

        string name { get { return resNameTB.Text; } set { resNameTB.Text = value; } }
        string value { get { return resValueTB.Text; } set { resValueTB.Text = value; } }

        // the currently loaded resources file
        string resFile;

        #endregion

        #region Constructor

        ExtractStringCmdForm(string resFile, string defaultValue)
        {
            InitializeComponent();

            this.resFile = resFile;
            value = defaultValue;
            checkStatus();

            Activate();
        }

        #endregion

        #region Accept and cancel

        void cancelB_Click(object sender, EventArgs e) { DialogResult = DialogResult.Cancel; }
        void acceptB_Click(object sender, EventArgs e)
        {
            if (StringsXMLEditor.ContainsKey(resFile, name))
            {
                int i = 0;
                while (StringsXMLEditor.ContainsKey(resFile, name + ++i)) ;
                name = name + i;
            }

            DialogResult = DialogResult.OK;
        }

        #endregion

        #region Events

        void resNameTB_TextChanged(object sender, EventArgs e)
        {
            checkStatus();
        }

        void resNameTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '"')
                e.Handled = true;
        }

        #endregion

        #region Status

        void checkStatus()
        {
            if (name.Length == 0)
            {
                acceptB.Enabled = false;
                statusCSL.SetError("Please specify a resource name");
            }
            else
            {
                acceptB.Enabled = true;

                if (StringsXMLEditor.ContainsKey(resFile, name))
                    statusCSL.SetWarn("Resource name will be changed due to another already exists with that name");
                else
                    statusCSL.SetStatus("Ready to extract the string resource");
            }
        }

        #endregion
    }
}
