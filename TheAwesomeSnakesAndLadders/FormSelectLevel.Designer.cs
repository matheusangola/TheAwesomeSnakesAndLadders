namespace TheAwesomeSnakesAndLadders
{
    partial class FormSelectLevel
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
            this.numberOfPlayers = new System.Windows.Forms.ComboBox();
            this.gameLevel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nextBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // numberOfPlayers
            // 
            this.numberOfPlayers.AutoCompleteCustomSource.AddRange(new string[] {
            "2",
            "3",
            "4"});
            this.numberOfPlayers.FormattingEnabled = true;
            this.numberOfPlayers.Items.AddRange(new object[] {
            "2",
            "3",
            "4"});
            this.numberOfPlayers.Location = new System.Drawing.Point(388, 115);
            this.numberOfPlayers.Name = "numberOfPlayers";
            this.numberOfPlayers.Size = new System.Drawing.Size(121, 32);
            this.numberOfPlayers.TabIndex = 3;
            // 
            // gameLevel
            // 
            this.gameLevel.FormattingEnabled = true;
            this.gameLevel.Items.AddRange(new object[] {
            "Easy",
            "Medium",
            "Hard"});
            this.gameLevel.Location = new System.Drawing.Point(388, 191);
            this.gameLevel.Name = "gameLevel";
            this.gameLevel.Size = new System.Drawing.Size(121, 32);
            this.gameLevel.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select Game Level:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select Number of Players:";
            // 
            // nextBtn
            // 
            this.nextBtn.Location = new System.Drawing.Point(531, 288);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(129, 55);
            this.nextBtn.TabIndex = 7;
            this.nextBtn.Text = "Next";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // FormSelectLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gameLevel);
            this.Controls.Add(this.numberOfPlayers);
            this.Name = "FormSelectLevel";
            this.Text = "FormSelectLevel";
            this.Load += new System.EventHandler(this.FormSelectLevel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox numberOfPlayers;
        private System.Windows.Forms.ComboBox gameLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button nextBtn;
    }
}