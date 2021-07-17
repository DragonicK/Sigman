namespace Sigman.Client {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GroupOutput = new System.Windows.Forms.GroupBox();
            this.ButtonFolder = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TextOutputFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ProgressReceive = new System.Windows.Forms.ProgressBar();
            this.GroupInput = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ProgressSend = new System.Windows.Forms.ProgressBar();
            this.ButtonFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TextInputFile = new System.Windows.Forms.TextBox();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.ButtonIcon = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TextInputIcon = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.GroupOutput.SuspendLayout();
            this.GroupInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GroupOutput);
            this.groupBox1.Controls.Add(this.GroupInput);
            this.groupBox1.Controls.Add(this.ButtonCancel);
            this.groupBox1.Controls.Add(this.ButtonStart);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 9F);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 447);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // GroupOutput
            // 
            this.GroupOutput.Controls.Add(this.ButtonFolder);
            this.GroupOutput.Controls.Add(this.label4);
            this.GroupOutput.Controls.Add(this.TextOutputFolder);
            this.GroupOutput.Controls.Add(this.label3);
            this.GroupOutput.Controls.Add(this.ProgressReceive);
            this.GroupOutput.Enabled = false;
            this.GroupOutput.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupOutput.Location = new System.Drawing.Point(17, 232);
            this.GroupOutput.Name = "GroupOutput";
            this.GroupOutput.Size = new System.Drawing.Size(401, 159);
            this.GroupOutput.TabIndex = 12;
            this.GroupOutput.TabStop = false;
            this.GroupOutput.Text = "Output Folder";
            // 
            // ButtonFolder
            // 
            this.ButtonFolder.Location = new System.Drawing.Point(335, 51);
            this.ButtonFolder.Name = "ButtonFolder";
            this.ButtonFolder.Size = new System.Drawing.Size(47, 28);
            this.ButtonFolder.TabIndex = 3;
            this.ButtonFolder.Text = "...";
            this.ButtonFolder.UseVisualStyleBackColor = true;
            this.ButtonFolder.Click += new System.EventHandler(this.ButtonFolder_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(370, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Folder";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextOutputFolder
            // 
            this.TextOutputFolder.Location = new System.Drawing.Point(15, 53);
            this.TextOutputFolder.Multiline = true;
            this.TextOutputFolder.Name = "TextOutputFolder";
            this.TextOutputFolder.Size = new System.Drawing.Size(314, 26);
            this.TextOutputFolder.TabIndex = 2;
            this.TextOutputFolder.TextChanged += new System.EventHandler(this.TextOutputFolder_TextChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(370, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "... Receiving ...";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressReceive
            // 
            this.ProgressReceive.Location = new System.Drawing.Point(15, 111);
            this.ProgressReceive.Name = "ProgressReceive";
            this.ProgressReceive.Size = new System.Drawing.Size(367, 23);
            this.ProgressReceive.Step = 1;
            this.ProgressReceive.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressReceive.TabIndex = 11;
            // 
            // GroupInput
            // 
            this.GroupInput.Controls.Add(this.ButtonIcon);
            this.GroupInput.Controls.Add(this.label5);
            this.GroupInput.Controls.Add(this.TextInputIcon);
            this.GroupInput.Controls.Add(this.label2);
            this.GroupInput.Controls.Add(this.ProgressSend);
            this.GroupInput.Controls.Add(this.ButtonFile);
            this.GroupInput.Controls.Add(this.label1);
            this.GroupInput.Controls.Add(this.TextInputFile);
            this.GroupInput.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupInput.Location = new System.Drawing.Point(17, 21);
            this.GroupInput.Name = "GroupInput";
            this.GroupInput.Size = new System.Drawing.Size(401, 196);
            this.GroupInput.TabIndex = 11;
            this.GroupInput.TabStop = false;
            this.GroupInput.Text = "Input File";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(370, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "... Sending ...";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressSend
            // 
            this.ProgressSend.Location = new System.Drawing.Point(15, 152);
            this.ProgressSend.Name = "ProgressSend";
            this.ProgressSend.Size = new System.Drawing.Size(367, 23);
            this.ProgressSend.Step = 1;
            this.ProgressSend.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressSend.TabIndex = 12;
            // 
            // ButtonFile
            // 
            this.ButtonFile.Location = new System.Drawing.Point(335, 42);
            this.ButtonFile.Name = "ButtonFile";
            this.ButtonFile.Size = new System.Drawing.Size(47, 28);
            this.ButtonFile.TabIndex = 1;
            this.ButtonFile.Text = "...";
            this.ButtonFile.UseVisualStyleBackColor = true;
            this.ButtonFile.Click += new System.EventHandler(this.ButtonFile_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(370, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "File";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextInputFile
            // 
            this.TextInputFile.Location = new System.Drawing.Point(15, 44);
            this.TextInputFile.Multiline = true;
            this.TextInputFile.Name = "TextInputFile";
            this.TextInputFile.Size = new System.Drawing.Size(314, 26);
            this.TextInputFile.TabIndex = 0;
            this.TextInputFile.TextChanged += new System.EventHandler(this.TextInputFile_TextChanged);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Enabled = false;
            this.ButtonCancel.Location = new System.Drawing.Point(241, 402);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(131, 28);
            this.ButtonCancel.TabIndex = 5;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonStart
            // 
            this.ButtonStart.Enabled = false;
            this.ButtonStart.Location = new System.Drawing.Point(62, 402);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(131, 28);
            this.ButtonStart.TabIndex = 4;
            this.ButtonStart.Text = "Start";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // ButtonIcon
            // 
            this.ButtonIcon.Location = new System.Drawing.Point(335, 94);
            this.ButtonIcon.Name = "ButtonIcon";
            this.ButtonIcon.Size = new System.Drawing.Size(47, 28);
            this.ButtonIcon.TabIndex = 15;
            this.ButtonIcon.Text = "...";
            this.ButtonIcon.UseVisualStyleBackColor = true;
            this.ButtonIcon.Click += new System.EventHandler(this.ButtonIcon_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(370, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Icon";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextInputIcon
            // 
            this.TextInputIcon.Location = new System.Drawing.Point(15, 96);
            this.TextInputIcon.Multiline = true;
            this.TextInputIcon.Name = "TextInputIcon";
            this.TextInputIcon.Size = new System.Drawing.Size(314, 26);
            this.TextInputIcon.TabIndex = 14;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 477);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.GroupOutput.ResumeLayout(false);
            this.GroupOutput.PerformLayout();
            this.GroupInput.ResumeLayout(false);
            this.GroupInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.GroupBox GroupInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar ProgressSend;
        private System.Windows.Forms.Button ButtonFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextInputFile;
        private System.Windows.Forms.GroupBox GroupOutput;
        private System.Windows.Forms.Button ButtonFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextOutputFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar ProgressReceive;
        private System.Windows.Forms.Button ButtonIcon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextInputIcon;
    }
}