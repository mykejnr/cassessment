using CAssessment.InputOutput;

namespace CAssessment.Views;

/// <summary>
/// Class to render a menu and execute selected menu actions
/// </summary>
/// <param name="menuItems"></param>
/// <param name="title"></param>
public class Menu(List<MenuItem> menuItems, string title)
{
    private int _width = ComputeWidth(menuItems, title);

    // Extract the action menu items that are not seperators ("-")
    private readonly List<MenuItem> actionMenus = menuItems
        .Where(x => x.Name != "-").ToList();

    // Menu will be rendered with a margin on the left
    private const string ML = "    "; // Margin Left

    /// <summary>
    /// Draws the menu with it items on the screen
    /// </summary>
    /// <param name="newTitle">
    /// Replace the title provided when constructing the menu
    /// </param>
    /// <returns>
    /// The index position of the selected menu item
    /// <para>
    /// NOTE: The indexing is "zero based" and excludes the any number 
    /// of seperators ("-")
    /// </para>
    /// </returns>
    public int Render(string? newTitle = null)
    {
        if (newTitle != null)
        {
            title = newTitle;
            _width = ComputeWidth(menuItems, title);
        }

        ConsoleHelper.ClearScreen();

        // Save cursor positions, we will reset cursor position
        // on each key down/up press
        var left = Console.CursorLeft;
        var top = Console.CursorTop;
        var selection = 0;

        while (true)
        {
            Console.SetCursorPosition(left, top);
            DrawMenu(selection);

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.Enter:
                    ExecuteMenu(selection);
                    return selection;
                case ConsoleKey.DownArrow:
                    selection += 1;
                    if (selection >= actionMenus.Count)
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

    private void DrawMenu(int selection)
    {
        var border = CreateBorder(); // Create (not draw) a horizontal border

        Console.WriteLine(border); // Draw top border
        DisplayTitle(); // Draw title
        Console.WriteLine(border); // Draw border below title

        // Items may contain "-" seperators, let's keep track 
        // of our own index of (actual menu items)
        int itemAt = 0;
        var consoleForeColor = Console.ForegroundColor;

        foreach (var item in menuItems)
        {
            if (item.Name == "-") // Menu Seperator
            {
                Console.WriteLine(border);
            }
            else
            {
                var str = ML + "| " + item.Name.PadRight(_width) + " |";
                if (itemAt == selection)
                {
                    str += " <"; // selected row indicator
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    str += "   "; // Clean the previous " <" that was drawn
                }

                Console.WriteLine(str);
                Console.ForegroundColor = consoleForeColor;
                itemAt += 1;
            }
        }

        Console.WriteLine(border); // draw bottom menu border
        Console.Write("\n  Press [x] to close menu.  "); // empty line
    }

    private void DisplayTitle()
    {
        // + 2 for left and right padding
        var fullWidth = _width + 2;
        var padding = (fullWidth - title.Length) / 2; // we geting int and not float, floating point will be chopped off

        var str = title.PadLeft(fullWidth - padding) + "".PadRight(padding);
        str = ML + "|" + str + "|";

        Console.WriteLine(str);
    }

    private string CreateBorder()
    {
        // + 2 for left and right padding for the items
        var str = ML + "+" + "".PadRight(_width + 2, '-') + "+";
        return str;
    }

    public static int ComputeWidth(List<MenuItem> items, string menuTitle)
    {
        var width = menuTitle.Length;
        // Set width to the menu item with the longest length
        foreach (var item in items)
        {
            width = item.Name.Length > width ? item.Name.Length : width;
        }
        return width;
    }

    private void ExecuteMenu(int option)
    {
        actionMenus[option].Execute();
    }
}


public class MenuItem()
{
    public string Name { get; set; } = string.Empty;
    public Action Execute { get; set; } = () => { };
}
