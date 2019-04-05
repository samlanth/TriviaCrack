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

namespace TP2_BD
{
    public partial class ChercherQuestion : Form
    {
        public int numquestion;
        OracleConnection connection = new OracleConnection();
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
        public ChercherQuestion()
        {
            InitializeComponent();
        }
        private void AjouterCategorie()
        {
            OracleCommand oraliste = new OracleCommand("gestioncategorie", connection);
            oraliste.CommandText = "gestioncategorie.listcodecategorie";
            oraliste.CommandType = CommandType.StoredProcedure;
            // pour une fonction, le paramètre de retour doit être déclaré en premier.
            OracleParameter OrapameResultat = new OracleParameter("RESULTAT", OracleDbType.RefCursor);
            OrapameResultat.Direction = ParameterDirection.ReturnValue;
            oraliste.Parameters.Add(OrapameResultat);
            OracleDataReader Oraread = oraliste.ExecuteReader();
            while (Oraread.Read())
            {
                if (Oraread.GetString(0) == "V")
                {
                    string artculture = " (Art-Culture)";
                    listBox1.Items.Add(Oraread.GetString(0) + artculture);
                }
                else if (Oraread.GetString(0) == "R")
                {
                    string  Sport = " (Sport)";
                    listBox1.Items.Add(Oraread.GetString(0) + Sport);
                }
                else if (Oraread.GetString(0) == "B")
                {
                    string Histoire = " (Histoire)";
                    listBox1.Items.Add(Oraread.GetString(0) + Histoire);
                }
                else if (Oraread.GetString(0) == "J")
                {
                    string Geographie = " (Geographie)";
                    listBox1.Items.Add(Oraread.GetString(0) +Geographie);
                }
                    listBox1.SelectedIndex = 0;
            }
        }
        private void ChercherQuestion_Load(object sender, EventArgs e)
        {
            SeConnecter();
            AjouterCategorie();
        }

        

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            DGV_Question.Rows.Clear();
            OracleCommand oraliste = new OracleCommand("GESTIONQUESTION", connection);
            oraliste.CommandText = "GESTIONQUESTION.chercherquestion";
            oraliste.CommandType = CommandType.StoredProcedure;
            // pour une fonction, le paramètre de retour doit être déclaré en premier.
            OracleParameter OrapameResultat = new OracleParameter("RESULTAT", OracleDbType.RefCursor);
            OrapameResultat.Direction = ParameterDirection.ReturnValue;
            oraliste.Parameters.Add(OrapameResultat);

            OracleParameter orapamecategorie = new OracleParameter("pcodecategorie", OracleDbType.Char);
            string codecategorie = listBox1.SelectedItem.ToString().Substring(0,1);
            orapamecategorie.Value = codecategorie;
            orapamecategorie.Direction = ParameterDirection.Input;
            oraliste.Parameters.Add(orapamecategorie);

            OracleDataReader Oraread = oraliste.ExecuteReader();
            while (Oraread.Read())
            {
                DGV_Question.Rows.Add(Oraread.GetInt32(1), Oraread.GetString(0));
                //numquestion = Oraread.GetInt32(1);
            }
            Oraread.Close();
            DGV_Question.Refresh();
            DGV_Question.Update();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lb_question_DoubleClick(object sender, EventArgs e)
        {
            lb_reponse.Items.Clear();
            OracleCommand oraliste = new OracleCommand("GESTIONQUESTION", connection);
            oraliste.CommandText = "GESTIONQUESTION.reponse";
            oraliste.CommandType = CommandType.StoredProcedure;
            // pour une fonction, le paramètre de retour doit être déclaré en premier.
            OracleParameter OrapameResultat = new OracleParameter("RESULTAT", OracleDbType.RefCursor);
            OrapameResultat.Direction = ParameterDirection.ReturnValue;
            oraliste.Parameters.Add(OrapameResultat);

            OracleParameter orapamecategorie = new OracleParameter("pnum", OracleDbType.Char);

            int selectedRow = DGV_Question.CurrentCell.RowIndex;

            orapamecategorie.Value = DGV_Question.Rows[selectedRow].Cells[0].Value.ToString();


            //orapamecategorie.Value = numquestion;
            orapamecategorie.Direction = ParameterDirection.Input;
            oraliste.Parameters.Add(orapamecategorie);

            OracleDataReader Oraread = oraliste.ExecuteReader();
            while (Oraread.Read())
            {
                lb_reponse.Items.Add(Oraread.GetString(1));
                lb_reponse.SelectedIndex = 0;
            }
        }

        private void DGV_Question_DoubleClick(object sender, EventArgs e)
        {
            lb_reponse.Items.Clear();
            OracleCommand oraliste = new OracleCommand("GESTIONQUESTION", connection);
            oraliste.CommandText = "GESTIONQUESTION.reponse";
            oraliste.CommandType = CommandType.StoredProcedure;
            // pour une fonction, le paramètre de retour doit être déclaré en premier.
            OracleParameter OrapameResultat = new OracleParameter("RESULTAT", OracleDbType.RefCursor);
            OrapameResultat.Direction = ParameterDirection.ReturnValue;
            oraliste.Parameters.Add(OrapameResultat);

            OracleParameter orapamecategorie = new OracleParameter("pnum", OracleDbType.Char);

            int selectedRow = DGV_Question.CurrentCell.RowIndex;

            orapamecategorie.Value = DGV_Question.Rows[selectedRow].Cells[0].Value.ToString();


            //orapamecategorie.Value = numquestion;
            orapamecategorie.Direction = ParameterDirection.Input;
            oraliste.Parameters.Add(orapamecategorie);
            OracleDataReader Oraread = oraliste.ExecuteReader();
            while (Oraread.Read())
            {
                lb_reponse.Items.Add(Oraread.GetString(1) + " " + Oraread.GetString(2));
                //lb_reponse.Items.Add(Oraread.GetString(1));
                lb_reponse.SelectedIndex = 0;
            }
        }
    }
    
}
