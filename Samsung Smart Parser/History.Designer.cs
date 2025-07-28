namespace Samsung_Smart_Parser
{
    partial class History
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
            this.listViewHist = new System.Windows.Forms.ListView();
            this.TIMESTAMP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.STEP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RESULT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelSNHist = new System.Windows.Forms.Label();
            this.buttonExitSN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewHist
            // 
            this.listViewHist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TIMESTAMP,
            this.STEP,
            this.RESULT});
            this.listViewHist.GridLines = true;
            this.listViewHist.HideSelection = false;
            this.listViewHist.Location = new System.Drawing.Point(12, 79);
            this.listViewHist.Name = "listViewHist";
            this.listViewHist.Size = new System.Drawing.Size(388, 410);
            this.listViewHist.TabIndex = 0;
            this.listViewHist.UseCompatibleStateImageBehavior = false;
            this.listViewHist.View = System.Windows.Forms.View.Details;
            // 
            // TIMESTAMP
            // 
            this.TIMESTAMP.Text = "TIMESTAMP";
            this.TIMESTAMP.Width = 141;
            // 
            // STEP
            // 
            this.STEP.Text = "STEP";
            this.STEP.Width = 140;
            // 
            // RESULT
            // 
            this.RESULT.Text = "RESULT";
            this.RESULT.Width = 102;
            // 
            // labelSNHist
            // 
            this.labelSNHist.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSNHist.ForeColor = System.Drawing.Color.White;
            this.labelSNHist.Location = new System.Drawing.Point(12, 29);
            this.labelSNHist.Name = "labelSNHist";
            this.labelSNHist.Size = new System.Drawing.Size(388, 27);
            this.labelSNHist.TabIndex = 1;
            this.labelSNHist.Text = "NÚMERO DE SÉRIE: ";
            this.labelSNHist.Click += new System.EventHandler(this.labelSNHist_Click);
            // 
            // buttonExitSN
            // 
            this.buttonExitSN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExitSN.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExitSN.Location = new System.Drawing.Point(150, 504);
            this.buttonExitSN.Name = "buttonExitSN";
            this.buttonExitSN.Size = new System.Drawing.Size(113, 43);
            this.buttonExitSN.TabIndex = 2;
            this.buttonExitSN.Text = "OK";
            this.buttonExitSN.UseVisualStyleBackColor = true;
            this.buttonExitSN.Click += new System.EventHandler(this.buttonExitSN_Click);
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.ClientSize = new System.Drawing.Size(412, 559);
            this.Controls.Add(this.buttonExitSN);
            this.Controls.Add(this.labelSNHist);
            this.Controls.Add(this.listViewHist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "History";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "History";
            this.Load += new System.EventHandler(this.History_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewHist;
        private System.Windows.Forms.ColumnHeader TIMESTAMP;
        private System.Windows.Forms.ColumnHeader STEP;
        private System.Windows.Forms.ColumnHeader RESULT;
        private System.Windows.Forms.Label labelSNHist;
        private System.Windows.Forms.Button buttonExitSN;
    }
}