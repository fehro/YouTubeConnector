using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeConnector.Contracts;
using YouTubeConnector.Models;
using System.Web;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls.Expressions;

namespace YouTubeConnector
{
    public class Connector : IConnector
    {
        #region Global Variables / Propertes

        private readonly string _apiKey;

        private const string YouTubeApiSearchUrl = "https://www.googleapis.com/youtube/v3/search?";

        private const string YouTubeApiContentDetailsUrl = "https://www.googleapis.com/youtube/v3/videos?";

        #endregion

        #region Constructor

        public Connector(string apiKey)
        {
            _apiKey = apiKey;
        }

        #endregion

        #region Implemented IConnector Members

        /// <summary>
        /// Search YouTube with the provided values.
        /// A search where includeContentDetails is false will cost 100 points.
        /// A search where includeContentDetails is true will cost 103 points.
        /// </summary>
        /// <param name="searchQuery">The search query string.</param>
        /// <param name="includeContentDetails">Inlude the content details.</param>
        /// <param name="totalResults">The total available results.</param>
        public IEnumerable<Item> Search(string searchQuery, bool includeContentDetails, out int totalResults)
        {
            //Build the search url.
            var url = BuildSearchUrl(searchQuery);

            //Get the response from the api.
            var response = GetFromUrl(url);

            //Deserialize the JSON.
            var searchResult = DeserializeJson<SearchResult>(response);

            //Get the content details.
            if (includeContentDetails)
            {
                searchResult = GetContentDetails(searchResult);
            }

            //Set the total results.
            totalResults = searchResult.PageInfo.TotalResults;

            //Return the items.
            return searchResult.Items;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Deserialize the JSON data to an object.
        /// </summary>
        protected T DeserializeJson<T>(string json)
        {
            return new JavaScriptSerializer().Deserialize<T>(json);
        }

        /// <summary>
        /// Get the string response from the provided url with a GET request.
        /// </summary>
        protected string GetFromUrl(string url)
        {
            using (var client = new HttpClient())
            {
                return client.GetStringAsync(url).Result;
            }
        }

        /// <summary>
        /// Get the content details for the provided search result.
        /// </summary>
        protected SearchResult GetContentDetails(SearchResult searchResult)
        {
            //Build the content details url.
            var url = BuildContentDetailsUrl(searchResult.Items.Select(x => x.Id.VideoId).ToArray());

            //Get the response from the api.
            var response = GetFromUrl(url);

            //Deserialize the JSON.
            var contentDetailsResult = DeserializeJson<ContentDetailsResult>(response);

            //Merge the content detail results with the search results.
            searchResult = MergeContentDetailResults(contentDetailsResult, searchResult);

            return searchResult;
        }

        /// <summary>
        /// Merge the content detail results with the search results.
        /// </summary>
        protected SearchResult MergeContentDetailResults(ContentDetailsResult contentDetailsResult, SearchResult searchResult) 
        {
            foreach (var searchItem in searchResult.Items)
            {
                foreach (var contentDetailItem in contentDetailsResult.Items)
                {
                    if (searchItem.Id.VideoId == contentDetailItem.Id)
                    {
                        //Match has been found.
                        searchItem.ContentDetails = contentDetailItem.ContentDetails;

                        break;
                    }
                }
            }

            return searchResult;
        }

        /// <summary>
        /// Build the search url with the provided values.
        /// </summary>
        protected string BuildSearchUrl(string searchQuery)
        {
            var values = new List<KeyValuePair<string, string>>();

            //Build the url key value collection.
            values.Add(new KeyValuePair<string, string>("order", "relevance"));
            values.Add(new KeyValuePair<string, string>("part", "snippet"));
            values.Add(new KeyValuePair<string, string>("q", HttpUtility.UrlEncode(searchQuery)));
            values.Add(new KeyValuePair<string, string>("maxResults", "50"));
            values.Add(new KeyValuePair<string, string>("key", _apiKey));

            return YouTubeApiSearchUrl + string.Join("&", values.Select(x => x.Key + "=" + x.Value));
        }

        /// <summary>
        /// Build the content details url with the provided values.
        /// </summary>
        protected string BuildContentDetailsUrl(string[] ids)
        {
            var values = new List<KeyValuePair<string, string>>();

            //Build the url key value collection.
            values.Add(new KeyValuePair<string, string>("part", "contentDetails"));
            values.Add(new KeyValuePair<string, string>("id", string.Join(",", ids)));
            values.Add(new KeyValuePair<string, string>("key", _apiKey));

            return YouTubeApiContentDetailsUrl + string.Join("&", values.Select(x => x.Key + "=" + x.Value));
        }

        #endregion
    }
}
