using CatRenta.Application;
using CatRenta.Application.Interfaces;
using CatRenta.EFData;
using CatRenta.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using Wpf.CatRenta.Views;

namespace Wpf.CatRenta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // створення колекції котів
        private ObservableCollection<CatVM> _cats = new ObservableCollection<CatVM>();
        
        // створення підключення до БД
        private DataContext _context = new DataContext();
        public int _catId{ get; set; }


        private ICatService _catService = new CatService();

        // створення обєкту, який зупиняє роботу потоку, керується вручну
        ManualResetEvent _mrse = new ManualResetEvent(false);
        bool abort = false;
        // методи, за домогою яких відбувається керування потоками
        public void Resume() => _mrse.Set();
        public void Pause() => _mrse.Reset();

        /// <summary>
        /// конструктор для ініціалізації вікна
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _catService.EventInsertItem += UpdateUIAsync;

           // DataSeed.SeedDataAsync(_context);
        }

        public async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblInfoStatus.Text = "Підключаємося до БД-----";
            // створення обєкту, для вимірювання часу підключення до баз даних
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // асинхроний потік, який підраховує кількість обєктів в базі даних(в даному випадку котів)
            await Task.Run(() =>
            {
                _context.Cats.Count(); //jніціалуємо підклчюення
            });

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            //Debug.WriteLine("Сідер 1 закінчив свою роботу: " + elapsedTime);
            lblCursorPosition.Text = elapsedTime;
            lblInfoStatus.Text = "Підключення до БД успішно";

            // потік, який заповнює бд
            await DataSeed.SeedDataAsync(_context);
            // вимірює затрачений час на завантаження котів з бд
            stopWatch = new Stopwatch();
            stopWatch.Start();

            // ініціалізація колекції елементів з бази даних
            var list = _context.Cats.AsQueryable()//.AsParallel()
                .Select(x => new CatVM()
                {
                    Name = x.Name,
                    Birthday = x.Birthday,
                    Details = x.Details,
                    ImageUrl = x.Image,
                    Price = x.AppCatPrices
                        .OrderByDescending(x => x.DateCreate)
                        .FirstOrDefault().Price
                })
                .OrderBy(x => x.Name)
                .Skip(0)
                .Take(20)
                .ToList();

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            //Debug.WriteLine("Сідер 1 закінчив свою роботу: " + elapsedTime);
            lblCursorPosition.Text = elapsedTime;
            lblInfoStatus.Text = "Читання даних із БД успішно";

            _cats = new ObservableCollection<CatVM>(list);
            dgSimple.ItemsSource = _cats;
        }

        /// <summary>
        /// Додавання обєкта в базу даних 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            AddCatWindow addCat = new AddCatWindow(this._cats);
            addCat.Show();
        }
        /// <summary>
        /// Дозволяє редагування даних про обєкти(котів), які вже створені
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditWindow win = new EditWindow();
            win.Show();
        }
        /// <summary>
        /// зупиняє роботу потоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPauseAddRange_Click(object sender, RoutedEventArgs e)
        {
            this.Pause();
        }
        /// <summary>
        /// відновлює роботу потоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResume_Click(object sender, RoutedEventArgs e)
        {
            this.Resume();
        }
        /// <summary>
        /// скасовує роботу потоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelAddRange_Click(object sender, RoutedEventArgs e)
        {
            _catService.CanselAsyncMethod = true;
        }
        /// <summary>
        /// запускає потік, який додає задану кількість котів в бд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAddRange_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            
            btnAddRange.IsEnabled = false;
            this.Resume();
            // кількість котів які додаються за один потік
            int count = 100;
            pbCats.Maximum = count;
            // асинхронний потік, який додає котів в бд
            await _catService.InsertCatsAsync(count, _mrse);
            btnAddRange.IsEnabled = true;
        }

        private void ShowMessage()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                btnAddRange.IsEnabled = false;
            }));
            ICatService catService = new CatService();
            catService.EventInsertItem += UpdateUIAsync;
            catService.InsertCats(240, _mrse);
            Dispatcher.Invoke(new Action(() =>
            {
                btnAddRange.IsEnabled = true;
            }));

        }

        void UpdateUIAsync(int i)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                btnAddRange.Content = $"{i}";
                pbCats.Value = i;
            }));

        }

        /// <summary>
        /// видалення обєкта з бд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var cat = dgSimple.SelectedItem as CatVM;
            var cust = (from c in _context.Cats
                        where c.Id == cat.Id
                        select c).FirstOrDefault();

            if (cust != null)
            {
               
                _context.Cats.Remove(cust);
            }
            _context.SaveChanges();
        }

        private void btnValidation_Click(object sender, RoutedEventArgs e)
        {
            UserView window = new UserView();
            window.Show();
        }

        
    }
}
