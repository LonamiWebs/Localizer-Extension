// Made by Lonami Exo | March 2016
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class Code
{
    // list of code lines
    List<CodeLine> lines;

    // regex used to match usings
    static readonly Regex usingsRegex = new Regex(@"using ((?:\w+\.?)+);", RegexOptions.Compiled);

    /// <summary>
    /// Gets a Code from a given file
    /// </summary>
    /// <param name="file">The file containing the code</param>
    /// <returns>The parsed code file</returns>
    public static Code FromFile(string file)
    {
        return new Code(File.ReadAllLines(file, Encoding.UTF8));
    }
    
    /// <summary>
    /// Gets a Code from a given source string
    /// </summary>
    /// <param name="source">The code source</param>
    public Code(string source) : this(source.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.None)) { }
    
    /// <summary>
    /// Gets a Code from a given source code lines
    /// </summary>
    /// <param name="sourceLines">The code source</param>
    public Code(string[] sourceLines)
    {
        lines = new List<CodeLine>(sourceLines.Length);
        foreach (var line in sourceLines)
            lines.Add(new CodeLine(line));
    }

    /// <summary>
    /// Does this code contain the following line?
    /// </summary>
    /// <param name="lineSource">The line source to check</param>
    /// <returns>True if this Code contains the specified line source</returns>
    public bool ContainsLineSource(string lineSource)
    {
        foreach (var line in lines)
            if (line.Content.Contains(lineSource))
                return true;

        return false;
    }

    /// <summary>
    /// Retrieves the code line containing this source. An exception is thrown if it's not found
    /// </summary>
    /// <param name="lineSource">The line source</param>
    /// <returns>The found code line</returns>
    public CodeLine FindLine(string lineSource)
    {
        foreach (var line in lines)
            if (line.Content.Contains(lineSource))
                return line;

        throw new KeyNotFoundException("Line source not found");
    }

    /// <summary>
    /// Appends source code at the start of the specified method
    /// </summary>
    /// <param name="methodName">The method name</param>
    /// <param name="lineSources">The line sources to append</param>
    public void AppendAtMethodStart(string methodName, params string[] lineSources)
    {
        bool foundMethod = false;
        for (int i = 0; i < lines.Count; i++)
        {
            if (foundMethod)
            {
                if (lines[i].Content.Contains("{"))
                {
                    AppendSourceAfter(lines[i + 1], lines[i].IndentationLevel + 1, lineSources);
                    break;
                }
            }
            else if (lines[i].Content.Contains(methodName))
            {
                foundMethod = true;
                if (lines[i].Content[lines[i].Content.Length - 1] == '{')
                {
                    AppendSourceAfter(lines[i + 1], lines[i].IndentationLevel + 1, lineSources);
                    break;
                }
            }
        }
    }

    // TODO left braces doesn't work with strings or characters, or if there's an empty line between method name and brace
    /// <summary>
    /// Appends source code at the end of the specified method
    /// </summary>
    /// <param name="methodName">The method name</param>
    /// <param name="lineSources">The line sources to append</param>
    public void AppendAtMethodEnd(string methodName, params string[] lineSources)
    {
        int leftBraces = 0;
        bool foundMethod = false;
        for (int i = 0; i < lines.Count; i++)
        {
            if (foundMethod)
            {
                leftBraces += checkBraces(lines[i]);
                if (leftBraces == 0)
                {
                    AppendSourceAfter(lines[i], lines[i].IndentationLevel + 1, lineSources);
                    break;
                }
            }
            else if (lines[i].Content.Contains(methodName))
            {
                foundMethod = true;
                leftBraces += checkBraces(lines[i]);
            }
        }
    }
    // check how many open/close braces there are
    int checkBraces(CodeLine line)
    {
        int braces = 0;
        foreach (var c in line.Content)
            if (c == '{')
                ++braces;
            else if (c == '}')
                --braces;
        return braces;
    }

    /// <summary>
    /// Append source after the specified line, with an optional indentation. If default indentation is desired, set to -1
    /// </summary>
    /// <param name="line">The line on which after the new source will be appended</param>
    /// <param name="indentation">-1 to use default, else specify the default manually</param>
    /// <param name="lineSources">The source lines to append</param>
    public void AppendSourceAfter(CodeLine line, int indentation, params string[] lineSources)
    {
        for (int i = 0; i < lines.Count; i++)
            if (lines[i] == line)
            {
                var newLines = new List<CodeLine>(lineSources.Length);
                foreach (var lineSource in lineSources)
                {
                    if (indentation < 0)
                    {
                        newLines.Add(new CodeLine(lineSource, lines[i].IndentationLevel));
                    }
                    else
                    {
                        var cl = new CodeLine(lineSource);
                        cl.IndentationLevel += indentation;
                        newLines.Add(cl);
                    }
                }

                lines.InsertRange(i, newLines);

                break;
            }
    }

    /// <summary>
    /// Does the code have the following using namespace?
    /// </summary>
    /// <param name="namespace">The namespace to check</param>
    /// <returns>True if the code has this namespace</returns>
    public bool HasUsing(string @namespace)
    {
        foreach (var line in lines)
        {
            var usingsMatch = usingsRegex.Match(line.Content);
            if (usingsMatch.Success)
                if (usingsMatch.Groups[1].Value == @namespace)
                    return true;
        }

        return false;
    }

    /// <summary>
    /// Adds the specified using namespace
    /// </summary>
    /// <param name="namespace">The namespace to add</param>
    public void AddUsing(string @namespace)
    {
        for (int i = 0; i < lines.Count; i++)
            if (!lines[i].IsComment)
            {
                AppendSourceAfter(lines[i], 0, "using " + @namespace + ";");
                break;
            }
    }

    /// <summary>
    /// Convert this code to string
    /// </summary>
    /// <returns>The code file</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var line in lines)
            sb.AppendLine(line.ToString());

        return sb.ToString();
    }

    /// <summary>
    /// Save this code to a desired file
    /// </summary>
    /// <param name="file">The output file</param>
    public void SaveTo(string file)
    {
        File.WriteAllText(file, ToString(), Encoding.UTF8);
    }
}