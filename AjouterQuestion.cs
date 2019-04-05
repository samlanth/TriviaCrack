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
    public partial class AjouterQuestion : Form
    {
        OracleConnection connection = new OracleConnection();
        DataSet monDataSet = new DataSet();
        public AjouterQuestion()
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
                CB_categorie.Items.Add(Oraread.GetString(0));
                CB_categorie.SelectedIndex = 0;
            }
        }
        private void InsererQuestion(string t)
        {
            OracleCommand oraAjout = new OracleCommand("GESTIONQUESTION", connection);
            oraAjout.CommandText = "GESTIONQUESTION.INSERTIONQUESTION";
            oraAjout.CommandType = CommandType.StoredProcedure;
            OracleParameter orapamEnoncer = new OracleParameter("enonceq", OracleDbType.Varchar2);
            orapamEnoncer.Value = t;
            oraAjout.Parameters.Add(orapamEnoncer);
            OracleParameter orapamFlag = new OracleParameter("flagq", OracleDbType.Char);
            orapamFlag.Value = '0';
            oraAjout.Parameters.Add(orapamFlag);
            OracleParameter orapamCode = new OracleParameter("codecategorieq", OracleDbType.Char);
            string selected = this.CB_categorie.GetItemText(this.CB_categorie.SelectedItem);
            char character = char.Parse(selected);
            orapamCode.Value = character;
            oraAjout.Parameters.Add(orapamCode);
            oraAjout.ExecuteNonQuery();
        }
        private void InsererReponses(string desc, char flag)
        {
            OracleCommand oraAjout = new OracleCommand("GESTIONQUESTION", connection);
            oraAjout.Parameters.Clear();
            oraAjout.CommandText = "GESTIONQUESTION.INSERTIONREPONSE";
            oraAjout.CommandType = CommandType.StoredProcedure;
            OracleParameter orapamDesc = new OracleParameter("descriptionq", OracleDbType.Char);
            orapamDesc.Value = desc;
            oraAjout.Parameters.Add(orapamDesc);
            OracleParameter orapamFlag = new OracleParameter("estbonneq", OracleDbType.Char);
            orapamFlag.Value = flag;
            oraAjout.Parameters.Add(orapamFlag);
            oraAjout.ExecuteNonQuery();
        }
        private void AjouterQuestion_Load(object sender, EventArgs e)
        {
            SeConnecter();
            AjouterCategorie();
        }
        private void bt_ajouter_Click(object sender, EventArgs e)
        {
            if (tb_reponse1.Text=="" || tb_reponse2.Text =="" || tb_reponse3.Text == "" || tb_reponse4.Text =="")
                MessageBox.Show("Réponses manquantes.");
            else if (TB_question.Text == "")
                MessageBox.Show("Question invalide.");
            else if (rb_rep1.Checked == false && rb_rep2.Checked == false && rb_rep3.Checked == false && rb_rep4.Checked == false)
                MessageBox.Show("Aucune bonne réponse n'a été sélectionnée.");
            else
            {
                if (bt_ajouter.Text == "Ajouter")
                {
                    InsererQuestion(TB_question.Text);
                    InsererReponses(tb_reponse1.Text,checkFlag(rb_rep1));
                    InsererReponses(tb_reponse2.Text, checkFlag(rb_rep2));
                    InsererReponses(tb_reponse3.Text, checkFlag(rb_rep3));
                    InsererReponses(tb_reponse4.Text, checkFlag(rb_rep4));
                }
                this.Close();
            }
        }
        private char checkFlag(RadioButton rb)
        {
            if (rb.Checked)
            {
                return 'Y';
            }
            else
            {
                return 'N';
            }
        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
