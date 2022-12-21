using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    // Using INotifyPropertyChanged, we don't have to access the element, text property etc and assign them thus reducing the amount of code.
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
            }
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);
        }

        // Trigger this event to notify that the prop has changed (whenever setter is called).
        public event PropertyChangedEventHandler? PropertyChanged;

        // Props bound to it will repond to the event.
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}