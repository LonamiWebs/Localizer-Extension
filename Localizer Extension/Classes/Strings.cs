//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Localizer_Extension
{
    public static class Strings
    {
        // from https://msdn.microsoft.com/en-us/library/h21280bw.aspx
        static readonly string[] replaceSequences = new string[]
        {
            //"\\a", "\a", // Bell (alert)            // invalid in xml
            //"\\b", "\b", // Backspace               // invalid in xml
            //"\\f", "\f", // Formfeed                // invalid in xml
            "\\r", "\r",   // Carriage return
            "\\n", "\n",   // New line
            "\\t", "\t",   // Horizontal tab
            //"\\v", "\v", // Vertical tab            // invalid in xml
            "\\'", "\'",   // Single quotation mark
            "\\\"", "\"",  // Double quotation mark
            //"\\0", null, // null // for safety reasons, this character won't be changed
            "\\\\", "\\",  // Backslash
        };

        // some unicodes to match escaped sequences
        static readonly Regex hexaRegex = new Regex(@"\\x([0-9a-fA-F]{2})");
        static readonly Regex unicodeRegex = new Regex(@"\\x([0-9a-fA-F]{4})");
        
        // normalize a C# code string to be ready for XML
        public static string Normalize(string str)
        {
            if (str[0] == '@') // literal (TODO note that it can be start like $@)
            {
                str = str.Substring(2, str.Length - 3); // omit @" and " at the end
                str = str.Replace("\"\"", "\""); // only replace "" to "
            }
            else // normal
            {
                str = str.Substring(1, str.Length - 2); // omit " and " at the end

                for (int i = 0; i < replaceSequences.Length; i += 2)
                    if (str.Contains(replaceSequences[i]))
                        str = str.Replace(replaceSequences[i], replaceSequences[i + 1]);

                while (testRegex(unicodeRegex, ref str)) ;
                while (testRegex(hexaRegex, ref str)) ;
            }

            return str;
        }

        // test a given regex to check if it's match
        static bool testRegex(Regex regex, ref string str)
        {
            var match = regex.Match(str);
            if (match.Success)
            {
                var value = match.Groups[1].Value;
                var hexArray = hexStringToBytes(value);

                if (hexArray.Length == 1) // utf8 encoding
                    str = str.Replace(match.Captures[0].Value, Encoding.UTF8.GetString(hexArray));

                else if (hexArray.Length == 2) // utf16 encoding
                    str = str.Replace(match.Captures[0].Value, Encoding.Unicode.GetString(hexArray));
            }
            return match.Success;
        }

        //converts an hhhhhh string to a (n = length/2) bytes array length
        static byte[] hexStringToBytes(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                 .Where(x => x % 2 == 0)
                 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                 .Reverse()
                 .ToArray();
        }
    }
}
