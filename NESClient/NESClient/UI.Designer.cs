namespace NESClient
{
    partial class UI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadOtherGamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeAScreenShotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectAsClientNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectViaIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controllerInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upDpadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downDpadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftDpadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightDpadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.returnToTitleScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IPText = new System.Windows.Forms.TextBox();
            this.GO = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.clientNumText = new System.Windows.Forms.TextBox();
            this.GO1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectViaIPToolStripMenuItem,
            this.controllerInputToolStripMenuItem,
            this.returnToTitleScreenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(496, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadOtherGamesToolStripMenuItem,
            this.takeAScreenShotToolStripMenuItem,
            this.connectAsClientNToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadOtherGamesToolStripMenuItem
            // 
            this.loadOtherGamesToolStripMenuItem.Name = "loadOtherGamesToolStripMenuItem";
            this.loadOtherGamesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.loadOtherGamesToolStripMenuItem.Text = "Load other Games";
            this.loadOtherGamesToolStripMenuItem.Click += new System.EventHandler(this.loadOtherGamesToolStripMenuItem_Click);
            // 
            // takeAScreenShotToolStripMenuItem
            // 
            this.takeAScreenShotToolStripMenuItem.Name = "takeAScreenShotToolStripMenuItem";
            this.takeAScreenShotToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.takeAScreenShotToolStripMenuItem.Text = "Take a ScreenShot";
            this.takeAScreenShotToolStripMenuItem.Click += new System.EventHandler(this.takeAScreenShotToolStripMenuItem_Click);
            // 
            // connectAsClientNToolStripMenuItem
            // 
            this.connectAsClientNToolStripMenuItem.Name = "connectAsClientNToolStripMenuItem";
            this.connectAsClientNToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.connectAsClientNToolStripMenuItem.Text = "Connect As Client N";
            this.connectAsClientNToolStripMenuItem.Click += new System.EventHandler(this.connectAsClientNToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // connectViaIPToolStripMenuItem
            // 
            this.connectViaIPToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.connectViaIPToolStripMenuItem.Name = "connectViaIPToolStripMenuItem";
            this.connectViaIPToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.connectViaIPToolStripMenuItem.Text = "Connect via IP";
            this.connectViaIPToolStripMenuItem.Click += new System.EventHandler(this.connectViaIPToolStripMenuItem_Click);
            // 
            // controllerInputToolStripMenuItem
            // 
            this.controllerInputToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aButtonToolStripMenuItem,
            this.bButtonToolStripMenuItem,
            this.selectButtonToolStripMenuItem,
            this.startButtonToolStripMenuItem,
            this.upDpadToolStripMenuItem,
            this.downDpadToolStripMenuItem,
            this.leftDpadToolStripMenuItem,
            this.rightDpadToolStripMenuItem});
            this.controllerInputToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.controllerInputToolStripMenuItem.Name = "controllerInputToolStripMenuItem";
            this.controllerInputToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.controllerInputToolStripMenuItem.Text = "Controller input";
            // 
            // aButtonToolStripMenuItem
            // 
            this.aButtonToolStripMenuItem.Name = "aButtonToolStripMenuItem";
            this.aButtonToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.aButtonToolStripMenuItem.Text = "A         button";
            this.aButtonToolStripMenuItem.Click += new System.EventHandler(this.AButtonToolStripMenuItem_Click);
            // 
            // bButtonToolStripMenuItem
            // 
            this.bButtonToolStripMenuItem.Name = "bButtonToolStripMenuItem";
            this.bButtonToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.bButtonToolStripMenuItem.Text = "B         button";
            this.bButtonToolStripMenuItem.Click += new System.EventHandler(this.BButtonToolStripMenuItem_Click);
            // 
            // selectButtonToolStripMenuItem
            // 
            this.selectButtonToolStripMenuItem.Name = "selectButtonToolStripMenuItem";
            this.selectButtonToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.selectButtonToolStripMenuItem.Text = "Select button";
            this.selectButtonToolStripMenuItem.Click += new System.EventHandler(this.SelectButtonToolStripMenuItem_Click);
            // 
            // startButtonToolStripMenuItem
            // 
            this.startButtonToolStripMenuItem.Name = "startButtonToolStripMenuItem";
            this.startButtonToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.startButtonToolStripMenuItem.Text = "Start    button";
            this.startButtonToolStripMenuItem.Click += new System.EventHandler(this.StartButtonToolStripMenuItem_Click);
            // 
            // upDpadToolStripMenuItem
            // 
            this.upDpadToolStripMenuItem.Name = "upDpadToolStripMenuItem";
            this.upDpadToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.upDpadToolStripMenuItem.Text = "Up-     D-pad";
            this.upDpadToolStripMenuItem.Click += new System.EventHandler(this.UpDpadToolStripMenuItem_Click);
            // 
            // downDpadToolStripMenuItem
            // 
            this.downDpadToolStripMenuItem.Name = "downDpadToolStripMenuItem";
            this.downDpadToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.downDpadToolStripMenuItem.Text = "Down-D-pad";
            this.downDpadToolStripMenuItem.Click += new System.EventHandler(this.DownDpadToolStripMenuItem_Click);
            // 
            // leftDpadToolStripMenuItem
            // 
            this.leftDpadToolStripMenuItem.Name = "leftDpadToolStripMenuItem";
            this.leftDpadToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.leftDpadToolStripMenuItem.Text = "Left-    D-pad";
            this.leftDpadToolStripMenuItem.Click += new System.EventHandler(this.LeftDpadToolStripMenuItem_Click);
            // 
            // rightDpadToolStripMenuItem
            // 
            this.rightDpadToolStripMenuItem.Name = "rightDpadToolStripMenuItem";
            this.rightDpadToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.rightDpadToolStripMenuItem.Text = "Right- D-pad";
            this.rightDpadToolStripMenuItem.Click += new System.EventHandler(this.RightDpadToolStripMenuItem_Click);
            // 
            // returnToTitleScreenToolStripMenuItem
            // 
            this.returnToTitleScreenToolStripMenuItem.Enabled = false;
            this.returnToTitleScreenToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.returnToTitleScreenToolStripMenuItem.Name = "returnToTitleScreenToolStripMenuItem";
            this.returnToTitleScreenToolStripMenuItem.Size = new System.Drawing.Size(131, 20);
            this.returnToTitleScreenToolStripMenuItem.Text = "Return to Title screen";
            this.returnToTitleScreenToolStripMenuItem.Visible = false;
            this.returnToTitleScreenToolStripMenuItem.Click += new System.EventHandler(this.returnToTitleScreenToolStripMenuItem_Click);
            // 
            // IPText
            // 
            this.IPText.ForeColor = System.Drawing.Color.Black;
            this.IPText.Location = new System.Drawing.Point(208, 54);
            this.IPText.Name = "IPText";
            this.IPText.Size = new System.Drawing.Size(100, 20);
            this.IPText.TabIndex = 1;
            this.IPText.Visible = false;
            this.IPText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPText_KeyPress);
            // 
            // GO
            // 
            this.GO.ForeColor = System.Drawing.Color.Black;
            this.GO.Location = new System.Drawing.Point(314, 54);
            this.GO.Name = "GO";
            this.GO.Size = new System.Drawing.Size(31, 23);
            this.GO.TabIndex = 2;
            this.GO.Text = "GO";
            this.GO.UseVisualStyleBackColor = true;
            this.GO.Visible = false;
            this.GO.Click += new System.EventHandler(this.GO_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox3.Location = new System.Drawing.Point(358, 130);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(202, 258);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox2.Location = new System.Drawing.Point(150, 130);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(202, 258);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Location = new System.Drawing.Point(-58, 130);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(202, 258);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox4.ErrorImage = null;
            this.pictureBox4.Location = new System.Drawing.Point(95, 80);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(310, 44);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 4;
            this.pictureBox4.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // clientNumText
            // 
            this.clientNumText.Location = new System.Drawing.Point(208, 54);
            this.clientNumText.Name = "clientNumText";
            this.clientNumText.Size = new System.Drawing.Size(100, 20);
            this.clientNumText.TabIndex = 5;
            this.clientNumText.Visible = false;
            this.clientNumText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.clientNumText_KeyPress);
            // 
            // GO1
            // 
            this.GO1.ForeColor = System.Drawing.Color.Black;
            this.GO1.Location = new System.Drawing.Point(314, 54);
            this.GO1.Name = "GO1";
            this.GO1.Size = new System.Drawing.Size(31, 23);
            this.GO1.TabIndex = 2;
            this.GO1.Text = "GO";
            this.GO1.UseVisualStyleBackColor = true;
            this.GO1.Visible = false;
            this.GO1.Click += new System.EventHandler(this.GO1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(205, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "ENTER SERVER IP:";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(205, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "ENTER CLIENT NUM:";
            this.label2.Visible = false;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(496, 441);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clientNumText);
            this.Controls.Add(this.GO1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IPText);
            this.Controls.Add(this.GO);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "UI";
            this.Text = "NESClient";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UI_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UI_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UI_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadOtherGamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectViaIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeAScreenShotToolStripMenuItem;
        private System.Windows.Forms.TextBox IPText;
        private System.Windows.Forms.Button GO;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controllerInputToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ToolStripMenuItem returnToTitleScreenToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem aButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upDpadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downDpadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftDpadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightDpadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectAsClientNToolStripMenuItem;
        private System.Windows.Forms.TextBox clientNumText;
        private System.Windows.Forms.Button GO1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

