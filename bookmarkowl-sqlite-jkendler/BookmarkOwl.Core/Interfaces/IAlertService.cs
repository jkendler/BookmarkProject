using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkOwl.Core.Interfaces
{
    // https://stackoverflow.com/questions/72429055/how-to-displayalert-in-a-net-maui-viewmodel
    public interface IAlertService
    {
        Task ShowAlertAsync(string title, string message);
        void ShowAlert(string title, string message);
    }
}
