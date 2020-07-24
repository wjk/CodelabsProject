using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodelabsSheet.Controls;
using System.IO;

namespace CodelabsSheet
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            saveToolStripMenuItem.Click += btnSave_Click;
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt";
            dialog.Title = "Open File";
            dialog.CheckPathExists = true;
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] lines = File.ReadAllLines(dialog.FileName);
            string[] sizes = lines[0].Split(" ");

            if (int.TryParse(sizes[0], out int rows) && int.TryParse(sizes[1], out int columns))
            {
                if (rows != spreadsheetControl1.RowCount || columns != spreadsheetControl1.ColumnCount)
                {
                    MessageBox.Show(this, "This file was not created by this program. It cannot be opened.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int row = -1, col = -1;
                foreach (string line in lines.Skip(1))
                {
                    if (line == "")
                    {
                        row++;
                        col = 0;
                        continue;
                    }

                    spreadsheetControl1.SetCellContents(row, col, line);
                    col++;
                }
            }
            else
            {
                MessageBox.Show(this, "This file was not created by this program. It cannot be opened.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            addStripMenuItem1.Click += AddStripMenuItem1_Click;
            subStripMenuItem1.Click += subsStripMenuItem1_Click;
            divStripMenuItem1.Click += divStripMenuItem1_Click;
        }

        private void AddStripMenuItem1_Click(object sender, EventArgs e)
        {
            string s1 = spreadsheetControl1.GetCellContents(0, 0);
            string s2 = spreadsheetControl1.GetCellContents(0, 1);
            if (s1 == "" || s2 == "")
            {
                return;
            }

            if (int.TryParse(s1, out int v1) && int.TryParse(s2, out int v2))
            {
                int sum = v1 + v2;
                spreadsheetControl1.SetCellContents(0, 2, sum.ToString());
            }
            else
            {
                MessageBox.Show(this, "Cannot add two non-integer values. Please enter only integers in fields R1/C1 and R1/C2.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save Text";
            saveFileDialog.Filter = "text files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"{spreadsheetControl1.RowCount} {spreadsheetControl1.ColumnCount}");

                for (int row = 0; row < spreadsheetControl1.RowCount; row++)
                {
                    sb.AppendLine();

                    for (int col = 0; col < spreadsheetControl1.ColumnCount; col++)
                    {
                        sb.AppendLine(spreadsheetControl1.GetCellContents(row, col));
                    }
                }

                File.WriteAllText(saveFileDialog.FileName, sb.ToString());
            }
        }

        private void spreadsheetControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
