namespace B19Ex05_LidorTevet_312465040_SaharHagbi_308145853
{
    public partial class OpenForm
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
            this.b_AgainstComputer = new System.Windows.Forms.Button();
            this.b_AgainstFriend = new System.Windows.Forms.Button();
            this.b_Size = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // b_AgainstComputer
            // 
            this.b_AgainstComputer.Location = new System.Drawing.Point(9, 104);
            this.b_AgainstComputer.Margin = new System.Windows.Forms.Padding(2);
            this.b_AgainstComputer.Name = "b_AgainstComputer";
            this.b_AgainstComputer.Size = new System.Drawing.Size(148, 56);
            this.b_AgainstComputer.TabIndex = 1;
            this.b_AgainstComputer.Text = "Play againt the computer";
            this.b_AgainstComputer.UseVisualStyleBackColor = true;
            this.b_AgainstComputer.Click += new System.EventHandler(this.againstComputer_Click);
            // 
            // b_AgainstFriend
            // 
            this.b_AgainstFriend.Location = new System.Drawing.Point(182, 104);
            this.b_AgainstFriend.Margin = new System.Windows.Forms.Padding(2);
            this.b_AgainstFriend.Name = "b_AgainstFriend";
            this.b_AgainstFriend.Size = new System.Drawing.Size(146, 56);
            this.b_AgainstFriend.TabIndex = 2;
            this.b_AgainstFriend.Text = "play againt your friend";
            this.b_AgainstFriend.UseVisualStyleBackColor = true;
            this.b_AgainstFriend.Click += new System.EventHandler(this.againstFriend_Click);
            // 
            // b_Size
            // 
            this.b_Size.Location = new System.Drawing.Point(59, 19);
            this.b_Size.Margin = new System.Windows.Forms.Padding(2);
            this.b_Size.Name = "b_Size";
            this.b_Size.Size = new System.Drawing.Size(201, 56);
            this.b_Size.TabIndex = 3;
            this.b_Size.Text = "Board Size: 6x6 (click to increase)";
            this.b_Size.UseVisualStyleBackColor = true;
            this.b_Size.Click += new System.EventHandler(this.size_Click);
            // 
            // OpenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 171);
            this.Controls.Add(this.b_Size);
            this.Controls.Add(this.b_AgainstFriend);
            this.Controls.Add(this.b_AgainstComputer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.Load += new System.EventHandler(this.OpenForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button b_AgainstComputer;
        private System.Windows.Forms.Button b_AgainstFriend;
        private System.Windows.Forms.Button b_Size;
    }
}