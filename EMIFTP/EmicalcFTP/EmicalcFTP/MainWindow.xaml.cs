using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmicalcFTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double LoanAm = 0.0;
        double Duration = 0.0;

        TcpClient tcpclnt = null;
        Stream stm = null;
        Int32 MyPort = 0;
        string MyIP = null;
        string str;
        public static string GetLocalIPAddress()
        {
            
            var Myhost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in Myhost.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("My Web :No network adapters with an IPv4 address in thesystem!");
        }
        public MainWindow()
        {
            InitializeComponent();

            MyIP = GetLocalIPAddress();
            MyPort = 21000;
            tcpclnt = new TcpClient();
            tcpclnt.Connect(MyIP, MyPort);
            str = @"D:\demo.xml";
            stm = tcpclnt.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(str);
            stm.Write(ba, 0, ba.Length);
            byte[] bb = new byte[1024];
            int k = stm.Read(bb, 0, 100);
            char[] CROI = new char[k];
            for (int i = 0; i < k; i++)
            {
                CROI[i]= Convert.ToChar(bb[i]);
            }
            str = new string(CROI);
            textBox2.Text = str;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            double roi = Convert.ToDouble(str);

            label3.Content = LoanAm * roi * Math.Pow(1 + roi, Duration) / (Math.Pow(1 + roi, Duration) - 1);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoanAm = Convert.ToDouble(textBox.Text);
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Duration = Convert.ToDouble(textBox1.Text);
        }
    }
}
