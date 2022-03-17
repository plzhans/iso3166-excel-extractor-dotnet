using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISO3166EE
{
    public class CountryBuilder
    {
        private ConfigSettings _config;

        public CountryBuilder(ConfigSettings config)
        {
            _config = config;
        }

        public IList<Country> Build()
        {
            var usCountryItems = LoadHttpUrlAsync(_config.Us);
            if (_config.Ko != null)
            {
                var map = usCountryItems.ToDictionary(item => item.Alpha2, item => new Country { Us = item });

                var koCountryItems = LoadHttpUrlAsync(_config.Ko);
                foreach (var item in koCountryItems)
                {
                    if(map.TryGetValue(item.Alpha2, out var country) == true)
                    {
                        country.Kor = item;
                    }
                }
                return map.Select(x=>x.Value).OrderBy(x=>(x.Kor != null ? x.Kor.Name : x.Us.Name)).ToList();
            } 
            else
            {
                return usCountryItems.Select(item => new Country { Us = item }).OrderBy(x=>x.Us.Name).ToList();
            }
        }

        private IList<CountryItem> LoadHttpUrlAsync(ConfigSettings.LangSection config)
        {
            var uri = new Uri(config.url);
            var prefixScheme = uri.Scheme + ":";

            var htmlWeb = new HtmlWeb();
            var htmlDoc = htmlWeb.Load(uri);
            var trNodes = htmlDoc.DocumentNode.SelectNodes(config.table.body.selector);

            var countryList = new List<CountryItem>();
            foreach (var trNode in trNodes)
            {
                var tdName = trNode.SelectSingleNode(config.table.body.name);
                if (tdName == null)
                {
                    continue;
                }

                var country = new CountryItem();
                country.Name = tdName.InnerText;
                country.Numeric = int.Parse(trNode.SelectSingleNode(config.table.body.numeric).InnerText?.Replace("\n", ""));
                country.Alpha2 = trNode.SelectSingleNode(config.table.body.alpha2).InnerText?.Replace("\n", "");
                country.Alpha3 = trNode.SelectSingleNode(config.table.body.alpha3).InnerText?.Replace("\n", "");
                country.Image = trNode.SelectSingleNode(config.table.body.image)?.GetAttributeValue("src", null);
                if (country.Image?.StartsWith("//") == true)
                {
                    country.Image = prefixScheme + country.Image;
                }
                countryList.Add(country);
            }

            return countryList;
        }

        public string GetHeaderCsv()
        {
            return string.Join(Environment.NewLine, _config.Head.columns, _config.Head.columns2);
        }

    }
}
