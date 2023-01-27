using System;
namespace BookmarkOwl.Core.Interfaces
{
	public interface IPreferencesService
	{
		void Set(string key, string value);

		string Get(string key);

		void Clear()
;
	}
}

