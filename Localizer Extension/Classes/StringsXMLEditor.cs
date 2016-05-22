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
using System.Xml;
using System.Xml.Linq;

namespace Localizer_Extension
{
    public static class StringsXMLEditor
    {
        #region Constant values

        // required xmlns and xml
        const string xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
        const string xmlns_x = "http://schemas.microsoft.com/winfx/2006/xaml";
        const string xmlns_system = "clr-namespace:System;assembly=mscorlib";
        const string xml_space = "preserve";

        // as namespaces
        static readonly XNamespace r_ns = xmlns; // "root" namespace
        static readonly XNamespace x_ns = xmlns_x; // x namespace
        static readonly XNamespace s_ns = xmlns_system; // system namespace

        // the rood node
        const string root_node = "ResourceDictionary";

        // xml line feed
        const string lf = "&#10;";

        #endregion

        public static void CreateDocument(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            SaveDocument
                (new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(r_ns + root_node,
                        new XAttribute(XNamespace.Xmlns + "x", x_ns),
                        new XAttribute(XNamespace.Xmlns + "system", s_ns),
                        new XAttribute(XNamespace.Xml + "space", xml_space))),
                        path
                );
        }

        public static void SaveDocument(XDocument doc, string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Encoding = Encoding.UTF8
            };

            using (var writer = XmlWriter.Create(path, settings))
                doc.Save(writer);
        }

        // add a string to a current xaml resources document
        public static void AddString(string file, string key, string value)
        {
            if (!File.Exists(file))
                return;
            
            var doc = XDocument.Load(file);

            doc.Root.Add(new XElement(
                s_ns + "String",                         // element name ("system:String")
                new XAttribute(x_ns + "Key", key),       // element attribute ("x:Key")
                value                                    // element value
            ));

            SaveDocument(doc, file);
        }

        // update a string key
        public static void UpdateStringKey(string file, string oldKey, string newKey)
        {
            if (!File.Exists(file))
                return; // TODO ensure!

            var doc = XDocument.Load(file);

            var attr = doc.Root.Elements().Attributes().FirstOrDefault(
                xa => xa.Name.LocalName.Equals("Key") && xa.Value.Equals(oldKey));

            if (attr != null)
                attr.SetValue(newKey);

            SaveDocument(doc, file);
        }

        // update a string value or add it if unexisting
        public static void UpdateString(string file, string key, string value)
        {
            if (!File.Exists(file))
                return; // TODO ensure!
            
            var doc = XDocument.Load(file);

            var ele = doc.Root.Elements().FirstOrDefault(xe => xe.Attributes()
                .Any(xa => xa.Name.LocalName.Equals("Key") && xa.Value.Equals(key)));

            if (ele == null)
                AddString(file, key, value);

            else
            {
                ele.SetValue(value);
                SaveDocument(doc, file);
            }
        }

        // get all the strings in a .xaml resources file
        public static Dictionary<string, string> GetStrings(string file)
        {
            var result = new Dictionary<string, string>();
            if (!File.Exists(file))
                return result;

            foreach (var xe in XDocument.Load(file).Root.Elements())
            {
                var attr = xe.Attribute(x_ns + "Key");
                if (attr != null)
                    try { result.Add(attr.Value, xe.Value.Replace("\n", Environment.NewLine)); }
                    catch (ArgumentException) { /* key already added, should not happen */ };
            }

            return result;
        }

        // does this .xaml contains a key?
        public static bool ContainsKey(string file, string key)
        {
            if (!File.Exists(file))
                return false;

            try
            {
                return XDocument.Load(file).Root.Elements()
                    .Attributes().Any(xa => xa.Value.Equals(key));
            }
            catch { return false; }
        }

        // does this .xaml contains a value?
        public static bool ContainsValue(string file, string value)
        {
            if (!File.Exists(file))
                return false;
            
            try
            {
                return XDocument.Load(file).Root.Elements()
                    .Any(xe => xe.Value.Replace("\n", Environment.NewLine).Equals(value));
            }
            catch { return false; }
        }

        // get the key given a value
        public static string GetKeyByValue(string file, string value)
        {
            if (!File.Exists(file))
                return string.Empty;
            
            return XDocument.Load(file).Root.Elements()
                .First(xe => xe.Value.Replace("\n", Environment.NewLine)
                .Equals(value)).Attribute(x_ns + "Key").Value;
        }
    }
}
