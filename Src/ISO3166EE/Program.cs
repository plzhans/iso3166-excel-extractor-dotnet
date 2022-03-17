using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ISO3166EE
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MainStart(args);
            }
            catch(Exception e)
            {
                Console.WriteLine("Parse error.");
                Console.WriteLine(e);
            }
        }

        static void MainStart(string[] args)
        {
            
            var configurationBuilder = new ConfigurationBuilder();

            string filePath = "config.ini";
            if( File.Exists(filePath) != true)
            {
                throw new FileNotFoundException("Config file notfound.", filePath);
            }
            configurationBuilder.AddIniFile(filePath);

            var configuration = configurationBuilder.Build();
            var config = configuration.Get<ConfigSettings>();

            var countryBuilder = new CountryBuilder(config);
            var countryList = countryBuilder.Build();

            string csvPath = "country_iso3166.csv";
            try
            {
                if (countryList.Count > 0)
                {
                    File.WriteAllLines(
                        csvPath,
                        new string[] { countryBuilder.GetHeaderCsv() }.Union(countryList.Select(x => x.ToCSV())),
                        Encoding.UTF8
                    );

                    Console.WriteLine($"File created. {csvPath}");
                }
                else
                {
                    File.Delete(csvPath);
                    Console.WriteLine("Country data empty.");
                }
            }
            catch(Exception e)
            {
                File.Delete(csvPath);
                Console.WriteLine("Country csv export error.");
                Console.WriteLine(e);
            }
        }
    }
}
