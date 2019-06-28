using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using app.Models;
using app.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace app.ViewModels
{
    public class NabilViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<Galeb> galebs;
        private bool isRefreshing;
        private string filter;
        private List<Galeb> galebsList;
        #endregion

        #region Properties
        public ObservableCollection<Galeb> Galebs
        {
            get { return this.galebs; }
            set { SetValue(ref this.galebs, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }
        #endregion

        #region Constructors
        public NabilViewModel()
        {
            this.apiService = new ApiService();
            this.LoadGalebs();
        }
        #endregion

        #region Methods
        private async void LoadGalebs()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<Galeb>(
                "http://apifinal2.azurewebsites.net",
                "/api",
                "/Students");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            this.galebsList = (List<Galeb>)response.Result;
            this.Galebs = new ObservableCollection<Galeb>(this.galebsList);
            this.IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadGalebs);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Galebs = new ObservableCollection<Galeb>(
                    this.galebsList);
            }
            else
            {
                this.Galebs = new ObservableCollection<Galeb>(
                    this.galebsList.Where(
                        l => l.Name.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion

    }

}
