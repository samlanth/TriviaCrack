using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TP2_BD
{
    public partial class ChoisirCategorie : Form
    {
        public string ReturnValue1 { get; set; }
        public ChoisirCategorie()
        {
            InitializeComponent();
        }

        private void ChoisirCategorie_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("R (Sport)");
            comboBox1.Items.Add("B (Histoire)");
            comboBox1.Items.Add("V (Geographie)");
            comboBox1.Items.Add("J (Art-culture)");
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ReturnValue1 = comboBox1.Text.Substring(0, 1);
            this.Close();
        }
    }
}
