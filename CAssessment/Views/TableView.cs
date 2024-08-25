using CAssessment.InputOutput;

namespace CAssessment.Views;

/// <summary>
/// Constructs and renders a list of <see cref="ITableItem"/> in a
/// table to the console
/// to the console
/// </summary>
/// <typeparam name="T">
/// A type that implements <see cref="ITableItem"/>
/// </typeparam>
public class TableView<T>  where T : ITableItem
{
    public List<TableColumn> Columns { get; private set; } = [];
    public List<T> Items { get; private set; }

    private readonly List<List<string>> _rows;
    private readonly List<string> _footer = [];
    private readonly string _title = "Table";
    private int _width = 0;

    // Table will be rendered with a margin on the left
    private const string ML = "    "; // Margin Left

    public TableView(List<string> columnNames, List<T> items, string title = "Table")
    {
        _title = title;
        Items = items;

        Columns = columnNames.Select(name => new TableColumn { 
            Name = name, Width = name.Length, }).ToList();

        _rows = new() {Capacity = items.Count};
        GenerateRows();
    }

    /// <summary>
    /// Set alignment for a table column
    /// </summary>
    /// <param name="columnAt">Index of column</param>
    /// <param name="align">alignment of type <see cref="ColumnAlign"/></param>
    /// <returns>This object</returns>
    public TableView<T> Align(int columnAt, ColumnAlign align)
    {
        Columns.ElementAt(columnAt).Align = align;
        return this;
    }

    /// <summary>
    /// Add a text to be rendered at the bottom of the table
    /// <para>
    /// This method can be called multiple times. Items will be rendered
    /// in the order in which they were added
    /// </para>
    /// </summary>
    /// <param name="footerString"></param>
    /// <returns></returns>
    public TableView<T> AddFooter(string footerString)
    {
        _footer.Add(footerString);
        return this;
    }

    private void GenerateRows()
    {
        // Generate a list of "list of strings".
        // Each "list of strings" in the main list represent a table row
        // generate from "Items"
        // We also take advantage of the loop to compute column widths

        int cellWidth;
        TableColumn col;

        foreach (var item in Items)
        {
            var row = item.ToTableRow(); // Get a list of strings
            _rows.Add(row);

            // For each item in the row, determine the new column widths
            for (int i = 0; i < row.Count; i++)
            {
                cellWidth = row[i].Length;
                col = Columns[i];
                col.Width = cellWidth > col.Width ? cellWidth : col.Width;
            }
        }

        // After determining the width of each column, now compute
        // the total width of the table by summing up the column widths.
        // Plus 2 for left and right border
        _width = Columns.Sum(c => c.Width + 2);

        _width += Columns.Count - 1; // add inner table borders

        // what if the title itself is longer than all the column widths combined?
        // 1. we could try and spread the extra space over the individual column width or
        // 2. we just add the extra space to the last column
        // ...Let's go easy on ourself, and choose option 2
        if (_title.Length + 2 > _width)
        { 
            // +2 left / right padding for the title
            Columns.Last().Width += _title.Length + 2 - _width;
            _width = _title.Length + 2;
        }
    }

    /// <summary>
    /// Render the table with the list of <see cref="ITableItem"/> provided
    /// </summary>
    /// <returns>
    /// The index of the selected row. Returns -1 if the table is closed
    /// without selecting any row.
    /// </returns>
    public int Render()
    {
        ConsoleHelper.ClearScreen();
        DrawCloseButton();
        RenderTableHeader();

        // Save cursor positions, we will reset cursor position
        // on each key down/up press
        var left = Console.CursorLeft;
        var top = Console.CursorTop;
        var selection = 0;

        while (true)
        {
            Console.SetCursorPosition(left, top);
            DrawTable(selection);

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.Enter:
                    // If there are no items in the table just close table
                    return Items.Count == 0 ? -1 : selection;
                case ConsoleKey.DownArrow:
                    selection += 1;
                    if (selection >= Items.Count)
                        selection = 0;
                    break;
                case ConsoleKey.UpArrow:
                    selection -= 1;
                    selection = Math.Max(0, selection);
                    break;
                case ConsoleKey.X:
                    return -1;
            }
        }
    }

    private void DrawCloseButton()
    {
        // +2 inner outer table borders
        Console.WriteLine(ML + "[x] Close".PadLeft(_width + 2));
    }

    private void RenderFooter()
    {
        // +4 table margin left
        // +2 inner outer table borders
        foreach (var footer in _footer)
            Console.WriteLine(footer.PadLeft(_width + 4 + 2));
    }

    private void DrawTable(int selection)
    {
        for (int i = 0; i < _rows.Count; i++)
        {
            RenderTableRow(_rows[i], selection == i);
        }
        if (Items.Count > 0)
            Console.WriteLine(HorizontalBorder);

        RenderFooter();
        Console.Write($"\n    *Number or records = {Items.Count} ");
    }

    private void RenderTableHeader()
    {
        RenderTableTitle();
        Console.WriteLine(HorizontalBorder);

        var str = ML + "|";
        foreach (var col in Columns)
        {
            str += " " + col.Name.PadRight(col.Width, ' ') + " |";
        }
        Console.WriteLine(str); // Column Names

        Console.WriteLine(HorizontalBorder); // Bottom Bord
    }

    private void RenderTableTitle()
    {
        // Draw a straight line on top of the title bar
        var topBorder = ML + "+" + "".PadRight(_width, '-') + "+";
        Console.WriteLine(topBorder);

        // We are trying to center align the table title
        // Se we find the extra space (difference of _width and title lenghth)
        // and divide it by two, there by getting an even left and and right paddings
        var padding = (_width - _title.Length) / 2;
        var str = _title.PadLeft(_width - padding) + "".PadRight(padding);
        str = ML + "|" + str + "|";

        Console.WriteLine(str);
    }

    private void RenderTableRow(List<string> row, bool isSelected)
    {
        var consoleForeColor = Console.ForegroundColor;
        TableColumn col;
        string cellData;

        var str = ML + "|"; // Draw left margin + left border of the row
        for (var i = 0; i < row.Count; i++)
        {
            col = Columns[i];
            cellData = row[i];

            // Align cell data accordingly
            cellData = col.Align == ColumnAlign.Left
                ? cellData.PadRight(col.Width)
                : cellData.PadLeft(col.Width);

            // for each cell draw left padding + cell data + right padding and border
            str += " " + cellData + " |";
        }
        if (isSelected)
        {
            str += " <";
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else
        {
            str += "   ";
        }

        Console.WriteLine(str);
        Console.ForegroundColor = consoleForeColor;
    }

    private string? _horizontalBorder;
    private string HorizontalBorder
    {
        // Draw eg +-----+------+--------
        get
        {
            if (_horizontalBorder != null)
                return _horizontalBorder;

            _horizontalBorder = ML + "+";
            foreach (var col in Columns)
                _horizontalBorder += "-" + "".PadRight(col.Width, '-') + "-+";

            return _horizontalBorder;
        }
    }
}


public interface ITableItem
{
    List<string> ToTableRow();
}


public class TableColumn
{
    public string Name { get; set; } = string.Empty;
    public ColumnAlign Align { get; set; } = ColumnAlign.Left;
    public int Width { get; set; }
}


public enum ColumnAlign
{
    Left,
    Right
}
