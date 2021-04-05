using CatRenta.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf.CatRenta.Views
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        private readonly UserViewModel user = new UserViewModel();
        public UserView()
        {
            InitializeComponent();
            user.EnableValidation = false;
            DataContext = user;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            user.EnableValidation = true;

            if (string.IsNullOrEmpty(user.Error))
                MessageBox.Show("Bomba");
            else
                MessageBox.Show(user.Error);
        }
    }
}
