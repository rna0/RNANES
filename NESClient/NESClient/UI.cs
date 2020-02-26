using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using SlimDX.DirectInput;

namespace NESClient
{
    public partial class UI : Form
    {
        /// <summary>
        /// the executable directory's path
        /// </summary>
        string DirPath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// all games images on client
        /// </summary>
        string[] imageArray;
        /// <summary>
        /// the current game showing on screen
        /// </summary>
        int currentImageIndex =1;
        /// <summary>
        /// the game chosen
        /// </summary>
        string gamePath = null;
        /// <summary>
        /// the bytes that are sent at the moment
        /// </summary>
        static List<Byte> allBytes = new List<Byte>();
        /// <summary>
        /// he bytes that are sent, the whole image
        /// </summary>
        static List<byte[]> message = new List<byte[]>();
        /// <summary>
        /// the current photo on screen
        /// </summary>
        public static Image img;
        /// <summary>
        /// the graphics on which the game is projected
        /// </summary>
        Graphics g;
        /// <summary>
        /// safe exit socket
        /// </summary>
        bool StopCommunication = false;
        /// <summary>
        /// safe close all
        /// </summary>
        bool xButtonEvent = false;
        /// <summary>
        /// true if firt game, otherwise false
        /// </summary>
        bool firstGame = true;
        /// <summary>
        /// the last key pressed
        /// </summary>
        Keys currentKey = Keys.Z;
        /// <summary>
        /// the last key pressed
        /// </summary>
        Keys aButton = Keys.Z;
        /// <summary>
        /// the last key pressed
        /// </summary>
        Keys bButton = Keys.X;
        /// <summary>
        /// the last key pressed
        /// </summary>
        Keys selectButton = Keys.W;
        /// <summary>
        /// the last key pressed
        /// </summary>
        Keys startButton = Keys.Q;
        /// <summary>
        /// the last key pressed
        /// </summary>
        Keys upDpad = Keys.Up;
        /// <summary>
        /// the last key pressed
        /// </summary>
        Keys downDpad = Keys.Down;
        /// <summary>
        /// the last key pressed
        /// </summary>
        Keys leftDpad = Keys.Left;
        /// <summary>
        /// the last key pressed
        /// </summary>
        Keys rightDpad = Keys.Right;
        /// <summary>
        /// the last key pressed
        /// </summary>
        int clientNum = 1;
        /// <summary>
        /// the last key pressed
        /// </summary>
        bool ControllerConnected = false;
        /// <summary>
        /// The buttons of the real nes controller
        /// </summary>
        public static bool[] inputButtons = new bool[8] { false, false, false, false, false, false, false, false };
        /// <summary>
        /// The buttons of the keyboard emulating nes controller
        /// </summary>
        public static bool[] keyboardinputButtons = new bool[8] { false, false, false, false, false, false, false, false };

        // setting up values needed for activation
        // setting up values needed for activation
        /// <summary>
        /// used from directX sdk - slimDX
        /// </summary>
        DirectInput Input = new DirectInput();
        /// <summary>
        /// current joystick
        /// </summary>
        Joystick stick;
        /// <summary>
        /// joystick array, all the joystickes that are connected
        /// </summary>
        Joystick[] Sticks;
        /// <summary>
        /// Thumstick variable y (+100 or -100 or 0)
        /// </summary>
        int yValue = 0;
        /// <summary>
        /// Thumstick variable x (+100 or -100 or 0)
        /// </summary>
        int xValue = 0;
        /// <summary>
        /// The buttons of the real nes controller
        /// </summary>
        bool[] buttons;

        /// <summary>
        /// Initializes the Components of the client Ui
        /// </summary>
        public UI()
        {
            InitializeComponent();
            //HelpToolStripMenuItem_Click(new object(), new EventArgs());
            FormBorderStyle = FormBorderStyle.FixedSingle;
            // Set the MaximizeBox to false to remove the maximize box.
            MaximizeBox = false;

            img = new Bitmap(DirPath + "Data\\null.png");
            pictureBox4.Image = new Bitmap(DirPath + "Data\\select.png");
            if (Directory.Exists(DirPath + "games\\"))
            {
                imageArray = Directory.GetFiles(DirPath + "games\\", "*.png");
            }
            else
            {
                MessageBox.Show("The path of " + DirPath + "games\\" + " is not valid! please make it and try again");
                CloseWinApp();
            }
            InitializeImages();
            g = CreateGraphics();
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            
            // joystick start thread for each controller
            Sticks = GetSticks();
            if (Sticks.Length >= 1)
            {
                timer1.Enabled = true;
                timer1.Interval = 1;

                new Thread(Gamepad_handle).Start();
            }

            CenterToScreen();
        }

        /// <summary>
        /// Initializes the images thowing after every click
        /// </summary>
        private void InitializeImages()
        {
                pictureBox1.Image = new Bitmap(imageArray[currentImageIndex> 0? currentImageIndex - 1:imageArray.Length-1]);
                pictureBox2.Image = new Bitmap(imageArray[currentImageIndex]);
                pictureBox3.Image = new Bitmap(imageArray[currentImageIndex < imageArray.Length - 1 ? currentImageIndex + 1 : 0]);
        }
        /// <summary>
        /// Initializes the picture in pictureBox3
        /// </summary>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (currentImageIndex < imageArray.Length - 1)
            {
                currentImageIndex++;
            }
            else
            {
                currentImageIndex = 0;
            }
            InitializeImages();
        }
        /// <summary>
        /// Initializes the picture in pictureBox2
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            gamePath = imageArray[currentImageIndex];
            gamePath = gamePath.Substring(0, gamePath.Length - 3) + "nes";
            if (File.Exists(gamePath))
            {
                pictureBox2.Image = new Bitmap(DirPath + "Data\\approved.png");
            }
            else
            {
                Console.WriteLine(gamePath);
                pictureBox2.Image = new Bitmap(DirPath + "Data\\reject.png");
                gamePath = null;
            }
            if (!firstGame)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
            }

        }

        /// <summary>
        /// Initializes the picture in pictureBox1
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (currentImageIndex > 0)
            {
                currentImageIndex--;
            }
            else
            {
                currentImageIndex = imageArray.Length-1;
            }
            InitializeImages();
        }
        /// <summary>
        /// need help? ask this function
        /// </summary>
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Emulator was made by roy arama and is \n" +
                            "completly free for anyone who wishes to play\n\n" +
                            "How to use:\nNOTE: if you only want to watch skip to step 3\n\n" +
                            "1. If you want a proper menu experience make\n" +
                            "   sure to place your nes games at a folder\n" +
                            "   named games in your client executable path.\n" +
                            "NOTE: you can open games that are not on\n" +
                            "your path by the 'Load other Games' button\n" +
                            "on the 'File' menu.\n\n" +
                            "2. If on menu press on the left and right\n" +
                            "   pictures of your games to move between your\n" +
                            "   games and press the middle one to start it.\n" +
                            "3. If you wish to change the buttons of the\n" +
                            "   keyboard for nes controller input simply type\n" +
                            "   it and click the button on the 'Controller input'\n" +
                            "   menu you wish to change it to.\n" +
                            "NOTE: the default controles are:\n" +
                            "   A   B   SELECT  START   UP  DOWN    LEFT    RIGHT\n" +
                            "   Z   X   W           Q          UP  DOWN    LEFT    RIGHT\n" +
                            "4. Press the 'Connect As Client N' button on the\n" +
                            "   'File' menu to choose the number of client you\n" +
                            "   are to connect to the emulator.\n" +
                            "NOTE: if you are the first client you don't have to\n" +
                            "type your number\n" +
                            "5. Press the 'Connect via IP' button on the menu\n" +
                            "   to enter the Ip of the server's computer.\n" +
                            "NOTE: if you are on the same computer as the server\n" +
                            "your ip will fill automaticly if you keep it blank.\n" +
                            "also be sure you the red button on the server's\n" +
                            "console you are entering is red\n" +
                            "6. Enjoy the game you chose as you can Explore\n" +
                            "   the other settings by yourself!");

        }
        /// <summary>
        /// open the game the client chose from his arcive
        /// </summary>
        private void loadOtherGamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if client has other games the server gets the game via tcp ind loads it
            
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "NES ROMs | *.nes",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                gamePath = openFileDialog.SafeFileName;
                pictureBox2.Image = new Bitmap(DirPath + "Data\\approved.png");
            }
            else
            {
                MessageBox.Show("invalid game path!");
            }
        }

        /// <summary>
        /// if clicked, you can see the text entry
        /// </summary>
        private void connectViaIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //enables new connection
            IPText.Visible = !IPText.Visible;
            GO.Visible = !GO.Visible;
            label1.Visible = !label1.Visible;
        }
        /// <summary>
        /// just gets ip from lan, even without wifi!
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
            IPText.Text = localIP;
            return localIP;
        }
        /// <summary>
        /// only ip values permitted
        /// </summary>
        private void IPText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// only numbers permited
        /// </summary>
        private void clientNumText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// if clicked text wont be visible
        /// </summary>
        private void GO1_Click(object sender, EventArgs e)
        {
            clientNumText.Visible = false;
            GO1.Visible = false;
            label2.Visible = false;
            clientNum = int.Parse(clientNumText.Text);
        }
        /// <summary>
        /// connect to server if possible
        /// </summary>
        private void GO_Click(object sender, EventArgs e)
        {
            IPText.Visible = false;
            GO.Visible = false;
            label1.Visible = false;

            if (gamePath is null && clientNum == 1)
            {
                pictureBox4.Image = new Bitmap(DirPath + "Data\\noGame.PNG");
                return;
            }
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            StopCommunication = true;
            //ip and port
            string SERVER_IP = IPText.Text == "" ? GetIp() : IPText.Text;

            int PORT_NO = 13000 + clientNum - 1;

            //---create a TCPClient object at the IP and port no.---
            TcpClient client = null;
            try
            {
                client = new TcpClient(SERVER_IP, PORT_NO);
            }
            catch (SocketException)
            {
                Console.WriteLine("socket is not available, is not real or has no NEServer");
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
                pictureBox4.Image = new Bitmap(DirPath + "Data\\error.png");
                return;
            }
            firstGame = false;
            IPText.ReadOnly = true;
            GO.Enabled = false;
            label1.Visible = false;
            returnToTitleScreenToolStripMenuItem.Visible = true;
            returnToTitleScreenToolStripMenuItem.Enabled = true;
            //client get image from server
            if (!(client is null))
            {
                Thread myNewThread = new Thread(() => SentFromServer(client));
                myNewThread.Start();
            }
        }


        /// <summary>
        /// get images from server
        /// </summary>
        private void SentFromServer(TcpClient client)
        {
            StopCommunication = false;
            Byte[] bytes;

            if (clientNum == 1)
            {
                SendGame(client);
            }

            NetworkStream stream;
            Thread getImageThread = new Thread(() => formImage());
            getImageThread.Start();
            while (!StopCommunication)
            {
                if (xButtonEvent)
                {
                    break;
                }
                stream = client.GetStream();
                try
                {
                    bytes = new byte[client.ReceiveBufferSize];
                    stream.Read(bytes, 0, client.ReceiveBufferSize);
                    if (clientNum == 1)
                    {
                        Thread.Sleep(50);
                        if (ControllerConnected)
                            stream.Write(ConvertBoolArrayToByte(inputButtons), 0, inputButtons.Length);
                        else
                            stream.Write(ConvertBoolArrayToByte(keyboardinputButtons), 0, keyboardinputButtons.Length);

                    }
                }
                catch
                {
                    MessageBox.Show("server stopped without closing properly");
                    message.Clear();
                    client.Close();
                    CloseWinApp();
                    return;
                }
                message.Add(bytes);
            }
            client.Close();
            return;
        }

        /// <summary>
        /// send the chosen game to the server
        /// </summary>
        private void SendGame(TcpClient client)
        {
            NetworkStream stream;
            stream = client.GetStream();
            byte[] bytesToSend = File.ReadAllBytes(gamePath);
            stream.Write(bytesToSend, 0, bytesToSend.Length);
            bytesToSend = Encoding.ASCII.GetBytes("stop");
            Thread.Sleep(70);
            stream.Write(bytesToSend, 0, bytesToSend.Length);
        }
        /// <summary>
        /// used for game controlles
        /// </summary>
        private static byte[] ConvertBoolArrayToByte(bool[] source)
        {
            byte []result = new byte [8];
            byte temp = 0;
            // This assumes the array never contains more than 8 elements!
            int index = result.Length - source.Length;

            // Loop through the array
            foreach (bool b in source)
            {
                // if the element is 'true' set the bit at that position
                if (b)
                    temp |= (byte)(1 << (7 - index));

                index++;
            }
            // check each bit in the byte. if 1 set to true, if 0 set to false
            for (int i = 0; i < 8; i++)
                result[i] = (temp & (1 << i)) == 0 ? (byte)0 : (byte)1;

            // reverse the array
            Array.Reverse(result);

            return result;
        }
        /// <summary>
        /// after getting the image make it visible to the user on the screen
        /// </summary>
        private void formImage()
        {
            byte[] bytes;
            string msg;
            while (true)
            {
                if (xButtonEvent)
                {
                    return;
                }
                if (message.Count > 0)
                {
                    bytes = message[0];
                    if (!(bytes is null))
                    {
                        msg = Encoding.ASCII.GetString(bytes);
                        if (msg.StartsWith("stop") && allBytes.Count != 0)
                        {
                            img = Image.FromStream(new MemoryStream(allBytes.ToArray()));
                            g.DrawImage(img, 0, 0 + 23, Size.Width - 16, Size.Height - 63);
                            allBytes.Clear();
                        }
                        else
                        {
                            if (StopCommunication)
                            {
                                break;
                            }
                            try
                            {
                                allBytes.AddRange(bytes);
                            } catch {  }
                        }
                        if (StopCommunication)
                            return;
                        message.RemoveAt(0);
                    }
                }
            }
        }


        /// <summary>
        /// enables view of textbox in context
        /// </summary>
        private void connectAsClientNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clientNumText.Visible = !clientNumText.Visible;
            GO1.Visible = !GO1.Visible;
            label2.Visible = !label2.Visible;
        }
        /// <summary>
        /// does the same as pressing the x in the menu
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopCommunication = true;
            CloseWinApp();
        }

        /// <summary>
        /// saves the games current image as screenshot at the 'my pictures' folder
        /// </summary>
        private void takeAScreenShotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String filename = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\screenshot_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            System.Console.WriteLine(filename);
            if(img!=null)
                img.Save(filename);
        }

        /// <summary>
        /// if the x button is pressed form a complete shutdown
        /// </summary>
        private void UI_FormClosed(object sender, FormClosedEventArgs e)
        {
            xButtonEvent = true;
            CloseWinApp();
        }

        /// <summary>
        /// if someone decides to look at the other games available he can use this.
        /// </summary>
        private void returnToTitleScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            returnToTitleScreenToolStripMenuItem.Visible = false;
            returnToTitleScreenToolStripMenuItem.Enabled = false;

            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
        }

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

        //############################################         nes keyboard segment          ############################################################


        /// <summary>
        /// if user press a button changed to last pressed button
        /// </summary>
        private void AButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aButton = currentKey;
        }

        /// <summary>
        /// if user press b button changed to last pressed button
        /// </summary>
        private void BButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bButton = currentKey;
        }

        /// <summary>
        /// if user press select button changed to last pressed button
        /// </summary>
        private void SelectButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectButton = currentKey;
        }

        /// <summary>
        /// if user press start button changed to last pressed button
        /// </summary>
        private void StartButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startButton = currentKey;
        }

        /// <summary>
        /// if user press up button changed to last pressed button
        /// </summary>
        private void UpDpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            upDpad = currentKey;
        }

        /// <summary>
        /// if user press down button changed to last pressed button
        /// </summary>
        private void DownDpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            downDpad = currentKey;
        }

        /// <summary>
        /// if user press left button changed to last pressed button
        /// </summary>
        private void LeftDpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftDpad = currentKey;
        }

        /// <summary>
        /// if user press right button changed to last pressed button
        /// </summary>
        private void RightDpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightDpad = currentKey;
        }

        /// <summary>
        /// if user press any button act accordingly
        /// </summary>
        private void UI_KeyDown(object sender, KeyEventArgs e)
        {
            SetControllerButton(true, e);
            currentKey = e.KeyCode;
            e.Handled = e.SuppressKeyPress = true;
        }

        /// <summary>
        /// if user press any button act accordingly
        /// </summary>
        private void UI_KeyUp(object sender, KeyEventArgs e)
        {
            SetControllerButton(false, e);
            e.Handled = e.SuppressKeyPress = true;
        }

        /// <summary>
        /// after every frame sent the client sends the buttons current state whitch is changed here
        /// </summary>
        void SetControllerButton(bool state, KeyEventArgs e)
        {
            if (e.KeyCode == aButton)
            {
                keyboardinputButtons[0] = state;
            }
            if (e.KeyCode == bButton)
            {
                keyboardinputButtons[1] = state;
            }
            if (e.KeyCode == selectButton)
            {
                keyboardinputButtons[2] = state;
            }
            if (e.KeyCode == startButton)
            {
                keyboardinputButtons[3] = state;
            }
            if (e.KeyCode == upDpad)
            {
                keyboardinputButtons[4] = state;
            }
            if (e.KeyCode == downDpad)
            {
                keyboardinputButtons[5] = state;
            }
            if (e.KeyCode == leftDpad)
            {
                keyboardinputButtons[6] = state;
            }
            if (e.KeyCode == rightDpad)
            {
                keyboardinputButtons[7] = state;
            }
        }

        //############################################         nes joystick segment          ############################################################


        /// <summary>
        /// send controller button state
        /// </summary>
        void StickHandlingLogic(Joystick stick, int id)
        {
            // Creates an object from the class JoystickState.
            JoystickState state = new JoystickState();
            //Gets the state of the joystick
            state = stick.GetCurrentState();
            //These are for the thumbstick readings
            yValue = -state.Y;
            xValue = state.X;
            // Stores the number of each button on the gamepad into the bool[] butons.
            buttons = state.GetButtons();

            //Button B 0 Button A 1 Button Select 2 Button Start 3 Button Up 4 Button Down 5 Button Right 6 Button Left 7
            // This is when button i of the gamepad is pressed, the label will change. Button i should be the square button.

            inputButtons[0] = buttons[1] | keyboardinputButtons[0];//a
            inputButtons[1] = buttons[0] | keyboardinputButtons[1];//b
            inputButtons[2] = buttons[8] | keyboardinputButtons[2];//select
            inputButtons[3] = buttons[9] | keyboardinputButtons[3];//start

            inputButtons[4] = yValue == +100 | keyboardinputButtons[4];//up
            inputButtons[5] = yValue == -100 | keyboardinputButtons[5];//down
            inputButtons[6] = xValue == -100 | keyboardinputButtons[6];//left
            inputButtons[7] = xValue == +100 | keyboardinputButtons[7];//right
        }

        /// <summary>
        /// always listen to the game port
        /// </summary>
        public void Gamepad_handle()
        {
            ControllerConnected = true;
            //called only by thread
            while (true)
            {
                StickHandlingLogic(Sticks[0], 0);
                Thread.Sleep(20);
            }
        }
        /// <summary>
        /// check if more than one stick is implemented
        /// </summary>
        public Joystick[] GetSticks()
        {

            List<Joystick> sticks = new List<Joystick>(); // Creates the list of joysticks connected to the computer via USB.

            foreach (DeviceInstance device in Input.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                // Creates a joystick for each game device in USB Ports
                try
                {
                    stick = new Joystick(Input, device.InstanceGuid);
                    stick.Acquire();

                    // Gets the joysticks properties and sets the range for them.
                    foreach (DeviceObjectInstance deviceObject in stick.GetObjects())
                    {
                        if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-100, 100);
                    }

                    // Adds how ever many joysticks are connected to the computer into the sticks list.
                    sticks.Add(stick);
                }
                catch (DirectInputException)
                {
                }
            }
            return sticks.ToArray();
        }


        /// <summary>
        /// Creates the StickHandlingLogic Method which takes all the joysticks in the sticks List and puts them into a timer.
        /// </summary>
        public void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < Sticks.Length; i++)
            {
                StickHandlingLogic(Sticks[i], i);
            }
        }

    }
}
