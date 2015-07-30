using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeConnector.Models;

namespace YouTubeConnector.Contracts
{
    public interface IConnector
    {
        IEnumerable<Item> Search(string searchQuery, out int totalResults);
    }
}
