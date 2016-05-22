//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Localizer_Extension
{
    public partial class ManageStringsForm : Form
    {
        #region Private variables

        // the currently loaded string resources (key - value - (optional)translation)
        public List<StringResource> StringResources;

        // are we using any locale?
        bool usingLocale;
        // did we use any locale the last time the locale box changed?
        bool lastWasNoLocale;

        // are we updating the translation textboxes
        bool updatingTranslationsTB;

        #endregion

        #region Single instance

        static ManageStringsForm form;
        public static void ShowSingle()
        {
            var p = VsUtils.GetCurrentProject();
            if (p == null) // either warning box no open project or show settings directly (prompting a warning perhaps)
            {
                SettingsForm.ShowSingle("Could not open the strings resource manager due to no project is currently open");
                return;
            }
            if (form == null)
            {
                form = new ManageStringsForm();
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

        #region Constructor

        public ManageStringsForm()
        {
            InitializeComponent();
            Ensurer.EnsureEverything();

            loadLocales();

            if (StringResources == null)
                updateTranslationsList();

            filterTypeCB.SelectedIndex = 1;
        }

        #endregion

        #region Settings

        void settingsLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SettingsForm.ShowSingle();
        }

        #endregion

        #region Custom resources

        void customResLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddResourceCmd.ShowAddCustomResource();
        }

        #endregion

        #region Load locales

        // load all the available locales to the locales combo box
        void loadLocales()
        {
            localeCB.Items.Clear();
            foreach (var locale in Resourcer.GetStringsResLocales())
                localeCB.Items.Add(locale);

            if (localeCB.Items.Count > 0)
            {
                localeCB.SelectedIndex = 0; // select first
                checkTranslationBoxes(true); // enable stuff
                statusCSL.SetStatus("Ready to manage string resources and translations");
            }
            else
            {
                checkTranslationBoxes(false); // disable stuff
                statusCSL.SetWarn("There are no translations yet! Consider adding some");
            }
        }

        #endregion

        #region Translations

        #region Add translations

        void addTranslationLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result =
                InputTextBox.Show("Enter the locale code you wish to use for your new translation",
                "Enter locale code", "es-ES", 5);

            if (string.IsNullOrEmpty(result))
                statusCSL.SetWarn("Adding locale operation cancelled");
            else // if the result is not null, add it
                addTranslation(Resourcer.GetStringsResName(result));
        }

        void addTranslation(string localeCode)
        {
            if (!string.IsNullOrWhiteSpace(localeCode))
            {
                var path = Resourcer.GetXamlResPath(localeCode);
                if (File.Exists(path)) // get the strings res path and check if it exists
                {
                    MessageBox.Show("This locale code was already added",
                         "Existing locale", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    statusCSL.SetError("Could not add an already existing locale");

                    return;
                }

                // create the xaml resources file and reload the available locales
                Resourcer.CreateXamlRes(path, false);
                Ensurer.EnsureAppXamlCs();

                loadLocales();

                statusCSL.SetStatus($"New locale {localeCode} has been added successfully");
            }
        }

        #endregion

        #region Update translations list

        // update the translations grid so the user can manage the strings
        void updateTranslationsList()
        {
            usingLocale = Resourcer.ExistsLocale(localeCB.Text);
            if (lastWasNoLocale && !usingLocale)
                return; // don't update everything if nothing changed
            lastWasNoLocale = !usingLocale;

            // current locale
            string locale = usingLocale ? localeCB.Text : null;

            // current locale strings
            Dictionary<string, string> localeDict = usingLocale ?
                Resourcer.GetStrings(locale) : null;

            StringResources = new List<StringResource>();
            foreach (var kvp in Resourcer.GetStrings())
            {
                if (usingLocale) // if we're using a locale, add locale values too
                {
                    string localeValue;
                    if (localeDict.TryGetValue(kvp.Key, out localeValue))
                        StringResources.Add(new StringResource(
                            kvp.Key, kvp.Value, locale, localeValue));

                    else
                        StringResources.Add(new StringResource(
                            kvp.Key, kvp.Value, locale, string.Empty));
                }

                else // only add string resources
                    StringResources.Add(new StringResource(kvp.Key, kvp.Value));
            }

            // update the source
            updateSource();

            // make sure to set the translation column (in)visible if required
            if (stringsResDGV.Columns.Count >= 2)
                stringsResDGV.Columns[2].Visible = usingLocale;

            checkTranslationBoxes(false);
        }

        #endregion

        #region Delete translations

        void delTranslationLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Resourcer.ExistsLocale(localeCB.Text))
            {
                if (MessageBox.Show($"The {localeCB.Text} locale will be lost forever. Are you sure you want to continue?",
                    $"{localeCB.Text} will be lost", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // delete the strings resources file and reload the locales
                    Resourcer.DeleteStrings(localeCB.Text);
                    statusCSL.SetStatus($"The locale {localeCB.Text} have been deleted from disk");
                    loadLocales();
                }
            }
        }

        #endregion

        #region Translation grid events

        void stringsResDGV_EditingControlShowing(object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            // make sure to use multiline in the cells
            if (stringsResDGV.CurrentCell.ColumnIndex == 1)
                ((TextBox)e.Control).Multiline = true;
        }

        void stringsResDGV_SelectionChanged(object sender, EventArgs e)
        {
            // update the translation textboxes values
            updatingTranslationsTB = true;

            if (stringsResDGV.SelectedRows.Count == 1 ||
                stringsResDGV.SelectedCells.Count == 1)
            {
                checkTranslationBoxes(true);

                keyTB.Text = getSelectedValue(0);
                defaultValueTB.Text = getSelectedValue(1);
                if (usingLocale) translationTB.Text = getSelectedValue(2);
            }
            else
            {
                checkTranslationBoxes(false);
            }

            updatingTranslationsTB = false;
        }

        #endregion

        #region Translation boxes

        // update key
        void keyTB_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(keyTB.Text)) return;
            int idx;
            if (cannotUseTranslationBoxes(out idx)) return;

            StringResources[idx].Key = keyTB.Text;
        }

        // update value
        void defaultValueTB_TextChanged(object sender, EventArgs e)
        {
            int idx;
            if (cannotUseTranslationBoxes(out idx)) return;

            StringResources[idx].DefaultValue = defaultValueTB.Text;
        }

        // update translation value
        void translationTB_TextChanged(object sender, EventArgs e)
        {
            int idx;
            if (cannotUseTranslationBoxes(out idx)) return;

            StringResources[idx].TranslationValue = translationTB.Text;
        }

        // enabled / disable
        void checkTranslationBoxes(bool enabled)
        {
            keyTB.Enabled = defaultValueTB.Enabled = defaultValueB.Enabled = enabled;
            translationTB.Enabled = translationB.Enabled = enabled ? usingLocale : false;
            delTranslationLL.Visible = usingLocale;
        }

        // short method so it's more comfortable to check this in the update events
        bool cannotUseTranslationBoxes(out int selectedIndex)
        {
            return updatingTranslationsTB | (selectedIndex = getSelectedIndex()) < 0;
        }

        // locale selector
        void localeCB_TextChanged(object sender, EventArgs e)
        {
            updateTranslationsList();
        }

        // update source
        void keyBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                updateSource();
            }
        }
        void translationBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                updateSource();
            }
        }
        void valuesBox_Leave(object sender, EventArgs e)
        {
            updateSource();
        }
        // TODO try changing when searching something and do other todos

        #endregion

        void updateSource()
        {
            stringsResDGV.DataSource = new SortableBindingList<StringResource>(StringResources);
        }

        #endregion

        #region Translations grid index management

        // get the selected value in the currently selected row for the n'th cell
        string getSelectedValue(int cell)
        {
            return stringsResDGV.SelectedRows.Count == 1 ?
                (string)stringsResDGV.SelectedRows[0].Cells[cell].Value :

                stringsResDGV.SelectedCells.Count == 1 ?
                (string)stringsResDGV.Rows
                [stringsResDGV.SelectedCells[0].RowIndex].Cells[cell].Value :

                string.Empty;
        }

        // get the currently selected row index
        int getSelectedIndex()
        {
            return stringsResDGV.SelectedRows.Count == 1 ?
                stringsResDGV.SelectedRows[0].Index :

                stringsResDGV.SelectedCells.Count == 1 ?
                stringsResDGV.SelectedCells[0].RowIndex :

                -1;
        }

        #endregion

        // show the edit string box for the default value
        void defaultValueB_Click(object sender, EventArgs e)
        {
            int idx;
            if (cannotUseTranslationBoxes(out idx)) return;

            var result = EditStringBox.Show(StringResources[idx].Key,
                StringResources[idx].DefaultValue);

            if (result != null)
            {
                defaultValueTB.Text = StringResources[idx].DefaultValue = result;
                statusCSL.SetStatus($"The default value has been updated for the {StringResources[idx].Key} key");
            }
        }

        // show the edit string box for the translation value
        void translationB_Click(object sender, EventArgs e)
        {
            int idx;
            if (cannotUseTranslationBoxes(out idx)) return;

            var result = EditStringBox.Show(StringResources[idx].Key,
                StringResources[idx].TranslationValue);

            if (result != null)
            {
                translationTB.Text = StringResources[idx].TranslationValue = result;
                statusCSL.SetStatus($"The translation value has been updated for the {StringResources[idx].Key} key");
            }
        }

        void filterTB_TextChanged(object sender, EventArgs e)
        {
            ((SortableBindingList<StringResource>)stringsResDGV.DataSource).Filter = getFilter();
        }

        string getFilter()
        {
            switch (filterTypeCB.SelectedIndex)
            {
                case 0: return "key:" + filterTB.Text;
                default:
                case 1: return "val:" + filterTB.Text;
                case 2: return "trs:" + filterTB.Text;
            }
        }

        public static bool ContainsAny(string str, params string[] values)
        {
            if (!string.IsNullOrEmpty(str) || values.Length > 0)
            {
                foreach (string value in values)
                {
                    if (str.Contains(value))
                        return true;
                }
            }

            return false;
        }
    }
}
