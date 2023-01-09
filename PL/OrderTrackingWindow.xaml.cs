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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        IBl bl = BLApi.Factory.Get();
        public OrderTrackingWindow(IBl bll)
        {
            InitializeComponent();
            bl = bll;
        }

        private void b_search_Click(object sender, RoutedEventArgs e)
        {
            try {
                OrderTracking orderTracking = bl.Order.OrderTracking(Convert.ToInt32(tb_id.Text));
                l_status.Visibility = Visibility.Visible;
                tb_status.Visibility = Visibility.Visible;
                tb_status.Text = orderTracking.status.ToString();
                lv_orderTracking.Visibility = Visibility.Visible;
                lv_orderTracking.ItemsSource = orderTracking.dateAndDescription;
            }
            catch (BO.ExceptionFromDal ex)
            {
                MessageBox.Show(ex.InnerException?.Message);
            }
        }

        private void b_details_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow(bl, Convert.ToInt32(tb_id.Text), "orderTracking");
            orderWindow.Show();
        }
    }
}
