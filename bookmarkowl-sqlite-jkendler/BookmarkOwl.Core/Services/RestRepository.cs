using System;
using BookmarkOwl.Core.Interfaces;
using BookmarkOwl.Core.Models;
using RestSharp;

namespace BookmarkOwl.Core
{
    public class RestRepository : IRepository, IRestEndpoint
    {
        RestClient Client { get; }

        public void SetEndpoint(string api, string token)
        {
            var options = new RestClientOptions(apiUrl)
            {
                ThrowOnAnyError = true,
                Timeout = 12000
            };

            this.Client = new RestClient(options);
            this.Client.AddDefaultHeaders(
                new Dictionary<string, string>()
                {

                {KnownHeaders.ContentType, "application/json" },
                {KnownHeaders.Accept, "application/json" },
                {KnownHeaders.Authorization, $"Bearer {token}" }

                });
        }

        public RestRepository(string apiUrl, string token)
        {

            SetEndpoint(apiUrl, token);
           
        }


        public async Task<bool> Add(LinkEntry entry)
        {
            try
            {
                var request = new RestRequest("bookmarks");

                request.AddJsonBody(entry);

                var result = await this.Client.PostAsync(request);

                return result.IsSuccessful;
            }
            catch
            {
                return false;
            }
        }

       

        public async Task<List<LinkEntry>> All()
        {
            try
            {
                var request = new RestRequest("bookmarks", Method.Get);

                var result = await this.Client.GetAsync<BookmarksResponse>(request);


                if (result.Status)
                {
                    return result.Bookmarks;
                }
                else
                {
                    return new List<LinkEntry>();
                }
            }
            catch
            {
                return new List<LinkEntry>();
            }



               


        }

        public async Task<bool> DeleteById(long id)
        {
            try
            {
                var request = new RestRequest($"bookmarks/{id})", Method.Delete);

                var result = await this.Client.DeleteAsync(request);

                return result.IsSuccessful;
            }

            catch
            {
                return false;
            }
        }

        public async Task<LinkEntry?> FindById(long id)
        {
            try
            {
                var request = new RestRequest("bookmarks", Method.Get);

                var result = await this.Client.GetAsync<BookmarksResponse>(request);

                if (result.Status)
                {
                    return result.Bookmarks.FirstOrDefault((link) => link.Id == id);
                }
                else
                {
                    return null;
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
                var request = new RestRequest($"bookmarks/{entry.Id}");

                request.AddJsonBody(entry);

                var result = await this.Client.PutAsync(request);

                return result.IsSuccessful;
            }
            catch
            {
                return false;
            }
        }

        
    }
}

