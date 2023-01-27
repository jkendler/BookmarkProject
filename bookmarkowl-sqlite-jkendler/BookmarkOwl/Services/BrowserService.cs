using BookmarkOwl.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkOwl.Services
{
    public class BrowserService : IBrowserService
    {
        // only on dispatcher thread
        public Task OpenAsync(Uri uri)
        {
            return Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        public void Open(Uri uri)
        {
            Application.Current.MainPage.Dispatcher.Dispatch(async () =>
            {
                await OpenAsync(uri);
            });
        }
    }
}
