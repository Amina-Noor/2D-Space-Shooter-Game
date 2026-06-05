namespace SpaceShooterGame
{
    partial class GameOverForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblGameOver = new System.Windows.Forms.Label();
            this.lblFinalScore = new System.Windows.Forms.Label();
            this.lblLevelReached = new System.Windows.Forms.Label();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.panelResults = new System.Windows.Forms.Panel();
            this.panelResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGameOver
            // 
            this.lblGameOver.AutoSize = true;
            this.lblGameOver.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblGameOver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblGameOver.Location = new System.Drawing.Point(130, 25);
            this.lblGameOver.Name = "lblGameOver";
            this.lblGameOver.Size = new System.Drawing.Size(200, 45);
            this.lblGameOver.TabIndex = 0;
            this.lblGameOver.Text = "GAME OVER";
            // 
            // lblFinalScore
            // 
            this.lblFinalScore.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblFinalScore.ForeColor = System.Drawing.Color.White;
            this.lblFinalScore.Location = new System.Drawing.Point(20, 85);
            this.lblFinalScore.Name = "lblFinalScore";
            this.lblFinalScore.Size = new System.Drawing.Size(420, 30);
            this.lblFinalScore.TabIndex = 1;
            this.lblFinalScore.Text = "Final Score: 0";
            this.lblFinalScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLevelReached
            // 
            this.lblLevelReached.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblLevelReached.ForeColor = System.Drawing.Color.Silver;
            this.lblLevelReached.Location = new System.Drawing.Point(20, 120);
            this.lblLevelReached.Name = "lblLevelReached";
            this.lblLevelReached.Size = new System.Drawing.Size(420, 25);
            this.lblLevelReached.TabIndex = 2;
            this.lblLevelReached.Text = "Level Reached: 1";
            this.lblLevelReached.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(200)))));
            this.btnRestart.FlatAppearance.BorderSize = 0;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRestart.ForeColor = System.Drawing.Color.White;
            this.btnRestart.Location = new System.Drawing.Point(50, 165);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(160, 45);
            this.btnRestart.TabIndex = 3;
            this.btnRestart.Text = "RESTART";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Location = new System.Drawing.Point(230, 165);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(160, 45);
            this.btnMenu.TabIndex = 4;
            this.btnMenu.Text = "MAIN MENU";
            this.btnMenu.UseVisualStyleBackColor = false;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // panelResults
            // 
            this.panelResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(45)))));
            this.panelResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResults.Controls.Add(this.lblGameOver);
            this.panelResults.Controls.Add(this.lblFinalScore);
            this.panelResults.Controls.Add(this.lblLevelReached);
            this.panelResults.Controls.Add(this.btnRestart);
            this.panelResults.Controls.Add(this.btnMenu);
            this.panelResults.Location = new System.Drawing.Point(100, 120);
            this.panelResults.Name = "panelResults";
            this.panelResults.Size = new System.Drawing.Size(460, 240);
            this.panelResults.TabIndex = 5;
            // 
            // GameOverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(12)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(660, 500);
            this.Controls.Add(this.panelResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameOverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game Over";
            this.panelResults.ResumeLayout(false);
            this.panelResults.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblGameOver;
        private System.Windows.Forms.Label lblFinalScore;
        private System.Windows.Forms.Label lblLevelReached;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Panel panelResults;
    }
}
