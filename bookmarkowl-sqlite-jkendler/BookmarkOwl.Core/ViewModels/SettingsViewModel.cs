using System;
using BookmarkOwl.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace BookmarkOwl.Core.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        IAlertService AlertService { get; }

        IPreferencesService PreferencesService { get; }

        public SettingsViewModel(IAlertService alertService, IPreferencesService preferencesService)
        {
            this.AlertService = alertService;
            this.PreferencesService = preferencesService;
            
            this.Token = PreferencesService.Get("ApiToken");
            this.Url = PreferencesService.Get("ApiUrl");
        }




        [ObservableProperty]
        string _url = string.Empty;

        [ObservableProperty]
        string _token = string.Empty;

        [RelayCommand]
        public void SaveSettings()
        {
            PreferencesService.Set("ApiUrl", this.Url);

            PreferencesService.Set("ApiToken", this.Token);

            AlertService.ShowAlert("Erfolg!", "Die Einstellungen wurden gespeichert");
        }

        [RelayCommand]
        public void DeleteSettings()
        {
            PreferencesService.Clear();
            this.Token = string.Empty;
            this.Url = string.Empty;
            AlertService.ShowAlert("Einstellungen", "Die Api Einstellungen wurden gelöscht");

        }

    }
}

