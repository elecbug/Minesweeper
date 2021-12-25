
namespace Minesweeper
{
    partial class RuleSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TextWidth = new System.Windows.Forms.RichTextBox();
            this.TextHeight = new System.Windows.Forms.RichTextBox();
            this.TextBombs = new System.Windows.Forms.RichTextBox();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Bombs";
            // 
            // TextWidth
            // 
            this.TextWidth.Location = new System.Drawing.Point(79, 12);
            this.TextWidth.Name = "TextWidth";
            this.TextWidth.Size = new System.Drawing.Size(100, 30);
            this.TextWidth.TabIndex = 1;
            this.TextWidth.Text = "";
            // 
            // TextHeight
            // 
            this.TextHeight.Location = new System.Drawing.Point(79, 48);
            this.TextHeight.Name = "TextHeight";
            this.TextHeight.Size = new System.Drawing.Size(100, 30);
            this.TextHeight.TabIndex = 2;
            this.TextHeight.Text = "";
            // 
            // TextBombs
            // 
            this.TextBombs.Location = new System.Drawing.Point(79, 84);
            this.TextBombs.Name = "TextBombs";
            this.TextBombs.Size = new System.Drawing.Size(100, 30);
            this.TextBombs.TabIndex = 3;
            this.TextBombs.Text = "";
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(14, 120);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(164, 25);
            this.ButtonOk.TabIndex = 4;
            this.ButtonOk.Text = "Set";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // RuleSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(191, 151);
            this.ControlBox = false;
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.TextBombs);
            this.Controls.Add(this.TextHeight);
            this.Controls.Add(this.TextWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "RuleSetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox TextWidth;
        private System.Windows.Forms.RichTextBox TextHeight;
        private System.Windows.Forms.RichTextBox TextBombs;
        private System.Windows.Forms.Button ButtonOk;
    }
}