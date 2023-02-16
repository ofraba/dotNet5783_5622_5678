using BLApi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
using System.Xml.Linq;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        IBl bl = BLApi.Factory.Get();
        int id1;
        public OrderWindow(IBl bl2,int id,string page="")
        {
            InitializeComponent();
            //UpdateDeliveryDate.Visibility = Visibility.Hidden;//
            bl = bl2;
            id1=id;
            BO.Order selectedItem = bl.Order.GetForManegar(id);
            lv_orderItems.ItemsSource=selectedItem.Items;
            t_id.Text = selectedItem.ID.ToString();
            t_name.Text = selectedItem.CustomerName;
            t_email.Text = selectedItem.CustomerEmail;
            t_address.Text = selectedItem.CustomerAddress;
            t_status.Text = selectedItem.Status.ToString();
            t_orderDate.Text = selectedItem.OrderDate.ToString();
            t_shipDate.Text = selectedItem.ShipDate.ToString();
            t_deliveryDate.Text = selectedItem.DeliveryDate.ToString();
            t_toatalPrice.Text = selectedItem.TotalPrice.ToString();
            if(page=="orderTracking")
            {
                UpdateOrderShip.Visibility = Visibility.Hidden;
            }
        }

        private void UpdateOrderShip_Click(object sender, RoutedEventArgs e)
        {
           BO.Order order = bl.Order.OrderShippingUpdate(id1);
            t_shipDate.Text= order.ShipDate.ToString();
            //UpdateDeliveryDate.Visibility = Visibility.Visible;
            //UpdateOrderShip.Visibility = Visibility.Hidden;
        }

        private void UpdateDeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            BO.Order order = bl.Order.OrderDeliveryUpdate(id1);
            t_deliveryDate.Text = order.DeliveryDate.ToString();
        }
    }


    public class DoItIsEnable : IValueConverter//לא עובד שיהיה לא מאופשר
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;//לבדוק אם עובד 
            if (date != DateTime.MinValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
