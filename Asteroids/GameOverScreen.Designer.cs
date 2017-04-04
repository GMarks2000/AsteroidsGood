namespace Asteroids
{
    partial class GameOverScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gameOverLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.replayButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameOverLabel
            // 
            this.gameOverLabel.AutoSize = true;
            this.gameOverLabel.Font = new System.Drawing.Font("Consolas", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameOverLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.gameOverLabel.Location = new System.Drawing.Point(124, 58);
            this.gameOverLabel.Name = "gameOverLabel";
            this.gameOverLabel.Size = new System.Drawing.Size(347, 75);
            this.gameOverLabel.TabIndex = 0;
            this.gameOverLabel.Text = "Game Over";
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.ForeColor = System.Drawing.Color.Snow;
            this.scoreLabel.Location = new System.Drawing.Point(201, 133);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(125, 37);
            this.scoreLabel.TabIndex = 1;
            this.scoreLabel.Text = "Score:";
            // 
            // replayButton
            // 
            this.replayButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.replayButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.replayButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replayButton.ForeColor = System.Drawing.Color.Snow;
            this.replayButton.Location = new System.Drawing.Point(235, 173);
            this.replayButton.Name = "replayButton";
            this.replayButton.Size = new System.Drawing.Size(116, 33);
            this.replayButton.TabIndex = 2;
            this.replayButton.Text = "Play Again";
            this.replayButton.UseVisualStyleBackColor = false;
            this.replayButton.Click += new System.EventHandler(this.replayButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.quitButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.quitButton.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitButton.ForeColor = System.Drawing.Color.Snow;
            this.quitButton.Location = new System.Drawing.Point(235, 212);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(116, 33);
            this.quitButton.TabIndex = 3;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = false;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // GameOverScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.replayButton);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.gameOverLabel);
            this.Name = "GameOverScreen";
            this.Size = new System.Drawing.Size(600, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label gameOverLabel;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Button replayButton;
        private System.Windows.Forms.Button quitButton;
    }
}
