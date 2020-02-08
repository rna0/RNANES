using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;


using System.Windows.Forms;

namespace NEServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //send the bitmap to server as bytes
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                int port = 13000;
                IPAddress localAddr = IPAddress.Parse(GetIp());//"127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                byte[] bytes = new byte[8192];
                this.pictureBox1.Image = new Bitmap(@"E:\FINALS\RoyNES  working one controller\WindowsFormsApp0\bin\x86\Debug\image1.png");
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");


                    // Get a stream object for reading and writing
                    NetworkStream nwStream = client.GetStream();
                    byte[] bytesToSend = ImageToByte();//ASCIIEncoding.ASCII.GetBytes(textToSend);


                    try
                    {
                        nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                    }
                    catch (SocketException e1)
                    {
                        Console.WriteLine("SocketException: " + e1);
                    }
                    Console.WriteLine("image sent");


                    client.Close();

                    // Shutdown and end connection
                }
            }
            catch (SocketException f)
            {
                Console.WriteLine("SocketException: {0}", f);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

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
        public static byte[] ImageToByte(Image img = null)
        {
            //convert my image into bytes for sending
            Image imag = new Bitmap(@"C:\Users\royar\Pictures\1.jpg");
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(imag, typeof(byte[]));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
