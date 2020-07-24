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
            subStripMenuItem1.Click += subsStripMenuItem1_Click;
            divStripMenuItem1.Click += divStripMenuItem1_Click;
        }

       

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }
        private void AddStripMenuItem1_Click(object sender, EventArgs e)
        {
            string s1 = spreadsheetControl1.GetCellContents(0, 0);
            string s2 = spreadsheetControl1.GetCellContents(0, 1);
            MessageBox.Show(s1 + "and" + s2);
        }

        private void subsStripMenuItem1_Click(object sender, EventArgs e)
        {
            string s1 = spreadsheetControl1.GetCellContents(0, 0);
            string s2 = spreadsheetControl1.GetCellContents(0, 1);
            MessageBox.Show(s1 + "and" + s2);
        }

        private void divStripMenuItem1_Click(object sender, EventArgs e)
        {
            string s1 = spreadsheetControl1.GetCellContents(0, 0);
            string s2 = spreadsheetControl1.GetCellContents(0, 1);
            MessageBox.Show(s1 + "and" + s2);
        }
    }
}
