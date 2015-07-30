using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeConnector.Models
{
    public class Item
    {
        #region Public Properties

        public Id Id { get; set; }

        public Snippet Snippet { get; set; }

        public ContentDetails ContentDetails { get; set; }

        #endregion
    }
}
