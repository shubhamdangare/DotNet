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
using System.Security.Cryptography;

namespace Dupicatefiledector
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string fileTofind = textBox1.Text; // @"C:\Users\sdangare\Desktop\FinalDapi\New folder\demo.txt";



            string path = textBox1.Text; //@"C: \Users\sdangare\Desktop\FinalDapi\New folder";

            HashSet<string> names = new HashSet<string>();
            string str3 = null;


            DirectoryInfo d = new DirectoryInfo(path); //@"C:\Users\sdangare\Desktop\FinalDapi\New folder");
            FileInfo[] Files = d.GetFiles("*.txt");
            FileInfo[] Files2 = d.GetFiles("*.txt");
            string str = path;
            string str2 = path;
            foreach (FileInfo file in Files)
            {
                str = str + "\\" + file.Name;

                foreach (FileInfo files in Files2)
                {
                    str2 = str2 + "\\" + files.Name;
                    if (str.Equals(str2))
                    {
                        str2 = path;
                        continue;
                    }
                    var dublicates = CalcDuplicates(new[] { str2, str });
                    foreach (var group in dublicates)
                    {
                        foreach (var file5 in group)
                        {
                            str3 = str3 + file5;
                        }
                        names.Add(str3);
                        str3 = null;
                    }

                    str2 = path;
                }
                str = path;
            }

            foreach (var fname in names)
            {

                textBox2.Text =  textBox2.Text + "\n" + fname;
                //Console.WriteLine(fname);

            }

        }


        public static IEnumerable<string> CalcDuplicates(IEnumerable<string> fileNames)
        {
            return fileNames.GroupBy(CalcMd5OfFile)
                            .Where(g => g.Count() > 1)
                            .SelectMany(g => g);
        }

        private static string CalcMd5OfFile(string path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }

    }


   
}
