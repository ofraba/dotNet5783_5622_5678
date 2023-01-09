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
    /// Interaction logic for WindowCart.xaml
    /// </summary>
    public partial class WindowCart : Window
    {
        IBl bl = BLApi.Factory.Get();
        Cart cart;
        public WindowCart(Cart c,IBl bll)
        {
            InitializeComponent();
            bll = bl;
            cart = c;
            lv_itemInCart.ItemsSource = cart.Items;
        }
       

        private void lv_itemInCart_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            OrderItem orderItem = (BO.OrderItem)((ListView)sender).SelectedItem;//
            UpdateCartWindow updateCartWindow = new UpdateCartWindow(cart,bl,orderItem.ID,this, orderItem);
            updateCartWindow.Show();
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            ConfirmWindow cw = new ConfirmWindow(bl, cart);
            cw.Show();
        }
    }
}
