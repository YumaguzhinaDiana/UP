using Microsoft.Win32;
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



namespace ShoesShop.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditTovarWindow.xaml
    /// </summary>
    public partial class AddEditTovarWindow : Window
    {
        public AddEditTovarWindow()
        {
            InitializeComponent();
            
        }
        Tovar currentTovar;
        public AddEditTovarWindow(Tovar tovar):this()
        {
            
            currentTovar = tovar;
            DataContext = tovar;
            LoadTovarData();

        }
        private void LoadTovarData()
        {
            if (currentTovar != null)
            {
                add_btn.Visibility = Visibility.Collapsed;
                edit_panel.Visibility = Visibility.Visible;
                title_tblk.Text = "Редактирование";
                articul_panel.Visibility = Visibility.Visible;

                articul_tbx.Text = currentTovar.TovarId.ToString();
                typeTovar_cbx.SelectedIndex = currentTovar.TovarType.Value - 1;
                categoryTovar_cbx.SelectedIndex = currentTovar.TovarCategory.Value - 1;
                manufacturTovar_cbx.SelectedIndex = currentTovar.TovarManufactur.Value - 1;
                supplyerTovar_cbx.SelectedIndex = currentTovar.TovarSupplyer.Value - 1;
                unitTovar_tbx.Text = currentTovar.TovarUnit;
                priceTovar_tbx.Text = currentTovar.TovarPrice.ToString();
                discountTovar_tbx.Text = currentTovar.TovarCurrentDiscount.ToString();
                amountTovar_tbx.Text = currentTovar.TovarStorageAmount.ToString();
                descriptionTovar_tbx.Document.Blocks.Clear();
                descriptionTovar_tbx.Document.Blocks.Add(new Paragraph(new Run($"{currentTovar.TovarDescription}")));



                if (currentTovar.TovarStatus == "deleted")
                {
                    delete_btn.Content = "Восстановить";
                }

                if (new DbBootsShopContext().OrderCompositions.Any(x => x.OcTovarId == currentTovar.TovarId))
                {
                    delete_btn.IsEnabled = false;
                }

            } 
        }

        private void LoadManufacturs()
        {
            var manufactures = new DbBootsShopContext().Manufacturs.ToList();
            foreach(var manufactur in manufactures)
            {
                manufacturTovar_cbx.Items.Add(manufactur.ManufacturName);

            }
            manufacturTovar_cbx.SelectedIndex = 0;
        }

        private void LoadSupplyers()
        {
            var supplyers = new DbBootsShopContext().Supplyers.ToList();
            foreach (var s in supplyers)
            {
                supplyerTovar_cbx.Items.Add(s.SupplyerName);

            }
            supplyerTovar_cbx.SelectedIndex = 0;
        }

        private void LoadCategories()
        {
            var categories = new DbBootsShopContext().TovarCategories.ToList();
            foreach (var cat in categories)
            {
                categoryTovar_cbx.Items.Add(cat.TovarCategoryName);

            }
            categoryTovar_cbx.SelectedIndex = 0;
        }
        private void LoadTypes()
        {
            var types = new DbBootsShopContext().TovarTypes.ToList();
            foreach (var t in types)
            {
                typeTovar_cbx.Items.Add(t.TovarTypeName);

            }
            typeTovar_cbx.SelectedIndex = 0;
        }
        string imageSource = "";
        string imagePath = "";
        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(var db = new DbBootsShopContext())
                {
                    if(discountTovar_tbx.Text.Trim().Length == 0)
                {
                    discountTovar_tbx.Text = "0";
                }
                if(unitTovar_tbx.Text.Trim().Length==0 || 
                    priceTovar_tbx.Text.Trim().Length==0 || 
                     amountTovar_tbx.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }
                if (ValidateInputs())
               { if(imageSource == "")
                {
                    imageSource = "/picture.png";
                }
                else { CopyToResources(imagePath); }

              
                var newTovar = new Tovar
                {
                    TovarType = typeTovar_cbx.SelectedIndex + 1,
                    TovarUnit = unitTovar_tbx.Text.Trim(),
                    TovarPrice = decimal.Parse(priceTovar_tbx.Text),
                    TovarSupplyer = supplyerTovar_cbx.SelectedIndex + 1,
                    TovarManufactur = manufacturTovar_cbx.SelectedIndex + 1,
                    TovarCategory = categoryTovar_cbx.SelectedIndex + 1,
                    TovarStorageAmount = int.Parse(amountTovar_tbx.Text),
                    TovarCurrentDiscount = float.Parse(discountTovar_tbx.Text),
                    TovarDescription = GetRichTextBoxText(descriptionTovar_tbx),
                    TovarImage = imageSource,
                    TovarStatus="active"
                };
                string articul = GenerateArticul();
                while(db.Tovars.Any(x => x.TovarId == articul))
                {
                    articul = GenerateArticul();
                }

                newTovar.TovarId = articul;
                db.Tovars.Add(newTovar);
                
                db.SaveChanges();
                        MessageBox.Show("Успешно добавлено");
                    }
}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private string GenerateArticul()
        {
            var rand = new Random();
            string articul = ((char)rand.Next('A', 'Z')).ToString();
            for(int i = 0; i < 3; i++)
            {
                articul += rand.Next(0, 9);
            }
            articul += ((char)rand.Next('A', 'Z')).ToString();
            return articul;
        }
        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            new CatalogWindow().Show();
            this.Close();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadManufacturs();
            LoadCategories();
            LoadSupplyers();
            LoadTypes();
        }
       
        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (discountTovar_tbx.Text.Trim().Length == 0)
                {
                    discountTovar_tbx.Text = currentTovar.TovarCurrentDiscount.ToString();
                }
                if (unitTovar_tbx.Text.Trim().Length == 0 ||
                    priceTovar_tbx.Text.Trim().Length == 0 ||
                     amountTovar_tbx.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }
                if (ValidateInputs()) { 
                if (imageSource == "")
                {
                    imageSource = currentTovar.TovarImage;
                }
                    using (var context = new DbBootsShopContext())
                    {
                        var editTovar = context.Tovars.FirstOrDefault(x => x.TovarId == currentTovar.TovarId);

                        editTovar.TovarType = typeTovar_cbx.SelectedIndex + 1;
                        editTovar.TovarUnit = unitTovar_tbx.Text.Trim();
                        editTovar.TovarPrice = decimal.Parse(priceTovar_tbx.Text);
                        editTovar.TovarSupplyer = supplyerTovar_cbx.SelectedIndex + 1;
                        editTovar.TovarManufactur = manufacturTovar_cbx.SelectedIndex + 1;
                        editTovar.TovarCategory = categoryTovar_cbx.SelectedIndex + 1;
                        editTovar.TovarStorageAmount = int.Parse(amountTovar_tbx.Text);
                        editTovar.TovarCurrentDiscount = float.Parse(discountTovar_tbx.Text);
                        editTovar.TovarDescription = GetRichTextBoxText(descriptionTovar_tbx);

                        editTovar.TovarStatus = currentTovar.TovarStatus;
                        if (editTovar.TovarImage != imageSource)
                        {
                            CopyToResources(imagePath);
                        }
                        editTovar.TovarImage = imageSource;

                        context.SaveChanges();
                        MessageBox.Show("Успешно сохранено", "Сохранено", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            using(var db = new DbBootsShopContext()) 
           { var editTovar = db.Tovars.FirstOrDefault(x => x.TovarId == currentTovar.TovarId);
            if(editTovar.TovarStatus=="active")
           {
                    var res = MessageBox.Show("Вы точно хотите удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(res == MessageBoxResult.Yes)
                    {
                        editTovar.TovarStatus = "deleted";
                        delete_btn.Content = "Восстановить";
                    }
              
            }
            else
            {
                editTovar.TovarStatus = "active";
                delete_btn.Content = "Удалить";
            }
                db.SaveChanges();
            }
        }
        public static string GetRichTextBoxText(RichTextBox richTextBox)
        {
            if (richTextBox == null) return string.Empty;

            return new TextRange(richTextBox.Document.ContentStart,
                                richTextBox.Document.ContentEnd).Text.Trim();
        }
        private void loadImage_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Image files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp",
                    Title = "Выберите фотографию"
                };

                if (openFileDialog.ShowDialog() == true)
                {

                    var filename = openFileDialog.FileName;
                    System.Drawing.Image image = System.Drawing.Image.FromFile(filename);
                    if(image.Width>301 || image.Height > 201)
                    {
                        MessageBox.Show("Фото должно быть размером не более 300Х200");
                        return;
                    }
                    imageSource = Path.GetFileName(filename);
                    imagePath = filename;
                    preview_image.Source = new BitmapImage(new Uri(filename));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private string CopyToResources(string sourceFilePath)
        {

            //string appDirectory = "C:\\Users\\noutb\\source\\repos\\ShoesShop\\ShoesShop\\Resources\\";
            string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            projectPath = Directory.GetParent(projectPath).Parent.FullName;
            string resourcesPath = System.IO.Path.Combine(projectPath, "ShoesShop\\Resources");


            if (!Directory.Exists(resourcesPath))
            {
                Directory.CreateDirectory(resourcesPath);
            }

            string fileName = Path.GetFileName(sourceFilePath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            string fileExtension = Path.GetExtension(fileName);
            

            string destinationFilePath = Path.Combine(resourcesPath, fileName);
            string newFileName = "";

            int counter = 1;
            while (File.Exists(destinationFilePath))
            {
                newFileName = $"{fileNameWithoutExt}_{counter}{fileExtension}";
                destinationFilePath = Path.Combine(resourcesPath, newFileName);
                counter++;
            }
            if(newFileName != "")
            {
                imageSource = newFileName;
            }
            
            File.Copy(sourceFilePath, destinationFilePath, false);

            return newFileName;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(typeTovar_cbx.Text))
            {
                MessageBox.Show("Введите название товара", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                typeTovar_cbx.Focus();
                return false;
            }

           
            if (string.IsNullOrWhiteSpace(unitTovar_tbx.Text))
            {
                MessageBox.Show("Введите единицу измерения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                unitTovar_tbx.Focus();
                return false;
            }

           
            if (!decimal.TryParse(priceTovar_tbx.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Введите корректную стоимость (число больше 0)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                priceTovar_tbx.Focus();
                return false;
            }

            
                if (!decimal.TryParse(discountTovar_tbx.Text, out decimal discount) || discount < 0 || discount > 99)
                {
                    MessageBox.Show("Скидка должна быть числом от 0 до 100", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    discountTovar_tbx.Focus();
                    return false;
               }
            

            
            if (!int.TryParse(amountTovar_tbx.Text, out int amount) || amount < 1)
            {
                MessageBox.Show("Введите корректное количество (целое число не меньше 1)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                amountTovar_tbx.Focus();
                return false;
            }

            return true;
        }
    }
}
