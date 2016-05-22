//------------------------------------------------------------------------------
// <copyright file="AddResourceCmd.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.IO;
using System.Windows.Forms;

namespace Localizer_Extension
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class AddResourceCmd
    {
        #region Constant variables

        // maximum windows path length
        const int MAX_PATH_LENGTH = 260;

        #endregion

        #region VS Fields

        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 256;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("9eac52c8-74b8-4e77-9174-c538f1e21171");

        // VS Package that provides this command, not null.
        private readonly Package package;

        // gets the instance of the command.
        public static AddResourceCmd Instance { get; private set; }

        // gets the service provider from the owner package.
        private IServiceProvider ServiceProvider => package;

        #endregion

        #region Initialization

        // initializes the singleton instance of the command with the specified owner package
        public static void Initialize(Package package) { Instance = new AddResourceCmd(package); }

        // initialize a new instance and add command handlers for the menu 
        // the commands must exist in the command table file
        private AddResourceCmd(Package package)
        {
            if (package == null)
                throw new ArgumentNullException("package");

            this.package = package;

            var cmd = (OleMenuCommandService)ServiceProvider.GetService(typeof(IMenuCommandService));
            if (cmd != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);

                var menuItem = new OleMenuCommand(MenuItemCallback, menuCommandID);
                menuItem.BeforeQueryStatus += menuCommand_BeforeQueryStatus;

                cmd.AddCommand(menuItem);
            }
        }

        #endregion

        #region Menu command

        // executed before the menu is open
        void menuCommand_BeforeQueryStatus(object sender, EventArgs e)
        {
            if (sender == null) return;

            string filePath;
            VsUtils.GetSingleProjectItemSelection(out filePath);

            var menuCommand = (OleMenuCommand)sender;
            menuCommand.Visible = menuCommand.Enabled = // visible if we selected the resources folder path
                Resourcer.GetResourcesFolderPath().Equals(filePath);
        }

        // executed when the menu command is clicked
        void MenuItemCallback(object sender, EventArgs e)
        {
            ShowAddCustomResource();
        }

        public static void ShowAddCustomResource()
        {
            // sanitized path
            string sanitized = string.Empty;
            DialogResult retry;
            do
            {
                retry = DialogResult.No;
                var result = InputTextBox.Show(
                    "Enter the name of the new custom resources XAML file", "Enter a name",
                    sanitized, MAX_PATH_LENGTH - Resourcer.GetResourcesFolderPath().Length - 7);
                // -7 = -(@"\.xaml".Length + 1) // it must be LESS than, not less or equal ^ (so substract 1 extra)
                
                if (string.IsNullOrEmpty(result))
                    return;

                sanitized = string.Join("_", result.Split(Path.GetInvalidFileNameChars(),
                    StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');

                if (!string.IsNullOrWhiteSpace(sanitized))
                {
                    var path = Resourcer.GetXamlResPath(result);
                    if (File.Exists(path))
                    {
                        retry = MessageBox.Show("A file with this name already exists! Do you want to change it?",
                            "Existing file", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                        continue;
                    }
                    Resourcer.CreateXamlRes(path, true);
                }
            }
            while (retry == DialogResult.Yes);
        }

        #endregion
    }
}
