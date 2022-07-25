using hey_url_Model;
using hey_url_Service.Interfaces;
using HeyUrlChallengeCodeDotnet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hey_url_Service.Implementations
{
    public class UrlService : IUrlService
    {

        public ApplicationContext DBContext;

        public UrlService()
        {
        }
        public UrlService(ApplicationContext Context)
        {
            this.DBContext = Context;
        }


        public void Add(Url Url)
        {
            using (var tr = DBContext.Database.BeginTransaction())
            {
                try
                {
                    DBContext.Url.Add(Url);
                    DBContext.SaveChanges();
                    Url.ShortUrl = Helpers.HashCalculator.CalculateHash(Url.UrlId);
                    DBContext.Url.Update(Url);
                    DBContext.SaveChanges();
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    throw new Exception("Error when saving data : " + ex.Message);
                }
            }
        }

        public Url Get(int urlid)
        {
            return DBContext.Url.FirstOrDefault(f => f.UrlId == urlid);
        }

        public Url Get(string shorUrl)
        {
            return DBContext.Url.FirstOrDefault(f => f.ShortUrl  == shorUrl);
        }

        public IEnumerable<Url> GetAll(int Limit)
        {
            if (Limit<0)
            {
                return DBContext.Url.OrderByDescending(o => o.UrlId);
            }
            else
            {
                return DBContext.Url.OrderByDescending(o => o.UrlId).TakeLast(Limit).ToList();
            }
            
        }
    }
}
