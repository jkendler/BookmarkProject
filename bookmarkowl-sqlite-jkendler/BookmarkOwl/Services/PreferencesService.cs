using System;
using BookmarkOwl.Core.Interfaces;

namespace BookmarkOwl.Services
{
    public class PreferencesService : IPreferencesService
    {
        public void Clear()
        {
            Preferences.Clear();
        }

        public string Get(string key)
        {
            return Preferences.Get(key, string.Empty);
        }

        public void Set(string key, string value)
        {
            Preferences.Set(key, string.Empty);
        }
    }
}

