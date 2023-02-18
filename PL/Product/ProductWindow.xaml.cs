using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        Cart c;
        int id1;
        IBl bl = BLApi.Factory.Get();
        static readonly Random random = new Random();
        ProductListView pv;
        string str1;
        public int source1 { get; set; }
        public string source2 { get; set; }
        public ProductWindow(IBl bl2,ProductListView pv1,string str, Cart c1,int id = 0)
        {
            InitializeComponent();
            id1 = id;
            pv = pv1;
            bl = bl2;
            c = c1;
            source1=id;
            source2 = str;
            str1 = str;
            if (source2 == "add")//הוספת פריט
            {
                tb_Id.Text = random.Next(100000, 999999).ToString();//לא מציג את ה-id
                cb_Category.ItemsSource = Enum.GetValues(typeof(BO.Category));//לעשות binding
                //b_Add.Visibility = Visibility.Visible;
                //b_UpDate.Visibility = Visibility.Hidden;
                //b_addToCart.Visibility = Visibility.Hidden;
            }
            else
            {
                BO.ProductItem selectedItem = bl.Product.GetForClient(id, c);
                DataContext=selectedItem;
                //tb_Id.Text = selectedItem.ID.ToString();
                cb_Category.SelectedItem = selectedItem.Category;//לעשות binding
                cb_Category.ItemsSource = Enum.GetValues(typeof(BO.Category));//לעשות binding
                //tb_Name.Text = selectedItem.Name?.ToString();
                //tb_Color.Text = selectedItem.Color?.ToString();
                //tb_Price.Text = selectedItem.Price.ToString();
                //tb_InStock.Text = selectedItem.Amount.ToString();
                //b_Add.Visibility = Visibility.Hidden;
                //if (source2 == "update")
                //{
                //b_UpDate.Visibility = Visibility.Visible;
                //b_addToCart.Visibility = Visibility.Hidden;
                //}
                //else//show
                //{
                //    b_UpDate.Visibility = Visibility.Hidden;
                //    b_addToCart.Visibility = Visibility.Visible;
                //tb_Id.IsEnabled = false;
                //cb_Category.IsEnabled = false;
                //tb_Name.IsEnabled = false;
                //tb_Color.IsEnabled = false;
                //tb_Price.IsEnabled = false;
                //tb_InStock.IsEnabled = false;
                //}
            }
        }

            private void b_Add_Click(object sender, RoutedEventArgs e)
        {
            BO.Product newProduct = new BO.Product();
            newProduct.ID = Convert.ToInt32(tb_Id.Text);
            newProduct.Category = (BO.Category)cb_Category.SelectedItem;
            newProduct.Name = tb_Name.Text;
            newProduct.Color = tb_Color.Text;
            newProduct.Price = Convert.ToInt32(tb_Price.Text);//אם מכניסים string נתקע...
            newProduct.InStock = Convert.ToInt32(tb_InStock.Text);//אם מכניסים string נתקע...
            try
            {
                int id = bl.Product.Add(newProduct);
                pv.lv_ProductListView.ItemsSource = bl.Product.GetAll();
                this.Close();
            }
            catch (BO.dataIsntInvalid ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.ExceptionFromDal ex)//
            {
                MessageBox.Show(ex.Message + " " + ex.InnerException?.Message);
            }
        }

        private void b_UpDate_Click(object sender, RoutedEventArgs e)
        {
            BO.Product updateProduct = new BO.Product();
            updateProduct.ID = Convert.ToInt32(tb_Id.Text);
            updateProduct.Category = (BO.Category)cb_Category.SelectedItem;
            updateProduct.Name = tb_Name.Text;
            updateProduct.Color = tb_Color.Text;
            updateProduct.Price = Convert.ToInt32(tb_Price.Text);
            updateProduct.InStock = Convert.ToInt32(tb_InStock.Text);
            try
            {
                bl.Product.Update(updateProduct);
                pv.lv_ProductListView.ItemsSource = bl.Product.GetAll();
                this.Close();
            }
            catch (BO.ExceptionFromDal ex)
            {
                MessageBox.Show(ex.Message + " " + ex.InnerException?.Message);
            }
        }

        private void b_addToCart_Click(object sender, RoutedEventArgs e)
        {
            if(c.Items==null)
                c.Items = new List<OrderItem>();
            try {
                c = bl.Cart.AddProductToCart(c, id1);
            }
            catch(BO.notEnoughAmount ex) {
                MessageBox.Show(ex.Message);
            }
                catch (BO.ExceptionFromDal ex)
            {
                MessageBox.Show(ex.Message + " " + ex.InnerException?.Message);
            }
            this.Close();
        }
    }
    public class convertAdd : IValueConverter
    {
        public object Convert(
 object value,
 Type targetType,
 object parameter,
 CultureInfo culture)
        {
            int intValue = (int)value;
            if (intValue == 0)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }
        public object ConvertBack(
 object value,
 Type targetType,
 object parameter,
 CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class convertUpdate : IValueConverter
    {
        public object Convert(
 object value,
 Type targetType,
 object parameter,
 CultureInfo culture)
        {
            string stringValue = (string)value;
            if (stringValue == "update")
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }
        public object ConvertBack(
 object value,
 Type targetType,
 object parameter,
 CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class changeIsEnabledInTextBox : IValueConverter
    {
        public object Convert(
 object value,
 Type targetType,
 object parameter,
 CultureInfo culture)
        {
            string stringValue = (string)value;
            if (stringValue == "show")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public object ConvertBack(
 object value,
 Type targetType,
 object parameter,
 CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class changeVisibilityForAddToCart : IValueConverter
    {
        public object Convert(
 object value,
 Type targetType,
 object parameter,
 CultureInfo culture)
        {
            string stringValue = (string)value;
            if (stringValue == "show")
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }
        public object ConvertBack(
 object value,
 Type targetType,
 object parameter,
 CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


