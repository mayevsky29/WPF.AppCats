using CatRenta.Application;
using CatRenta.Domain;
using CatRenta.EFData;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Wpf.CatRenta
{
    /// <summary>
    /// Interaction logic for AddCatWindow.xaml
    /// </summary>
    public partial class AddCatWindow : Window
    {
        private readonly ObservableCollection<CatVM> _cats;
        private DataContext _context = new DataContext();

        /// <summary>
        /// Повний шлях до файла
        /// </summary>
        public string FileName { get; set; }
        private bool _gender { get; set; }

        public AddCatWindow(ObservableCollection<CatVM> cats)
        {
            InitializeComponent();
            _cats = cats;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


        }

        // Вибір фото кота
        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
            }
        }

        // Збереження нового кота
        private void SaveCat_Click(object sender, RoutedEventArgs e)
        {
            var extension = Path.GetExtension(FileName);
            var imageName = Path.GetRandomFileName() + extension;
            var dir = Directory.GetCurrentDirectory();
            var saveDir = Path.Combine(dir, "images");
            if (!Directory.Exists(saveDir))
                Directory.CreateDirectory(saveDir);
            var fileSave = Path.Combine(saveDir, imageName);
            File.Copy(FileName, fileSave);

            var cat =
                    new AppCat
                    {
                        Name = tbName.Text,
                        Gender = _gender,
                        Birthday = (DateTime)dpDate.SelectedDate,
                        Details = tbDetails.Text,
                        Image = fileSave
                    };
            cat.AppCatPrices = new List<AppCatPrice>
            {
                new AppCatPrice
                {
                    CatId = cat.Id,
                    DateCreate=DateTime.Now,
                    Price=decimal.Parse(tbPrice.Text)
                }
            };
            _context.Add(cat);
            _context.SaveChanges();

            _cats.Add(new CatVM
            {
                Id = cat.Id,
                Name = cat.Name,
                Birthday = cat.Birthday,
                Details = cat.Details,
                ImageUrl = cat.Image

            });
            this.Close();
        }

        // Виставлення статі кота за радіобаттонами
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rbtn = (RadioButton)sender;
            _gender = Convert.ToBoolean(rbtn.Tag);
        }

    }
}
