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
using EnvDTE;
using System.Collections.Generic;

namespace Localizer_Extension
{
    internal sealed class ExtractStringCmd
    {
        #region Constant values

        const string LANGUAGE_XAML = "XAML";

        #endregion

        #region Private fields

        Scanner.VsTextViewRange? csRange; // current string range

        #endregion

        #region VS Fields

        // Command ID
        public const int CommandId = 0x0100;

        // command menu group (command set GUID).
        public static readonly Guid CommandSet = new Guid("8bd17ec9-fe22-4991-993d-73be41ca7528");

        // VS Package that provides this command, not null.
        private readonly Package package;

        // gets the instance of the command.
        public static ExtractStringCmd Instance { get; private set; }

        // gets the service provider from the owner package.
        private IServiceProvider ServiceProvider => package;

        #endregion

        #region Initialization

        // initializes the singleton instance of the command with the specified owner package
        public static void Initialize(Package package) { Instance = new ExtractStringCmd(package); }

        // initialize a new instance and add command handlers for the menu 
        // the commands must exist in the command table file
        private ExtractStringCmd(Package package)
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
            menuCommand.Visible = menuCommand.Enabled = searchRange();
        }

        // executed when the menu command is clicked
        void MenuItemCallback(object sender, EventArgs e)
        {
            if (!csRange.HasValue) return;

            var range = csRange.Value; // get the text in the range
            var text = VsUtils.GetTextForTextView(VsUtils.GetActiveTextView(),
                range.topLine, range.topCol, range.bottomLine, range.bottomCol);

            // default values resource file
            var resFile = Resourcer.GetResourcePath(Resourcer.GetStringsResName());

            string normalized = Strings.Normalize(text);
            bool resourceExisted = StringsXMLEditor.ContainsValue(resFile, normalized);

            var result = resourceExisted ?
                // if a key with this value already exists, use it
                new KeyValuePair<string, string>(StringsXMLEditor.GetKeyByValue(resFile, normalized), normalized) :
                // else, prompt a new dialog asking for a key
                ExtractStringCmdForm.PromptDialog(resFile, normalized);
            
            // if the result has value, replace the string with the method call
            if (result.HasValue) // replace the old string with the new method call
                ((TextDocument)VsUtils.GetDTE().ActiveDocument.Object())
                    .ReplacePattern(text, Resourcer.AddString(
                        result.Value,
                        VsUtils.GetActiveDocumentLanguage() == LANGUAGE_XAML,
                        resourceExisted));
        }

        #endregion

        #region Private methods

        // return true if it the ial of a string were found
        bool searchRange()
        {
            var tv = VsUtils.GetActiveTextView();
            if (tv == null) return false;

            // scan the current text for strings
            var ranges = Scanner.ScanVs(VsUtils.GetTextForTextView(tv));

            // find the positions
            int caretLine, caretCol;
            tv.GetCaretPos(out caretLine, out caretCol);

            foreach (var range in ranges)
            {
                if (range.ContainsPos(caretLine, caretCol))
                { // if this range was found set it and notify we found it
                    csRange = range;
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
