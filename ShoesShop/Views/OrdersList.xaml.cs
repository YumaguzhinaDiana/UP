using Microsoft.EntityFrameworkCore;
using ShoesShop.Models;
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

namespace ShoesShop.Views
{
    /// <summary>
    /// Логика взаимодействия для OrdersList.xaml
    /// </summary>
    public partial class OrdersList : Window
    {
        public OrdersList()
        {
            InitializeComponent();
            user_tbx.Text = $"{App.currentUser.UserRoleNavigation.UserRoleName} {App.currentUser.UserSurname} {App.currentUser.UserName}";
            if (App.currentUser.UserRole != 2)
            {
                addOrder_btn.Visibility = Visibility.Collapsed;
                orders_lstv.ContextMenu = null;
            }
        }

        private void LoadOrders()
        {
            using(var db = new DbBootsShopContext())
            {
                var orders = db.Orders.Include(x => x.OrderPickupPointNavigation).OrderByDescending(x => x.OrderDeliveryDate).ToList();
                orders_lstv.Items.Clear();
                foreach(var o in orders)
                {
                    orders_lstv.Items.Add(new OrderItemCard(o));
                }
            }
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(orders_lstv.SelectedItem is OrderItemCard oc)
            {
                if(oc.DataContext is Order selOrder)
                {
                    new AddEditOrderWindow(selOrder).Show();
                    this.Close();
                }
            }
        }

        private void addOrder_btn_Click(object sender, RoutedEventArgs e)
        {
            new AddEditOrderWindow(null).Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            new CatalogWindow().Show();
            this.Close();
        }
    }
}
