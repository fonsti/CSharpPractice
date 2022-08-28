using System;
using System.Collections.Generic;
using System.ComponentModel;
using WeatherApp.ViewModel.Helpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using System.Collections.ObjectModel;

namespace WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set { query = value; OnPropertyChanged("Query"); }
        }

        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions currentConditions;

        public CurrentConditions MyCurrentConditions
        {
            get { return currentConditions; }
            set { currentConditions = value; OnPropertyChanged("MyCurrentConditions"); }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set { 
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "New York"
                };
                MyCurrentConditions = new CurrentConditions
                {
                    WeatherText = "Partly cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "21"
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        private async void GetCurrentConditions()
        {
            if (SelectedCity != null)
            {
                MyCurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
            }
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();
            if (cities != null)
            {
                foreach (var city in cities)
                {
                    Cities.Add(city);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
