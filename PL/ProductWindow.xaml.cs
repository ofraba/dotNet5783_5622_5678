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
using BlImplementation;
using BLApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = new Bl();
        static readonly Random random = new Random();
        public ProductWindow(IBl bl,BO.ProductItem product=null)
        {
            InitializeComponent();
            if (product == null)
            {
                tb_Id.Text = random.Next(100000, 999999).ToString();
                cb_Category.ItemsSource = Enum.GetValues(typeof(BO.Category));
                b_Add.Visibility = Visibility.Visible;
                b_UpDate.Visibility = Visibility.Hidden;
            }
            else
            {
                tb_Id.Text = product.ID.ToString();
                cb_Category.SelectedItem = product.Category;
                cb_Category.ItemsSource = Enum.GetValues(typeof(BO.Category));
                tb_Name.Text = product.Name.ToString();
                //tb_Color.Text 
                tb_Price.Text = product.Price.ToString();
                tb_InStock.Text = product.Amount.ToString();
                b_Add.Visibility = Visibility.Hidden;
                b_UpDate.Visibility = Visibility.Visible;
            }
           
        }

        private void b_Add_Click(object sender, RoutedEventArgs e)
        {
            BO.Product newProduct = new BO.Product();
            newProduct.ID = Convert.ToInt32(tb_Id.Text);
            newProduct.Category = (BO.Category)cb_Category.SelectedItem;
            newProduct.Name = tb_Name.Text;
            newProduct.Color = tb_Color.Text;
            newProduct.Price = Convert.ToInt32(tb_Price.Text);
            newProduct.InStock = Convert.ToInt32(tb_InStock.Text);
            try
            {
                int id = bl.Product.Add(newProduct);
                this.Close();
            }
            catch (BO.dataIsntInvalid ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (BO.ExceptionFromDal ex)//
            {
                Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
            }
        }

        private void b_UpDate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
