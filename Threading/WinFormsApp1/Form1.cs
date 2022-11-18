using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        TcpClient client;
        StreamReader reader ;
        StreamWriter writer ;
       static ListBox list;
        public Form1()
        {
            InitializeComponent();
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            client.Connect("127.0.0.1", 8888);
            reader = new(client.GetStream());
            writer = new(client.GetStream());
            string name = "Admin";
            await writer.WriteLineAsync(name);
            await writer.FlushAsync();
            Task.Run(But);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new();
            list = listBox1;
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
         Thread test =new Thread(() => {
                string message = textBox.Text;
                writer.WriteLine(message);
                writer.Flush();
            });
            test.Start();
            test.Join();
        }
        

        private void But()
        {
            object locker = new();
            while (true)
            {
                lock (locker)
                {
                    string message = reader.ReadLine();
                    if (!string.IsNullOrEmpty(message)) list.Items.Add(message);
                }
            }
        }

    }
}
