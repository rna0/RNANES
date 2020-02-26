using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace RoyNES
{
    /// <summary>
    /// Construct the Ui of the server.
    /// </summary>
    class Ui : Form
    {
        // The buttons of the keyboard nes controller
        bool[] keyButtons = new bool[8] { false, false, false, false, false, false, false, false };


        public static Bitmap _frame;
        BitmapData _frameData;
        Console uploadConsole;

        Thread _nesThread;
        private Button ResetButton;
        private Button PowerButton;
        private PictureBox pictureBox1;
        Graphics g;
        Thread threadinput;
        bool PowerOn = false;
        bool showGameDetailes = false;
        bool isFirstThread = true;
        private Label label1;
        string DirPath = AppDomain.CurrentDomain.BaseDirectory;


        /// <summary>
        /// inithialize menu, buttons and varlables of the game
        /// </summary>
        public Ui()
        {
            
            InitializeComponent();
            pictureBox1.Image = new Bitmap(DirPath + "Data\\image1.png");
            PowerButton.BackgroundImage = new Bitmap(DirPath + "Data\\PowerButton.png");
            ResetButton.BackgroundImage = new Bitmap(DirPath + "Data\\ResetButton.png");


            Text = "RoyNES";
            Size = new Size(512, 480);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            // Set the MaximizeBox to false to remove the maximize box.
            MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            MinimizeBox = false;

            g = CreateGraphics();
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            
            InitMenus();

            //an instance of the virtual consol to exist and
            //one for the new thead a game opens
            uploadConsole = new Console
            {
                DrawAction = Draw
            };

            _frame = new Bitmap(Width / 2, Height / 2, PixelFormat.Format8bppIndexed);
            InitPalette();
            

            _nesThread = new Thread(new ThreadStart(StartNes))
            {
                IsBackground = true
            };
        }

        /// <summary>
        /// stops the game 
        /// </summary>
        void StopConsole()
        {
            uploadConsole.Stop = true;

            if (_nesThread.ThreadState == ThreadState.Running)
            {
                _nesThread.Join();
            }
        }

        /// <summary>
        /// start the game
        /// </summary>
        void StartConsole()
        {
            _nesThread = new Thread(new ThreadStart(StartNes))
            {
                IsBackground = true
            };
            _nesThread.Start();
        }

        /// <summary>
        /// load cartrage from server menu
        /// </summary>
        void LoadCartridge(object sender, EventArgs e)
        {
            StopConsole();

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "NES ROMs | *.nes",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (uploadConsole.LoadCartridge(openFileDialog.FileName))
                {
                    //Text = openFileDialog.SafeFileName;
                    StartConsole();
                }
                else
                {
                    MessageBox.Show("Could not load ROM, see standard output for details");
                }
            }
            showGameDetailes = true;
        }
        /// <summary>
        /// used in reset to reload cartrage
        /// </summary>
        void LoadCartridge()
        {
            StopConsole();
            Thread.Sleep(50);
            if (uploadConsole.LoadCartridge(DirPath + "game.nes"))
            {
                StartConsole();
            }
            else
            {
                MessageBox.Show("Could not load ROM, see standard output for details");
            }

            showGameDetailes = true;
        }

        /// <summary>
        /// same as exit menu
        /// </summary>
        void Exit_console(object sender, EventArgs e)
        {
            CloseWinApp();
        }

        /// <summary>
        /// initialize the menu bar and all on it
        /// </summary>
        void InitMenus()
        {
            MenuStrip ms = new MenuStrip();

            // File menu
            var fileMenu = new ToolStripMenuItem("File");

            var fileLoadMenu = new ToolStripMenuItem("Load ROM", null, new EventHandler(LoadCartridge));
            fileMenu.DropDownItems.Add(fileLoadMenu);

            var screenshotMenu = new ToolStripMenuItem("Take Screenshot", null, new EventHandler(TakeScreenshot));
            fileMenu.DropDownItems.Add(screenshotMenu);

            var cardDetailesMenu = new ToolStripMenuItem("Show Game Detailes", null, new EventHandler(ShowGameDetailes));
            fileMenu.DropDownItems.Add(cardDetailesMenu);
            var Exit = new ToolStripMenuItem("Exit", null, new EventHandler(Exit_console));
            fileMenu.DropDownItems.Add(Exit);

            ms.Items.Add(fileMenu);

            Controls.Add(ms);
        }

        /// <summary>
        /// save the game's frame screen as a photo.
        /// </summary>
        void TakeScreenshot(object sender, EventArgs e)
        {
            String filename = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\screenshot_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            System.Console.WriteLine(filename);
            lock (_frame)
                _frame.Save(filename);
        }

        /// <summary>
        /// show the stats the emulator opens
        /// </summary>
        void ShowGameDetailes(object sender, EventArgs e)
        {
            if (showGameDetailes)
                MessageBox.Show(uploadConsole.ToString() + uploadConsole.Cartridge.ToString());
        }
        /// <summary>
        /// call the start function
        /// </summary>
        void StartNes()
        {
            uploadConsole.Start();
        }

        /// <summary>
        /// address the pallettes one by one
        /// </summary>
        void InitPalette()
        {
            ColorPalette palette;
            lock (_frame)
                palette = _frame.Palette;
            palette.Entries[0x0] = Color.FromArgb(84, 84, 84);
            palette.Entries[0x1] = Color.FromArgb(0, 30, 116);
            palette.Entries[0x2] = Color.FromArgb(8, 16, 144);
            palette.Entries[0x3] = Color.FromArgb(48, 0, 136);
            palette.Entries[0x4] = Color.FromArgb(68, 0, 100);
            palette.Entries[0x5] = Color.FromArgb(92, 0, 48);
            palette.Entries[0x6] = Color.FromArgb(84, 4, 0);
            palette.Entries[0x7] = Color.FromArgb(60, 24, 0);
            palette.Entries[0x8] = Color.FromArgb(32, 42, 0);
            palette.Entries[0x9] = Color.FromArgb(8, 58, 0);
            palette.Entries[0xa] = Color.FromArgb(0, 64, 0);
            palette.Entries[0xb] = Color.FromArgb(0, 60, 0);
            palette.Entries[0xc] = Color.FromArgb(0, 50, 60);
            palette.Entries[0xd] = Color.FromArgb(0, 0, 0);
            palette.Entries[0xe] = Color.FromArgb(0, 0, 0);
            palette.Entries[0xf] = Color.FromArgb(0, 0, 0);
            palette.Entries[0x10] = Color.FromArgb(152, 150, 152);
            palette.Entries[0x11] = Color.FromArgb(8, 76, 196);
            palette.Entries[0x12] = Color.FromArgb(48, 50, 236);
            palette.Entries[0x13] = Color.FromArgb(92, 30, 228);
            palette.Entries[0x14] = Color.FromArgb(136, 20, 176);
            palette.Entries[0x15] = Color.FromArgb(160, 20, 100);
            palette.Entries[0x16] = Color.FromArgb(152, 34, 32);
            palette.Entries[0x17] = Color.FromArgb(120, 60, 0);
            palette.Entries[0x18] = Color.FromArgb(84, 90, 0);
            palette.Entries[0x19] = Color.FromArgb(40, 114, 0);
            palette.Entries[0x1a] = Color.FromArgb(8, 124, 0);
            palette.Entries[0x1b] = Color.FromArgb(0, 118, 40);
            palette.Entries[0x1c] = Color.FromArgb(0, 102, 120);
            palette.Entries[0x1d] = Color.FromArgb(0, 0, 0);
            palette.Entries[0x1e] = Color.FromArgb(0, 0, 0);
            palette.Entries[0x1f] = Color.FromArgb(0, 0, 0);
            palette.Entries[0x20] = Color.FromArgb(236, 238, 236);
            palette.Entries[0x21] = Color.FromArgb(76, 154, 236);
            palette.Entries[0x22] = Color.FromArgb(120, 124, 236);
            palette.Entries[0x23] = Color.FromArgb(176, 98, 236);
            palette.Entries[0x24] = Color.FromArgb(228, 84, 236);
            palette.Entries[0x25] = Color.FromArgb(236, 88, 180);
            palette.Entries[0x26] = Color.FromArgb(236, 106, 100);
            palette.Entries[0x27] = Color.FromArgb(212, 136, 32);
            palette.Entries[0x28] = Color.FromArgb(160, 170, 0);
            palette.Entries[0x29] = Color.FromArgb(116, 196, 0);
            palette.Entries[0x2a] = Color.FromArgb(76, 208, 32);
            palette.Entries[0x2b] = Color.FromArgb(56, 204, 108);
            palette.Entries[0x2c] = Color.FromArgb(56, 180, 204);
            palette.Entries[0x2d] = Color.FromArgb(60, 60, 60);
            palette.Entries[0x2e] = Color.FromArgb(0, 0, 0);
            palette.Entries[0x2f] = Color.FromArgb(0, 0, 0);
            palette.Entries[0x30] = Color.FromArgb(236, 238, 236);
            palette.Entries[0x31] = Color.FromArgb(168, 204, 236);
            palette.Entries[0x32] = Color.FromArgb(188, 188, 236);
            palette.Entries[0x33] = Color.FromArgb(212, 178, 236);
            palette.Entries[0x34] = Color.FromArgb(236, 174, 236);
            palette.Entries[0x35] = Color.FromArgb(236, 174, 212);
            palette.Entries[0x36] = Color.FromArgb(236, 180, 176);
            palette.Entries[0x37] = Color.FromArgb(228, 196, 144);
            palette.Entries[0x38] = Color.FromArgb(204, 210, 120);
            palette.Entries[0x39] = Color.FromArgb(180, 222, 120);
            palette.Entries[0x3a] = Color.FromArgb(168, 226, 144);
            palette.Entries[0x3b] = Color.FromArgb(152, 226, 180);
            palette.Entries[0x3c] = Color.FromArgb(160, 214, 228);
            palette.Entries[0x3d] = Color.FromArgb(160, 162, 160);
            palette.Entries[0x3e] = Color.FromArgb(0, 0, 0);
            palette.Entries[0x3f] = Color.FromArgb(0, 0, 0);

            lock (_frame)
                _frame.Palette = palette;
        }
        /// <summary>
        /// draw the pallettes
        /// </summary>
        unsafe void Draw(byte[] screen)
        {
            try
            {
                lock (_frame)
                _frameData = _frame.LockBits(new Rectangle(0, 0, 256, 240), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            byte* ptr = (byte*)_frameData.Scan0;
            for (int i = 0; i < 256 * 240; i++)
            {
                ptr[i] = screen[i];
            }

                lock (_frame)
                    _frame.UnlockBits(_frameData);
            }
            catch { return; }
            //lock(_frame)
            //    g.DrawImage(_frame, 0, 0, Size.Width, Size.Height);
        }

        /// <summary>
        /// the designer part
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ui));
            this.ResetButton = new System.Windows.Forms.Button();
            this.PowerButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ResetButton
            // 
            this.ResetButton.BackColor = System.Drawing.Color.Transparent;
            this.ResetButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ResetButton.Enabled = false;
            this.ResetButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.ResetButton.Location = new System.Drawing.Point(134, 265);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(51, 30);
            this.ResetButton.TabIndex = 5;
            this.ResetButton.UseVisualStyleBackColor = false;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // PowerButton
            // 
            this.PowerButton.BackColor = System.Drawing.Color.Transparent;
            this.PowerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PowerButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.PowerButton.Location = new System.Drawing.Point(84, 265);
            this.PowerButton.Name = "PowerButton";
            this.PowerButton.Size = new System.Drawing.Size(51, 30);
            this.PowerButton.TabIndex = 4;
            this.PowerButton.UseVisualStyleBackColor = false;
            this.PowerButton.Click += new System.EventHandler(this.PowerButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 115);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(496, 240);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP: ";
            // 
            // Ui
            // 
            this.ClientSize = new System.Drawing.Size(496, 441);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.PowerButton);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 100);
            this.Name = "Ui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ui_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        /// <summary>
        /// the real button on the nes image
        /// </summary>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            LoadCartridge();
        }
        /// <summary>
        /// the real button on the nes image
        /// </summary>
        private void PowerButton_Click(object sender, EventArgs e)
        {
            if (PowerOn)
            {
                pictureBox1.Image = new Bitmap(DirPath + "Data\\image1.png");
                //StopConsole();
                //StartConsole();
                System.Diagnostics.Process.Start(Application.ExecutablePath); // to start new instance of application
                CloseWinApp();
            }
            else
            {
                ResetButton.Enabled = true;
                threadinput = new Thread(SendImageToClient);
                threadinput.Start();
                pictureBox1.Image = new Bitmap(DirPath + "Data\\image2.png");
                label1.Text += GetIp();
            }
            PowerOn = !PowerOn;
        }
        /// <summary>
        /// define and connect every client
        /// </summary>
        void SendImageToClient()
        {
            // Set the TcpListener on port 13000.
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse(GetIp());

            // TcpListener server = new TcpListener(port);
            TcpListener server;

            List<Thread> myNewThread = new List<Thread>();

            System.Console.WriteLine("in find client");
            //send the bitmap to server as bytes
            try
            {
                while (true)
                {
                    server = new TcpListener(localAddr, port);
                    System.Console.WriteLine("listenning on port"+port);
                    // Start listening for client requests.
                    server.Start();
                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here
                    TcpClient client = server.AcceptTcpClient();

                    myNewThread.Add(new Thread(() => SendToClient(client, isFirstThread)));
                    myNewThread[port-13000].Start();
                    port++;
                }
            }
            catch (SocketException f)
            {
                System.Console.WriteLine("SocketException: {0}", f);
                CloseWinApp();
            }
        }
        /// <summary>
        /// send the image to the specific client
        /// </summary>
        void SendToClient(TcpClient client, bool sendInput)
        {
            isFirstThread = false;
            System.Console.WriteLine("in send to client");
            Image img = new Bitmap(DirPath + "Data\\ResetButton.png");
            if (sendInput)
                GetGame(client);


            // Get a stream object for reading and writingNetworkStream nwStream;
            ImageConverter converter = new ImageConverter();
            byte[] bytesToSend;

            try
            {
                NetworkStream nwStream;
                byte[] tempByteArr;
                while (true)
                {
                    lock (_frame)
                    {
                         bytesToSend = (byte[])converter.ConvertTo(_frame, typeof(byte[]));
                    }

                    //convert my image into bytes for sending
                    try
                    {
                        nwStream = client.GetStream();
                        nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                        bytesToSend = Encoding.ASCII.GetBytes("stop");
                        Thread.Sleep(50);
                        nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                        Thread.Sleep(50);
                        if (sendInput)
                        {
                            tempByteArr = new byte[client.ReceiveBufferSize];
                            nwStream.Read(tempByteArr, 0, client.ReceiveBufferSize);
                            lock (keyButtons)
                            {
                                keyButtons=ConvertByteToBoolArray(tempByteArr);
                                KeyHandle();
                            }
                        }
                    }
                    catch (Exception e1)
                    {
                        System.Console.WriteLine(e1);
                        break;
                    }

                }
                pictureBox1.Image = new Bitmap(DirPath + "Data\\image1.png");
                MessageBox.Show("client stopped without closing properly");
                return;
            }
            catch (SocketException f)
            {
                System.Console.WriteLine("SocketException: {0}", f);
            }
            finally
            {
                // Stop listening for new clients.
                System.Console.WriteLine("client closed");
                client.Close();
            }

        }

        /// <summary>
        /// send the current frame to the client, secured
        /// </summary>
        /// <param name="client">the connected client param</param>
        void GetGame(TcpClient client)
        {
            Byte[] bytes;
            List<Byte> allBytes = new List<Byte>();
            NetworkStream stream = client.GetStream();
            string msg;
            while (true)
            {
                bytes = new byte[1000];
                stream.Read(bytes, 0, 1000);
                msg = Encoding.ASCII.GetString(bytes);

                if (msg.StartsWith("stop"))
                {
                    if (File.Exists(DirPath + "game.nes"))
                    {
                        File.Delete(DirPath + "game.nes");
                    }
                    File.WriteAllBytes(DirPath + "game.nes", allBytes.ToArray());
                    allBytes.Clear();
                    break;
                }
                allBytes.AddRange(bytes);
            }
            LoadCartridge();
        }
        /// <summary>
        /// convert the boolean image to byte array to sent to client
        /// </summary>
        private static bool[] ConvertByteToBoolArray(byte[] b)
        {
            // prepare the return result
            bool[] result = new bool[8];
            // check each bit in the byte. if 1 set to true, if 0 set to false
            for (int i = 0; i < result.Length; i++)
                result[i] = b[i] == 0 ? false : true;
            return result;
        }
        /// <summary>
        /// get the ip from lan, no need for internet
        /// </summary>
        private string GetIp()
        {
            //gets ip via internet
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return localIP;
        }

        /// <summary>
        /// close event
        /// </summary>
        private void Ui_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseWinApp();
        }
        /// <summary>
        /// the close event, cloase all threads and then himself
        /// </summary>
        void CloseWinApp()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            if (Application.MessageLoop)
            {
                // WinForms app
                Application.Exit();
            }
            else
            {
                // Console app
                Environment.Exit(1);
            }

        }
        /// <summary>
        /// the controllers client send are transfered here
        /// </summary>
        void KeyHandle()
        {
            //in order: a b select start up down left right
            Controller.Button key_pressed = Controller.Button.A;
            for (int i = 0; i < keyButtons.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        key_pressed = Controller.Button.A;
                        break;
                    case 1:
                        key_pressed = Controller.Button.B;
                        break;
                    case 2:
                        key_pressed = Controller.Button.Select;
                        break;
                    case 3:
                        key_pressed = Controller.Button.Start;
                        break;
                    case 4:
                        key_pressed = Controller.Button.Up;
                        break;
                    case 5:
                        key_pressed = Controller.Button.Down;
                        break;
                    case 6:
                        key_pressed = Controller.Button.Left;
                        break;
                    case 7:
                        key_pressed = Controller.Button.Right;
                        break;
                }
                uploadConsole.Controller.setButtonState(key_pressed, keyButtons[i] ? true : false);
            }
        }
    }
}
