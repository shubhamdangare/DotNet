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
using System.Reflection;
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
        static string ReadFileAsHexString(string filename)
        {
          
            var bytes = File.ReadAllBytes(filename);

            string str = bytes.Aggregate(new StringBuilder(), (sb, v) => sb.AppendFormat("{0:X2} ", v)).ToString();
            return str;

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //List<string> items = new List<string>();
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            
            Nullable<bool> result = openFileDlg.ShowDialog();
            textBox1.Text = openFileDlg.FileName;
            if (result == true)
            {


                String HexString = ReadFileAsHexString(openFileDlg.FileName);

                string Mz = "4D 5A";
                string SectionHeader = "50 45 00 00 4C 01 03 00";
                string EntryPointRVA = "FF 25";
                string w_directory = Directory.GetCurrentDirectory();
                DateTime c3 = File.GetLastWriteTime(System.IO.Path.Combine(w_directory, openFileDlg.FileName));
                if (DateTime.Now < c3)
                {
                    label1.Content = "Somthing Is Wrong";
                    
                }
                if (HexString.Contains(Mz) && HexString.Contains(SectionHeader) && HexString.Contains(EntryPointRVA))
                {
                    label1.Content = "Everything Seems Fine";

                }
                else
                {
                    label1.Content = "Malicious file";
                }
                
            }
            
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
        

    }
}
