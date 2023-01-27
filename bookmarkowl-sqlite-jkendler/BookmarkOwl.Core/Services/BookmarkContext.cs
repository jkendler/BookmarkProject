using BookmarkOwl.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookmarkOwl.Core.Services;

public class BookmarkContext : DbContext
{
    public DbSet<LinkEntry> Links { get; set; }

    string DbPath { get; } = string.Empty;

    public BookmarkContext(string dbPath)
    {
        DbPath = dbPath;
        SQLitePCL.Batteries_V2.Init();
        this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path
            .Combine(this.DbPath, "bookmarkowl.db3");

        Debug.WriteLine(dbPath);

        optionsBuilder.UseSqlite($"Filename={dbPath}");

        // base.OnConfiguring(optionsBuilder);
    }
}

