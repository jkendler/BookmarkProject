using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkOwl.Core.Interfaces
{
    public interface IBrowserService
    {
        Task OpenAsync(Uri uri);
        void Open(Uri uri);
    }
}
