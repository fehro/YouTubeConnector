using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeConnector.Models
{
    public class SearchResult
    {
        #region Public Properties

        public string Id { get; set; }

        public string Title { get; set; }

        public string PublishedOn { get; set; }

        public string Description { get; set; }

        public string ChannelTitle { get; set; }

        public List<Thumbnail> Thumbnails { get; set; }

        #endregion
    }
}
