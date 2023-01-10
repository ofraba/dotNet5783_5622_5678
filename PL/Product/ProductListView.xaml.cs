using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DalApi;
using System.Windows.Input;
using BLApi;
using BO;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ProductListView : Window
    {
        Cart c = new Cart();
        string str1;
        IBl bl = BLApi.Factory.Get();
        public ProductListView(IBl bl2,string str)
        {
            InitializeComponent();
            str1=str;
            bl = bl2;
            if (str1 == "customer") 
            {
                lv_ProductListView.ItemsSource = bl.Product.GetForCatalog();
                //b_AddNewProduct.Visibility = Visibility.Hidden;
            }
            else
            {
                lv_ProductListView.ItemsSource = bl.Product.GetAll();
                //GoToCart.Visibility= Visibility.Hidden;
            }
            cb_CategoryFilter.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void cb_CategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (str1 == "customer")
            {
                var itemCategory = cb_CategoryFilter.SelectedItem;
                lv_ProductListView.ItemsSource = bl.Product.GetForCatalog(element => element.Category == (BO.Category)itemCategory);
            }
            else
            {
                var itemCategory = cb_CategoryFilter.SelectedItem;
                lv_ProductListView.ItemsSource = bl.Product.GetAll(element => element.Category == (BO.Category)itemCategory);
            }
              
        }

        private void b_AddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(bl,this,"add",c);
            productWindow.Show();
        }

        private void lv_ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (str1 == "customer")
            {
                ProductItem product = (BO.ProductItem)((ListView)sender).SelectedItem;
                ProductWindow productWindow = new ProductWindow(bl, this,"show",c,product.ID);
                productWindow.Show();
            }
            else
            {
                ProductForList product = (BO.ProductForList)((ListView)sender).SelectedItem;
                ProductWindow productWindow = new ProductWindow(bl, this,"update",c,product.ID);
                productWindow.Show();
            }
            
        }


        private void forAllProducts_Click(object sender, RoutedEventArgs e)
        {
            if (str1 == "customer")
            {
                lv_ProductListView.ItemsSource = bl.Product.GetForCatalog();
            }
            else
            {
                lv_ProductListView.ItemsSource = bl.Product.GetAll();
            }
        }

        private void GoToCart_Click(object sender, RoutedEventArgs e)
        {
           WindowCart windowCart = new WindowCart(c,bl);
            windowCart.Show();
        }
    }
}
