using System;
using System.Collections.Generic;
using System.Linq;
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

namespace GOLDSILVERREST
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Class1 cobj = new Class1();
            cobj.endpoint = textBox.Text;

            string Response = string.Empty;
            Response = cobj.makeRequest();


            label.Content = cobj.SilverPrice;
            label1.Content = cobj.Goldprice;

            textBox1.Text = Response;
        }
    }
}
