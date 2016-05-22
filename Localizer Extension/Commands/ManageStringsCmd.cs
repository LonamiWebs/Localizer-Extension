//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;

namespace Localizer_Extension
{
    internal sealed class ManageStringsCmd
    {
        #region VS fields

        // Command ID
        public const int CommandId = 256;

        // command menu group (command set GUID).
        public static readonly Guid CommandSet = new Guid("7596de13-5f95-4036-97d3-bb0d5872aa54");

        // VS Package that provides this command, not null.
        private readonly Package package;

        // gets the instance of the command.
        public static ManageStringsCmd Instance { get; private set; }

        // gets the service provider from the owner package.
        private IServiceProvider ServiceProvider => package;

        #endregion
        
        #region Initialization

        // initializes the singleton instance of the command with the specified owner package
        public static void Initialize(Package package) { Instance = new ManageStringsCmd(package); }

        // initialize a new instance and add command handlers for the menu 
        // the commands must exist in the command table file
        private ManageStringsCmd(Package package)
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

            // get the menu that fired the event
            var menuCommand = (OleMenuCommand)sender;
            menuCommand.Visible = menuCommand.Enabled = VsUtils.GetCurrentProject() != null;
        }

        // executed when the menu command is clicked
        void MenuItemCallback(object sender, EventArgs e)
        {
            ManageStringsForm.ShowSingle();
        }

        #endregion
    }
}
