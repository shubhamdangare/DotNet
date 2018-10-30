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
using System.Reflection;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Assembly DLL = Assembly.LoadFile(@"C:\Users\shubham\Desktop\WpfApplication1\WpfApplication1\Mydll.dll");

        Type type;
        object instance;
        MethodInfo[] methods;
        object res;

        public MainWindow()
        {
            InitializeComponent();


            //type = DLL.GetType("DLL.Artimatic"); 
            //instance = Activator.CreateInstance(type);
            //methods = type.GetMethods();
           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           // method = theType.GetMethod("Add");
            try
            {
                Assembly DLL = Assembly.LoadFile(@"C:\Users\shubham\Desktop\WpfApplication1\WpfApplication1\Mydll.dll");
                Type[] types = DLL.GetTypes();
                foreach (Type typ in types)
                {
                    object obj = Activator.CreateInstance(typ);
                    MethodInfo mi = typ.GetMethod("add");
                    res = mi.Invoke(obj, new object[] { 10, 30 });
                }
                textBox3.Text = Convert.ToString(res);

            }
            catch (Exception eobjs)
            {

            }
            
        }
        
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) - Convert.ToInt32(textBox2.Text));

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text));
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                textBox3.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) / Convert.ToDouble(textBox2.Text));
            }
            catch (Exception eobj) {

            }

            
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            //octal
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox2.Text), 8);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            //binary
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox2.Text), 2);
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            //hexa
            textBox3.Text = Convert.ToInt32(textBox2.Text).ToString("X");
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Visible;
            button6.Visibility = Visibility.Visible;
            button7.Visibility = Visibility.Visible;

            button1.Visibility = Visibility.Hidden;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Hidden;
            button4.Visibility = Visibility.Hidden;




        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Visible;


            textBox1.Visibility = Visibility.Visible;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button7.Visibility = Visibility.Hidden;

        }
    }
}
