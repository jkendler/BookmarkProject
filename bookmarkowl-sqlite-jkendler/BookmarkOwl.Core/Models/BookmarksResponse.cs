using System;
namespace BookmarkOwl.Core.Models
{
	public class BookmarksResponse
	{
		public bool Status { get; set; }

		public List<LinkEntry> Bookmarks { get; set; }

	}
}

