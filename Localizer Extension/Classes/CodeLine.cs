// Made by Lonami Exo | February 2016
public class CodeLine
{
    /// <summary>
    /// The content of this code line
    /// </summary>
    public string Content { get; set; }
    /// <summary>
    /// The indentation level of this code line
    /// </summary>
    public int IndentationLevel { get; set; }
    /// <summary>
    /// Is this line a comment?
    /// </summary>
    public bool IsComment => Content.StartsWith("//");

    /// <summary>
    /// Creates a new Code Line instance. Indentation is automatically calculated
    /// </summary>
    /// <param name="sourceLine">The original source line</param>
    public CodeLine(string sourceLine)
    {
        Content = sourceLine.Trim(' ', '\t');
        IndentationLevel = CalculateIndentation(sourceLine);
    }

    /// <summary>
    /// Creates a new Code Line instance. The content should have no indentation, being this specified after
    /// </summary>
    /// <param name="content">Code line content</param>
    /// <param name="indentation">Code line indentation</param>
    public CodeLine(string content, int indentation)
    {
        Content = content;
        IndentationLevel = indentation;
    }

    /// <summary>
    /// Calculate the indentation level 
    /// </summary>
    /// <param name="content">The content of which calculating the indentation</param>
    /// <returns>The indentation of this content</returns>
    public static int CalculateIndentation(string content)
    {
        if (content.Length == 0)
            return 0;

        int count = 0;
        char c = content[0] == ' ' ? ' ' : '\t';
        for (int i = 0; i < content.Length; i++)
        {
            if (content[i] != c)
                break;

            ++count;
        }

        return c == ' ' ? count / 4 : count;
    }

    /// <summary>
    /// Convert this code line to string
    /// </summary>
    /// <returns>The code line</returns>
    public override string ToString()
    {
        return new string(' ', IndentationLevel * 4) + Content;
    }
}