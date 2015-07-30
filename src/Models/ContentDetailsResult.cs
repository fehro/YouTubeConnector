using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeConnector.Models
{
    public class ContentDetailsResult
    {
        #region Public Properties

        public PageInfo PageInfo { get; set; }

        public List<ContentDetailsItem> Items { get; set; }

        #endregion
    }
}
