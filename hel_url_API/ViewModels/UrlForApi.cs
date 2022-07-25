using hey_url_API.Helper;
using hey_url_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace hey_url_API.ViewModels
{
    [Serializable]
    public class UrlForApi
    {
        public UrlForApi(Url url)
        {

            //included 
            UrlForApi_included_data included_data = new UrlForApi_included_data(url.UrlId, "...");
            UrlForApi_included included  = new UrlForApi_included(new List<UrlForApi_included_data> { included_data });

            //relation ships
            UrlForApi_data_data_relationships_clicksData relationships_clicksData = new UrlForApi_data_data_relationships_clicksData(url.UrlId);
            UrlForApi_data_data_relationships_clicks relationships_clicks = new UrlForApi_data_data_relationships_clicks(new List<UrlForApi_data_data_relationships_clicksData> { relationships_clicksData });
            UrlForApi_data_data_relationships relationships = new UrlForApi_data_data_relationships(relationships_clicks);

            //data
            UrlForApi_data_data_attributes _data_attributes = new UrlForApi_data_data_attributes(url.CretionDate, url.OrinalUrl,url.ShortUrl, url.Count);
            UrlForApi_data_data data_data = new UrlForApi_data_data(url.UrlId, _data_attributes, relationships);
            UrlForApi_data data = new UrlForApi_data(new List<UrlForApi_data_data> { data_data });

            this.data = data_data;
            this.included = included;
        }
        public UrlForApi_data_data data { get; set; }
        public  UrlForApi_included included { get; set; }

    }
    [Serializable]
    public class UrlForApi_data
    {
        public UrlForApi_data(List<UrlForApi_data_data> data)
        {
            this.data = data;
        }
        public List<UrlForApi_data_data> data { get; set; }
    }
    [Serializable]
    public class UrlForApi_data_data
    {
        public UrlForApi_data_data(int id, UrlForApi_data_data_attributes attributes, UrlForApi_data_data_relationships relationships)
        {
            this.id = id;
            this.attributes = attributes;
            this.relationships = relationships;
            type = "urls";
        }
        public string type { get; set; }
        public int id { get; set; }
        public UrlForApi_data_data_attributes attributes { get; set; }
        public UrlForApi_data_data_relationships relationships { get; set; }
    }
    [Serializable]
    public class UrlForApi_data_data_attributes
    {
        public UrlForApi_data_data_attributes(DateTime CreatedAt, string originaUrl, string url, int clicks)
        {
            this.CreatedAt = CreatedAt;
            this.originaUrl = originaUrl;
            this.url = GlobalValues.BaseUrl + url;
            this.clicks = clicks;
        }

        [XmlElement(ElementName = "created-at")]
        public DateTime CreatedAt;

        [XmlElement(ElementName ="original-Url")]
        public string originaUrl { get; set; }

        public string url { get; set; }

        public int clicks { get; set; }
    }
    [Serializable]
    public class UrlForApi_data_data_relationships
    {
        public UrlForApi_data_data_relationships(UrlForApi_data_data_relationships_clicks clicks)
        {
            this.clicks = clicks;
        }
        public UrlForApi_data_data_relationships_clicks clicks { get; set; }
    }

    [Serializable]
    public class UrlForApi_data_data_relationships_clicks
    {
        public UrlForApi_data_data_relationships_clicks(List<UrlForApi_data_data_relationships_clicksData> data)
        {
            this.data = data;
        }
        public List<UrlForApi_data_data_relationships_clicksData> data { get; set; }
    }
    [Serializable]
    public class UrlForApi_data_data_relationships_clicksData
    {
        public UrlForApi_data_data_relationships_clicksData(int id)
        {
            this.id = id;
            this.type = "Clicks";
        }
        public int id { get; set; }
        public string type { get; set; }
}
    [Serializable]
    public class UrlForApi_included
    {
        public UrlForApi_included(List<UrlForApi_included_data> data)
        {
            this.data = data;
        }
        [XmlElement(ElementName = "")]
        public List<UrlForApi_included_data> data { get; set; }
    }
    [Serializable]
    public class UrlForApi_included_data
    {
        public UrlForApi_included_data(int id, string atributes)
        {
            this.id = id;
            this.atributes = atributes;
            this.type = "Clicks";
        }
        public  string type { get; set; }
        public int id { get; set; }
        public string atributes { get; set; }

    }
}
