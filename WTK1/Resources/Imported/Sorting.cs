using System.Collections;
using System.Windows.Forms;
using WinToolkit.Classes;

/// <summary>
/// This class is an implementation of the 'IComparer' interface.
/// </summary>
public class ListViewColumnSorter : IComparer
{
    /// <summary>
    /// Specifies the column to be sorted
    /// </summary>
    private int ColumnToSort;
    /// <summary>
    /// Specifies the order in which to sort (i.e. 'Ascending').
    /// </summary>
    private SortOrder OrderOfSort;
    /// <summary>
    /// Case insensitive comparer object
    /// </summary>
    private CaseInsensitiveComparer ObjectCompare;

    /// <summary>
    /// Class constructor.  Initializes various elements
    /// </summary>
    public ListViewColumnSorter()
    {
        // Initialize the column to '0'
        ColumnToSort = 0;

        // Initialize the sort order to 'none'
        OrderOfSort = SortOrder.None;

        // Initialize the CaseInsensitiveComparer object
        ObjectCompare = new CaseInsensitiveComparer();
    }

    public string StringToBytes(string Size, bool AppendS = true)
    {
        string NewSize = Size.Substring(0, Size.LastIndexOf(' '));
        double dSize = 0;
        try
        {
            double.TryParse(NewSize, out dSize);
            if (Size.EndsWithIgnoreCase("bytes"))
            {
                return NewSize;
            }
            if (Size.EndsWithIgnoreCase("KB"))
            {
                return (dSize * 1024).ToString();
            }
            if (Size.EndsWithIgnoreCase("MB"))
            {
                return (dSize * 1024 * 1024).ToString();
            }
            if (Size.EndsWithIgnoreCase("GB"))
            {
                return (dSize * 1024 * 1024 * 1024).ToString();
            }
        }
        catch { }
        return NewSize;
    }

    /// <summary>
    /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
    /// </summary>
    /// <param name="x">First object to be compared</param>
    /// <param name="y">Second object to be compared</param>
    /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
    public int Compare(object x, object y)
    {
        int compareResult;
        ListViewItem listviewX, listviewY;

        // Cast the objects to be compared to ListViewItem objects
        listviewX = (ListViewItem)x;
        listviewY = (ListViewItem)y;

        string sText1 = listviewX.SubItems[ColumnToSort].Text;
        string sText2 = listviewY.SubItems[ColumnToSort].Text;

        double d1 = -1;
        double d2 = -1;

        if (sText1.ContainsIgnoreCase("-KB") && sText1.ContainsIgnoreCase("-x"))
        {
            while (!sText1.StartsWithIgnoreCase("KB")) { sText1 = sText1.Substring(1); }
            while (sText1.ContainsIgnoreCase("-")) { sText1 = sText1.Substring(0, sText1.Length - 1); }
        }
        if (sText2.ContainsIgnoreCase("-KB") && sText2.ContainsIgnoreCase("-x"))
        {
            while (!sText2.StartsWithIgnoreCase("KB")) { sText2 = sText2.Substring(1); }
            while (sText2.ContainsIgnoreCase("-")) { sText2 = sText2.Substring(0, sText2.Length - 1); }
        }
            

        if (sText1.StartsWithIgnoreCase("KB") && sText1.Length > 2) { d1 = double.Parse(sText1.Substring(2)); }
        if (sText2.StartsWithIgnoreCase("KB") && sText2.Length > 2) { d2 = double.Parse(sText2.Substring(2)); }
            

        if (sText1.EndsWithIgnoreCase("bytes") || sText1.EndsWithIgnoreCase("KB") || sText1.EndsWithIgnoreCase("MB") || sText1.EndsWithIgnoreCase("GB"))
        {
            d1 = double.Parse(StringToBytes(sText1));
        }
        if (sText2.EndsWithIgnoreCase("bytes") || sText2.EndsWithIgnoreCase("KB") || sText2.EndsWithIgnoreCase("MB") || sText2.EndsWithIgnoreCase("GB"))
        {
            d2 = double.Parse(StringToBytes(sText2));
        }

        if (d1 >= 0 && d2 >= 0)
        {
           if (d1 > d2) {compareResult = -1;} else {compareResult = 1;}
        }
        else
        {
            compareResult = ObjectCompare.Compare(sText1, sText2);
        }

        if (OrderOfSort == SortOrder.Ascending)
        {
            // Ascending sort is selected, return normal result of compare operation
            return compareResult;
        }
        else if (OrderOfSort == SortOrder.Descending)
        {
            // Descending sort is selected, return negative result of compare operation
            return (-compareResult);
        }
        else
        {
            // Return '0' to indicate they are equal
            return 0;
        }
    }

    /// <summary>
    /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
    /// </summary>
    public int SortColumn
    {
        set
        {
            ColumnToSort = value;
        }
        get
        {
            return ColumnToSort;
        }
    }

    /// <summary>
    /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
    /// </summary>
    public SortOrder Order
    {
        set
        {
            OrderOfSort = value;
        }
        get
        {
            return OrderOfSort;
        }
    }

}