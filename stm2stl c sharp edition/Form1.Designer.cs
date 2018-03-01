namespace stm2stl_c_sharp_edition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileBut = new System.Windows.Forms.Button();
            this.exitBut = new System.Windows.Forms.Button();
            this.inputFilenameBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fixBut = new System.Windows.Forms.Button();
            this.stlBut = new System.Windows.Forms.Button();
            this.fieldSelector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openFileBut
            // 
            this.openFileBut.Location = new System.Drawing.Point(300, 25);
            this.openFileBut.Name = "openFileBut";
            this.openFileBut.Size = new System.Drawing.Size(75, 21);
            this.openFileBut.TabIndex = 0;
            this.openFileBut.Text = "Browse";
            this.openFileBut.UseVisualStyleBackColor = true;
            this.openFileBut.Click += new System.EventHandler(this.openFileBut_Click);
            // 
            // exitBut
            // 
            this.exitBut.Location = new System.Drawing.Point(300, 62);
            this.exitBut.Name = "exitBut";
            this.exitBut.Size = new System.Drawing.Size(75, 23);
            this.exitBut.TabIndex = 1;
            this.exitBut.Text = "Exit";
            this.exitBut.UseVisualStyleBackColor = true;
            this.exitBut.Click += new System.EventHandler(this.exitBut_Click);
            // 
            // inputFilenameBox
            // 
            this.inputFilenameBox.Location = new System.Drawing.Point(11, 25);
            this.inputFilenameBox.Name = "inputFilenameBox";
            this.inputFilenameBox.ReadOnly = true;
            this.inputFilenameBox.Size = new System.Drawing.Size(283, 20);
            this.inputFilenameBox.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // fixBut
            // 
            this.fixBut.Enabled = false;
            this.fixBut.Location = new System.Drawing.Point(138, 62);
            this.fixBut.Name = "fixBut";
            this.fixBut.Size = new System.Drawing.Size(75, 23);
            this.fixBut.TabIndex = 3;
            this.fixBut.Text = "Fix Plane";
            this.fixBut.UseVisualStyleBackColor = true;
            this.fixBut.Click += new System.EventHandler(this.fixBut_Click);
            // 
            // stlBut
            // 
            this.stlBut.Enabled = false;
            this.stlBut.Location = new System.Drawing.Point(219, 62);
            this.stlBut.Name = "stlBut";
            this.stlBut.Size = new System.Drawing.Size(75, 23);
            this.stlBut.TabIndex = 4;
            this.stlBut.Text = "Save as .stl";
            this.stlBut.UseVisualStyleBackColor = true;
            this.stlBut.Click += new System.EventHandler(this.stlBut_Click);
            // 
            // fieldSelector
            // 
            this.fieldSelector.Enabled = false;
            this.fieldSelector.FormattingEnabled = true;
            this.fieldSelector.Location = new System.Drawing.Point(11, 64);
            this.fieldSelector.MaxDropDownItems = 100;
            this.fieldSelector.Name = "fieldSelector";
            this.fieldSelector.Size = new System.Drawing.Size(121, 21);
            this.fieldSelector.TabIndex = 5;
            this.fieldSelector.SelectedIndexChanged += new System.EventHandler(this.fieldSelector_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Field selector";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Open file";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 101);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fieldSelector);
            this.Controls.Add(this.stlBut);
            this.Controls.Add(this.fixBut);
            this.Controls.Add(this.inputFilenameBox);
            this.Controls.Add(this.exitBut);
            this.Controls.Add(this.openFileBut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Stm2Stl c# edition";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openFileBut;
        private System.Windows.Forms.Button exitBut;
        private System.Windows.Forms.TextBox inputFilenameBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button fixBut;
        private System.Windows.Forms.Button stlBut;
        private System.Windows.Forms.ComboBox fieldSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

