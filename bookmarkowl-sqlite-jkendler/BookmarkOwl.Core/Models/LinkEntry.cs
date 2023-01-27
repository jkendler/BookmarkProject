using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkOwl.Core.Models
{
    public partial class LinkEntry : ObservableObject
    {

        public LinkEntry(string title, string destination)
        {
            this.Title = title;
            this.Destination = destination;
        }

        [ObservableProperty]
        string _title = string.Empty;

        [ObservableProperty]
        string _destination = string.Empty;

        [ObservableProperty]
        bool _favorite = false;

        [ObservableProperty]
        long _id = 0;


    }
}
