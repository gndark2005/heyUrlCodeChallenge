using hey_url_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace hey_url_Service.Interfaces
{
    public interface IUrlService
    {
        public void Add(Url Url);

        public Url Get(int urlid);

        public Url Get(string shorUrl);

        public IEnumerable<Url> GetAll(int Limit);

    }
}
