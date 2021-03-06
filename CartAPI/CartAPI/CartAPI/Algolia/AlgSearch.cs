﻿
using Algolia.Search.Clients;
using Algolia.Search.Http;
using Algolia.Search.Iterators;
using Algolia.Search.Models.Common;
using Algolia.Search.Models.Search;
using CartAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CartAPI.Algolia
{
    public class AlgSearch
    {
        public List<Item> Search(string text)
        {
            SearchClient client = new SearchClient("BIW6EL1FTD", "8ae9274c008b76fdd8046bd43447044b");
            SearchIndex index = client.InitIndex("Items");
            var result = index.Search<Item>(new Query(text));

            return result.Hits;
        }

        public List<Item> RandomItems()
        {
            SearchClient client = new SearchClient("BIW6EL1FTD", "a5af55b1831c11747c108cc179f2d790");
            SearchIndex index = client.InitIndex("Items");
            IndexIterator<Item> indexIterator = index.Browse<Item>(new BrowseIndexQuery { });

            var hits = new List<Item>();

            int max = 0;
            foreach (var hit in indexIterator)
            {
                if(hit.ImageURL!="null")
                    hits.Add(hit);

                if (max > 100)
                    break;
                else
                    max++;
            }
            Random rnd = new Random();
            return hits.Where(t=>t.ImageURL.ToLower() !="null").OrderBy(x => rnd.Next()).Take(20).ToList();
        }

        public List<Item> Search(List<string> text)
        {
            // perform 3 queries in a single API call:
            //  - 1st query targets index `categories`
            //  - 2nd and 3rd queries target index `products`
            var indexQueries = new List<MultipleQueries>();
            foreach (var item in text)
            {
                indexQueries.Add(new MultipleQueries() { IndexName = "Items" ,Params = new Query(item) });
            }

            MultipleQueriesRequest request = new MultipleQueriesRequest
            {
                Requests = indexQueries
            };

            SearchClient client = new SearchClient("BIW6EL1FTD", "8ae9274c008b76fdd8046bd43447044b");
            SearchIndex index = client.InitIndex("Items");

            var res = client.MultipleQueries<Item>(request);

            List<Item> results = new List<Item>();

            foreach (var item in res.Results)
            {
                results = results.Union(item.Hits).ToList();
            };

            return results.Distinct(new ItemComparer()).ToList();
        }
    }
}