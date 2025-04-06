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
            btndet = new Button();
            btnbrow = new Button();
            PB = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PB).BeginInit();
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
            splitContainer1.Panel1.Controls.Add(btndet);
            splitContainer1.Panel1.Controls.Add(btnbrow);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(PB);
            splitContainer1.Size = new Size(664, 337);
            splitContainer1.SplitterDistance = 221;
            splitContainer1.TabIndex = 0;
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
            PB.Size = new Size(318, 280);
            PB.SizeMode = PictureBoxSizeMode.StretchImage;
            PB.TabIndex = 0;
            PB.TabStop = false;
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
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Button btndet;
        private Button btnbrow;
        private PictureBox PB;
    }
}