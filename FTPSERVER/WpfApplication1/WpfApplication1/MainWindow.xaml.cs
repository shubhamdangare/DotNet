using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpClient tcpclnt = null;
        Stream stm = null;
        Int32 MyPort = 0;
        string MyIP = null;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                MyIP = GetLocalIPAddress();
                MyPort = 21000;
                tcpclnt = new TcpClient();
                tcpclnt.Connect(MyIP, MyPort);
                IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections().Where(x => x.LocalEndPoint.Equals(tcpclnt.Client.LocalEndPoint) && x.RemoteEndPoint.Equals(tcpclnt.Client.RemoteEndPoint)).ToArray();
                if (tcpConnections != null && tcpConnections.Length > 0)
                {
                    TcpState stateOfConnection = tcpConnections.First().State;
                    if (stateOfConnection == TcpState.Established)
                    {
                        button.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        button.Visibility = Visibility.Hidden;
                    }

                }
            }
            catch (Exception e) { }
        }

        bool SocketConnected(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }

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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string str;
            try
            {
                str = textBox.Text;
                string verbatim = string.Format(@"{0}", str);
                stm = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(verbatim);
                stm.Write(ba, 0, ba.Length);
                byte[] bb = new byte[1024];
                int k = stm.Read(bb, 0, 1024);
                char[] brr = new char[k];
                for (int i = 0; i < k; i++)
                {
                    brr[i] = Convert.ToChar(bb[i]);
                }
                str = new string(brr);

                textBox1.Text = str;
            }
            catch(Exception eo) { }

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            

        }
    }
}
