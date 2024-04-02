namespace RealTimeWriter
{
    partial class WriterForm
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
            DisposeComponents();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WriterForm));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.btnInput = new System.Windows.Forms.Button();
            this.btnOutput = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbDataRate = new System.Windows.Forms.TextBox();
            this.tbDataRefreshSpeed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSpeedMultiplier = new System.Windows.Forms.TextBox();
            this.pbDataRate = new System.Windows.Forms.PictureBox();
            this.pbDataRefreshSpeed = new System.Windows.Forms.PictureBox();
            this.pbSpeedMultiplier = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbDataRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDataRefreshSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpeedMultiplier)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(72, 179);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 6;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(178, 179);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 30);
            this.button2.TabIndex = 7;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Input";
            // 
            // tbInput
            // 
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.Location = new System.Drawing.Point(72, 119);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(368, 20);
            this.tbInput.TabIndex = 4;
            this.tbInput.Text = "*File_to_read_from*";
            this.tbInput.MouseHover += new System.EventHandler(this.tbInput_Hover);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Output";
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.Location = new System.Drawing.Point(72, 153);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(368, 20);
            this.tbOutput.TabIndex = 5;
            this.tbOutput.Text = "*File_to_write_to*";
            // 
            // btnInput
            // 
            this.btnInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInput.Location = new System.Drawing.Point(446, 117);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(26, 23);
            this.btnInput.TabIndex = 0;
            this.btnInput.TabStop = false;
            this.btnInput.Text = "...";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // btnOutput
            // 
            this.btnOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutput.Location = new System.Drawing.Point(446, 150);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(26, 23);
            this.btnOutput.TabIndex = 0;
            this.btnOutput.TabStop = false;
            this.btnOutput.Text = "...";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 30000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // tbDataRate
            // 
            this.tbDataRate.Location = new System.Drawing.Point(161, 34);
            this.tbDataRate.Name = "tbDataRate";
            this.tbDataRate.Size = new System.Drawing.Size(106, 20);
            this.tbDataRate.TabIndex = 1;
            this.toolTip1.SetToolTip(this.tbDataRate, "TESTY");
            // 
            // tbDataRefreshSpeed
            // 
            this.tbDataRefreshSpeed.Location = new System.Drawing.Point(161, 60);
            this.tbDataRefreshSpeed.Name = "tbDataRefreshSpeed";
            this.tbDataRefreshSpeed.Size = new System.Drawing.Size(106, 20);
            this.tbDataRefreshSpeed.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Data Rate (Hz)";
            this.toolTip1.SetToolTip(this.label4, "TTTTTTYYY");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Data Refresh Speed (s)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "x Speed Multiplier";
            // 
            // tbSpeedMultiplier
            // 
            this.tbSpeedMultiplier.Location = new System.Drawing.Point(161, 85);
            this.tbSpeedMultiplier.Name = "tbSpeedMultiplier";
            this.tbSpeedMultiplier.Size = new System.Drawing.Size(106, 20);
            this.tbSpeedMultiplier.TabIndex = 3;
            this.tbSpeedMultiplier.Text = "1";
            // 
            // pbDataRate
            // 
            this.pbDataRate.Image = ((System.Drawing.Image)(resources.GetObject("pbDataRate.Image")));
            this.pbDataRate.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbDataRate.InitialImage")));
            this.pbDataRate.Location = new System.Drawing.Point(273, 34);
            this.pbDataRate.Name = "pbDataRate";
            this.pbDataRate.Size = new System.Drawing.Size(20, 20);
            this.pbDataRate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDataRate.TabIndex = 15;
            this.pbDataRate.TabStop = false;
            this.toolTip1.SetToolTip(this.pbDataRate, "Set the data rate to match the data rate in the input file.");
            // 
            // pbDataRefreshSpeed
            // 
            this.pbDataRefreshSpeed.Image = ((System.Drawing.Image)(resources.GetObject("pbDataRefreshSpeed.Image")));
            this.pbDataRefreshSpeed.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbDataRefreshSpeed.InitialImage")));
            this.pbDataRefreshSpeed.Location = new System.Drawing.Point(273, 60);
            this.pbDataRefreshSpeed.Name = "pbDataRefreshSpeed";
            this.pbDataRefreshSpeed.Size = new System.Drawing.Size(20, 20);
            this.pbDataRefreshSpeed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDataRefreshSpeed.TabIndex = 16;
            this.pbDataRefreshSpeed.TabStop = false;
            this.toolTip1.SetToolTip(this.pbDataRefreshSpeed, "Set the rate in seconds at which you want the input file to be incrementally read" +
        " and pushed to the output file.");
            // 
            // pbSpeedMultiplier
            // 
            this.pbSpeedMultiplier.Image = ((System.Drawing.Image)(resources.GetObject("pbSpeedMultiplier.Image")));
            this.pbSpeedMultiplier.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbSpeedMultiplier.InitialImage")));
            this.pbSpeedMultiplier.Location = new System.Drawing.Point(273, 85);
            this.pbSpeedMultiplier.Name = "pbSpeedMultiplier";
            this.pbSpeedMultiplier.Size = new System.Drawing.Size(20, 20);
            this.pbSpeedMultiplier.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSpeedMultiplier.TabIndex = 17;
            this.pbSpeedMultiplier.TabStop = false;
            this.toolTip1.SetToolTip(this.pbSpeedMultiplier, resources.GetString("pbSpeedMultiplier.ToolTip"));
            // 
            // WriterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 221);
            this.Controls.Add(this.pbSpeedMultiplier);
            this.Controls.Add(this.pbDataRefreshSpeed);
            this.Controls.Add(this.pbDataRate);
            this.Controls.Add(this.tbSpeedMultiplier);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbDataRefreshSpeed);
            this.Controls.Add(this.tbDataRate);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MaximumSize = new System.Drawing.Size(1920, 260);
            this.MinimumSize = new System.Drawing.Size(450, 260);
            this.Name = "WriterForm";
            this.Text = "Realtime Writer";
            ((System.ComponentModel.ISupportInitialize)(this.pbDataRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDataRefreshSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpeedMultiplier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox tbDataRate;
        private System.Windows.Forms.TextBox tbDataRefreshSpeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSpeedMultiplier;
        private System.Windows.Forms.PictureBox pbDataRate;
        private System.Windows.Forms.PictureBox pbDataRefreshSpeed;
        private System.Windows.Forms.PictureBox pbSpeedMultiplier;
    }
}

