namespace RealtimeReader
{
    partial class ReaderForm
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.btnScenarioBrowse = new System.Windows.Forms.Button();
            this.btnStartRead = new System.Windows.Forms.Button();
            this.btnStopRead = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbData = new System.Windows.Forms.TextBox();
            this.btnInput = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUpdate = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBrowseColumnData = new System.Windows.Forms.Button();
            this.tbColumnData = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnObject = new System.Windows.Forms.Button();
            this.rbtnNewObject = new System.Windows.Forms.RadioButton();
            this.rbtnExisting = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.tbObjectName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnManualRefresh = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdBtnNoRefresh = new System.Windows.Forms.RadioButton();
            this.rdBtnRefresh = new System.Windows.Forms.RadioButton();
            this.btnHelp = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnBrowseSave = new System.Windows.Forms.Button();
            this.tbSave = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(361, 112);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(102, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load Scenario";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(13, 68);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(190, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save Scenario to new directory";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Scenario directory:";
            // 
            // tbInput
            // 
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.Location = new System.Drawing.Point(13, 41);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(450, 20);
            this.tbInput.TabIndex = 3;
            this.tbInput.Text = "D:\\Realtime\\TE_Scenario\\Firebird_2.sc";
            this.tbInput.MouseHover += new System.EventHandler(this.tbInput_MouseHover);
            // 
            // btnScenarioBrowse
            // 
            this.btnScenarioBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScenarioBrowse.Location = new System.Drawing.Point(469, 39);
            this.btnScenarioBrowse.Name = "btnScenarioBrowse";
            this.btnScenarioBrowse.Size = new System.Drawing.Size(24, 23);
            this.btnScenarioBrowse.TabIndex = 4;
            this.btnScenarioBrowse.Text = "...";
            this.btnScenarioBrowse.UseVisualStyleBackColor = true;
            this.btnScenarioBrowse.Click += new System.EventHandler(this.btnScenarioBrowse_Click);
            // 
            // btnStartRead
            // 
            this.btnStartRead.Enabled = false;
            this.btnStartRead.Location = new System.Drawing.Point(6, 123);
            this.btnStartRead.Name = "btnStartRead";
            this.btnStartRead.Size = new System.Drawing.Size(75, 23);
            this.btnStartRead.TabIndex = 5;
            this.btnStartRead.Text = "Start Read";
            this.btnStartRead.UseVisualStyleBackColor = true;
            this.btnStartRead.Click += new System.EventHandler(this.btnStartRead_Click);
            // 
            // btnStopRead
            // 
            this.btnStopRead.Enabled = false;
            this.btnStopRead.Location = new System.Drawing.Point(136, 123);
            this.btnStopRead.Name = "btnStopRead";
            this.btnStopRead.Size = new System.Drawing.Size(75, 23);
            this.btnStopRead.TabIndex = 6;
            this.btnStopRead.Text = "Stop Read";
            this.btnStopRead.UseVisualStyleBackColor = true;
            this.btnStopRead.Click += new System.EventHandler(this.btnStopRead_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Data Location:";
            // 
            // tbData
            // 
            this.tbData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbData.Location = new System.Drawing.Point(13, 35);
            this.tbData.Name = "tbData";
            this.tbData.Size = new System.Drawing.Size(450, 20);
            this.tbData.TabIndex = 8;
            this.tbData.Text = "D:\\RealTime\\s4586-tetk-tool-2.1.0-SNAPSHOT\\data\\output.csv";
            this.tbData.MouseHover += new System.EventHandler(this.tbData_MouseHover);
            // 
            // btnInput
            // 
            this.btnInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInput.Location = new System.Drawing.Point(469, 35);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(24, 23);
            this.btnInput.TabIndex = 9;
            this.btnInput.Text = "...";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Auto-Refresh update interval: (sec)";
            // 
            // tbUpdate
            // 
            this.tbUpdate.Location = new System.Drawing.Point(186, 85);
            this.tbUpdate.Name = "tbUpdate";
            this.tbUpdate.Size = new System.Drawing.Size(100, 20);
            this.tbUpdate.TabIndex = 11;
            this.tbUpdate.Text = "10";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnBrowseColumnData);
            this.groupBox1.Controls.Add(this.tbColumnData);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbInput);
            this.groupBox1.Controls.Add(this.btnScenarioBrowse);
            this.groupBox1.Location = new System.Drawing.Point(25, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(540, 246);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scenario Setup";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Data Definitions:";
            // 
            // btnBrowseColumnData
            // 
            this.btnBrowseColumnData.Location = new System.Drawing.Point(469, 83);
            this.btnBrowseColumnData.Name = "btnBrowseColumnData";
            this.btnBrowseColumnData.Size = new System.Drawing.Size(24, 23);
            this.btnBrowseColumnData.TabIndex = 10;
            this.btnBrowseColumnData.Text = "...";
            this.btnBrowseColumnData.UseVisualStyleBackColor = true;
            this.btnBrowseColumnData.Click += new System.EventHandler(this.btnBrowseColumnData_Click);
            // 
            // tbColumnData
            // 
            this.tbColumnData.Location = new System.Drawing.Point(13, 85);
            this.tbColumnData.Name = "tbColumnData";
            this.tbColumnData.Size = new System.Drawing.Size(450, 20);
            this.tbColumnData.TabIndex = 9;
            this.tbColumnData.Text = "D:\\Realtime\\Writer\\XF1_ColumnData.xml";
            this.tbColumnData.MouseHover += new System.EventHandler(this.tbColumnData_MouseHover);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnObject);
            this.groupBox6.Controls.Add(this.rbtnNewObject);
            this.groupBox6.Controls.Add(this.rbtnExisting);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.tbObjectName);
            this.groupBox6.Location = new System.Drawing.Point(13, 141);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(504, 90);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Object Association";
            // 
            // btnObject
            // 
            this.btnObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObject.Location = new System.Drawing.Point(456, 29);
            this.btnObject.Name = "btnObject";
            this.btnObject.Size = new System.Drawing.Size(24, 23);
            this.btnObject.TabIndex = 10;
            this.btnObject.Text = "...";
            this.btnObject.UseVisualStyleBackColor = true;
            this.btnObject.Click += new System.EventHandler(this.btnObject_Click);
            // 
            // rbtnNewObject
            // 
            this.rbtnNewObject.AutoSize = true;
            this.rbtnNewObject.Enabled = false;
            this.rbtnNewObject.Location = new System.Drawing.Point(6, 48);
            this.rbtnNewObject.Name = "rbtnNewObject";
            this.rbtnNewObject.Size = new System.Drawing.Size(105, 17);
            this.rbtnNewObject.TabIndex = 9;
            this.rbtnNewObject.Text = "New STK Object";
            this.rbtnNewObject.UseVisualStyleBackColor = true;
            this.rbtnNewObject.CheckedChanged += new System.EventHandler(this.rbtnNewObject_CheckedChanged);
            // 
            // rbtnExisting
            // 
            this.rbtnExisting.AutoSize = true;
            this.rbtnExisting.Checked = true;
            this.rbtnExisting.Enabled = false;
            this.rbtnExisting.Location = new System.Drawing.Point(6, 25);
            this.rbtnExisting.Name = "rbtnExisting";
            this.rbtnExisting.Size = new System.Drawing.Size(119, 17);
            this.rbtnExisting.TabIndex = 8;
            this.rbtnExisting.TabStop = true;
            this.rbtnExisting.Text = "Existing STK Object";
            this.rbtnExisting.UseVisualStyleBackColor = true;
            this.rbtnExisting.CheckedChanged += new System.EventHandler(this.rbtnExisting_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(152, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Object Name:";
            // 
            // tbObjectName
            // 
            this.tbObjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbObjectName.Enabled = false;
            this.tbObjectName.Location = new System.Drawing.Point(155, 32);
            this.tbObjectName.Name = "tbObjectName";
            this.tbObjectName.Size = new System.Drawing.Size(295, 20);
            this.tbObjectName.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.btnInput);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbData);
            this.groupBox2.Location = new System.Drawing.Point(25, 278);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(540, 241);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Read Data";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnManualRefresh);
            this.groupBox4.Location = new System.Drawing.Point(322, 71);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(195, 80);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Manual Data Refresh";
            // 
            // btnManualRefresh
            // 
            this.btnManualRefresh.Enabled = false;
            this.btnManualRefresh.Location = new System.Drawing.Point(15, 19);
            this.btnManualRefresh.Name = "btnManualRefresh";
            this.btnManualRefresh.Size = new System.Drawing.Size(126, 23);
            this.btnManualRefresh.TabIndex = 0;
            this.btnManualRefresh.Text = "Refresh Now";
            this.btnManualRefresh.UseVisualStyleBackColor = true;
            this.btnManualRefresh.Click += new System.EventHandler(this.btnManualRefresh_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdBtnNoRefresh);
            this.groupBox3.Controls.Add(this.rdBtnRefresh);
            this.groupBox3.Controls.Add(this.btnStartRead);
            this.groupBox3.Controls.Add(this.btnStopRead);
            this.groupBox3.Controls.Add(this.tbUpdate);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(13, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 152);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Auto Data Refresh";
            // 
            // rdBtnNoRefresh
            // 
            this.rdBtnNoRefresh.AutoSize = true;
            this.rdBtnNoRefresh.Location = new System.Drawing.Point(12, 55);
            this.rdBtnNoRefresh.Name = "rdBtnNoRefresh";
            this.rdBtnNoRefresh.Size = new System.Drawing.Size(136, 17);
            this.rdBtnNoRefresh.TabIndex = 13;
            this.rdBtnNoRefresh.Text = "Do not use auto-refresh";
            this.rdBtnNoRefresh.UseVisualStyleBackColor = true;
            this.rdBtnNoRefresh.CheckedChanged += new System.EventHandler(this.rdBtnNoRefresh_CheckedChanged);
            // 
            // rdBtnRefresh
            // 
            this.rdBtnRefresh.AutoSize = true;
            this.rdBtnRefresh.Checked = true;
            this.rdBtnRefresh.Location = new System.Drawing.Point(12, 32);
            this.rdBtnRefresh.Name = "rdBtnRefresh";
            this.rdBtnRefresh.Size = new System.Drawing.Size(103, 17);
            this.rdBtnRefresh.TabIndex = 12;
            this.rdBtnRefresh.TabStop = true;
            this.rdBtnRefresh.Text = "Use auto-refresh";
            this.rdBtnRefresh.UseVisualStyleBackColor = true;
            this.rdBtnRefresh.CheckedChanged += new System.EventHandler(this.rdBtnRefresh_CheckedChanged);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(442, 71);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 10;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnHelp);
            this.groupBox5.Controls.Add(this.btnBrowseSave);
            this.groupBox5.Controls.Add(this.tbSave);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(25, 536);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(540, 100);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Save Scenario";
            // 
            // btnBrowseSave
            // 
            this.btnBrowseSave.Location = new System.Drawing.Point(469, 42);
            this.btnBrowseSave.Name = "btnBrowseSave";
            this.btnBrowseSave.Size = new System.Drawing.Size(24, 23);
            this.btnBrowseSave.TabIndex = 2;
            this.btnBrowseSave.Text = "...";
            this.btnBrowseSave.UseVisualStyleBackColor = true;
            this.btnBrowseSave.Click += new System.EventHandler(this.btnBrowseSave_Click);
            // 
            // tbSave
            // 
            this.tbSave.Location = new System.Drawing.Point(13, 42);
            this.tbSave.Name = "tbSave";
            this.tbSave.Size = new System.Drawing.Size(450, 20);
            this.tbSave.TabIndex = 1;
            this.tbSave.Text = "D:\\Realtime\\Archive\\Firebird.sc";
            this.tbSave.MouseHover += new System.EventHandler(this.tbSave_MouseHover);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Scenario directory";
            // 
            // ReaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 646);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(590, 685);
            this.Name = "ReaderForm";
            this.Text = "Realtime Reader";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Button btnScenarioBrowse;
        private System.Windows.Forms.Button btnStartRead;
        private System.Windows.Forms.Button btnStopRead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbData;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbObjectName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnManualRefresh;
        private System.Windows.Forms.RadioButton rdBtnNoRefresh;
        private System.Windows.Forms.RadioButton rdBtnRefresh;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbtnNewObject;
        private System.Windows.Forms.RadioButton rbtnExisting;
        private System.Windows.Forms.Button btnObject;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tbSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowseSave;
        private System.Windows.Forms.Button btnBrowseColumnData;
        private System.Windows.Forms.TextBox tbColumnData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

