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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ProductListView : Window
    {
        private IBl bl = new Bl();
        Cart c= new Cart();
        public ProductListView()
        {
            InitializeComponent();
            lv_ProductListView.ItemsSource = bl.Product.GetAll();
            //cb_CategoryFilter.Items.Add("all");
            cb_CategoryFilter.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void cb_CategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemCategory = cb_CategoryFilter.SelectedItem;
             lv_ProductListView.ItemsSource = bl.Product.GetAll(element => element.Category==(BO.Category)itemCategory);
        }

        private void b_AddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(bl);
            productWindow.Show();
        }

        private void lv_ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductForList product = (BO.ProductForList)(sender as ListView).SelectedItem;
            BO.ProductItem selectedItem = bl.Product.GetForClient(product.ID, c);
            ProductWindow productWindow = new ProductWindow(bl, selectedItem);
            productWindow.Show();
           

            //newProduct.ID = Convert.ToInt32(tb_Id.Text);
            //newProduct.Category = (BO.Category)cb_Category.SelectedItem;
            //newProduct.Name = tb_Name.Text;
            //newProduct.Color = tb_Color.Text;
            //newProduct.Price = Convert.ToInt32(tb_Price.Text);
            //newProduct.InStock = Convert.ToInt32(tb_InStock.Text);
        }
    }
}
