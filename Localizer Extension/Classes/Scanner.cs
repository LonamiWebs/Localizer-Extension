//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

public static class Scanner
{
    enum States
    {
        Scanning,
        SkipNextCharNormal,
        SkipNextCharChar,

        OnChar,

        OnNormalString,

        AwaitingLiteralString,
        OnLiteralString,
        AwaitingLiteralExit,

        AwaitingComment,
        OnSingleLineComment,
        OnMultilineComment,
        AwaitingMultilineCommentExit
    };

    struct FoundMatch
    {
        public VsTextViewRange vs; // vs mode (lines and columns)
        public KeyValuePair<int, int> normal; // normal mode (index and length)

        public FoundMatch(VsTextViewRange vs, KeyValuePair<int, int> normal)
        {
            this.vs = vs;
            this.normal = normal;
        }
    }

    public struct VsTextViewRange
    {
        public int topLine, topCol;
        public int bottomLine, bottomCol;

        public VsTextViewRange(int topLine, int topCol, int bottomLine, int bottomCol)
        {
            this.topLine = topLine;
            this.topCol = topCol;
            this.bottomLine = bottomLine;
            this.bottomCol = bottomCol;
        }

        /* Table for checking contains pos:

                   1 2 3 4 5 6 7 8 9
                  ___________________
                1 |_|_|_|_|_|_|_|_|_|
                2 |_|_|a|_|_|_|_|_|_|
                3 |_|b|x|c|_|_|_|_|_|
                4 |_|_|_|_|_|_|_|_|_|
                5 |d|_|_|_|_|_|_|_|_|
                6 |_|_|_|_|_|_|_|_|_|
                7 |_|_|_|_|_|e|x|f|_|
                8 |_|_|_|_|_|_|g|_|_|
                9 |_|_|_|_|_|_|_|_|_|
        */
        public bool ContainsPos(int line, int col)
        {
            // this one is obvious
            if (line > topLine && line < bottomLine) return true;

            if (line < topLine || line > bottomLine) return false;
            if (line == topLine && col < topCol) return false;
            if (line == bottomLine && col > bottomCol) return false;

            // all the other left cases are valid
            return true;
        }
    }

    /// <summary>
    /// Scans the file to find indicies and lengths of the strings
    /// </summary>
    /// <param name="txt">The text to scan</param>
    /// <returns>The found indicies and lengths</returns>
    public static Dictionary<int, int> ScanNormal(string txt)
    {
        var found = Scan(txt);
        var result = new Dictionary<int, int>(found.Count);

        foreach (var f in found)
            result.Add(f.normal.Key, f.normal.Value);

        return result;
    }

    // scan specifically for vs
    public static List<VsTextViewRange> ScanVs(string txt)
    {
        var found = Scan(txt);
        var result = new List<VsTextViewRange>(found.Count);

        foreach (var f in found)
            result.Add(f.vs);

        return result;
    }

    // TODO hello, i'm here to annoy u; $"strings"!
    static List<FoundMatch> Scan(string txt)
    {
        //var indiciesLengths = new Dictionary<int, int>();
        var foundMatches = new List<FoundMatch>();

        States state = States.Scanning;
        int curFound = -1; // Current found string index


        int curFoundCol = 0;
        int curFoundLine = 0;

        int curLine = 0;
        int curCol = 0;
        for (int i = 0; i < txt.Length; i++)
        {
            var chr = txt[i];

            switch (state)
            {
                // We're scanning the document
                case States.Scanning:
                    switch (chr)
                    {
                        case '"': // We're now in a normal string

                            // set curFounds
                            curFound = i;
                            curFoundCol = curCol;
                            curFoundLine = curLine;

                            state = States.OnNormalString;
                            break;
                            
                        case '@': // Maybe we're getting in a literal string
                            state = States.AwaitingLiteralString;
                            break;
                        case '\'': // We're getting in a char
                            state = States.OnChar;
                            break;
                        case '/': // Maybe we're getting in a comment
                            state = States.AwaitingComment;
                            break;
                    }
                    break;

                // We're scanning a character ('a')
                case States.OnChar:
                    switch (chr)
                    {
                        case '\\': // Skip the next character
                            state = States.SkipNextCharChar;
                            break;
                        case '\'': // We're out!
                            state = States.Scanning;
                            break;
                    }
                    break;

                // We must skip the next character ('\'')
                case States.SkipNextCharChar:
                    state = States.OnChar;
                    break;

                // We're scanning a normal string
                case States.OnNormalString:
                    switch (chr)
                    {
                        case '\\': // Skip the next character
                            state = States.SkipNextCharNormal;
                            break;
                        case '"': // We're out! Save the string (we +1 to include the trailing ")

                            // add match
                            foundMatches.Add(new FoundMatch(new VsTextViewRange(curFoundLine, curFoundCol,
                                curLine, curCol + 1), new KeyValuePair<int, int>(curFound, i + 1 - curFound)));

                            state = States.Scanning;
                            break;
                    }
                    break;

                // We're scanning a literal string
                case States.OnLiteralString:
                    if (chr == '"') // We could either get out or fake warning
                        state = States.AwaitingLiteralExit;
                    break;

                // We're awaiting to check if it was a literal string or not (@" or @var?)
                case States.AwaitingLiteralString:
                    if (chr == '"') // Yes, we're in a literal string
                    {
                        // set curFounds
                        curFound = i - 1;  // We want to include the '@' at the beginning
                        curFoundCol = curCol - 1;
                        curFoundLine = curLine;

                        state = States.OnLiteralString;
                    }
                    else // It was a fake warning
                    {
                        state = States.Scanning;
                    }
                    break;

                // We must skip the next character in a string ("\"")
                case States.SkipNextCharNormal:
                    state = States.OnNormalString; // Get back to work!
                    break;

                // We're awaiting to check if we should get out the literal string or not (@""" or @"")
                case States.AwaitingLiteralExit:
                    if (chr == '"') // No, it was a fake warning
                    {
                        state = States.OnLiteralString;
                    }
                    else // Yes, we're out the literal string
                    {
                        // add match
                        foundMatches.Add(new FoundMatch(new VsTextViewRange(curFoundLine, curFoundCol,
                            curLine, curCol), new KeyValuePair<int, int>(curFound, i - curFound)));

                        state = States.Scanning;
                    }
                    break;

                // We're awaiting to check if we're to check if it was a comment or not (/ or //)
                case States.AwaitingComment:
                    switch (chr)
                    {
                        case '/': // Yes, it was a single line comment
                            state = States.OnSingleLineComment;
                            break;
                        case '*': // Yes, it was a multiline comment
                            state = States.OnMultilineComment;
                            break;
                        default:  // No, it wasn't a comment
                            state = States.Scanning;
                            break;
                    }
                    break;

                // We're scanning a single line comment
                case States.OnSingleLineComment:
                    if (chr == '\n') // We're getting out the single line comment
                    {
                        state = States.Scanning;
                    }
                    break;

                // We're scanning a multiline comment
                case States.OnMultilineComment:
                    if (chr == '*') // Maybe we're getting out the multiline comment
                    {
                        state = States.AwaitingMultilineCommentExit;
                    }
                    break;

                // We're awaiting to see if we're getting out the multiline comment
                case States.AwaitingMultilineCommentExit:
                    if (chr == '/') // Yes, we were getting out the multiline comment
                    {
                        state = States.Scanning;
                    }
                    else // No, we weren't getting out the multiline comment so get back in
                    {
                        state = States.OnMultilineComment;
                    }
                    break;
            }
            
            // required for vs mode
            if (chr == '\n')
            {
                ++curLine;
                curCol = 0;
            }
            else
                ++curCol;
        }

        return foundMatches;
        //return indiciesLengths;
    }
}