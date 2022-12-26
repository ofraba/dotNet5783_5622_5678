﻿using System;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        Cart c = new Cart();
        IBl bl = BLApi.Factory.Get();
        static readonly Random random = new Random();
        public ProductWindow(IBl bl2, int id = 0)//,ProductListView pv
        {
            InitializeComponent();
            bl = bl2;
            //ProductListView pv1;
            //pv1 = pv;
            if (id == 0)
            {
                tb_Id.Text = random.Next(100000, 999999).ToString();
                cb_Category.ItemsSource = Enum.GetValues(typeof(BO.Category));
                b_Add.Visibility = Visibility.Visible;
                b_UpDate.Visibility = Visibility.Hidden;
            }
            else
            {
                BO.ProductItem selectedItem = bl.Product.GetForClient(id, c);
                tb_Id.Text = selectedItem.ID.ToString();
                cb_Category.SelectedItem = selectedItem.Category;
                cb_Category.ItemsSource = Enum.GetValues(typeof(BO.Category));
                tb_Name.Text = selectedItem.Name.ToString();
                tb_Color.Text = selectedItem.Color.ToString();
                tb_Price.Text = selectedItem.Price.ToString();
                tb_InStock.Text = selectedItem.Amount.ToString();
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
                //ProductListView pv = new ProductListView();
                //pv.lv_ProductListView.ItemsSource = bl.Product.GetAll();
                this.Close();
            }
            catch (BO.dataIsntInvalid ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.ExceptionFromDal ex)//
            {
                MessageBox.Show(ex.Message + " " + ex.InnerException.Message);
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
                this.Close();
            }
            catch (BO.ExceptionFromDal ex)//
            {
                MessageBox.Show(ex.Message + " " + ex.InnerException.Message);
            }
            //pv1.lv_ProductListView.ItemsSource = bl.Product.GetAll();
        }
    }
}