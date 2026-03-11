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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShoesShop.Views
{
    /// <summary>
    /// Логика взаимодействия для TovarControl.xaml
    /// </summary>
    public partial class TovarControl : UserControl
    {

        Tovar currentTovar;
        public TovarControl(Tovar tovar)
        {
            InitializeComponent();
            currentTovar = tovar;
            DataContext = tovar;
        }

        public void GetPriceWithDiscount()
        {
            if (currentTovar != null)
            {
                if (currentTovar.TovarCurrentDiscount > 0)
                {
                    var newPrice = currentTovar.TovarPrice.Value * (decimal)(1 - currentTovar.TovarCurrentDiscount.Value / 100);
                    price_tblc.Foreground = Brushes.Red;
                    price_tblc.TextDecorations = TextDecorations.Strikethrough;

                    priceWithDiscount_tblc.Text = $"{newPrice:F2} руб.";
                }
            }
        }
    }
}
