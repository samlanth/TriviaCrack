using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace TP2_BD
{
    public partial class AfficherQuestion : Form
    {
        private OracleConnection connection = new OracleConnection();
        private DataSet monDataSet = new DataSet();
        private OracleDataAdapter OraAdapter = new OracleDataAdapter();
        private int i = 0;
        private static Random random = new Random();
        public string question;
        public int indexBonneReponse = 0;
        public string reponse;
        public int points;
        string ReponseValide;
        string CategorieChoisi;
        private DataSet monDataSet2 = new DataSet();
        private OracleDataAdapter OraAdapter2 = new OracleDataAdapter();

        public AfficherQuestion()
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
        private void UpdateDGV()
        {
            DGV_joueurs.Refresh();
            DGV_joueurs.Update();
            dgv_score.Refresh();
            dgv_score.Update();
        }
        private void ClearTB()
        {
            tb_alias.Clear();
            tb_prenom.Clear();
            tb_nom.Clear();
            tb_rep1.Text = "";
            tb_rep2.Text = "";
            tb_rep3.Text = "";
            tb_rep4.Text = "";
        }
        private void ListerJoueurDGV()
        {
            try
            {
                OracleCommand oraliste = new OracleCommand("gestionjoueur", connection);
                oraliste.CommandText = "gestionjoueur.LISTJOUEUR";
                oraliste.CommandType = CommandType.StoredProcedure;
                OracleParameter OrapamResultat = new OracleParameter("RESULTAT", OracleDbType.RefCursor);
                OrapamResultat.Direction = ParameterDirection.ReturnValue;
                oraliste.Parameters.Add(OrapamResultat);
                oraliste.ExecuteNonQuery();
                UpdateDGV();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        private void ListerScoreDGV()
        {
            try
            {
                OracleCommand oraliste = new OracleCommand("GESTIONQUESTION", connection);
                oraliste.CommandText = "GESTIONQUESTION.LISTER";
                oraliste.CommandType = CommandType.StoredProcedure;
                OracleParameter OrapamResultat = new OracleParameter("RESULTAT", OracleDbType.RefCursor);
                OrapamResultat.Direction = ParameterDirection.ReturnValue;
                oraliste.Parameters.Add(OrapamResultat);
                OracleDataAdapter orAdapter = new OracleDataAdapter(oraliste);
                if (monDataSet2.Tables.Contains("ListeScore"))
                {
                    monDataSet2.Tables["ListeScore"].Clear();
                }
                orAdapter.Fill(monDataSet2, "ListeScore");
                oraliste.Dispose();
                BindingSource maSource = new BindingSource(monDataSet2, "ListeScore");
                dgv_score.DataSource = maSource;
                UpdateDGV();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        private void Ajouter_Joueur()
        {
            try
            {
                OracleCommand oraAjout = new OracleCommand("gestionjoueur", connection);
                oraAjout.CommandText = "gestionjoueur.INSERTIONJOUEUR";
                oraAjout.CommandType = CommandType.StoredProcedure;
                OracleParameter orapamAlias = new OracleParameter("aliass", OracleDbType.Varchar2);
                orapamAlias.Direction = ParameterDirection.Input;
                orapamAlias.Value = tb_alias.Text;
                oraAjout.Parameters.Add(orapamAlias);
                OracleParameter orapamNom = new OracleParameter("nomm", OracleDbType.Varchar2);
                orapamNom.Direction = ParameterDirection.Input;
                orapamNom.Value = tb_nom.Text;
                oraAjout.Parameters.Add(orapamNom);
                OracleParameter orapamPrenom = new OracleParameter("prenomm", OracleDbType.Varchar2);
                orapamPrenom.Direction = ParameterDirection.Input;
                orapamPrenom.Value = tb_prenom.Text;
                oraAjout.Parameters.Add(orapamPrenom);
                oraAjout.ExecuteNonQuery();
                DGV_joueurs.Rows.Add(tb_alias.Text, tb_nom.Text, tb_prenom.Text);
                UpdateDGV();
                ClearTB();
                ListerScoreDGV();
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message.ToString());
            }
        }
        string JoueurScore1, JoueurScore2;
        string codeR, codeB, codeJ, codeV;
        private void ScoreDesJoueurs()
        {
            int i = 0;
            OracleCommand oracmdListJoueur = new OracleCommand("gestionjoueur", connection);
            oracmdListJoueur.CommandText = "gestionjoueur.ListerJoueurDansScore";
            oracmdListJoueur.CommandType = CommandType.StoredProcedure;
            OracleParameter orapamRes = new OracleParameter("res", OracleDbType.RefCursor);
            orapamRes.Direction = ParameterDirection.ReturnValue;
            oracmdListJoueur.Parameters.Add(orapamRes);
            OracleDataReader Oraread = oracmdListJoueur.ExecuteReader();
            while (Oraread.Read())
            {
                i++;
                if (i==1)
                {
                    JoueurScore1 = Oraread.GetString(0);
                    codeB = Oraread.GetString(1);
                }
                else if (i==2)
                {
                    JoueurScore2 = Oraread.GetString(0);
                }
                else if (i==3)
                {
                    codeJ = Oraread.GetString(1);
                }
                else if (i==5)
                {
                    codeR = Oraread.GetString(1);
                }
                else if (i==7)
                {
                    codeV = Oraread.GetString(1);
                }
                else
                {
                }
            }
            Oraread.Close();
        }
        int[] Total = new int[8];
        private int NombrePointCategorie(string nomJoueur, string codeCategorie)
        {
            try
            {
                OracleCommand oraCmdTotal = new OracleCommand("gestionjoueur", connection);
                oraCmdTotal.CommandText = "gestionjoueur.NombrePoint";
                oraCmdTotal.CommandType = CommandType.StoredProcedure;
                OracleParameter orapamTotal = new OracleParameter("total", OracleDbType.Int32);
                orapamTotal.Direction = ParameterDirection.ReturnValue;
                oraCmdTotal.Parameters.Add(orapamTotal);
                OracleParameter orapamAlias = new OracleParameter("palias", OracleDbType.Varchar2);
                orapamAlias.Direction = ParameterDirection.Input;
                orapamAlias.Value = nomJoueur;
                oraCmdTotal.Parameters.Add(orapamAlias);
                OracleParameter orapamCode = new OracleParameter("ccode", OracleDbType.Varchar2);
                orapamCode.Direction = ParameterDirection.Input;
                orapamCode.Value = codeCategorie.Substring(0,1);
                oraCmdTotal.Parameters.Add(orapamCode);
                oraCmdTotal.ExecuteScalar();
                string s = orapamTotal.Value.ToString();
                int a;
                Int32.TryParse(s, out a);
                return a;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            return 0;
        }
        public void Disable_btreps()
        {
            tb_rep1.Enabled = false;
            tb_rep2.Enabled = false;
            tb_rep3.Enabled = false;
            tb_rep4.Enabled = false;
        }
        public void Enable_btreps()
        {
            tb_rep1.Enabled = true;
            tb_rep2.Enabled = true;
            tb_rep3.Enabled = true;
            tb_rep4.Enabled = true;
        }
        private void AjouterCategorie()
        {
            CB_categorie.Items.Clear();
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
                //CB_categorie.SelectedIndex = 0;
            }
        }
        private void AfficherQuestion_Load(object sender, EventArgs e)
        {

            SeConnecter();
            //ListerJoueurDGV();
            //ScoreDesJoueurs();
            //AjouterCategorie();
            CB_categorie.Enabled = false;
            LB_Question.MaximumSize = new Size(1000, 0);
            LB_Question.AutoSize = true;
            De.MaximumSize = new Size(1000, 0);
            De.AutoSize = true;
            De.Font = new Font("Arial", 15, FontStyle.Bold);
            //Titre.Font = new Font("Arial", 15, FontStyle.Bold);
            LB_Question.Font = new Font("Arial", 10, FontStyle.Bold);
            Disable_btreps();
            Choisir_Categorie.Enabled = false;
            button2.Enabled = false;
            AjouterCategorie();
            
        }
        // Button Ajouter_Joueur
        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_alias.Text == "" || tb_nom.Text == "" || tb_prenom.Text == "")
            {
                MessageBox.Show("Réponses manquantes.");
            }
            else
            {
                Ajouter_Joueur();
                i++;
                button2.Enabled = true;
            }
            if (DGV_joueurs.Rows.Count == 3)
            {
                ScoreDesJoueurs();
                Choisir_Categorie.Enabled = true;
                Total[0] = NombrePointCategorie(JoueurScore1, codeR);
                Total[1] = NombrePointCategorie(JoueurScore1, codeB);
                Total[2] = NombrePointCategorie(JoueurScore1, codeJ);
                Total[3] = NombrePointCategorie(JoueurScore1, codeV);
                Total[4] = NombrePointCategorie(JoueurScore2, codeR);
                Total[5] = NombrePointCategorie(JoueurScore2, codeB);
                Total[6] = NombrePointCategorie(JoueurScore2, codeJ);
                Total[7] = NombrePointCategorie(JoueurScore2, codeV);
                bt_ajouter.Enabled = false;
                lb_joueur1.Text = "Tour de : "+JoueurScore1;
            }
        }
        private void Delete_Joueur()
        {
            try
            {
                int selectedRow = DGV_joueurs.CurrentCell.RowIndex;
                DataGridViewRow row = DGV_joueurs.Rows[selectedRow];
                if (selectedRow >= 0)
                {
                    deleteJoueur();
                    DGV_joueurs.Rows.RemoveAt(selectedRow);
                }

                DGV_joueurs.Refresh();
                DGV_joueurs.Update();
            }
            catch (Exception ea)
            {
                MessageBox.Show(ea.Message.ToString());
            }
            DGV_joueurs.Refresh();
            DGV_joueurs.Update();
        }
        private void ScoreDesCategories(string nomJoueur, string ccode)
        {
            try
            {
                OracleCommand oraupdate = new OracleCommand("GESTIONQUESTION", connection);
                oraupdate.CommandText = "GESTIONQUESTION.listscoresport";
                oraupdate.CommandType = CommandType.StoredProcedure;

                OracleParameter orapamres = new OracleParameter("RES", OracleDbType.RefCursor);
                orapamres.Direction = ParameterDirection.ReturnValue;
                oraupdate.Parameters.Add(orapamres);


                OracleParameter orapamAlias = new OracleParameter("palias", OracleDbType.Varchar2);
                orapamAlias.Direction = ParameterDirection.Input;
                orapamAlias.Value = nomJoueur;
                oraupdate.Parameters.Add(orapamAlias);

                OracleParameter OraDesc = new OracleParameter("ccode", OracleDbType.Char);
                OraDesc.Value = ccode;
                OraDesc.Direction = ParameterDirection.Input;
                oraupdate.Parameters.Add(OraDesc);
                OracleDataReader Oraread = oraupdate.ExecuteReader();
                while (Oraread.Read())
                {
                    if (Oraread.GetInt32(0) == 2)
                    {
                        MessageBox.Show("Vous avez gagner la categorie : " + CategorieChoisi);
                        // Inserer le nomJoueur et le nom de la categorie qu'il a gagner dans un lb
                        DataGridViewRow row = (DataGridViewRow)dgv_categorieGagner.Rows[0].Clone();
                        row.Cells[0].Value = Oraread.GetString(1);
                        string a;
                        if (ccode == "V")
                        {
                            a = " (Art-Culture)";
                            row.Cells[1].Value = ccode + a;
                        }
                        else if (ccode == "R")
                        {
                            a = " (Sport)";
                            row.Cells[1].Value = ccode + a;
                        }
                        else if (ccode == "B")
                        {
                            a = " (Histoire)";
                            row.Cells[1].Value = ccode + a;
                        }
                        else if (ccode == "J")
                        {
                            a = " (Geographie)";
                            row.Cells[1].Value = ccode + a;
                        }
                        
                        dgv_categorieGagner.Rows.Add(row);
                    }
                    //MessageBox.Show(Oraread.GetInt32(0).ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            try
            {
                OracleCommand oraupdate = new OracleCommand("gestioncategorie", connection);
                oraupdate.CommandText = "gestioncategorie.faiblecategorie";
                oraupdate.CommandType = CommandType.StoredProcedure;

                OracleParameter orapamres = new OracleParameter("RES", OracleDbType.RefCursor);
                orapamres.Direction = ParameterDirection.ReturnValue;
                oraupdate.Parameters.Add(orapamres);


                OracleParameter orapamAlias = new OracleParameter("palias", OracleDbType.Varchar2);
                orapamAlias.Direction = ParameterDirection.Input;
                orapamAlias.Value = nomJoueur;
                oraupdate.Parameters.Add(orapamAlias);

                OracleDataReader Oraread = oraupdate.ExecuteReader();
                while (Oraread.Read())
                {
                    if (nomJoueur == JoueurScore2)
                    {
                        DataGridViewRow row2 = (DataGridViewRow)dgv_faible.Rows[1].Clone();
                        row2.Cells[0].Value = Oraread.GetString(1);
                        row2.Cells[1].Value = Oraread.GetString(0);
                        dgv_faible.Rows.Add(row2);
                    }
                    else
                    {
                        dgv_faible.Rows.Clear();
                        DataGridViewRow row = (DataGridViewRow)dgv_faible.Rows[0].Clone();
                        row.Cells[0].Value = Oraread.GetString(1);
                        row.Cells[1].Value = Oraread.GetString(0);
                        dgv_faible.Rows.Add(row);
                    }
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        private void deleteJoueur()
        {
            try
            {
                OracleCommand oraDelete = new OracleCommand("gestionjoueur", connection);
                oraDelete.CommandText = "gestionjoueur.DELETEJOUEUR";
                oraDelete.CommandType = CommandType.StoredProcedure;

                OracleParameter orapamAlias = new OracleParameter("palias", OracleDbType.Varchar2);
                orapamAlias.Direction = ParameterDirection.Input;

                int selectedRow = DGV_joueurs.CurrentCell.RowIndex;

                orapamAlias.Value = DGV_joueurs.Rows[selectedRow].Cells[0].Value.ToString();
                oraDelete.Parameters.Add(orapamAlias);

                oraDelete.ExecuteNonQuery();
                DGV_joueurs.Refresh();
                DGV_joueurs.Update();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DGV_joueurs.Rows.Count > 1 && DGV_joueurs.Rows != null)
            {
                Delete_Joueur();
            }
            else
            {
                MessageBox.Show("Il n'a aucun joueur a supprimer");
            }
            if (DGV_joueurs.Rows.Count == 1)
            {
                MessageBox.Show("Il n'a plus de joueur dans la partie");
                this.Close();
            }
        }
        public static string RandomCategorie(int length)
        {
            const string chars = "RBJVA";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void AfficherNewQuestion()
        {
            CB_categorie.Enabled = false;
            Enable_btreps();
            De.Enabled = false;
            int Numquestion;
            string s = RandomCategorie(1);
            if (s == "A")
            {
                ChoisirCategorie dlg = new ChoisirCategorie();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    s = dlg.ReturnValue1;
                }
            }
            CategorieChoisi = s;
            OracleCommand Oracmd = new OracleCommand("GESTIONQUESTION", connection);
            Oracmd.CommandText = "GESTIONQUESTION.questionaleatoire";
            Oracmd.CommandType = CommandType.StoredProcedure;
            OracleParameter orapamres = new OracleParameter("RES", OracleDbType.RefCursor);
            orapamres.Direction = ParameterDirection.ReturnValue;
            Oracmd.Parameters.Add(orapamres);
            OracleParameter OraDesc = new OracleParameter("PCATEGORIE", OracleDbType.Char);
            OraDesc.Value = s;  
            De.Text = s;
            OraDesc.Direction = ParameterDirection.Input;
            Oracmd.Parameters.Add(OraDesc);
            OracleDataReader Oraread = Oracmd.ExecuteReader();
            if (Oraread.Read())
            {
                Numquestion = Oraread.GetInt32(0);
                question = Oraread.GetString(1);
                LB_Question.Text = question;

                try
                {
                    OracleCommand oraCmdUpdate = new OracleCommand("GESTIONQUESTION", connection);
                    oraCmdUpdate.CommandText = "GESTIONQUESTION.updatequestion";
                    oraCmdUpdate.CommandType = CommandType.StoredProcedure;
                    OracleParameter orapamNum = new OracleParameter("pnum", OracleDbType.Varchar2);
                    orapamNum.Direction = ParameterDirection.Input;
                    orapamNum.Value = Numquestion;
                    oraCmdUpdate.Parameters.Add(orapamNum);

                    OracleParameter orapamFlag = new OracleParameter("pflag", OracleDbType.Varchar2);
                    orapamFlag.Direction = ParameterDirection.Input;
                    orapamFlag.Value = "1";
                    oraCmdUpdate.Parameters.Add(orapamFlag);
                    oraCmdUpdate.ExecuteReader();    
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message.ToString());
                    }


                OracleCommand Oracmd2 = new OracleCommand("GESTIONQUESTION", connection);
                Oracmd2.CommandText = "GESTIONQUESTION.reponse";
                Oracmd2.CommandType = CommandType.StoredProcedure;
                OracleParameter orapamres2 = new OracleParameter("RES", OracleDbType.RefCursor);
                orapamres2.Direction = ParameterDirection.ReturnValue;
                Oracmd2.Parameters.Add(orapamres2);
                OracleParameter OraDesc2 = new OracleParameter("PNUM", OracleDbType.Int32);
                OraDesc2.Value = Numquestion;
                OraDesc2.Direction = ParameterDirection.Input;
                Oracmd2.Parameters.Add(OraDesc2);
                OracleDataReader Oraread2 = Oracmd2.ExecuteReader();
                if (Oraread2.HasRows)
                {
                    try
                    {
                        OracleCommand oraCmdTotal = new OracleCommand("GESTIONQUESTION", connection);
                        oraCmdTotal.CommandText = "GESTIONQUESTION.ValiderReponse";
                        oraCmdTotal.CommandType = CommandType.StoredProcedure;
                        OracleParameter orapamTotal = new OracleParameter("total", OracleDbType.RefCursor);
                        orapamTotal.Direction = ParameterDirection.ReturnValue;
                        oraCmdTotal.Parameters.Add(orapamTotal);
                        OracleParameter orapamNum = new OracleParameter("pnum", OracleDbType.Int32);
                        orapamNum.Direction = ParameterDirection.Input;
                        orapamNum.Value = Numquestion;
                        oraCmdTotal.Parameters.Add(orapamNum);
                        OracleDataReader Oraread3 = oraCmdTotal.ExecuteReader();
                        if (Oraread3.Read())
                        {
                            ReponseValide = Oraread3.GetString(2); // Donne 'Y'
                        }
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message.ToString());
                    }
                    int i = 0;
                    while (Oraread2.Read())
                    {
                        if (Oraread2.GetString(2) == ReponseValide)
                        {
                            indexBonneReponse = i;
                            reponse = Oraread2.GetString(1);
                        }
                        if (i == 0)
                        {
                            tb_rep1.Text = Oraread2.GetString(1);
                        }
                        if (i == 1)
                        {
                            tb_rep2.Text = Oraread2.GetString(1);
                        }
                        if (i == 2)
                        {
                            tb_rep3.Text = Oraread2.GetString(1);
                        }
                        if (i == 3)
                        {
                            tb_rep4.Text = Oraread2.GetString(1);
                        }
                        i++;
                    }
                }
            }
            if (De.Enabled == true)
            {
                
            }
        }
        private void Choisir_Categorie_Click(object sender, EventArgs e)
        {
            Enable_btreps();
            AfficherNewQuestion();
            Choisir_Categorie.Enabled = false;
        }
        public int ScoreJoueur1 = 0;
        public int ScoreJoueur2 = 0;
        private void UpdaterScore(Button b)
        {

            b.BackColor = Color.Green;
            De.Text = "";
            LB_Question.Text = "";
            tb_rep1.Text = "";
            tb_rep2.Text = "";
            tb_rep3.Text = "";
            tb_rep4.Text = "";
            Thread.Sleep(1000);
            Disable_btreps();

            try
            {
                OracleCommand oraCmdUpdate = new OracleCommand("gestionjoueur", connection);
                oraCmdUpdate.CommandText = "gestionjoueur.miseajour";
                oraCmdUpdate.CommandType = CommandType.StoredProcedure;
                OracleParameter orapamAlias = new OracleParameter("palias", OracleDbType.Varchar2);
                orapamAlias.Direction = ParameterDirection.Input;
                OracleParameter orapamPoints = new OracleParameter("ppoints", OracleDbType.Varchar2);
                orapamPoints.Direction = ParameterDirection.Input;
                OracleParameter orapamCode = new OracleParameter("pcode", OracleDbType.Varchar2);
                orapamCode.Direction = ParameterDirection.Input;
                if (lb_joueur1.Text.Contains(JoueurScore1))
                {
                    int temp = NombrePointCategorie(JoueurScore1, CategorieChoisi);
                    temp++;
                    orapamAlias.Value = JoueurScore1;
                    oraCmdUpdate.Parameters.Add(orapamAlias);
                    orapamPoints.Value = temp;
                    oraCmdUpdate.Parameters.Add(orapamPoints);
                    orapamCode.Value = CategorieChoisi;
                    oraCmdUpdate.Parameters.Add(orapamCode);
                    oraCmdUpdate.ExecuteReader();
                    ListerScoreDGV();
                    UpdateDGV();
                }
                else
                {
                    int temp = NombrePointCategorie(JoueurScore2, CategorieChoisi);
                    temp++;
                    orapamAlias.Value = JoueurScore2;
                    oraCmdUpdate.Parameters.Add(orapamAlias);
                    orapamPoints.Value = temp;
                    oraCmdUpdate.Parameters.Add(orapamPoints);
                    orapamCode.Value = CategorieChoisi;
                    oraCmdUpdate.Parameters.Add(orapamCode);
                    oraCmdUpdate.ExecuteReader();
                    ListerScoreDGV();
                    UpdateDGV();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
            ScoreDesCategories(JoueurScore1, CategorieChoisi);
            ScoreDesCategories(JoueurScore2, CategorieChoisi);
        }
        public void VerifyJoueur()
        {
            if (lb_joueur1.Text.Contains(JoueurScore2))
            {
                lb_joueur1.Text = "Tour de : " + JoueurScore1;
                Disable_btreps();
            }
            else
            {
                lb_joueur1.Text = "Tour de : " + JoueurScore2;
                Disable_btreps();
            }
        }
        public void ChangePlayer(Button b)
        {
            b.BackColor = Color.Red;
            De.Text = "";
            LB_Question.Text = "";
            tb_rep1.Text = "";
            tb_rep2.Text = "";
            tb_rep3.Text = "";
            tb_rep4.Text = "";
            Thread.Sleep(1000);
            Enable_btreps();
            VerifyJoueur();
            try
            {
                OracleCommand oraCmdUpdate = new OracleCommand("gestionjoueur", connection);
                oraCmdUpdate.CommandText = "gestionjoueur.miseajour";
                oraCmdUpdate.CommandType = CommandType.StoredProcedure;
                OracleParameter orapamAlias = new OracleParameter("palias", OracleDbType.Varchar2);
                orapamAlias.Direction = ParameterDirection.Input;
                OracleParameter orapamPoints = new OracleParameter("ppoints", OracleDbType.Varchar2);
                orapamPoints.Direction = ParameterDirection.Input;
                OracleParameter orapamCode = new OracleParameter("pcode", OracleDbType.Varchar2);
                orapamCode.Direction = ParameterDirection.Input;
                if (lb_joueur1.Text.Contains(JoueurScore1))
                {
                    int temp = NombrePointCategorie(JoueurScore1, CategorieChoisi);
                    //temp++;
                    orapamAlias.Value = JoueurScore1;
                    oraCmdUpdate.Parameters.Add(orapamAlias);
                    orapamPoints.Value = temp;
                    oraCmdUpdate.Parameters.Add(orapamPoints);
                    orapamCode.Value = CategorieChoisi;
                    oraCmdUpdate.Parameters.Add(orapamCode);
                    oraCmdUpdate.ExecuteReader();
                    ListerScoreDGV();
                    UpdateDGV();
                }
                else
                {
                    int temp = NombrePointCategorie(JoueurScore2, CategorieChoisi);
                    //temp++;
                    orapamAlias.Value = JoueurScore2;
                    oraCmdUpdate.Parameters.Add(orapamAlias);
                    orapamPoints.Value = temp;
                    oraCmdUpdate.Parameters.Add(orapamPoints);
                    orapamCode.Value = CategorieChoisi;
                    oraCmdUpdate.Parameters.Add(orapamCode);
                    oraCmdUpdate.ExecuteReader();
                    ListerScoreDGV();
                    UpdateDGV();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
            }
        }
        private void tb_rep1_Click(object sender, EventArgs e)
        {
            Disable_btreps();
            De.Enabled = true;
            if (indexBonneReponse == 0)
            {
                //tb_rep1.BackColor = Color.Green;
                UpdaterScore(tb_rep1);
                tb_rep1.BackColor = Color.Gray;
                Choisir_Categorie.Enabled = true;
            }
            else
            {
                ChangePlayer(tb_rep1);
                tb_rep1.BackColor = Color.Gray;
                Choisir_Categorie.Enabled = true;
            }
        }

        private void lb_joueur1_Click(object sender, EventArgs e)
        {

        }

        private void tb_rep2_Click(object sender, EventArgs e)
        {
            Disable_btreps();
            De.Enabled = true;
            if (indexBonneReponse == 1)
            {
                //tb_rep2.BackColor = Color.Green;
                UpdaterScore(tb_rep2);
                tb_rep2.BackColor = Color.Gray;
                Choisir_Categorie.Enabled = true;
            }
            else
            {
                ChangePlayer(tb_rep2);
                tb_rep2.BackColor = Color.Gray;
                Choisir_Categorie.Enabled = true;
            }
        }

        private void tb_rep3_Click(object sender, EventArgs e)
        {
            Disable_btreps();
            De.Enabled = true;
            if (indexBonneReponse == 2)
            {
                //tb_rep3.BackColor = Color.Green;
                UpdaterScore(tb_rep3);
                //AfficherNewQuestion();
                tb_rep3.BackColor = Color.Gray;
                Choisir_Categorie.Enabled = true;
            }
            else
            {
                ChangePlayer(tb_rep3);
                tb_rep3.BackColor = Color.Gray;
                Choisir_Categorie.Enabled = true;
            }
        }

        private void tb_rep4_Click(object sender, EventArgs e)
        {
            Disable_btreps();
            De.Enabled = true;
            if (indexBonneReponse == 3)
            {
                //tb_rep4.BackColor = Color.Green;
                UpdaterScore(tb_rep4);
                //AfficherNewQuestion();
                tb_rep4.BackColor = Color.Gray;
                Choisir_Categorie.Enabled = true;
            }
            else
            {
                ChangePlayer(tb_rep4);
                tb_rep4.BackColor = Color.Gray;
                Choisir_Categorie.Enabled = true;
            }
        }
        private void AfficherQuestion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DGV_joueurs.Rows.Count > 1 && DGV_joueurs.Rows != null)
            {
                Delete_Joueur();
                Delete_Joueur();
            }
        }
    }
}