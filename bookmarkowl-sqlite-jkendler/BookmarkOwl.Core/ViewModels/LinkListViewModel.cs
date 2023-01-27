using BookmarkOwl.Core.Interfaces;
using BookmarkOwl.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkOwl.Core.ViewModels
{
    public partial class LinkListViewModel : ObservableObject
    {
        IRepository Repository { get; }
        IAlertService AlertService { get; }

        IBrowserService BrowserService { get; }

        public LinkListViewModel(IRepository repository, 
            IAlertService alertService, IBrowserService browserService)
        {
            this.Repository = repository;
            this.AlertService = alertService;
            this.BrowserService = browserService;
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        string _linkTitle = string.Empty;

        // ObservableProperty erzeugt die Eigenschaft zB LinkDestination.
        // Diese Eigenschaft kann in der View für die Datenbindung verwendet
        // werden.

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        string _linkDestination = "";


        public static string Title => "BookmarkOwl";

        // check if title and destination are available
        public bool CanCreate => 
            _linkTitle != string.Empty && 
            _linkDestination != string.Empty;


        [RelayCommand(CanExecute = nameof(CanCreate))]
        private async void Create()
        {
            // this.Title = "BookmarkOwl 2022";

            LinkEntry le = new(this.LinkTitle, this.LinkDestination);
            bool res = await Repository.Add(le);

            if (res == true)
            {
                this._entries.Add(le);
                this.LinkTitle = "";
                this.LinkDestination = string.Empty;

                // string.Empty ist ""
            }

        }

        [ObservableProperty]
        ObservableCollection<LinkEntry> _entries =
            new();
        /*
            {
                new LinkEntry("Google", 
                    "https://www.google.at"),
                new LinkEntry("BHAK/BHAS Zell am See", 
                    "https://www.hakzell.at"),
            };
        */


        [RelayCommand]
        public async void LoadData()
        {
            this.Entries.Clear();

            var entries = await Repository.All();

            foreach (LinkEntry entry in entries)
            {
                _entries.Add(entry);
            }
        }

        [RelayCommand]
        private async void ToggleFavorite(LinkEntry entry)
        {
            entry.Favorite = !entry.Favorite;
            var res = await Repository.Update(entry);

            AlertService.ShowAlert("Info", "Fehlertest");

            if (res == false)
            {
                // error message
                AlertService.ShowAlert("Fehler", "Der Favoriten-Status konnte nicht gespeichert werden.");
            }
        }


        [RelayCommand]
        public async void RemoveLink(LinkEntry link)
        {
            bool res = await Repository.DeleteById(link.Id);

            if (res)
            {
                this.Entries.Remove(link);
            }
            else
            {
                AlertService.ShowAlert("Fehler", "Der Eintrag konnte nicht gelöscht werden.");
            }
        }


        [RelayCommand]
        void OpenLink(LinkEntry link)
        {
            try
            {
                Uri uri = new(link.Destination);

                BrowserService.Open(uri);
                // await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch
            {

                // error
                AlertService.ShowAlert("Fehler", "Der Link konnte nicht geöffnet werden.");
            }
        }


    }
}
