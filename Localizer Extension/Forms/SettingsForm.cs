//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Localizer_Extension
{
    public partial class SettingsForm : Form
    {
        #region Single instance

        static SettingsForm form;
        public static void ShowSingle(string warning = null)
        {
            if (form == null)
            {
                form = new SettingsForm(warning);
                form.FormClosed += (s, e) => form = null;
                form.Show();
            }
            else
                form.Activate();
        }
        // no public stuff allowed
        new void Show() { base.Show(); }
        new void ShowDialog() { base.ShowDialog(); }
        new void Activate() { TopMost = true; base.Activate(); TopMost = false; }

        #endregion

        #region Variables
        
        // old size before showing the warning
        Size beforeSize;

        #endregion

        #region Constructor

        public SettingsForm(string warning)
        {
            InitializeComponent();
            LoadSettings();
            showWarning(warning);
        }

        #endregion

        #region Warning

        // show warning
        void showWarning(string warning)
        {
            bool warningVisible = !string.IsNullOrEmpty(warning);
            if (warningVisible)
            {
                beforeSize = Size;
                Size = new Size(measureString(warning, warningL.Font.SizeInPoints), Height);

                warningL.Text = warning;
                warningP.Visible = true;
                warningB.Image = getCross(warningB.Width);
            }
        }

        // hide warning
        void warningB_Click(object sender, EventArgs e)
        {
            Size = beforeSize;
            warningP.Visible = false;
        }

        // get the cross for the warning
        static Bitmap getCross(int size, float paddingT = 4, float paddingB = 6) // padding top/bottom
        {
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            {
                g.DrawLine(Pens.Black, paddingT, paddingT, size - paddingB, size - paddingB);
                g.DrawLine(Pens.Black, paddingT, size - paddingB, size - paddingB, paddingT);
            }
            return bmp;
        }

        // "measure" a string manually (fits great in the warning message btw)
        static int measureString(string str, float fontSize)
        {
            float result = 0;

            foreach (var c in str)
                switch (c)
                {
                    // i consider these letters smaller
                    case 't':
                    case 'i':
                    case 'f':
                    case 'l':
                    case 'j':
                        result += fontSize * 0.2f;
                        break;

                    // the others are all ok
                    default:
                        result += fontSize;
                        break;
                }

            return (int)result;
        }

        #endregion

        #region Load and save settings

        void LoadSettings()
        {
            // set all the values from the settings
            selectLocaleStartCB.Checked = Settings.SetLocaleOnStartup;
            useDynamicXamlCB.Checked = !Settings.UseStaticResourceXAML;
            resFolderNameTB.Text = Settings.ResourcesFolderName;
            resManNameTB.Text = Settings.ResourcesManagerName;
        }

        void SaveSettings()
        {
            checkChanges();

            // set all the values
            Settings.SetLocaleOnStartup = selectLocaleStartCB.Checked;
            Settings.UseStaticResourceXAML = !useDynamicXamlCB.Checked;

            if (resFolderNameTB.Text.Length > 0)
                Settings.ResourcesFolderName = resFolderNameTB.Text;

            if (resManNameTB.Text.Length > 0)
                Settings.ResourcesManagerName = resManNameTB.Text;
        }

        #endregion

        #region Update changes

        // are the current settings the old ones (i.e. we reset the settings)?
        void checkChanges(bool currentOld = false)
        {
            // no loaded project, no need to check changes
            if (VsUtils.GetCurrentProject() == null) return;

            Settings.UseStaticResourceXAML = !useDynamicXamlCB.Checked;
            
            // if the settings changed and they're not empty, update them
            if (Settings.SetLocaleOnStartup != selectLocaleStartCB.Checked)
                Ensurer.EnsureAppXamlCs();

            if (Settings.ResourcesFolderName != resFolderNameTB.Text
                && resFolderNameTB.Text.Length > 0)
                if (currentOld) // change the order depending on which are the old settings
                    Ensurer.RenameResFolder(resFolderNameTB.Text, Settings.ResourcesFolderName);
                else
                    Ensurer.RenameResFolder(Settings.ResourcesFolderName, resFolderNameTB.Text);

            if (Settings.ResourcesManagerName != resManNameTB.Text
                && resManNameTB.Text.Length > 0)
                if (currentOld)
                    Ensurer.RenameResManager(resManNameTB.Text, Settings.ResourcesManagerName);
                else
                    Ensurer.RenameResManager(Settings.ResourcesManagerName, resManNameTB.Text);
        }

        #endregion

        #region Event handling

        // reset all the settings and check for changes
        void resetB_Click(object sender, EventArgs e)
        {
            Settings.Reset();
            checkChanges(true);
            LoadSettings();
        }

        // save the settings and exit
        void acceptB_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Settings.Save();
            Close();
        }

        #endregion
    }
}
