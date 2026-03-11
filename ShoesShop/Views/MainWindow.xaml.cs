using Microsoft.EntityFrameworkCore;
using ShoesShop.Models;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShoesShop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
           
        }

       private void Authorization()
        {
            try
            {
                string login = login_tbx.Text.Trim();
                string password = password_tbx.Text.Trim(); 
                if(login.Length==0 || password.Length == 0)
                {
                    MessageBox.Show("Введите логин и пароль","Заполните данные", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var context = new DbBootsShopContext();
                var user = context.Users
                    .Include(x => x.UserRoleNavigation)
                    .FirstOrDefault(x => x.UserLogin == login && x.UserPassword == password);
                if (user == null)
                {
                    MessageBox.Show("Неверный логин или пароль", "Не найдено",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                App.currentUser = user;
                new CatalogWindow().Show();
                this.Close();
            }
            catch { }
        }

        private void enter_btn_Click(object sender, RoutedEventArgs e)
        {
            Authorization();
        }

        private void catalog_btn_Click(object sender, RoutedEventArgs e)
        {
            new CatalogWindow().Show();
            this.Close();
        }
    }
}