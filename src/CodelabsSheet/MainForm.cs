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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            addStripMenuItem1.Click += AddStripMenuItem1_Click;
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
                MessageBox.Show("Error Only Integers Allowed");
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
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = "Text";
            if (saveFileDialog.ShowDialog() == DialogResult.OK) 
            {
                File.WriteAllText(saveFileDialog.FileName, spreadsheetControl1.Text);
            }
        }

        private void spreadsheetControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
