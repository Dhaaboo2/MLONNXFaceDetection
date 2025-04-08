namespace MLONNXFaceDetection.GUIS
{
    partial class MLOnnxFaceDetectionFrm
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
            splitContainer1 = new SplitContainer();
            btnCom = new Button();
            btndet = new Button();
            btnbrow = new Button();
            PB = new PictureBox();
            PB1 = new PictureBox();
            PB2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PB2).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(btnCom);
            splitContainer1.Panel1.Controls.Add(btndet);
            splitContainer1.Panel1.Controls.Add(btnbrow);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(PB2);
            splitContainer1.Panel2.Controls.Add(PB1);
            splitContainer1.Panel2.Controls.Add(PB);
            splitContainer1.Size = new Size(664, 337);
            splitContainer1.SplitterDistance = 221;
            splitContainer1.TabIndex = 0;
            // 
            // btnCom
            // 
            btnCom.Location = new Point(12, 100);
            btnCom.Name = "btnCom";
            btnCom.Size = new Size(119, 38);
            btnCom.TabIndex = 2;
            btnCom.Text = "Compare Face";
            btnCom.UseVisualStyleBackColor = true;
            btnCom.Click += btnCom_Click;
            // 
            // btndet
            // 
            btndet.Location = new Point(12, 56);
            btndet.Name = "btndet";
            btndet.Size = new Size(119, 38);
            btndet.TabIndex = 1;
            btndet.Text = "Detect Face";
            btndet.UseVisualStyleBackColor = true;
            btndet.Click += btndet_Click;
            // 
            // btnbrow
            // 
            btnbrow.Location = new Point(12, 12);
            btnbrow.Name = "btnbrow";
            btnbrow.Size = new Size(119, 38);
            btnbrow.TabIndex = 0;
            btnbrow.Text = "Brows Pic";
            btnbrow.UseVisualStyleBackColor = true;
            btnbrow.Click += btnbrow_Click;
            // 
            // PB
            // 
            PB.BackColor = Color.Cyan;
            PB.Location = new Point(55, 12);
            PB.Name = "PB";
            PB.Size = new Size(318, 150);
            PB.SizeMode = PictureBoxSizeMode.StretchImage;
            PB.TabIndex = 0;
            PB.TabStop = false;
            // 
            // PB1
            // 
            PB1.BackColor = Color.Cyan;
            PB1.Location = new Point(23, 175);
            PB1.Name = "PB1";
            PB1.Size = new Size(181, 150);
            PB1.SizeMode = PictureBoxSizeMode.StretchImage;
            PB1.TabIndex = 1;
            PB1.TabStop = false;
            // 
            // PB2
            // 
            PB2.BackColor = Color.Cyan;
            PB2.Location = new Point(232, 175);
            PB2.Name = "PB2";
            PB2.Size = new Size(195, 150);
            PB2.SizeMode = PictureBoxSizeMode.StretchImage;
            PB2.TabIndex = 2;
            PB2.TabStop = false;
            // 
            // MLOnnxFaceDetectionFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(664, 337);
            Controls.Add(splitContainer1);
            Font = new Font("Times New Roman", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            Name = "MLOnnxFaceDetectionFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MLOnnxFaceDetectionFrm";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PB).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PB2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Button btndet;
        private Button btnbrow;
        private PictureBox PB;
        private Button btnCom;
        private PictureBox PB2;
        private PictureBox PB1;
    }
}