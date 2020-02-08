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
        }

        private string GetIp()
        {
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
            Image imag = new Bitmap(@"D:\Photos\MeshulamPro.jpg");
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(imag, typeof(byte[]));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
                TcpListener server = null;
                try
                {
                    // Set the TcpListener on port 13000.
                    Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse(GetIp());//"127.0.0.1");

                    // TcpListener server = new TcpListener(port);
                    server = new TcpListener(localAddr, port);

                    // Start listening for client requests.
                    server.Start();

                    // Buffer for reading data
                    Byte[] bytes = new Byte[8192];
                    String data = null;

                //---data to send to the server---
                string textToSend = "hello world"; //DateTime.Now.ToString();
                                                   // Enter the listening loop.
                while (true)
                    {
                        Console.Write("Waiting for a connection... ");

                        // Perform a blocking call to accept requests.
                        // You could also user server.AcceptSocket() here.
                        TcpClient client = server.AcceptTcpClient();
                        Console.WriteLine("Connected!");

                        data = null;

                        // Get a stream object for reading and writing
                        NetworkStream nwStream = client.GetStream();
                    byte[] bytesToSend = ImageToByte();//ASCIIEncoding.ASCII.GetBytes(textToSend);

                    //---send the text---
                    Console.WriteLine("Sending : " + textToSend);
                    nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                    //---read back the text---
                    byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                    //int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                    //Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
                    Console.ReadLine();



                    // Shutdown and end connection
                    client.Close();
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
    }
}
