
namespace Note_Taker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.delBtn = new System.Windows.Forms.Button();
            this.noteList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.noteName = new System.Windows.Forms.TextBox();
            this.noteBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.savBtn = new System.Windows.Forms.Button();
            this.newBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // delBtn
            // 
            this.delBtn.Location = new System.Drawing.Point(365, 37);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(133, 23);
            this.delBtn.TabIndex = 2;
            this.delBtn.Text = "Delete Selected Note";
            this.delBtn.UseVisualStyleBackColor = true;
            this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
            // 
            // noteList
            // 
            this.noteList.FormattingEnabled = true;
            this.noteList.ItemHeight = 15;
            this.noteList.Location = new System.Drawing.Point(12, 37);
            this.noteList.Name = "noteList";
            this.noteList.Size = new System.Drawing.Size(337, 154);
            this.noteList.TabIndex = 3;
            this.noteList.SelectedIndexChanged += new System.EventHandler(this.noteList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Note List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Note Name";
            // 
            // noteName
            // 
            this.noteName.Location = new System.Drawing.Point(12, 217);
            this.noteName.Name = "noteName";
            this.noteName.Size = new System.Drawing.Size(337, 23);
            this.noteName.TabIndex = 6;
            // 
            // noteBox
            // 
            this.noteBox.Location = new System.Drawing.Point(12, 277);
            this.noteBox.Name = "noteBox";
            this.noteBox.Size = new System.Drawing.Size(486, 161);
            this.noteBox.TabIndex = 7;
            this.noteBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Note";
            // 
            // savBtn
            // 
            this.savBtn.Location = new System.Drawing.Point(365, 217);
            this.savBtn.Name = "savBtn";
            this.savBtn.Size = new System.Drawing.Size(133, 23);
            this.savBtn.TabIndex = 9;
            this.savBtn.Text = "Save Changes";
            this.savBtn.UseVisualStyleBackColor = true;
            this.savBtn.Click += new System.EventHandler(this.savBtn_Click);
            // 
            // newBtn
            // 
            this.newBtn.Location = new System.Drawing.Point(365, 188);
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(133, 23);
            this.newBtn.TabIndex = 10;
            this.newBtn.Text = "New Note";
            this.newBtn.UseVisualStyleBackColor = true;
            this.newBtn.Click += new System.EventHandler(this.newBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 450);
            this.Controls.Add(this.newBtn);
            this.Controls.Add(this.savBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.noteBox);
            this.Controls.Add(this.noteName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.noteList);
            this.Controls.Add(this.delBtn);
            this.Name = "Form1";
            this.Text = "Note Taker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button delBtn;
        private System.Windows.Forms.ListBox noteList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox noteName;
        private System.Windows.Forms.RichTextBox noteBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button savBtn;
        private System.Windows.Forms.Button newBtn;
    }
}

