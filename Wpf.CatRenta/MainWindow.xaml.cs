using CatRenta.Application;
using CatRenta.EFData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Wpf.CatRenta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<CatVM> _cats = new ObservableCollection<CatVM>();
        private DataContext _context = new DataContext();
        public MainWindow()
        {
            InitializeComponent();
            DataSeed.SeedDataAsync(_context);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = _context.Cats
                .Select(x => new CatVM()
                {
                    Name = x.Name,
                    Birthday = x.Birthday,
                    Details = x.Details,
                    ImageUrl = x.Image
                }).ToList();
            _cats = new ObservableCollection<CatVM>(list);
            dgSimple.ItemsSource = _cats;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCatWindow addCat = new AddCatWindow(this._cats);
            addCat.Show();

            //_cats.Add(new CatVM
            //{
            //    Name = "Петро",
            //    Birthday = new DateTime(2000, 5, 15),
            //    Details = "Дружить із директром Іванкой",
            //    ImageUrl = "https://icdn.lenta.ru/images/2020/01/28/17/20200128170822958/square_320_9146846fb3b1bfae5672755bc1896214.jpg"
            //});
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgSimple.SelectedItem != null)
            {
                if (dgSimple.SelectedItem is CatVM)
                {
                    var userView = dgSimple.SelectedItem as CatVM;
                    userView.Birthday = new DateTime(2003, 1, 23);
                    userView.Details = "Пішов в гори!";
                    userView.ImageUrl = "https://i.pinimg.com/originals/ec/5a/a9/ec5aa93a38113ea5b346cb87b5c2c941.jpg";
                }
            }
        }

    }
}
