//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell.Settings;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Localizer_Extension
{
    // lots of utilities for vs!
    public static class VsUtils
    {
        #region Initialization

        static IServiceProvider serviceProvider;
        public static void Init(IServiceProvider serviceProvider)
        {
            VsUtils.serviceProvider = serviceProvider;
        }

        #endregion

        #region DTE related

        // returns the "base" object used in almost everything
        public static DTE GetDTE()
        {
            return (DTE)serviceProvider.GetService(typeof(DTE));
        }

        #endregion

        #region Solution related

        public static string GetSolutionDirectory()
        {
            return GetDTE().Solution.FullName;
        }

        #endregion

        #region Project related

        // returns the containing project of the active document
        public static Project GetCurrentProject()
        {
            var dte = GetDTE();

            var doc = dte.ActiveDocument;
            if (doc != null) // return the current one
                return doc.ProjectItem.ContainingProject;

            var sol = dte.Solution;
            if (sol != null && sol.Projects.Count > 0)
            {
                var enumerator = sol.Projects.GetEnumerator();
                enumerator.MoveNext(); // return the first found
                return (Project)enumerator.Current;
            }

            // return nothing :/
            return null;
        }

        // save the current project if it's not null
        public static void SaveProject(Project project)
        {
            if (project != null)
                project.Save();
        }

        // returns the directory containing the current project
        public static string GetCurrentProjectPath()
        {
            var project = GetCurrentProject();
            if (project == null) return string.Empty;

            return Path.GetDirectoryName(project.FullName);
        }

        #endregion

        #region Project items related

        #region Path utilities

        // get the first full path for a file name in the given project, null if it wasn't found
        public static string GetFullPath(Project project, string fileName)
        {
            if (project == null) return null;

            foreach (var pi in GetProjectItems(project))
                if (string.Equals(fileName, pi.Name, StringComparison.OrdinalIgnoreCase))
                    return GetFullPath(pi);

            return null;
        }

        // get the full path for a project item
        public static string GetFullPath(ProjectItem pi)
        {
            if (pi == null) return string.Empty;
            return (string)pi.Properties.Item("FullPath").Value;
        }

        #endregion

        #region Add directories

        // adds a directory (which is already in the project structure) to the project
        public static bool AddDirectory(Project project, string dirPath)
        {
            try { project.ProjectItems.AddFromDirectory(dirPath); }
            catch { return false; }

            return true;
        }

        // if the directory is not added to the project yet, add it
        public static bool AddDirectoryIfUnexisting(Project project, string dirPath)
        {
            bool dirFound = false; // is the directory added to the project?
            foreach (var dir in GetProjectFiles(project))
                if (dirPath == dir)
                {
                    dirFound = true;
                    break;
                }

            if (!dirFound) // if not, add it
                return AddDirectory(project, dirPath);

            return false;
        }

        #endregion

        #region Add files

        // adds a file (which is already in the project structure) to the project
        public static bool AddFile(Project project, string filePath)
        {
            try { project.ProjectItems.AddFromFile(filePath); }
            catch { return false; }

            return true;
        }

        // if the file is not added to the project yet, add it
        public static bool AddFileIfUnexisting(Project project, string filePath)
        {
            bool fileFound = false; // is the file added to the project?
            foreach (var file in GetProjectFiles(project))
                if (filePath == file)
                {
                    fileFound = true;
                    break;
                }

            if (!fileFound) // if not, add it
                return AddFile(project, filePath);

            return false;
        }

        #endregion

        #region Enumerate items

        // get all the items in the specified project http://stackoverflow.com/a/13375037
        public static IEnumerable<ProjectItem> GetProjectItems(Project project)
        {
            foreach (ProjectItem item in Recurse(project.ProjectItems))
                yield return item;
        }

        // get all the project files as paths
        public static IEnumerable<string> GetProjectFiles(Project project)
        {
            foreach (var item in GetProjectItems(project))
                yield return GetFullPath(item);
        }

        // get all the project items in the specified folder
        public static IEnumerable<ProjectItem> GetProjectItemsInFolder(Project project, string folder)
        {
            foreach (ProjectItem item in Recurse(project.ProjectItems))
                if (GetFullPath(item).StartsWith(folder, StringComparison.OrdinalIgnoreCase))
                    yield return item;
        }

        #endregion

        #region Open files

        // open a file in the editor
        public static void OpenInEditor(string filePath)
        {
            GetDTE().ItemOperations.OpenFile(filePath);
        }

        #endregion

        #region Rename items

        // renames a folder
        public static bool RenameFolder(Project project, string oldName, string newName)
        {
            if (project == null) return false;

            try
            {
                var item = project.ProjectItems.Item(oldName);
                if (item == null) return false;

                var fullOldPath = GetFullPath(item).TrimEnd(Path.DirectorySeparatorChar);
                var fullNewPath = Path.Combine(Path.GetDirectoryName(fullOldPath), newName);

                // if the new path exists
                if (Directory.Exists(fullNewPath))
                {
                    var files = Directory.GetFiles(fullNewPath);
                    if (files.Length == 0 ||
                        (files.Length == 1 && Path.GetFileName(files[0]) == "desktop.ini"))
                    {
                        // try deleting it or rename will fail
                        try { Directory.Delete(fullNewPath, true); }
                        catch { }
                    }
                }

                try { item.Name = newName; }
                catch { return false; } // perhaps the folder already exists and it's not empty :(
            }
            catch { return false; } // perhaps the name changed but not manually after a fail...

            // jeez, it was hard to get here!
            return true;
        }

        #endregion
        
        #region Exclude and delete files

        // exclude the file from the project and delete it from disk
        public static bool DeleteFile(Project project, string filePath)
        {
            return excludeFile(project, filePath, true);
        }

        // exclude the file from the project
        public static bool ExcludeFile(Project project, string filePath)
        {
            return excludeFile(project, filePath, false);
        }

        // exclude a file, and optionally delete it
        static bool excludeFile(Project project, string filePath, bool delete)
        {
            foreach (var item in GetProjectItems(project))
                if (GetFullPath(item).Equals(filePath))
                    try
                    {
                        if (delete) item.Delete();
                        else item.Remove();

                        return true;
                    }
                    catch { return false; }

            return false;
        }

        #endregion

        #region Selection

        // from http://www.diaryofaninja.com/blog/2014/02/18/who-said-building-visual-studio-extensions-was-hard
        public static bool IsSingleProjectItemSelection(out IVsHierarchy hierarchy, out uint itemid)
        {
            hierarchy = null;
            itemid = VSConstants.VSITEMID_NIL;
            int hr = VSConstants.S_OK;

            var monitorSelection = (IVsMonitorSelection)Package.GetGlobalService(typeof(SVsShellMonitorSelection));
            var solution = (IVsSolution)Package.GetGlobalService(typeof(SVsSolution));
            if (monitorSelection == null || solution == null)
                return false;

            IVsMultiItemSelect multiItemSelect = null;
            IntPtr hierarchyPtr = IntPtr.Zero;
            IntPtr selectionContainerPtr = IntPtr.Zero;

            try
            {
                hr = monitorSelection.GetCurrentSelection(out hierarchyPtr, out itemid, out multiItemSelect, out selectionContainerPtr);

                if (ErrorHandler.Failed(hr) || hierarchyPtr == IntPtr.Zero
                    || itemid == VSConstants.VSITEMID_NIL)
                    return false; // there is no selection

                // multiple items are selected
                if (multiItemSelect != null) return false;

                // there is a hierarchy root node selected, thus it is not a single item inside a project
                if (itemid == VSConstants.VSITEMID_ROOT) return false;

                hierarchy = (IVsHierarchy)Marshal.GetObjectForIUnknown(hierarchyPtr);
                if (hierarchy == null) return false;

                Guid guidProjectID;
                if (ErrorHandler.Failed(solution.GetGuidOfProject(hierarchy, out guidProjectID)))
                    return false; // hierarchy is not a project inside the Solution if it does not have a ProjectID Guid

                // if we got this far then there is a single project item selected
                return true;
            }
            finally
            {
                if (selectionContainerPtr != IntPtr.Zero)
                    Marshal.Release(selectionContainerPtr);

                if (hierarchyPtr != IntPtr.Zero)
                    Marshal.Release(hierarchyPtr);
            }
        }

        public static bool GetSingleProjectItemSelection(out string filePath)
        {
            filePath = null;
            IVsHierarchy hierarchy;
            uint itemid;

            if (!IsSingleProjectItemSelection(out hierarchy, out itemid))
                return false;

            // Get the file path
            ((IVsProject)hierarchy).GetMkDocument(itemid, out filePath);
            if (filePath != null)
                filePath = filePath.TrimEnd(Path.DirectorySeparatorChar);

            return true;
        }

        #endregion

        #endregion

        #region Current document related

        // gets the current language for the active document (ie "XAML")
        public static string GetActiveDocumentLanguage()
        {
            var dte = GetDTE();
            if (dte == null) return string.Empty;

            var doc = dte.ActiveDocument;
            if (doc == null) return string.Empty;

            return doc.Language;
        }

        // returns the current active text view (i.e. the editor window)
        public static IVsTextView GetActiveTextView()
        {
            IVsTextView vTextView;

            IVsTextManager txtMgr = (IVsTextManager)serviceProvider.GetService(typeof(SVsTextManager));
            int mustHaveFocus = 1;
            txtMgr.GetActiveView(mustHaveFocus, null, out vTextView);

            return vTextView;
        }

        #endregion

        #region Views related

        // returns all the text for the specified text view
        public static string GetTextForTextView(IVsTextView vTextView)
        {
            if (vTextView == null) return string.Empty;

            // get all the lines
            IVsTextLines txtLines;
            vTextView.GetBuffer(out txtLines);

            // determine which are the bottom line / column
            int bottomLine, bottomCol;
            txtLines.GetLastLineIndex(out bottomLine, out bottomCol);

            // return all the text from the top the bottom line
            return GetTextForTextView(vTextView, 0, 0, bottomLine, bottomCol);
        }

        // returns the text from the textview from the specified positions
        public static string GetTextForTextView(IVsTextView vTextView,
            int topLine, int topCol, int bottomLine, int bottomCol)
        {
            string text;
            vTextView.GetTextStream(topLine, topCol, bottomLine, bottomCol, out text);
            return text;
        }

        #endregion

        #region Settings related

        // get settings
        public static WritableSettingsStore GetSettingsStore()
        {
            return new ShellSettingsManager(serviceProvider)
                .GetWritableSettingsStore(SettingsScope.UserSettings);
        }

        #endregion

        #region Private methods

        // used by GetProjectItems
        static IEnumerable<ProjectItem> Recurse(ProjectItems i)
        {
            if (i != null)
                foreach (ProjectItem j in i)
                    foreach (ProjectItem k in Recurse(j))
                        yield return k;
        }
        static IEnumerable<ProjectItem> Recurse(ProjectItem i)
        {
            yield return i;
            foreach (ProjectItem j in Recurse(i.ProjectItems))
                yield return j;
        }

        #endregion
    }
}
