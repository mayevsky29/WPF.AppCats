using CatRenta.Application.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CatRenta.Application
{
    public class CatVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly CatValidator _catValidator;
        public bool EnableValidation { get; set; }

        private int _id;
        private string _name;
        private DateTime _birthday;
        private string _details;
        private string _imageUrl;
        private decimal _price;

        public CatVM()
        {
            _catValidator = new CatValidator();
        }
        

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

        public string this[string columnName]
        {
            get
            {
                if (EnableValidation)
                {
                    var firstOrDefault = _catValidator.Validate(this)
                        .Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                    if (firstOrDefault != null)
                        return _catValidator != null ? firstOrDefault.ErrorMessage : "";
                }
                return "";
            }
        }

        public string Error
        {
            get
            {
                if (_catValidator != null)
                {
                    if (EnableValidation)
                    {
                        var results = _catValidator.Validate(this);
                        if (results != null && results.Errors.Any())
                        {
                            var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
                            return errors;
                        }
                    }
                }
                return string.Empty;
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
