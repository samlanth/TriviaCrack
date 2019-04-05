namespace TP2_BD
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Titre = new System.Windows.Forms.Label();
            this.Apropos = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Details1 = new System.Windows.Forms.Label();
            this.bt_ajouter = new System.Windows.Forms.Button();
            this.bt_chercherq = new System.Windows.Forms.Button();
            this.bt_aff_q = new System.Windows.Forms.Button();
            this.Apropos.SuspendLayout();
            this.SuspendLayout();
            // 
            // Titre
            // 
            this.Titre.AutoSize = true;
            this.Titre.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Titre.Location = new System.Drawing.Point(27, 22);
            this.Titre.Name = "Titre";
            this.Titre.Size = new System.Drawing.Size(105, 13);
            this.Titre.TabIndex = 2;
            this.Titre.Text = "Jeu de questionnaire";
            // 
            // Apropos
            // 
            this.Apropos.Controls.Add(this.label1);
            this.Apropos.Controls.Add(this.Details1);
            this.Apropos.Location = new System.Drawing.Point(12, 169);
            this.Apropos.Name = "Apropos";
            this.Apropos.Size = new System.Drawing.Size(236, 85);
            this.Apropos.TabIndex = 4;
            this.Apropos.TabStop = false;
            this.Apropos.Text = "A propos du jeu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Creer par Samuel Lanthier et Cedric Briand";
            // 
            // Details1
            // 
            this.Details1.AutoSize = true;
            this.Details1.Location = new System.Drawing.Point(9, 28);
            this.Details1.Name = "Details1";
            this.Details1.Size = new System.Drawing.Size(138, 13);
            this.Details1.TabIndex = 0;
            this.Details1.Text = "4 categorie de 20 questions";
            // 
            // bt_ajouter
            // 
            this.bt_ajouter.Location = new System.Drawing.Point(12, 123);
            this.bt_ajouter.Name = "bt_ajouter";
            this.bt_ajouter.Size = new System.Drawing.Size(236, 40);
            this.bt_ajouter.TabIndex = 5;
            this.bt_ajouter.Text = "Ajouter Des Questions";
            this.bt_ajouter.UseVisualStyleBackColor = true;
            this.bt_ajouter.Click += new System.EventHandler(this.bt_ajouter_Click);
            // 
            // bt_chercherq
            // 
            this.bt_chercherq.Location = new System.Drawing.Point(12, 86);
            this.bt_chercherq.Name = "bt_chercherq";
            this.bt_chercherq.Size = new System.Drawing.Size(236, 40);
            this.bt_chercherq.TabIndex = 9;
            this.bt_chercherq.Text = "Rechercher Des Questions";
            this.bt_chercherq.UseVisualStyleBackColor = true;
            this.bt_chercherq.Click += new System.EventHandler(this.bt_chercherq_Click);
            // 
            // bt_aff_q
            // 
            this.bt_aff_q.Location = new System.Drawing.Point(12, 50);
            this.bt_aff_q.Name = "bt_aff_q";
            this.bt_aff_q.Size = new System.Drawing.Size(236, 40);
            this.bt_aff_q.TabIndex = 10;
            this.bt_aff_q.Text = "Commencer Une Partie";
            this.bt_aff_q.UseVisualStyleBackColor = true;
            this.bt_aff_q.Click += new System.EventHandler(this.bt_aff_q_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 266);
            this.Controls.Add(this.bt_aff_q);
            this.Controls.Add(this.bt_chercherq);
            this.Controls.Add(this.bt_ajouter);
            this.Controls.Add(this.Apropos);
            this.Controls.Add(this.Titre);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Apropos.ResumeLayout(false);
            this.Apropos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Titre;
        private System.Windows.Forms.GroupBox Apropos;
        private System.Windows.Forms.Label Details1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_ajouter;
        private System.Windows.Forms.Button bt_chercherq;
        private System.Windows.Forms.Button bt_aff_q;
    }
}

