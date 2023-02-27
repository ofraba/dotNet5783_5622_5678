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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        IBl bl = BLApi.Factory.Get();
        Cart cart;
        public ConfirmWindow(IBl bll,Cart c)
        {
            InitializeComponent();
            bl = bll;
            cart = c;
        }

        private void b_ok_Click(object sender, RoutedEventArgs e)
        {
            string name = tb_name.Text;
            string email = tb_email.Text;
            string address = tb_address.Text;
            try
            {
                bl.Cart.Confirm(cart, name, email, address);
                MainWindow m=new MainWindow();
                m.Show();
                Close();
            }
            catch (BO.dataIsntInvalid ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.ExceptionFromDal ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
