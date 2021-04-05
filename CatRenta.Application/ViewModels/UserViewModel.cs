using CatRenta.Application.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CatRenta.Application.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly UserValidator _userValidator;
        private string _zip;
        private string _email;
        private string _name;
        public bool EnableValidation { get; set; }

        public UserViewModel()
        {
            _userValidator = new UserValidator();
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Zip
        {
            get { return _zip; }
            set
            {
                _zip = value;
                OnPropertyChanged("Zip");
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (EnableValidation)
                {
                    var firstOrDefault = _userValidator.Validate(this)
                        .Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                    if (firstOrDefault != null)
                        return _userValidator != null ? firstOrDefault.ErrorMessage : "";
                }
                return "";
            }
        }

        public string Error
        {
            get
            {
                if (_userValidator != null)
                {
                    if (EnableValidation)
                    {
                        var results = _userValidator.Validate(this);
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
