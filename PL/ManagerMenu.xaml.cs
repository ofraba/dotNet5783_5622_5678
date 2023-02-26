using BLApi;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerMenu.xaml
    /// </summary>
    public partial class ManagerMenu : Window
    {
        IBl bl = Factory.Get();
        public ManagerMenu(IBl bll)
        {
            InitializeComponent();
            bl = bll;
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            ProductListView pl = new ProductListView(bl,"manager");
            pl.Show();
            Close();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            OrderListView ol = new OrderListView(bl);
            ol.Show();
            Close();
        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            Close();
        }
    }
}
