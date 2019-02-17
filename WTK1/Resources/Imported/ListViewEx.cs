/* ListViewEx.cs 
 * This file contains the definition of the class ListViewEx, which is a
 * reusable class derived from ListView. 
*/

#region Namespaces

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace WinToolkit
{
    /// <summary>
    /// Class derived from ListView to give ability to display controls
    /// like TextBox and Combobox
    /// </summary>
    /// 
    public class ListViewEx : ListView
    {
        #region The RECT structure

        /// <summary>
        /// This struct type will be used as the output 
        /// param of the SendMessage( GetSubItemRect ). 
        /// Actually it is a representation for the structure 
        /// RECT in Win32
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        #endregion

        public static ListViewEx LVE;

        #region Win32 Class

        /// <summary>
        /// Summary description for Win32.
        /// </summary>
        internal class Win32
        {
            /// <summary>
            /// This is the number of the message for getting the sub item rect.
            /// </summary>
            public const int LVM_GETSUBITEMRECT = (0x1000) + 56;

            /// <summary>
            /// As we are using the detailed view for the list,
            /// LVIR_BOUNDS is the best parameters for RECT's 'left' member.
            /// </summary>
            public const int LVIR_BOUNDS = 0;

            /// <summary>
            /// Sending message to Win32
            /// </summary>
            /// <param name="hWnd">Handle to the control</param>
            /// <param name="messageID">ID of the message</param>
            /// <param name="wParam"></param>
            /// <param name="lParam"></param>
            /// <returns></returns>
            [DllImport("user32.dll", SetLastError = true)]
            public static extern int SendMessage(IntPtr hWnd, int messageID, int wParam, ref RECT lParam);
        }

        #endregion

        #region SubItem Class

        /// <summary>
        /// This class is used to represent 
        /// a listview subitem.
        /// </summary>
        internal class SubItem
        {
            /// <summary>
            /// Subitem index
            /// </summary>
            public readonly int col;

            /// <summary>
            /// Item index
            /// </summary>
            public readonly int row;

            /// <summary>
            /// Parameterized contructor
            /// </summary>
            /// <param name="row"></param>
            /// <param name="col"></param>
            public SubItem(int row, int col)
            {
                this.row = row;
                this.col = col;
            }
        }

        #endregion

        #region Variables && Properties

        /// <summary>
        /// Combo box to display in the associated cells
        /// </summary>
        private readonly ComboBox combo = new ComboBox();

        /// <summary>
        /// To store, subitems that contains comboboxes and text boxes
        /// </summary>
        private readonly Hashtable customCells = new Hashtable();

        /// <summary>
        /// Textbox to display in the editable cells
        /// </summary>
        private readonly TextBox textBox = new TextBox();

        /// <summary>
        /// If this variable is true, then 
        /// subitems for an item is added 
        /// automatically, if not present.
        /// </summary>
        private bool addSubItem;

        /// <summary>
        /// Represents current column
        /// </summary>
        private int col = -1;

        /// <summary>
        /// This variable tells whether the combo box 
        /// is needed to be displayed after its selection
        /// is changed
        /// </summary>
        private bool hideComboAfterSelChange;

        /// <summary>
        /// This is a flag variable. This is used to determine whether
        /// Mousebutton is pressed within the listview
        /// </summary>
        private bool mouseDown;

        /// <summary>
        /// Represents current rows
        /// </summary>
        private int row = -1;

        public bool AddSubItem
        {
            set { addSubItem = value; }
        }

        public bool HideComboAfterSelChange
        {
            set { hideComboAfterSelChange = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public ListViewEx()
        {
            // Initialize controls
            InitializeComponent();

        }

        /// <summary>
        /// Initializes the text box and combo box
        /// </summary>
        private void InitializeComponent()
        {
            // Text box
            textBox.Visible = false;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Leave += textBox_Leave;
            textBox.KeyDown += textBox_KeyDown;

            // Combo box
            combo.Visible = false;
            Controls.Add(textBox);
            Controls.Add(combo);
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.SelectedIndexChanged += combo_SelectedIndexChanged;
            combo.KeyDown += combo_KeyDown;
        }

        /// <summary>
        /// This method will send LVM_GETSUBITEMRECT message to 
        /// get the current subitem bounds of the listview
        /// </summary>
        /// <param name="clickPoint"></param>
        /// <returns></returns>
        private RECT GetSubItemRect(Point clickPoint)
        {
            // Create output param
            var subItemRect = new RECT();

            // Reset the indices
            row = col = -1;

            // Check whether there is any item at the mouse point
            ListViewItem item = GetItemAt(clickPoint.X, clickPoint.Y);

            if (item != null)
            {
                for (int index = 0; index < Columns.Count; index++)
                {
                    // We need to pass the 1 based index of the subitem.
                    subItemRect.top = index + 1;

                    // To get the boudning rectangle, as we are using the report view
                    subItemRect.left = Win32.LVIR_BOUNDS;
                    try
                    {
                        // Send Win32 message for getting the subitem rect. 
                        // result = 0 means error occurred
                        int result = Win32.SendMessage(Handle, Win32.LVM_GETSUBITEMRECT, item.Index, ref subItemRect);
                        if (result != 0)
                        {
                            // This case happens when items in the first columns selected.
                            // So we need to set the column number explicitly
                            if (clickPoint.X < subItemRect.left)
                            {
                                row = item.Index;
                                col = 0;
                                break;
                            }
                            if (clickPoint.X >= subItemRect.left && clickPoint.X <=
                                 subItemRect.right)
                            {
                                row = item.Index;
                                // Add 1 because of the presence of above condition
                                col = index + 1;
                                break;
                            }
                        }
                        else
                        {
                            // This call will create a new Win32Exception with the last Win32 Error.
                            throw new Win32Exception();
                        }
                    }
                    catch (Win32Exception ex)
                    {
                        Trace.WriteLine(string.Format("Exception while getting subitem rect, {0}", ex.Message));
                    }
                }
            }
            return subItemRect;
        }

        /// <summary>
        /// Set a text box in a cell
        /// </summary>
        /// <param name="rows">The 0-based index of the item.  Give -1 if you
        ///					  want to set a text box for every items for a
        ///					  given "col" variable.
        ///	</param>
        /// <param name="cols">The 0-based index of the column. Give -1 if you
        ///					  want to set a text box for every subitems for a
        ///					  given "rows" variable.
        ///	</param>
        public void AddEditableCell(int rows, int cols)
        {
            // Add the cell into the hashtable
            // Value is setting as null because it is an editable cell
            customCells[new SubItem(rows, cols)] = null;
        }

        /// <summary>
        /// Set a combobox in a cell
        /// </summary>
        /// <param name="rows"> The 0-based index of the item.  Give -1 if you
        ///					   want to set a combo box for every items for a
        ///					   given "col" variable.
        ///	</param>
        /// <param name="cols"> The 0-based index of the column. Give -1 if you
        ///					   want to set a combo box for every subitems for a
        ///					   given "rows" variable.
        ///	</param>
        /// <param name="data"> Items of the combobox 
        /// </param>
        public void AddComboBoxCell(int rows, int cols, StringCollection data)
        {
            // Add the cell into the hashtable
            // Value for the hashtable is the combobox items
            customCells[new SubItem(rows, cols)] = data;
        }

        /// <summary>
        /// Set a combobox in a cell
        /// </summary>
        /// <param name="rows"> The 0-based index of the item.  Give -1 if you
        ///					   want to set a combo box for every items for a
        ///					   given "col" variable.
        ///	</param>
        /// <param name="cols"> The 0-based index of the column. Give -1 if you
        ///					   want to set a combo box for every subitems for a
        ///					   given "rows" variable.
        ///	</param>
        /// <param name="data"> Items of the combobox 
        /// </param>
        public void AddComboBoxCell(int rows, int cols, string[] data)
        {
            try
            {
                var param = new StringCollection();
                param.AddRange(data);
                AddComboBoxCell(rows, cols, param);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// This method will display the combobox
        /// </summary>
        /// <param name="location">Location of the combobox</param>
        /// <param name="sz">Size of the combobox</param>
        /// <param name="data">Combobox items</param>
        private void ShowComboBox(Point location, Size sz, StringCollection data)
        {
            try
            {
                // Initialize the combobox
                combo.Size = sz;
                combo.Location = location;
                // Add items
                combo.Items.Clear();
                foreach (string text in data)
                {
                    combo.Items.Add(text);
                }
                // Set the current text, take it from the current listview cell
                combo.Text = Items[row].SubItems[col].Text;
                // Calculate and set drop down width
                combo.DropDownWidth = GetDropDownWidth(data);
                // Show the combo
                combo.Show();
                combo.Focus();
            }
            catch (ArgumentOutOfRangeException)
            {
                // Sink
            }
        }

        private string previousValue = "";

        /// <summary>
        /// This method will display the textbox
        /// </summary>
        /// <param name="location">Location of the textbox</param>
        /// <param name="sz">Size of the textbox</param>
        private void ShowTextBox(Point location, Size sz)
        {
            try
            {
                // Initialize the textbox
                textBox.Size = sz;
                textBox.Location = location;
                // Set text, take it from the current list view cell
                textBox.Text = Items[row].SubItems[col].Text;
                previousValue = Items[row].SubItems[col].Text;
                // Show the text box
                textBox.Show();
                textBox.Focus();
            }
            catch (ArgumentOutOfRangeException)
            {
                // Sink
            }
        }

        private ListViewColumnSorter lvwColumnSorter;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// 
        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            try
            {
                if (LVE != null && LVE.Items.Count > 0)
                {
                    if (LVE.ListViewItemSorter == null || lvwColumnSorter == null)
                    {
                        lvwColumnSorter = new ListViewColumnSorter();
                        LVE.ListViewItemSorter = lvwColumnSorter;
                    }
                    // Determine if clicked column is already the column that is being sorted.
                    if (e.Column == lvwColumnSorter.SortColumn)
                    {
                        // Reverse the current sort direction for this column.
                        if (lvwColumnSorter.Order == SortOrder.Ascending)
                        {
                            lvwColumnSorter.Order = SortOrder.Descending;
                        }
                        else
                        {
                            lvwColumnSorter.Order = SortOrder.Ascending;
                        }
                    }
                    else
                    {
                        // Set the column number that is to be sorted; default to ascending.
                        lvwColumnSorter.SortColumn = e.Column;
                        lvwColumnSorter.Order = SortOrder.Ascending;
                    }

                    // Perform the sort with these new sort options.
                    LVE.Sort();
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(null, "Error on ColumnClick", Ex.Message, LVE.ToString());
                MessageBox.Show(Ex.Message, "Error");
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            {
                // Hide the controls
                textBox.Visible = combo.Visible = false;

                // If no mouse down happened in this list view, 
                // no need to show anything
                if (!mouseDown)
                {
                    return;
                }

                // The list view should be having the following properties enabled
                // 1. FullRowSelect = true
                // 2. View should be Detail;
                if (!FullRowSelect || View != View.Details)
                {
                    return;
                }

                // Reset the mouse down flag
                mouseDown = false;

                // Get the subitem rect at the mouse point.
                // Remember that the current rows index and column index will also be
                // Modified within the same method
                RECT rect = GetSubItemRect(new Point(e.X, e.Y));

                // If the above method is executed with any error,
                // The rows index and column index will be -1;
                if (row != -1 && col != -1)
                {
                    // Check whether combo box or text box is set for the current cell
                    SubItem cell = GetKey(new SubItem(row, col));

                    if (cell != null)
                    {
                        // Set the size of the control(combo box/edit box)
                        // This should be composed of the height of the current items and
                        // width of the current column
                        var sz = new Size(Columns[col].Width, Items[row].Bounds.Height);

                        // Determine the location where the control(combobox/edit box) to be placed
                        Point location = col == 0 ? new Point(0, rect.top) : new Point(rect.left, rect.top);

                        ValidateAndAddSubItems();

                        // Decide which control to be displayed.
                        if (customCells[cell] == null)
                        {
                            ShowTextBox(location, sz);
                        }
                        else
                        {
                            ShowComboBox(location, sz, (StringCollection)customCells[cell]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ValidateAndAddSubItems()
        {
            try
            {
                while (Items[row].SubItems.Count < Columns.Count && addSubItem)
                {
                    Items[row].SubItems.Add("");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// This message will get the largest text from the given
        /// string array, and will calculate the width of a control which 
        /// will contain that text.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private int GetDropDownWidth(StringCollection data)
        {
            // If array is empty just return the combo box width
            if (data.Count == 0)
            {
                return combo.Width;
            }

            // Set the first text as the largest
            string maximum = data[0];

            // Now iterate thru each string, to find out the
            // largest
            foreach (string text in data)
            {
                if (maximum.Length < text.Length)
                {
                    maximum = text;
                }
            }
            // Calculate and return the width .
            return (int)(CreateGraphics().MeasureString(maximum, Font).Width);
        }

        /// <summary>
        /// For this method, we will get a Subitem. 
        /// Then we will iterate thru each of the keys and will 
        /// check whether any key contains the given cells rows/column.
        /// If it is not found we will check for -1 in any one
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private SubItem GetKey(SubItem cell)
        {
            try
            {
                foreach (SubItem key in customCells.Keys)
                {
                    // Case 1: Any particular cells is  enabled for a control(Textbox/combobox)
                    if (key.row == cell.row && key.col == cell.col)
                    {
                        return key;
                    }
                    // Case 2: Any particular column is  enabled for a control(Textbox/combobox)
                    if (key.row == -1 && key.col == cell.col)
                    {
                        return key;
                    }
                    // Entire col for a rows is enabled for a control(Textbox/combobox)
                    if (key.row == cell.row && key.col == -1)
                    {
                        return key;
                    }
                    // All cells are enabled for a control(Textbox/combobox)
                    if (key.row == -1 && key.col == -1)
                    {
                        return key;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return null;
        }

        private void combo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    // Mouse down happened inside list view
                    mouseDown = true;

                    // Hide the controls
                    textBox.Hide();
                    combo.Hide();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                // Mouse down happened inside list view
                mouseDown = true;

                // Hide the controls
                textBox.Hide();
                combo.Hide();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

        private void textBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (row != -1 && col != -1)
                    {
                        Items[row].SubItems[col].Text = textBox.Text;
                        if (string.IsNullOrEmpty(Items[row].SubItems[col].Text))
                        {
                            Items[row].SubItems[col].Text = previousValue;
                        }
                        textBox.Hide();
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }
        }

        /// <summary>
        /// This event handler will set the current text in the textbox
        /// as the list view's current cell's text, while the textbox 
        /// focus is lost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (row != -1 && col != -1)
                {
                    Items[row].SubItems[col].Text = textBox.Text;
                    if (string.IsNullOrEmpty(Items[row].SubItems[col].Text))
                    {
                        Items[row].SubItems[col].Text = previousValue;
                    }
                    textBox.Hide();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// This event handler will set the current text in the combo box
        /// as the listview's current cell's text, while the combobox 
        /// selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (row != -1 && col != -1)
                {
                    Items[row].SubItems[col].Text = combo.Text;
                    if (Items[row].SubItems[col].Text != Items[row].SubItems[col + 1].Text)
                    {
                        Items[row].Checked = true;
                    }
                    else
                    {
                        Items[row].Checked = false;
                    }
                    combo.Visible = false;
                    combo.Hide();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

            this.Focus();
        }

        #endregion
    }
}