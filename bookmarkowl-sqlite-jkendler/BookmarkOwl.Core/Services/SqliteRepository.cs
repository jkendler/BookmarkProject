using BookmarkOwl.Core.Interfaces;
using BookmarkOwl.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookmarkOwl.Core.Services;

public class SqliteRepository : IRepository
{
    string DbPath { get; } = string.Empty;

    public SqliteRepository(string dbPath)
    {
        this.DbPath = dbPath;
    }


    public async Task<bool> Add(LinkEntry entry)
    {
        try
        {
            using(var context = new BookmarkContext(this.DbPath))
            {
                context.Add(entry);
                await context.SaveChangesAsync();
            }

            

            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }

    public async Task<List<LinkEntry>> All()
    {
        try
        {
            using (var context = new BookmarkContext(this.DbPath))
            {
                var links = await (from entry in context.Links
                                   select entry).ToListAsync();

                return links;
            }
        }
        catch (Exception)
        {

            return new List<LinkEntry>();
        }
    }

    

    public async Task<bool> DeleteById(long id)
    {
        try
        {
            using (var context = new BookmarkContext(this.DbPath))
            {
                
                await context.Database.ExecuteSqlRawAsync("DELETE FROM Links WHERE Id={0}", id);
            }



            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }

    public async Task<LinkEntry?> FindById(long id)
    {
        try
        {
            using (var context = new BookmarkContext(this.DbPath))
            {

                var link = await (from entry in context.Links
                                  where entry.Id == id
                                  select entry).FirstOrDefaultAsync();
                return link;
            }
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> Update(LinkEntry entry)
    {
        try
        {
            using (var context = new BookmarkContext(this.DbPath))
            {
                context.Entry(entry).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
        }
        catch
        {

            return false;
        }
    }
}
