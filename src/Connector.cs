using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeConnector.Contracts;
using YouTubeConnector.Models;

namespace YouTubeConnector
{
    public class Connector : IConnector
    {
        #region Global Variables / Propertes

        private readonly string _apiKey;

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
        /// </summary>
        /// <param name="searchQuery">The search query</param>
        public IEnumerable<SearchResult> Search(string searchQuery)
        {
            return new List<SearchResult>();
        }

        #endregion
    }
}
