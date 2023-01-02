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
using BO;
using BLApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateCartWindow.xaml
    /// </summary>
    public partial class UpdateCartWindow : Window
    {
        IBl bl = BLApi.Factory.Get();
        Cart cart;
        public UpdateCartWindow(Cart c,IBl bll,int id,OrderItem orderItem)
        {
            InitializeComponent();
            bl = bll;
            cart = c;
            tb_Id.Text = id.ToString();
            tb_Name.Text =orderItem.Name;
            tb_ProductID.Text =orderItem.ProductID.ToString();
            tb_Price1.Text=orderItem.Price.ToString();
            tb_Amount.Text=orderItem.Amount.ToString();
            tb_TotalPrice.Text=orderItem.TotalPrice.ToString();
        }

        private void b_UpDate_Click(object sender, RoutedEventArgs e)//צריך לעשות שישר יעדכן בעמוד 
        {
            int amount= Convert.ToInt32(tb_Amount.Text);
            int productId = Convert.ToInt32(tb_ProductID.Text);
            bl.Cart.Update(cart, productId, amount);
            this.Close();
        }

        private void b_Remove_Click(object sender, RoutedEventArgs e)//צריך לעשות שישר יעדכן בעמוד 
        {
            int productId = Convert.ToInt32(tb_ProductID.Text);
            bl.Cart.Update(cart, productId, 0);
            this.Close();
        }
    }
}
