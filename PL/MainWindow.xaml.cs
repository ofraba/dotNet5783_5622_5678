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
using BLApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBl bl = Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoToThe_List_Click(object sender, RoutedEventArgs e)
        {
            ManagerMenu m=new ManagerMenu(bl);
            m.Show();
        }

        private void New_Order_Click(object sender, RoutedEventArgs e)
        {
            ProductListView p = new ProductListView(bl,"customer");
            p.Show();
        }

        private void Order_Tracking_Click(object sender, RoutedEventArgs e)
        {
            OrderTrackingWindow orderTrackingWindow = new OrderTrackingWindow(bl);
            orderTrackingWindow.Show();
        }
    }
}
