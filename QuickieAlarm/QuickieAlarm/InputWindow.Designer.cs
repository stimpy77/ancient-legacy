namespace QuickieAlarm
{
    partial class InputWindow
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
            this.AlarmTime = new DevExpress.XtraEditors.TimeEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GoButton = new DevExpress.XtraEditors.SimpleButton();
            this.AlarmType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.SoundFilePicker = new DevExpress.XtraEditors.ButtonEdit();
            this.SoundFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.AlarmTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlarmType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoundFilePicker.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // AlarmTime
            // 
            this.AlarmTime.EditValue = new System.DateTime(2009, 5, 9, 0, 0, 0, 0);
            this.AlarmTime.Location = new System.Drawing.Point(150, 10);
            this.AlarmTime.Name = "AlarmTime";
            this.AlarmTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.AlarmTime.Size = new System.Drawing.Size(100, 20);
            this.AlarmTime.TabIndex = 0;
            this.AlarmTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AlarmTime_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "What time to ring alarm?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alarm type?";
            // 
            // GoButton
            // 
            this.GoButton.Location = new System.Drawing.Point(16, 89);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(233, 23);
            this.GoButton.TabIndex = 4;
            this.GoButton.Text = "Go";
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // AlarmType
            // 
            this.AlarmType.EditValue = "Beep";
            this.AlarmType.Location = new System.Drawing.Point(96, 37);
            this.AlarmType.Name = "AlarmType";
            this.AlarmType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.AlarmType.Properties.Items.AddRange(new object[] {
            "Beep",
            "Sound File",
            "Silent Visual"});
            this.AlarmType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.AlarmType.Size = new System.Drawing.Size(153, 20);
            this.AlarmType.TabIndex = 5;
            this.AlarmType.SelectedIndexChanged += new System.EventHandler(this.AlarmType_SelectedIndexChanged);
            this.AlarmType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AlarmType_KeyPress);
            // 
            // SoundFilePicker
            // 
            this.SoundFilePicker.Enabled = false;
            this.SoundFilePicker.Location = new System.Drawing.Point(16, 64);
            this.SoundFilePicker.Name = "SoundFilePicker";
            this.SoundFilePicker.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.SoundFilePicker.Size = new System.Drawing.Size(232, 20);
            this.SoundFilePicker.TabIndex = 6;
            this.SoundFilePicker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SoundFilePicker_KeyPress);
            this.SoundFilePicker.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.SoundFilePicker_ButtonPressed);
            // 
            // SoundFileDialog
            // 
            this.SoundFileDialog.Filter = "Sound files|*.wav;*.mp3";
            this.SoundFileDialog.InitialDirectory = "%windir%\\media";
            // 
            // InputWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 124);
            this.Controls.Add(this.SoundFilePicker);
            this.Controls.Add(this.AlarmType);
            this.Controls.Add(this.GoButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AlarmTime);
            this.DoubleBuffered = true;
            this.Name = "InputWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alarm Setup";
            this.Load += new System.EventHandler(this.InputWindow_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputWindow_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.AlarmTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlarmType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoundFilePicker.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TimeEdit AlarmTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton GoButton;
        private DevExpress.XtraEditors.ComboBoxEdit AlarmType;
        private DevExpress.XtraEditors.ButtonEdit SoundFilePicker;
        private System.Windows.Forms.OpenFileDialog SoundFileDialog;
    }
}

