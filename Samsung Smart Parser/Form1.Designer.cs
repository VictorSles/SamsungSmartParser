using System.Windows.Forms;

namespace Samsung_Smart_Parser
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel11 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxPlay = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxConfig = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLine = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.labelStation = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelModel = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.labelAPIStatus = new System.Windows.Forms.Label();
            this.timerAPIError = new System.Windows.Forms.Timer(this.components);
            this.timerConexão = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxStop = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewPVError = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataGridViewHistory = new System.Windows.Forms.DataGridView();
            this.ColumnTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFailure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEquipment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSentToJems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBoxDLOFF = new System.Windows.Forms.PictureBox();
            this.pictureBoxDLON = new System.Windows.Forms.PictureBox();
            this.labelDLStatus = new System.Windows.Forms.Label();
            this.panel11.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlay)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConfig)).BeginInit();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStop)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDLOFF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDLON)).BeginInit();
            this.SuspendLayout();
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.panel11.Controls.Add(this.tableLayoutPanel12);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1277, 100);
            this.panel11.TabIndex = 35;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tableLayoutPanel12.ColumnCount = 13;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.02128F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.97872F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 179F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 172F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 225F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel8, 1, 1);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel7, 12, 1);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel6, 11, 1);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel13, 2, 1);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel2, 4, 1);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel1, 6, 1);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel4, 8, 1);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel5, 10, 1);
            this.tableLayoutPanel12.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 3;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.278351F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.41237F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.30928F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(1295, 97);
            this.tableLayoutPanel12.TabIndex = 8;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Controls.Add(this.pictureBoxPlay, 0, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(19, 12);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(74, 66);
            this.tableLayoutPanel8.TabIndex = 15;
            // 
            // pictureBoxPlay
            // 
            this.pictureBoxPlay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPlay.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPlay.Image")));
            this.pictureBoxPlay.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxPlay.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxPlay.Name = "pictureBoxPlay";
            this.pictureBoxPlay.Size = new System.Drawing.Size(70, 65);
            this.pictureBoxPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPlay.TabIndex = 7;
            this.pictureBoxPlay.TabStop = false;
            this.pictureBoxPlay.Click += new System.EventHandler(this.pictureBoxPlay_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Controls.Add(this.pictureBoxClose, 0, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(1197, 12);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(69, 66);
            this.tableLayoutPanel7.TabIndex = 15;
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxClose.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxClose.Image")));
            this.pictureBoxClose.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxClose.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(69, 66);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxClose.TabIndex = 16;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.pictureBoxConfig, 0, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(1116, 12);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(75, 66);
            this.tableLayoutPanel6.TabIndex = 15;
            // 
            // pictureBoxConfig
            // 
            this.pictureBoxConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxConfig.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxConfig.Image")));
            this.pictureBoxConfig.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxConfig.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxConfig.Name = "pictureBoxConfig";
            this.pictureBoxConfig.Size = new System.Drawing.Size(75, 66);
            this.pictureBoxConfig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxConfig.TabIndex = 11;
            this.pictureBoxConfig.TabStop = false;
            this.pictureBoxConfig.Click += new System.EventHandler(this.pictureBoxConfig_Click);
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 1;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel13.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.labelLine, 0, 1);
            this.tableLayoutPanel13.Location = new System.Drawing.Point(99, 12);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 2;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(173, 66);
            this.tableLayoutPanel13.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 31);
            this.label2.TabIndex = 6;
            this.label2.Text = "LINHA";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLine
            // 
            this.labelLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelLine.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLine.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelLine.Location = new System.Drawing.Point(3, 33);
            this.labelLine.Name = "labelLine";
            this.labelLine.Size = new System.Drawing.Size(167, 33);
            this.labelLine.TabIndex = 7;
            this.labelLine.Text = "Aguardando...";
            this.labelLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelLine.Click += new System.EventHandler(this.labelLine_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelStation, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(286, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(163, 66);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 31);
            this.label4.TabIndex = 6;
            this.label4.Text = "ESTAÇÃO/PC";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStation
            // 
            this.labelStation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStation.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStation.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelStation.Location = new System.Drawing.Point(3, 33);
            this.labelStation.Name = "labelStation";
            this.labelStation.Size = new System.Drawing.Size(157, 33);
            this.labelStation.TabIndex = 7;
            this.labelStation.Text = "Aguardando...";
            this.labelStation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelModel, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(464, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(163, 66);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 31);
            this.label1.TabIndex = 6;
            this.label1.Text = "MODELO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelModel
            // 
            this.labelModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelModel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelModel.Location = new System.Drawing.Point(3, 33);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(157, 33);
            this.labelModel.TabIndex = 7;
            this.labelModel.Text = "Aguardando...";
            this.labelModel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(644, 12);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.60606F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.39394F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(118, 66);
            this.tableLayoutPanel4.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.tableLayoutPanel4.SetRowSpan(this.label6, 2);
            this.label6.Size = new System.Drawing.Size(112, 66);
            this.label6.TabIndex = 6;
            this.label6.Text = "TESTER PERFORMANCE";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.labelAPIStatus, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(891, 12);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.60606F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.39394F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(219, 66);
            this.tableLayoutPanel5.TabIndex = 18;
            // 
            // labelAPIStatus
            // 
            this.labelAPIStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelAPIStatus.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAPIStatus.ForeColor = System.Drawing.Color.Yellow;
            this.labelAPIStatus.Location = new System.Drawing.Point(3, 0);
            this.labelAPIStatus.Name = "labelAPIStatus";
            this.tableLayoutPanel5.SetRowSpan(this.labelAPIStatus, 2);
            this.labelAPIStatus.Size = new System.Drawing.Size(213, 66);
            this.labelAPIStatus.TabIndex = 9;
            this.labelAPIStatus.Text = "Aguardando conexão com o JEMSmm...";
            this.labelAPIStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerAPIError
            // 
            this.timerAPIError.Interval = 3000;
            this.timerAPIError.Tick += new System.EventHandler(this.timerAPIError_Tick);
            // 
            // timerConexão
            // 
            this.timerConexão.Enabled = true;
            this.timerConexão.Interval = 3000;
            // 
            // pictureBoxStop
            // 
            this.pictureBoxStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.pictureBoxStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxStop.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxStop.Image")));
            this.pictureBoxStop.Location = new System.Drawing.Point(21, 13);
            this.pictureBoxStop.Name = "pictureBoxStop";
            this.pictureBoxStop.Size = new System.Drawing.Size(70, 65);
            this.pictureBoxStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxStop.TabIndex = 37;
            this.pictureBoxStop.TabStop = false;
            this.pictureBoxStop.Click += new System.EventHandler(this.pictureBoxStop_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.77145F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.22855F));
            this.tableLayoutPanel3.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.listViewPVError, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.dataGridViewHistory, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(90, 152);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.13402F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.86598F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 191F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1099, 413);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(868, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(228, 25);
            this.label5.TabIndex = 8;
            this.label5.Text = "ERROS DE PV";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(859, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "HISTÓRICO";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listViewPVError
            // 
            this.listViewPVError.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listViewPVError.GridLines = true;
            this.listViewPVError.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPVError.HideSelection = false;
            this.listViewPVError.Location = new System.Drawing.Point(868, 28);
            this.listViewPVError.Name = "listViewPVError";
            this.tableLayoutPanel3.SetRowSpan(this.listViewPVError, 2);
            this.listViewPVError.Size = new System.Drawing.Size(228, 382);
            this.listViewPVError.TabIndex = 11;
            this.listViewPVError.UseCompatibleStateImageBehavior = false;
            this.listViewPVError.View = System.Windows.Forms.View.Details;
            this.listViewPVError.SelectedIndexChanged += new System.EventHandler(this.listViewPVError_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "                     SERIAL NUMBER";
            this.columnHeader3.Width = 224;
            // 
            // dataGridViewHistory
            // 
            this.dataGridViewHistory.AllowUserToResizeColumns = false;
            this.dataGridViewHistory.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTimestamp,
            this.ColumnSerialNumber,
            this.ColumnResult,
            this.ColumnFailure,
            this.ColumnEquipment,
            this.ColumnModel,
            this.ColumnSentToJems});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewHistory.DefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewHistory.Location = new System.Drawing.Point(3, 28);
            this.dataGridViewHistory.Name = "dataGridViewHistory";
            this.dataGridViewHistory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.tableLayoutPanel3.SetRowSpan(this.dataGridViewHistory, 2);
            this.dataGridViewHistory.Size = new System.Drawing.Size(859, 382);
            this.dataGridViewHistory.TabIndex = 12;
            // 
            // ColumnTimestamp
            // 
            this.ColumnTimestamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Silver;
            this.ColumnTimestamp.DefaultCellStyle = dataGridViewCellStyle11;
            this.ColumnTimestamp.HeaderText = "TIMESTAMP";
            this.ColumnTimestamp.Name = "ColumnTimestamp";
            this.ColumnTimestamp.ReadOnly = true;
            this.ColumnTimestamp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnTimestamp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnTimestamp.Width = 125;
            // 
            // ColumnSerialNumber
            // 
            this.ColumnSerialNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Silver;
            this.ColumnSerialNumber.DefaultCellStyle = dataGridViewCellStyle12;
            this.ColumnSerialNumber.HeaderText = "SERIAL NUMBER";
            this.ColumnSerialNumber.Name = "ColumnSerialNumber";
            this.ColumnSerialNumber.ReadOnly = true;
            this.ColumnSerialNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnSerialNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnSerialNumber.Width = 140;
            // 
            // ColumnResult
            // 
            this.ColumnResult.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Silver;
            this.ColumnResult.DefaultCellStyle = dataGridViewCellStyle13;
            this.ColumnResult.HeaderText = "RESULT";
            this.ColumnResult.Name = "ColumnResult";
            this.ColumnResult.ReadOnly = true;
            this.ColumnResult.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnResult.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnResult.Width = 60;
            // 
            // ColumnFailure
            // 
            this.ColumnFailure.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Silver;
            this.ColumnFailure.DefaultCellStyle = dataGridViewCellStyle14;
            this.ColumnFailure.HeaderText = "FAILURE";
            this.ColumnFailure.Name = "ColumnFailure";
            this.ColumnFailure.ReadOnly = true;
            this.ColumnFailure.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnFailure.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnFailure.Width = 190;
            // 
            // ColumnEquipment
            // 
            this.ColumnEquipment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Silver;
            this.ColumnEquipment.DefaultCellStyle = dataGridViewCellStyle15;
            this.ColumnEquipment.HeaderText = "EQUIPMENT";
            this.ColumnEquipment.Name = "ColumnEquipment";
            this.ColumnEquipment.ReadOnly = true;
            this.ColumnEquipment.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnEquipment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnEquipment.Width = 140;
            // 
            // ColumnModel
            // 
            this.ColumnModel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.Silver;
            this.ColumnModel.DefaultCellStyle = dataGridViewCellStyle16;
            this.ColumnModel.HeaderText = "MODEL";
            this.ColumnModel.Name = "ColumnModel";
            this.ColumnModel.ReadOnly = true;
            this.ColumnModel.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnModel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnModel.Width = 70;
            // 
            // ColumnSentToJems
            // 
            this.ColumnSentToJems.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Silver;
            this.ColumnSentToJems.DefaultCellStyle = dataGridViewCellStyle17;
            this.ColumnSentToJems.HeaderText = "SENT TO JEMS";
            this.ColumnSentToJems.Name = "ColumnSentToJems";
            this.ColumnSentToJems.ReadOnly = true;
            this.ColumnSentToJems.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnSentToJems.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnSentToJems.Width = 90;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(-9, 611);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1295, 13);
            this.textBox1.TabIndex = 38;
            this.textBox1.Text = "© 2025 Samsung Smart Parser – Jabil Manaus – Versão 1.2     ";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pictureBoxDLOFF
            // 
            this.pictureBoxDLOFF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.pictureBoxDLOFF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxDLOFF.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxDLOFF.Image")));
            this.pictureBoxDLOFF.Location = new System.Drawing.Point(763, 13);
            this.pictureBoxDLOFF.Name = "pictureBoxDLOFF";
            this.pictureBoxDLOFF.Size = new System.Drawing.Size(110, 37);
            this.pictureBoxDLOFF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDLOFF.TabIndex = 0;
            this.pictureBoxDLOFF.TabStop = false;
            this.pictureBoxDLOFF.Click += new System.EventHandler(this.pictureBoxDLOFF_Click);
            // 
            // pictureBoxDLON
            // 
            this.pictureBoxDLON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.pictureBoxDLON.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxDLON.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxDLON.Image")));
            this.pictureBoxDLON.Location = new System.Drawing.Point(763, 13);
            this.pictureBoxDLON.Name = "pictureBoxDLON";
            this.pictureBoxDLON.Size = new System.Drawing.Size(110, 37);
            this.pictureBoxDLON.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDLON.TabIndex = 39;
            this.pictureBoxDLON.TabStop = false;
            this.pictureBoxDLON.Click += new System.EventHandler(this.pictureBoxDLON_Click);
            // 
            // labelDLStatus
            // 
            this.labelDLStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.labelDLStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDLStatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDLStatus.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelDLStatus.Location = new System.Drawing.Point(763, 49);
            this.labelDLStatus.Name = "labelDLStatus";
            this.labelDLStatus.Size = new System.Drawing.Size(110, 30);
            this.labelDLStatus.TabIndex = 40;
            this.labelDLStatus.Text = "CONNECTION: N/A";
            this.labelDLStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1277, 624);
            this.Controls.Add(this.pictureBoxStop);
            this.Controls.Add(this.labelDLStatus);
            this.Controls.Add(this.pictureBoxDLON);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.pictureBoxDLOFF);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jabil Smart Parser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel11.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlay)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConfig)).EndInit();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStop)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDLOFF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDLON)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Timer timerConexão;
        public System.Windows.Forms.Timer timerAPIError;
        private System.Windows.Forms.PictureBox pictureBoxStop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer;
        private TextBox textBox1;
        private TableLayoutPanel tableLayoutPanel12;
        private TableLayoutPanel tableLayoutPanel13;
        private Label label2;
        private Label labelLine;
        private PictureBox pictureBoxConfig;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label4;
        private Label labelStation;
        private PictureBox pictureBoxPlay;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label labelModel;
        private PictureBox pictureBoxClose;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label6;
        private PictureBox pictureBoxDLOFF;
        private PictureBox pictureBoxDLON;
        private Label labelDLStatus;
        public Label labelAPIStatus;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel8;
        private TableLayoutPanel tableLayoutPanel7;
        private DataGridView dataGridViewHistory;
        private ListView listViewPVError;
        private ColumnHeader columnHeader3;
        private DataGridViewTextBoxColumn ColumnTimestamp;
        private DataGridViewTextBoxColumn ColumnSerialNumber;
        private DataGridViewTextBoxColumn ColumnResult;
        private DataGridViewTextBoxColumn ColumnFailure;
        private DataGridViewTextBoxColumn ColumnEquipment;
        private DataGridViewTextBoxColumn ColumnModel;
        private DataGridViewTextBoxColumn ColumnSentToJems;
    }
}

