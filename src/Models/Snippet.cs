using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeConnector.Models
{
    public class Snippet
    {
        #region Public Properties

        public DateTime PublishedAt { get; set; }

        public string ChannelId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Thumbnails Thumbnails { get; set; }

        public string ChannelTitle { get; set; }

        #endregion
    }
}
