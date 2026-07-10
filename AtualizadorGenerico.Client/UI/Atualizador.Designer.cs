
namespace Updater
{
    partial class Atualizador
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
            this.lblNomeSistema = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblNomeSistema
            // 
            this.lblNomeSistema.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeSistema.Location = new System.Drawing.Point(12, 6);
            this.lblNomeSistema.Name = "lblNomeSistema";
            this.lblNomeSistema.Size = new System.Drawing.Size(396, 31);
            this.lblNomeSistema.TabIndex = 0;
            this.lblNomeSistema.Text = "Nome do Sistema";
            this.lblNomeSistema.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(12, 175);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(396, 43);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Consultando Atualização...";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExecutar
            // 
            this.btnExecutar.AutoSize = true;
            this.btnExecutar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExecutar.ForeColor = System.Drawing.Color.Black;
            this.btnExecutar.Location = new System.Drawing.Point(147, 361);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(135, 42);
            this.btnExecutar.TabIndex = 1;
            this.btnExecutar.Text = "Executar Offline";
            this.btnExecutar.UseVisualStyleBackColor = true;
            this.btnExecutar.Visible = false;
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // lblLog
            // 
            this.lblLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLog.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblLog.Location = new System.Drawing.Point(12, 32);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(396, 17);
            this.lblLog.TabIndex = 0;
            this.lblLog.Text = "Log do sistema";
            this.lblLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(54, 243);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(313, 30);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // Atualizador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(420, 415);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnExecutar);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.lblNomeSistema);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Atualizador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atualizador";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Atualizador_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNomeSistema;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnExecutar;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

