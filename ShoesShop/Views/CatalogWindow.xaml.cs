using Microsoft.EntityFrameworkCore;
using ShoesShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ShoesShop.Views
{
    /// <summary>
    /// Логика взаимодействия для CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        public CatalogWindow()
        {
            InitializeComponent();
            if(App.currentUser != null)
            {
                name_tblc.Text = $"{App.currentUser.UserRoleNavigation.UserRoleName} {App.currentUser.UserSurname} {App.currentUser.UserName}";
                if (App.currentUser.UserRole == 1)
                {
                    searching_grid.Visibility = Visibility.Collapsed;
                    admin_panel.Visibility = Visibility.Hidden;
                    tovars_lstv.ContextMenu = null;
                }
                if (App.currentUser.UserRole == 3)
                {

                    addTovar_btn.Visibility = Visibility.Hidden;
                    tovars_lstv.ContextMenu = null;
                }
            }
            else
            {
                name_tblc.Text = "Гость";
                searching_grid.Visibility = Visibility.Collapsed;
                admin_panel.Visibility = Visibility.Hidden;
                tovars_lstv.ContextMenu = null;
            }
            
        }
        private void LoadTovars()
        {
            try
            {
                string searchingText = searchField_tbx.Text.Trim();
                int filtr = filtr_cmbx.SelectedIndex;
                int sorty = sort_cmbx.SelectedIndex ;
                var context = new DbBootsShopContext();
                var tovars = context.Tovars
                    .Include(x => x.TovarTypeNavigation)
                    .Include(x=> x.TovarManufacturNavigation)
                    .Include(x => x.TovarSupplyerNavigation)
                    .Include(x => x.TovarCategoryNavigation)
                    .Where(x => x.TovarStatus=="active").ToList();

                if (searchingText.Length > 0)
                {
                    tovars = tovars.Where(x => x.TovarPublicName.Contains(searchingText, StringComparison.CurrentCultureIgnoreCase) ||
                            x.TovarDescription.Contains(searchingText,StringComparison.CurrentCultureIgnoreCase) ||
                             x.TovarManufacturNavigation.ManufacturName.Contains(searchingText, StringComparison.CurrentCultureIgnoreCase) ||
                               x.TovarSupplyerNavigation.SupplyerName.Contains(searchingText, StringComparison.CurrentCultureIgnoreCase)).ToList();
                }
                if(filtr > 0)
                {
                    tovars = tovars.Where(x => x.TovarSupplyer == filtr).ToList();
                }
                if (sorty != -1)
                {
                    if (sorty == 0) tovars = tovars.OrderBy(x => x.TovarStorageAmount).ToList();
                    if(sorty == 1) tovars = tovars.OrderByDescending(x => x.TovarStorageAmount).ToList();
                }
                if (App.currentUser==null || App.currentUser.UserRole == 1 )
                {
                    tovars = tovars.Where(x => x.TovarStatus == "active").ToList();
                }
                tovars_lstv.Items.Clear();
                foreach ( var item in tovars )
                {
                    var card = new TovarControl(item);
                    card.GetPriceWithDiscount();
                    tovars_lstv.Items.Add(card);
                }
                if (tovars.Count == 0)
                {
                    tovars_lstv.Items.Add(new TextBlock { Text = "Тoвар не найден" });
                }
            }
            catch
            {

            }
        }

        private void LoadFiltrItems()
        {
            var supplyers = new DbBootsShopContext().Supplyers.ToList();
            filtr_cmbx.Items.Clear();
            filtr_cmbx.Items.Add("Все поставщики");
            foreach(var supplyer in supplyers)
            {
                filtr_cmbx.Items.Add(supplyer.SupplyerName);
            }
            filtr_cmbx.SelectedIndex = 0;
            
        }

        private void LoadSortsItems()
        {
            var sorts = new string[]{ "Количество на складе (сначала меньше)", "Количество на складе (сначала больше)" };
            sort_cmbx.ItemsSource = sorts;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSortsItems();
            LoadFiltrItems();
            LoadTovars();
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            App.currentUser = null;
            new MainWindow().Show();
            this.Close();
        }

        private void searchField_tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadTovars();
        }

        private void filtr_cmbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadTovars();
        }

        private void addTovar_btn_Click(object sender, RoutedEventArgs e)
        {
            new AddEditTovarWindow().Show();
            this.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            if(tovars_lstv.SelectedItem is TovarControl control)
            {
                if(control.DataContext is Tovar selectedTovar)
                {
                    new AddEditTovarWindow(selectedTovar).Show();
                    this.Close();
                }
            }
        }

        private void orders_btn_Click(object sender, RoutedEventArgs e)
        {
            new OrdersList().Show();
            this.Close();
        }
    }
}
