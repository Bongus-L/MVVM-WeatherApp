using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    // Using INotifyPropertyChanged, we don't have to access the element, text property etc and assign them thus reducing the amount of code.
    // INotifyPropertyChanged will also let CommandParameter know of any changes which will then trigger the CanExecuteChanged event & CanExecute method.
    public class WeatherViewModel : INotifyPropertyChanged 
    {
        private string query;
        public string Query
        {
            get { return query; }
            set 
            { 
                query = value;
                // OnPropertyChanged only accepts strings and it has to match the name of the Property. In this case, 'Query'.
                OnPropertyChanged("Query");
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions currentConditions;
        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set 
            { 
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            { 
                selectedCity = value; 
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public WeatherViewModel()
        {
            // Generate default weather info. Only display this when in design mode.
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City()
                {
                    LocalizedName = "London"
                };
                CurrentConditions = new CurrentConditions()
                {
                    WeatherText = "Snowy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = 21
                        }
                    }
                };
            }
            SearchCommand = new SearchCommand(this);

            // Instantiating the Cities list here because if you instantiate another one, binding will be lost.
            Cities = new ObservableCollection<City>();
        }

        private async void GetCurrentConditions()
        {
            // Clear the Query and Cities because the item has been selected at this point.
            Query = string.Empty;
            Cities.Clear();
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);
            Cities.Clear();
            foreach (var city in cities) 
            {
                Cities.Add(city);
            }
        }

        // Trigger this event to notify that the prop has changed (whenever setter is called).
        public event PropertyChangedEventHandler? PropertyChanged;

        // Props bound to it will respond to the event.
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}