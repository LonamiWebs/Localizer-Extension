//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using EnvDTE;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Localizer_Extension
{
    // ensure everything!
    public static class Ensurer
    {
        #region Codes

        // TODO use Code.cs and CodeLine.cs

        #endregion

        #region Everything

        // ensure everything and save the project
        public static void EnsureEverything()
        {
            var project = VsUtils.GetCurrentProject();

            // omit these checks, perform them manually
            //EnsureResourcesFolder(project);
            //EnsureStringResources(project);

            // only if res dir contains resources
            if (Resourcer.AnyResource())
            {
                EnsureResourceManager(project);
                EnsureAppXaml(project);
            }

            // only if there are translations added
            if (Resourcer.AnyLocale())
            {
                EnsureAppXamlCs(project);
            }

            VsUtils.SaveProject(VsUtils.GetCurrentProject());
        }

        #endregion

        #region Resources folder

        public static void EnsureResourcesFolder(Project project = null)
        {
            if (project == null)
                project = VsUtils.GetCurrentProject();
            var resFolder = Resourcer.GetResourcesFolderPath();

            if (!Directory.Exists(resFolder)) // if the directory doesn't exist, create it
                Directory.CreateDirectory(resFolder);

            VsUtils.AddDirectoryIfUnexisting(project, resFolder);
        }

        #endregion

        #region String resources

        // ensure the strings.xml file exists
        public static void EnsureStringResources(Project project = null)
        {
            if (project == null)
                project = VsUtils.GetCurrentProject();
            var resFile = Resourcer.GetResourcePath(Resourcer.GetStringsResName());

            if (!File.Exists(resFile)) // if the file doesn't exist, create it
                StringsXMLEditor.CreateDocument(resFile);

            VsUtils.AddFileIfUnexisting(VsUtils.GetCurrentProject(), resFile);
        }

        #endregion

        #region Resource manager

        #region Required resource manager variables

        // the source required usings
        const string resManagerUsing = "using System.Windows;";

        // the whole source
        const string resManagerSource = @"
public static class @CLASSNAME
{
    static readonly FrameworkElement context = new FrameworkElement();
    
    public static T GetRes<T>(string name)
    { return (T)context.FindResource(name); }

    // returns null if the resource was not found
    public static string GetStr(string name, params object[] args)
    {
        try {
            var result = GetRes<string>(name);
            return args.Length > 0 ? string.Format(result, args) : result;
        }
        catch { return null; }
    }
}";

        // get the whole source
        static string getResManagerSource()
        { return resManagerSource.Replace("@CLASSNAME", Settings.ResourcesManagerName); }

        // checks for the file
        const string resCheck1 = "static readonly FrameworkElement context = new FrameworkElement();";
        const string resCheck2 = "static T GetRes<T>(string name)";
        const string resCheck3 = "public static string GetStr(string name, params object[] args)";

        // add the missing parts one by one
        const string resPart1 = @"    static readonly FrameworkElement context = new FrameworkElement();
";
        const string resPart2 = @"    public static T GetRes<T>(string name)
    { return (T)context.FindResource(name); }
";
        const string resPart3 = @"    public static string GetStr(string name, params object[] args)
    {
        try {
            var result = GetRes<string>(name);
            return args.Length > 0 ? string.Format(result, args) : result;
        }
        catch { return null; }
    }
";

        #endregion

        // ensure that the resource manager class exists, or that it's coplete
        public static void EnsureResourceManager(Project project = null)
        {
            var managerFile = Resourcer.GetResourcesManagerPath();

            if (File.Exists(managerFile)) // if the file exists, ensure it has the required methods
            {
                var source = File.ReadAllText(managerFile, Encoding.UTF8);

                var rc1 = source.Contains(resCheck1);
                var rc2 = source.Contains(resCheck2);
                var rc3 = source.Contains(resCheck3);
                if (!rc1 || !rc2 || !rc3) // seems like it doesn't have everything!
                {
                    var bracketRegex = new Regex(Settings.ResourcesManagerName + @"\s*{");
                    var match = bracketRegex.Match(source); // the class may exist

                    var newFile = new StringBuilder();
                    if (match.Success) // there's a match, append only the missing code to the existing class
                    {
                        var bracket = match.Index + match.Length;
                        newFile.Append(source.Substring(0, bracket));

                        if (!rc1) newFile.AppendLine(resPart1);
                        if (!rc2) newFile.AppendLine(resPart2);
                        if (!rc3) newFile.AppendLine(resPart3);

                        newFile.Append(source.Substring(bracket));
                    }
                    else // no match (no resourcemanager class), append the entire code
                    {
                        if (!source.Contains(resManagerUsing))
                            newFile.AppendLine(resManagerUsing);

                        newFile.AppendLine(source);
                        newFile.AppendLine(getResManagerSource());
                    }

                    File.WriteAllText(managerFile, newFile.ToString());
                }
            }
            else // file doesn't exist, just create it
                createResourceManagerFile(managerFile);

            if (project == null)
                project = VsUtils.GetCurrentProject();
            VsUtils.AddFileIfUnexisting(project, managerFile);
        }
        // create a full resourcemanager file
        static void createResourceManagerFile(string path)
        { File.WriteAllText(path, resManagerUsing + getResManagerSource(), Encoding.UTF8); }

        #endregion

        #region App.xaml.cs

        #region Required App.xaml.cs variables

        public static readonly string[] requiredAppUsings = new string[]
        {
            "using System.Collections.Generic;",
            "using System.Globalization;",
            "using System.Linq;",
            "using System.Threading;",
            "using System.Windows;",
        };

        public const string selectCultureConstructor =
@"        public App()
        {
            InitializeComponent();
            SelectCulture(Thread.CurrentThread.CurrentCulture.ToString());
        }";


        public const string initializeComponent = "InitializeComponent();";

        public const string selectCultureMethodCall =
            @"SelectCulture(Thread.CurrentThread.CurrentCulture.ToString());";

        public const string selectCultureMethodCheck = "public static void SelectCulture(string culture)";
        public const string selectCultureMethod =
@"        public static void SelectCulture(string culture)
        {
            // List all our resources      
            List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
                dictionaryList.Add(dictionary);

            // We want our specific culture      
            string requestedCulture = string.Format(""Strings.{0}.xaml"", culture);
            ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault
                (d => d.Source.OriginalString.EndsWith(requestedCulture));

            if (resourceDictionary == null)
            {
                requestedCulture = ""Strings.xaml"";
                resourceDictionary = dictionaryList.FirstOrDefault
                    (d => d.Source.OriginalString.EndsWith(requestedCulture));
            }

            // If we have the requested resource, remove it from the list and place at the end
            // Then this language will be our string table to use.     
            if (resourceDictionary != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }

            // Inform the threads of the new culture      
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }";

        #endregion

        // ensure that the App.xaml.cs contains SelectCulture method
        public static void EnsureAppXamlCs(Project project = null)
        {
            if (project == null)
                project = VsUtils.GetCurrentProject();

            var appXamlCs = VsUtils.GetFullPath(project, "App.xaml.cs");
            if (string.IsNullOrEmpty(appXamlCs)) return; // should never happen...

            var source = File.ReadAllText(appXamlCs, Encoding.UTF8);

            var startRegex = new Regex(@"class\s*App\s*:\s*Application\s*{");
            var start = startRegex.Match(source);
            if (!start.Success) return; // should never happen either!...
            var startIdx = start.Index + start.Length;

            if (!source.Contains(selectCultureMethodCheck)) // make sure to add the method
            {
                var sb = new StringBuilder();

                foreach (var rusing in requiredAppUsings)
                    if (!source.Contains(rusing))
                        sb.AppendLine(rusing);

                appendAfterStart(sb, startIdx, ref source, selectCultureMethod);
                File.WriteAllText(appXamlCs, source);
            }

            if (Settings.SetLocaleOnStartup && !source.Contains(selectCultureMethodCall))
            {
                var constructorMatch = new Regex(@"public\s+App\s*\(\)\s*{");
                var constructor = constructorMatch.Match(source);

                var sb = new StringBuilder();
                if (constructor.Success) // append only the method call
                {
                    var constructorIdx = constructor.Index + constructor.Length;

                    // indentation = 3 indent levels * 4 spaces each
                    appendAfterStart(sb, constructorIdx, ref source,
                        new string(' ', 3 * 4) + selectCultureMethodCall);

                    File.WriteAllText(appXamlCs, source);
                }
                else // append a whole new constructor
                {
                    appendAfterStart(sb, startIdx, ref source, selectCultureConstructor);
                    File.WriteAllText(appXamlCs, source);
                }
            }
            else if (source.Contains(selectCultureMethodCall))
            {
                if (source.Contains(selectCultureMethodCall))
                {
                    source = source.Replace(selectCultureMethodCall, string.Empty);
                    File.WriteAllText(appXamlCs, source);
                }
            }
            if (!source.Contains(initializeComponent))
            {
                var constructorMatch = new Regex(@"public\s+App\s*\(\)\s*{");
                var constructor = constructorMatch.Match(source);

                var sb = new StringBuilder();
                if (constructor.Success) // append only the method call
                {
                    var constructorIdx = constructor.Index + constructor.Length;

                    // indentation = 3 indent levels * 4 spaces each
                    appendAfterStart(sb, constructorIdx, ref source,
                        new string(' ', 3 * 4) + initializeComponent);

                    File.WriteAllText(appXamlCs, source);
                }
                else // append a whole new constructor
                {
                    appendAfterStart(sb, startIdx, ref source, selectCultureConstructor);
                    File.WriteAllText(appXamlCs, source);
                }
            }
        }

        static void appendAfterStart(StringBuilder sb, int start, ref string source, string toAppend)
        {
            sb.AppendLine(source.Substring(0, start));
            sb.AppendLine(toAppend);
            sb.AppendLine(source.Substring(start));

            source = sb.ToString();
        }

        #endregion

        #region App.xaml

        // add all the .xaml dictionaries to the App.xaml resources
        public static void EnsureAppXaml(Project project = null)
        {
            try
            {
                if (project == null)
                    project = VsUtils.GetCurrentProject();

                var appXaml = VsUtils.GetFullPath(project, "App.xaml");
                if (string.IsNullOrEmpty(appXaml)) return; // should never happen...

                var doc = XDocument.Load(appXaml);

                var appResources = getNode(doc.Root, "Application.Resources");
                var resDictionary = getNode(appResources, "ResourceDictionary");
                var mergedDictionaries = getNode(resDictionary, "ResourceDictionary.MergedDictionaries");

                var resFolder = Resourcer.GetResourcesFolderPath();
                if (Directory.Exists(resFolder))
                {
                    // clear all resources
                    mergedDictionaries.RemoveAll();

                    // add them again
                    foreach (var file in VsUtils.GetProjectItemsInFolder(project, resFolder))
                    {
                        // perhaps it's not a dictionary (.xaml) file
                        if (!file.Name.EndsWith(".xaml")) continue;
                        // perhaps it is a folder
                        if (!File.Exists(file.Properties.Item("FullPath").Value.ToString())) continue;

                        mergedDictionaries.Add(new XElement(
                            mergedDictionaries.Name.Namespace + "ResourceDictionary",
                            new XAttribute("Source", Settings.ResourcesFolderName + "/" + file.Name)
                        ));
                    }
                }

                StringsXMLEditor.SaveDocument(doc, appXaml);
            }
            catch { /* nobody likes errors (which shouldn't happen) :( */ }
        }

        // gets a node, if it doesn't exist, it's created
        static XElement getNode(XElement root, string name)
        {
            var node = root.Elements().FirstOrDefault(e => e.Name.LocalName.Equals(name));
            if (node == null)
                root.Add(node = new XElement(root.Name.Namespace + name));

            return node;
        }

        #endregion

        #region Rename stuff

        public static void RenameResFolder(string oldName, string newName)
        {
            if (!VsUtils.RenameFolder(VsUtils.GetCurrentProject(), oldName, newName))
                MessageBox.Show("The resources folder could not be renamed. It will have to be renamed manually",
                    "Rename failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void RenameResManager(string oldName, string newName)
        {
            // TODO RenameResManager
        }

        #endregion
    }
}
