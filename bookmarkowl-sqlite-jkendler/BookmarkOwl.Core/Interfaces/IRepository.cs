using BookmarkOwl.Core.Models;

namespace BookmarkOwl.Core.Interfaces;

public interface IRepository
{
    // create
    Task<bool> Add(LinkEntry entry);


    // read
    Task<LinkEntry?> FindById(long id);
    Task<List<LinkEntry>> All();

    // update
    Task<bool> Update(LinkEntry entry);

    // delete
    Task<bool> DeleteById(long id);

}