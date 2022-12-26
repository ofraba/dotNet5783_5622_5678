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

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ProductListView : Window
    {
        int debily = 0;
        IBl bl = BLApi.Factory.Get();
        public ProductListView(IBl bl2)
        {
            InitializeComponent();
            bl = bl2;
            lv_ProductListView.ItemsSource = bl.Product.GetAll();
            cb_CategoryFilter.ItemsSource = Enum.GetValues(typeof(BO.Category));
            debily=lv_ProductListView.Items.Count;
        }

        private void cb_CategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemCategory = cb_CategoryFilter.SelectedItem;
             lv_ProductListView.ItemsSource = bl.Product.GetAll(element => element.Category==(BO.Category)itemCategory);
        }

        private void b_AddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(bl);//,this
            productWindow.Show();
        }

        private void lv_ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductForList product = (BO.ProductForList)(sender as ListView).SelectedItem;
            ProductWindow productWindow = new ProductWindow(bl, product.ID);//,this
            productWindow.Show();
        }


        private void forAllProducts_Click(object sender, RoutedEventArgs e)
        {
            lv_ProductListView.ItemsSource = bl.Product.GetAll();
        }
    }
}
