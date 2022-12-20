using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel
{
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

        //public WeatherViewModel()
        //{
        //    selectedCity = new City
        //    {
        //        LocalizedName = "New York"
        //    };
        //}

        // Trigger this event to notify that the prop has changed (whenever setter is called).
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}