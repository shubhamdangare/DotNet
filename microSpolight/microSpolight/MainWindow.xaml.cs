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
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace microSpolight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string str;
        static string Box;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }


        public static void findFill(string name)
        {
            DirectoryInfo diTop = new DirectoryInfo(name);
            try
            {
                foreach (var fi in diTop.EnumerateFiles())
                {
                    try
                    {

                        if (fi.Name.Equals(str))
                        {
                            Box += fi.FullName;
                            Box += "\n";
                        }
                    }
                    catch (UnauthorizedAccessException UnAuthTop)
                    {
                        Box +=  UnAuthTop.Message ;
                        Box += "\n";
                    }
                    catch (Exception e) { }
                }

                foreach (var di in diTop.EnumerateDirectories("*"))
                {
                    try
                    {
                        foreach (var fi in di.EnumerateFiles("*", SearchOption.AllDirectories))
                        {
                            try
                            {

                                if (fi.Name.Equals(str))
                                {
                                    Box += fi.FullName;
                                    Box += "\n";
                                   
                                }
                            }
                            catch (UnauthorizedAccessException UnAuthFile)
                            {
                                Box += UnAuthFile.Message;
                                 Box += "\n";
                            }
                        }
                    }
                    catch (UnauthorizedAccessException UnAuthSubDir)
                    {
                        Box += UnAuthSubDir.Message;
                        Box += "\n";
                    }
                }
            }
            catch (DirectoryNotFoundException DirNotFound)
            {
                Box += DirNotFound.Message;
                Box += "\n";
            }
            catch (UnauthorizedAccessException UnAuthDir)
            {
                Box += UnAuthDir.Message;
                Box += "\n";
            }
            catch (PathTooLongException LongPath)
            {
                Box += LongPath.Message;
                Box += "\n";
            }
            catch (Exception e) {
                Box += e.Message;
                Box += "\n";
            
            }

        }


        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DriveInfo[] dire = DriveInfo.GetDrives();

            Thread[] t1 = new Thread[dire.Length];
            int i = 0;

            foreach (DriveInfo d in dire)
            {
                string name = d.Name;
                t1[i] = new Thread(() => MainWindow.findFill(name));
                i++;
            }
            for (int j = 0; j < i; j++)
            {
                t1[j].Start();
            }
            for (int j = 0; j < i; j++)
            {
                t1[j].Join();
            }
            textBox2.Text = Box;


        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            str = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
