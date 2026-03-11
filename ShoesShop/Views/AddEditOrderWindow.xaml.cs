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
    /// Логика взаимодействия для AddEditOrderWindow.xaml
    /// </summary>
    public partial class AddEditOrderWindow : Window
    {
        private Order curOrder;
        public AddEditOrderWindow(Order order)
        {
            InitializeComponent();
            
            curOrder = order;
            if (order != null)
            {
                add_btn.Visibility = Visibility.Collapsed;
                edit_panel.Visibility = Visibility.Visible;
                addGood_btn.Visibility = Visibility.Collapsed;

                title_tblk.Text = "Редактирование";
                client_cmbx.IsEnabled = false;
                LoadOrderInfo();

            }
            else
            {
                orderCode_tbx.Text = new Random().Next(1000, 10000).ToString();
                status_cmbx.IsEnabled = false;
                orderDate_dpk.DisplayDateStart = DateTime.Today;
                orderDate_dpk.SelectedDate = DateTime.Today;
                orderDate_dpk.IsEnabled = false;
            }
            orderDate_dpk.DisplayDateEnd = DateTime.Today.AddDays(60);
            orderDeliveryDate_dpk.DisplayDateEnd = DateTime.Today.AddDays(60);
        }
        private void LoadAllPickups()
        {
            using(var db = new DbBootsShopContext())
            {
                var pickups = db.PickUpPoints.Select(x => x.PickUpAddress).ToList();
                address_cmbx.ItemsSource = pickups;
                address_cmbx.SelectedIndex = 0;
                
            }
        }
        string[] statuses = { "Новый", "Собирается", "Готов", "Завершен" };
        private void LoadAllClients()
        {
            using (var db = new DbBootsShopContext())
            {
                var clients = db.Users.Where(x => x.UserRole == 1).Select(x => new ClientItem
                {
                    Id = x.UserId,
                    CLientFio = $"{x.UserSurname.Trim()} {x.UserName.Trim()} {x.UserPatronymic.Trim()}"
                }).ToList();
                client_cmbx.DisplayMemberPath = "CLientFio";
                client_cmbx.SelectedValuePath = "Id";
                client_cmbx.ItemsSource = clients;
                client_cmbx.SelectedIndex = 0;

            }
        }

        public void LoadStatuses()
        {
            status_cmbx.ItemsSource = statuses;
            status_cmbx.SelectedIndex = 0;
        }

        public void LoadGoodsArticuls()
        {
            using (var db = new DbBootsShopContext())
            {
                var goods = db.Tovars.Where(x => x.TovarStatus == "active").Select(x => new TovarItem
                {
                    TovarArticul = x.TovarId,
                    TovarAmount = x.TovarStorageAmount.Value
                }).ToList();
                goods_cmbx.DisplayMemberPath = "TovarArticul";
                goods_cmbx.ItemsSource = goods;
                goods_cmbx.SelectedIndex = 0;   

            }
        }

        public void LoadOrderInfo()
        {
            using(var db = new DbBootsShopContext())
            {
                var orderInfo = db.Orders.Include(x => x.OrderCompositions).FirstOrDefault(x => x.OrderId == curOrder.OrderId);
                id_tbx.Text = orderInfo.OrderId.ToString();
                client_cmbx.SelectedValue = orderInfo.OrderClientId;
                address_cmbx.SelectedIndex = orderInfo.OrderPickupPoint.Value-1;
                orderDate_dpk.SelectedDate = orderInfo.OrderDate.Value.ToDateTime(TimeOnly.MaxValue);
                orderDeliveryDate_dpk.SelectedDate = orderInfo.OrderDeliveryDate.Value.ToDateTime(TimeOnly.MaxValue);
                orderCode_tbx.Text = orderInfo.OrderCode;
                status_cmbx.SelectedValue = orderInfo.OrderStatus;
                orderDate_dpk.IsEnabled = false;
                LoadOrderItems(orderInfo.OrderCompositions.ToList());

            }
        }

        private void LoadOrderItems(List<OrderComposition> ords)
        {
            if(ords!= null)
            {
                goods_lst.Items.Clear();
                foreach(var oc in ords)
                {
                    goods_lst.Items.Add(new TextBlock
                    {
                        Text = $"{oc.OcTovarId} - {oc.OcTovarAmount} шт", DataContext = oc
                        
                    });
                }
            }
        }
        List<OrderComposition> ordersItems = new List<OrderComposition>();

        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(ValidateEnter())
                {var clientId = (int)curOrder.OrderClientId;
                var pickupId = address_cmbx.SelectedIndex + 1;
                var orderDate = orderDate_dpk.SelectedDate.Value;
                var orderDateDelivery = orderDeliveryDate_dpk.SelectedDate.Value;
                var code = curOrder.OrderCode;
                var status = status_cmbx.SelectedValue.ToString();
                using (var db = new DbBootsShopContext())
                {
                    var maxId = curOrder.OrderId;
                    var editedOrder = db.Orders.FirstOrDefault(x => x.OrderId == maxId);


                    editedOrder.OrderClientId = clientId;
                    editedOrder.OrderDate = DateOnly.FromDateTime(orderDate);
                    editedOrder.OrderDeliveryDate = DateOnly.FromDateTime(orderDateDelivery);
                    editedOrder.OrderPickupPoint = pickupId;
                    editedOrder.OrderCode = code;
                    editedOrder.OrderStatus = status;

                    
                    
                   
                    db.SaveChanges();
                        MessageBox.Show("Успешно сохранено");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(ValidateEnter())
                {
                    var clientId = (int)(client_cmbx.SelectedValue);
                var pickupId = address_cmbx.SelectedIndex + 1;
                var orderDate = orderDate_dpk.SelectedDate.Value;
                var orderDateDelivery = orderDeliveryDate_dpk.SelectedDate.Value;
                var code = orderCode_tbx.Text;
                var status = status_cmbx.SelectedValue.ToString();
                using(var db = new DbBootsShopContext())
                {
                    var maxId = db.Orders.Count() > 0 ? db.Orders.Max(x => x.OrderId) + 1 : 1;
                    var newOrder = new Order
                    {
                        OrderId = maxId,
                        OrderClientId = clientId,
                        OrderDate = DateOnly.FromDateTime(orderDate),
                        OrderDeliveryDate = DateOnly.FromDateTime(orderDateDelivery),
                        OrderPickupPoint = pickupId,
                        OrderCode = code,
                        OrderStatus = status

                    };
                    db.Orders.Add(newOrder);
                    foreach(var oc in ordersItems)
                    {
                        db.OrderCompositions.Add(new OrderComposition
                        {
                            OrderCompositionId = 0,
                            OcOrderId = maxId,
                            OcTovarId = oc.OcTovarId,
                            OcTovarAmount = oc.OcTovarAmount
                        });
                    }
                    db.SaveChanges();
                        MessageBox.Show("Успешно добавлено");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы уверены, что хотите удалить данные о заказе? Восстановить будет невозможно", "?", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.Yes)
            {
                using(var db = new DbBootsShopContext())
                {
                    var order = db.Orders.FirstOrDefault(o => o.OrderId == curOrder.OrderId);
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }
                new OrdersList().Show();
                this.Close();
            }

        }

        private void addGood_btn_Click(object sender, RoutedEventArgs e)
        {
            if(goods_cmbx.SelectedItem is TovarItem good)
            {
                if(ordersItems.Any(x => x.OcTovarId == good.TovarArticul))
                {
                    if(ordersItems.FirstOrDefault(x => x.OcTovarId == good.TovarArticul).OcTovarAmount <= good.TovarAmount)
                        ordersItems.FirstOrDefault(x => x.OcTovarId == good.TovarArticul).OcTovarAmount++;
                }
                else
                {
                    ordersItems.Add(new OrderComposition { OcTovarId = good.TovarArticul, OcTovarAmount = 1 });
                }
                LoadOrderItems(ordersItems);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAllPickups();
            LoadAllClients();
            LoadStatuses();
            LoadGoodsArticuls();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            new OrdersList().Show();
            this.Close();
        }

        private void orderDate_dpk_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            orderDeliveryDate_dpk.DisplayDateStart = orderDate_dpk.SelectedDate;
        }

        private void orderDeliveryDate_dpk_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            orderDate_dpk.DisplayDateEnd = orderDeliveryDate_dpk.SelectedDate;
        }

        private bool ValidateEnter()
        {
            if(client_cmbx.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите получателя заказа");
                return false;
            }
            if(orderDate_dpk.SelectedDate == null)
            {
                MessageBox.Show("Введите дату заказа");
                return false;
            }
            if (orderDeliveryDate_dpk.SelectedDate == null)
            {
                MessageBox.Show("Введите дату доставки");
                return false;
            }
            if (address_cmbx.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите адрес пункта выдачи");
                return false;
            }

            return true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(goods_lst.SelectedItem is TextBlock tbx)
            {

                if(tbx.DataContext is OrderComposition good){
                if (ordersItems.Any(x => x.OcTovarId == good.OcTovarId))
                {
                    if (ordersItems.FirstOrDefault(x => x.OcTovarId == good.OcTovarId).OcTovarAmount > 1)
                        ordersItems.FirstOrDefault(x => x.OcTovarId == good.OcTovarId).OcTovarAmount--;
                    else ordersItems.Remove(ordersItems.FirstOrDefault(x => x.OcTovarId == good.OcTovarId));
                }
                    LoadOrderItems(ordersItems);
                }
            }
        }
    }
}
