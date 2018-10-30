using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        
        }
        static void ExeReader(string filename)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(filename));
            string str = null;

            for (int i = 0; i < 100; i++) {


                br.BaseStream.Position = i;
                str += br.ReadByte().ToString("X2");
            }
            /*
            var bytes = File.ReadAllBytes(filename);

            string str = bytes.Aggregate(new StringBuilder(), (sb, v) => sb.AppendFormat("{0:X2} ", v)).ToString();
            return str;*/
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //List<string> items = new List<string>();
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {

                BinaryReader br = new BinaryReader(File.OpenRead(openFileDlg.FileName));
                string strn = null;

                for (int i = 0; i < 100; i++)
                {
                    br.BaseStream.Position = i;
                    strn = br.ReadByte().ToString("X2");
                   // items.Add(strn);
                    //lbTodoList.ItemsSource = items;
                    textBox2.Text += strn;
                }
                
            }
            textBox1.Text = openFileDlg.FileName;

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string chk = textBox3.Text;
           // byte[] bytes = Encoding.ASCII.GetBytes(chk);

            BinaryReader br = new BinaryReader(File.OpenRead(textBox1.Text));
            
            byte[] brr = new byte[100];

            for (int i = 0; i < 100; i++)
            {
                br.BaseStream.Position = i;
                brr[i] = br.ReadByte();
            }

            string result = System.Text.Encoding.UTF8.GetString(brr);

            if (result.Contains(chk))
            {

                textBox4.Text = "Founud";
            }
            else {
                textBox4.Text = "Not Founud";
            }

            
        }

    }
}
