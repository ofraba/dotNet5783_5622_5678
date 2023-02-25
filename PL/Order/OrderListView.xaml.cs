using BLApi;
using BO;
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
    /// Interaction logic for OrderListView.xaml
    /// </summary>
    public partial class OrderListView : Window
    {
        IBl bl = Factory.Get();
        public OrderListView(IBl bll)
        {
            InitializeComponent();
            bl = bll;
            lv_OrderListView.ItemsSource = bl.Order.GetAll();
            
        }
        private void lv_OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderForList order = (OrderForList)((ListView)sender).SelectedItem;
            OrderWindow orderWindow = new OrderWindow(bl,order.ID);
            orderWindow.Show();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            ManagerMenu m=new ManagerMenu(bl);
            m.Show();
            Close();
        }
    }
}
