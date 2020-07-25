using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodelabsSheet.Controls
{
    public class SpreadsheetControl : Panel
    {
        private int DpiScale(int constant) => constant * DeviceDpi / 96;

        private readonly TableLayoutPanel LayoutPanel = new TableLayoutPanel();
        private readonly Button AddRowButton = new Button();
        private readonly Button AddColumnButton = new Button();

        public SpreadsheetControl()
        {
            AddRowButton.Text = "Add Row";
            AddRowButton.AutoSize = true;
            AddRowButton.Click += (s, e) => RowCount++;
            AddColumnButton.Text = "Add Column";
            AddColumnButton.AutoSize = true;
            AddColumnButton.Click += (s, e) => ColumnCount++;

            LayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            LayoutPanel.RowCount = 5;
            LayoutPanel.ColumnCount = 5;
            LayoutPanel.Controls.Add(AddRowButton, 1, 4);
            LayoutPanel.Controls.Add(AddColumnButton, 4, 1);
            LayoutPanel.SetColumnSpan(AddRowButton, 4);

            for (int row = 0; row < LayoutPanel.RowCount; row++)
            {
                RowStyle style = new RowStyle(SizeType.AutoSize);
                LayoutPanel.RowStyles.Add(style);
            }
            for (int col = 0; col < LayoutPanel.ColumnCount; col++)
            {
                ColumnStyle style = new ColumnStyle();
                if (col == 0 || col == LayoutPanel.ColumnCount - 1)
                {
                    style.SizeType = SizeType.AutoSize;
                }
                else
                {
                    style.SizeType = SizeType.Absolute;
                    style.Width = DpiScale(200);
                }

                LayoutPanel.ColumnStyles.Add(style);
            }

            for (int row = 0; row < LayoutPanel.RowCount; row++)
            {
                for (int col = 0; col < LayoutPanel.ColumnCount; col++)
                {
                    if (col == 0 && row == 0) continue;

                    if (col == 0 && row != LayoutPanel.RowCount - 1)
                    {
                        Label label = new Label();
                        label.Text = $"R{row:D}";
                        label.TextAlign = ContentAlignment.MiddleRight;
                        label.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                        LayoutPanel.Controls.Add(label, col, row);
                    }
                    else if (row == 0 && col != LayoutPanel.ColumnCount - 1)
                    {
                        Label label = new Label();
                        label.Text = $"C{col:D}";
                        label.TextAlign = ContentAlignment.BottomCenter;
                        label.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                        label.AutoSize = true;
                        LayoutPanel.Controls.Add(label, col, row);
                    }
                    else if (col != LayoutPanel.ColumnCount - 1 && row != LayoutPanel.RowCount - 1)
                    {
                        TextBox box = new TextBox();
                        box.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                        LayoutPanel.Controls.Add(box, col, row);
                    }
                }
            }

            LayoutPanel.AutoSize = true;
            LayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            LayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            LayoutPanel.Location = new Point(DpiScale(5), DpiScale(5));
            LayoutPanel.BackColor = SystemColors.Control;

            Panel scrollPanel = new Panel();
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(LayoutPanel);
            scrollPanel.Dock = DockStyle.Fill;
            Controls.Add(scrollPanel);
        }

        public string GetCellContents(int row, int column)
        {
            if (row < 0 || row >= RowCount) throw new ArgumentOutOfRangeException(nameof(row));
            if (column < 0 || column >= ColumnCount) throw new ArgumentOutOfRangeException(nameof(column));

            TextBox box = (TextBox)LayoutPanel.GetControlFromPosition(column + 1, row + 1);
            return box.Text;
        }

        public void SetCellContents(int row, int column, string text)
        {
            if (row < 0 || row >= RowCount) throw new ArgumentOutOfRangeException(nameof(row));
            if (column < 0 || column >= ColumnCount) throw new ArgumentOutOfRangeException(nameof(column));

            TextBox box = (TextBox)LayoutPanel.GetControlFromPosition(column + 1, row + 1);
            box.Text = text;
        }

        public int RowCount
        {
            get
            {
                // Subtract 2 to remove the header and footer rows.
                return LayoutPanel.RowCount - 2;
            }

            set
            {
                if (value < 3) throw new InvalidOperationException("RowCount cannot be less than 3");

                int oldRowCount = LayoutPanel.RowCount;
                int newRowCount = value + 2;
                if (newRowCount == oldRowCount) return;

                LayoutPanel.SuspendLayout();
                LayoutPanel.Controls.Remove(AddRowButton);

                if (newRowCount < oldRowCount)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    LayoutPanel.RowCount = newRowCount;
                    LayoutPanel.RowStyles.Clear();
                    for (int row = 0; row < newRowCount; row++)
                    {
                        LayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    }

                    for (int row = oldRowCount; row < newRowCount; row++)
                    {
                        Label label = new Label();
                        label.Text = $"R{row - 1:D}";
                        label.TextAlign = ContentAlignment.MiddleRight;
                        label.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                        LayoutPanel.Controls.Add(label, 0, row - 1);

                        for (int col = 1; col < LayoutPanel.ColumnCount - 1; col++)
                        {
                            TextBox box = new TextBox();
                            box.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                            LayoutPanel.Controls.Add(box, col, row - 1);
                        }
                    }
                }

                LayoutPanel.Controls.Add(AddRowButton, 1, newRowCount - 1);
                LayoutPanel.ResumeLayout();
            }
        }

        public int ColumnCount
        {
            get
            {
                // Subtract 2 to remove the header and footer rows.
                return LayoutPanel.ColumnCount - 2;
            }

            set
            {
                if (value < 3) throw new InvalidOperationException("ColumnCount cannot be less than 3");
                
                int oldColumnCount = LayoutPanel.ColumnCount;
                int newColumnCount = value + 2;
                if (newColumnCount == oldColumnCount) return;

                LayoutPanel.SuspendLayout();
                LayoutPanel.Controls.Remove(AddColumnButton);
                LayoutPanel.ColumnStyles.RemoveAt(LayoutPanel.ColumnStyles.Count - 1);
                LayoutPanel.ColumnCount--;

                if (newColumnCount < oldColumnCount)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    for (int col = oldColumnCount; col < newColumnCount; col++)
                    {
                        ColumnStyle columnStyle = new ColumnStyle(SizeType.Absolute, DpiScale(200));
                        int index = LayoutPanel.ColumnStyles.Add(columnStyle);
                        LayoutPanel.ColumnCount++;

                        Label label = new Label();
                        label.Text = $"C{col - 1:D}";
                        label.TextAlign = ContentAlignment.MiddleRight;
                        label.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                        LayoutPanel.Controls.Add(label, col - 1, 0);

                        for (int row = 1; row < LayoutPanel.RowCount - 1; row++)
                        {
                            TextBox box = new TextBox();
                            box.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                            LayoutPanel.Controls.Add(box, col - 1, row);
                        }
                    }
                }

                LayoutPanel.ColumnCount++;
                LayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                LayoutPanel.Controls.Add(AddColumnButton, newColumnCount - 1, 1);
                LayoutPanel.ResumeLayout();
            }
        }
    }
}
