using CatRenta.Application;
using CatRenta.EFData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf.CatRenta
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
       
        // private readonly ObservableCollection<CatVM> _cats;
        // private DataContext _context = new DataContext();
       
        public string ChangeDetails { get; set; }
        public bool IsChangeDetails { get; set; } = false;
        
        public string ChangeName { get; set; }
        public bool IsChangeName { get; set; } = false;

        public EditWindow()
        {
           
            InitializeComponent();
            
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbName.Text))
            {
                ChangeName = tbName.Text;
                IsChangeName = true;
            }
            if (!string.IsNullOrEmpty(tbDetails.Text))
            {
                ChangeDetails = tbDetails.Text;
                IsChangeDetails = true;
            }
            DialogResult = true;
            this.Close();
        }

        

    }
}
