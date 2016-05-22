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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Localizer_Extension
{
    public static class Resourcer
    {
        #region Constant values

        // default name for strings
        const string stringsRes = "Strings.xaml";
        const string stringsResLocale = "Strings.{0}.xaml";
        static readonly Regex localeRegex = new Regex(@"Strings.(\w{2}(?:-\w{2})?).xaml", RegexOptions.Compiled);

        const string extensionXaml = ".xaml";
        const string extensionCs = ".cs";

        // get string method name
        const string getStrMethodName = "{0}.GetStr(\"{1}\")";

        const string staticMethodName = "\"{{StaticResource {0}}}\"";
        const string dynamicMethodName = "\"{{DynamicResource {0}}}\"";

        #endregion

        #region Manage strings

        #region Add strings

        // add a string to the strings resources
        // returns a call (as C# code) to retrieve the new added resource
        public static string AddString(KeyValuePair<string, string> keyValue,
            bool isXaml, bool resourceExisted, string locale = null)
        {
            Ensurer.EnsureStringResources();

            // TODO no need to save if addstring document is used nowhere else...
            if (!resourceExisted)
            {
                var resFile = GetResourcePath(GetStringsResName(locale));
                StringsXMLEditor.AddString(resFile, keyValue.Key, keyValue.Value);
            }

            Ensurer.EnsureEverything();
            return GetStringCall(keyValue.Key, isXaml);
        }

        #endregion

        #region Update strings

        // update a string key to a new key name
        public static void UpdateStringKey(string oldKey, string newKey)
        {
            Ensurer.EnsureEverything();

            updateStringKey(oldKey, newKey, string.Empty);
            foreach (var locale in GetStringsResLocales())
                updateStringKey(oldKey, newKey, locale);

            var oldCs = GetStringCall(oldKey, false);
            var oldXaml = GetStringCall(oldKey, true);
            var newCs = GetStringCall(newKey, false);
            var newXaml = GetStringCall(newKey, true);

            foreach (var file in VsUtils.GetProjectFiles(VsUtils.GetCurrentProject()))
            {
                if (file.EndsWith(extensionCs))
                    updateSource(file, oldCs, newCs);

                else if (file.EndsWith(extensionXaml))
                    updateSource(file, oldXaml, newXaml);
            }
        }

        // update a string value
        public static void UpdateString(string key, string value, string locale)
        {
            var resFile = GetResourcePath(GetStringsResName(locale));
            StringsXMLEditor.UpdateString(resFile, key, value);
        }

        #endregion

        #region Get strings

        // get all the strings in a given locale
        public static Dictionary<string, string> GetStrings(string locale = null)
        {
            return StringsXMLEditor.GetStrings(GetResourcePath(GetStringsResName(locale)));
        }

        #endregion

        #region Locales management

        // get the available resource locales
        public static IEnumerable<string> GetStringsResLocales()
        {
            var resFolder = GetResourcesFolderPath();
            if (Directory.Exists(resFolder))
            {
                foreach (var file in Directory.EnumerateFiles(resFolder))
                {
                    if (!file.EndsWith(extensionXaml)) continue;

                    var match = localeRegex.Match(Path.GetFileName(file));
                    if (match.Success)
                        yield return match.Groups[1].Value;
                }
            }
        }

        // does the specified locale exist?
        public static bool ExistsLocale(string locale)
        {
            if (string.IsNullOrEmpty(locale))
                return false;

            return GetStringsResLocales().Any(l => l.Equals(locale));
        }

        #endregion

        #region Delete strings

        // delete a strings resource provided a locale
        public static void DeleteStrings(string locale = null)
        {
            var resFile = Path.Combine(GetResourcesFolderPath(), GetStringsResName(locale));
            if (File.Exists(resFile))
                try
                {
                    VsUtils.DeleteFile(VsUtils.GetCurrentProject(), resFile);
                    Ensurer.EnsureAppXaml();
                }
                catch { }
        }

        #endregion

        #region Code calls

        // get a GetString call ready to be used in code
        public static string GetStringCall(string key, bool isXaml)
        {
            if (isXaml)
                return string.Format(Settings.UseStaticResourceXAML ?
                    staticMethodName : dynamicMethodName, key);
            else
                return string.Format(getStrMethodName, Settings.ResourcesManagerName, key);
        }

        #endregion

        #endregion

        #region Get paths

        // get the resources folder path
        public static string GetResourcesFolderPath()
        { return Path.Combine(VsUtils.GetCurrentProjectPath(), Settings.ResourcesFolderName); }

        // get the resources manager .cs file path
        public static string GetResourcesManagerPath()
        { return Path.Combine(GetResourcesFolderPath(), Settings.ResourcesManagerName + extensionCs); }

        // get an x resource path contained in the resources folder
        public static string GetResourcePath(string res)
        { return Path.Combine(GetResourcesFolderPath(), res); }

        // is there any resource available?
        public static bool AnyResource()
        {
            var resFolder = GetResourcesFolderPath();
            return Directory.Exists(resFolder) && Directory.EnumerateFiles(resFolder)
                .Any(f => f.EndsWith(extensionXaml));
        }

        // is there any locale resource available?
        public static bool AnyLocale()
        {
            var resFolder = GetResourcesFolderPath();
            return Directory.Exists(resFolder) && Directory.EnumerateFiles(resFolder)
                .Any(f => localeRegex.IsMatch(Path.GetFileName(f)));
        }

        // get the strings resources name for a specified locale
        public static string GetStringsResName(string locale = null)
        {
            if (string.IsNullOrEmpty(locale))
                return stringsRes;
            else
                return string.Format(stringsResLocale, locale);
        }

        #endregion

        #region XAML files

        // get the full path for the given name
        public static string GetXamlResPath(string name)
        {
            return GetResourcePath
               (name.EndsWith(extensionXaml, StringComparison.OrdinalIgnoreCase) ?
               name : name + extensionXaml);
        }

        // create the resources file in the desired path
        public static void CreateXamlRes(string path, bool openInEditor)
        {
            StringsXMLEditor.CreateDocument(path);
            VsUtils.AddFileIfUnexisting(VsUtils.GetCurrentProject(), path);
            Ensurer.EnsureAppXaml();

            if (openInEditor)
                VsUtils.OpenInEditor(path);

            Ensurer.EnsureEverything();
        }

        #endregion

        #region Private methods

        // update a source file, replacing and saving the new source if it exists
        static void updateSource(string file, string oldCall, string newCall)
        {
            var source = File.ReadAllText(file, Encoding.UTF8);
            if (source.Contains(oldCall))
            {
                source = source.Replace(oldCall, newCall);
                File.WriteAllText(file, source, Encoding.UTF8);
            }
            source = string.Empty;
        }

        // update a string key for a given locale
        static void updateStringKey(string oldKey, string newKey, string locale)
        {
            var resFile = GetResourcePath(GetStringsResName(locale));
            StringsXMLEditor.UpdateStringKey(resFile, oldKey, newKey);
        }

        #endregion
    }
}
