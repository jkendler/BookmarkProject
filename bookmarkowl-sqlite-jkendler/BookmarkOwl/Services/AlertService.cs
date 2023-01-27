using BookmarkOwl.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkOwl.Services
{
    // https://stackoverflow.com/questions/72429055/how-to-displayalert-in-a-net-maui-viewmodel
    public class AlertService : IAlertService
    {
        // only on dispatcher thread
        public Task ShowAlertAsync(string title, string message)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        public void ShowAlert(string title, string message)
        {
            Application.Current.MainPage.Dispatcher.Dispatch(async () =>
            {
                await ShowAlertAsync(title, message);
            });
        }
    }
}
