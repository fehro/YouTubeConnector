using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeConnector.Models
{
    public class Thumbnails
    {
        #region Public Properties

        public Thumbnail Default { get; set; }

        public Thumbnail Medium { get; set; }

        public Thumbnail High { get; set; }

        #endregion
    }
}
