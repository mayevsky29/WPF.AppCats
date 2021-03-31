using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CatRenta.Application
{
    public class CatVM : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private DateTime _birthday;
        private string _details;
        private string _imageUrl;
        private decimal _price;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                this.NotifyPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.NotifyPropertyChanged("Name");
            }
        }


        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                this.NotifyPropertyChanged("Birthday");
            }
        }

        public string Details
        {
            get { return _details; }
            set
            {
                _details = value;
                NotifyPropertyChanged("Details");
            }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                this.NotifyPropertyChanged("ImageUrl");
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                this.NotifyPropertyChanged("Price");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            //if (this.PropertyChanged != null)
            //    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
