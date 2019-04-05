using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Globalization;
namespace TP2_BD
{
    public partial class Form1 : Form
    {
        private static Random random = new Random();
        OracleConnection connection = new OracleConnection();
        public Form1()
        {
            InitializeComponent();
        }
        private void SeConnecter()
        {
            try
            {
                string dsource = "(DESCRIPTION="
                     + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)"
                     + "(HOST=mercure.clg.qc.ca)(PORT=1521)))"
                     + "(CONNECT_DATA=(SERVICE_NAME=ORCL.clg.qc.ca)))";
                string chaineDeconnexion = "Data Source = " + dsource + "; User Id = lanthies; password = ORACLE1";
                connection.ConnectionString = chaineDeconnexion;
                connection.Open();
            }
            catch (Exception sqlConnex)
            {
                MessageBox.Show(sqlConnex.Message.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SeConnecter();
            Titre.Font = new Font("Arial", 15, FontStyle.Bold);
            //Titre.Left = (this.ClientSize.Width - Titre.Width) / 2;
            //Start.Left = (this.ClientSize.Width - Start.Width) / 2;
            //Start.Top = (this.ClientSize.Height - Start.Height) / 3;
            //Apropos.Left = (this.ClientSize.Width - Apropos.Width) / 2;

        }
        

        private void bt_ajouter_Click(object sender, EventArgs e)
        {
            AjouterQuestion dlg = new AjouterQuestion();
            if (dlg.ShowDialog() == DialogResult.OK)
            {

            }
        }
        

        private void bt_chercherq_Click(object sender, EventArgs e)
        {
            ChercherQuestion dlg = new ChercherQuestion();
            if (dlg.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void bt_aff_q_Click(object sender, EventArgs e)
        {
            AfficherQuestion dlg = new AfficherQuestion();
            if (dlg.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
