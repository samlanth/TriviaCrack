namespace TP2_BD
{
    partial class ChercherQuestion
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Bt_close = new System.Windows.Forms.Button();
            this.lb_reponse = new System.Windows.Forms.ListBox();
            this.DGV_Question = new System.Windows.Forms.DataGridView();
            this.NumQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Question = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Question)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 168);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 56);
            this.listBox1.TabIndex = 2;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // Bt_close
            // 
            this.Bt_close.Location = new System.Drawing.Point(285, 230);
            this.Bt_close.Name = "Bt_close";
            this.Bt_close.Size = new System.Drawing.Size(114, 23);
            this.Bt_close.TabIndex = 3;
            this.Bt_close.Text = "Close";
            this.Bt_close.UseVisualStyleBackColor = true;
            this.Bt_close.Click += new System.EventHandler(this.Close_Click);
            // 
            // lb_reponse
            // 
            this.lb_reponse.FormattingEnabled = true;
            this.lb_reponse.Location = new System.Drawing.Point(138, 168);
            this.lb_reponse.Name = "lb_reponse";
            this.lb_reponse.Size = new System.Drawing.Size(261, 56);
            this.lb_reponse.TabIndex = 4;
            // 
            // DGV_Question
            // 
            this.DGV_Question.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_Question.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Question.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumQ,
            this.Question});
            this.DGV_Question.Location = new System.Drawing.Point(12, 12);
            this.DGV_Question.Name = "DGV_Question";
            this.DGV_Question.Size = new System.Drawing.Size(387, 150);
            this.DGV_Question.TabIndex = 5;
            this.DGV_Question.DoubleClick += new System.EventHandler(this.DGV_Question_DoubleClick);
            // 
            // NumQ
            // 
            this.NumQ.FillWeight = 30F;
            this.NumQ.HeaderText = "NumQ";
            this.NumQ.Name = "NumQ";
            // 
            // Question
            // 
            this.Question.HeaderText = "Question";
            this.Question.Name = "Question";
            // 
            // ChercherQuestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 261);
            this.Controls.Add(this.DGV_Question);
            this.Controls.Add(this.lb_reponse);
            this.Controls.Add(this.Bt_close);
            this.Controls.Add(this.listBox1);
            this.Name = "ChercherQuestion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChercherQuestion";
            this.Load += new System.EventHandler(this.ChercherQuestion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Question)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button Bt_close;
        private System.Windows.Forms.ListBox lb_reponse;
        private System.Windows.Forms.DataGridView DGV_Question;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Question;
    }
}