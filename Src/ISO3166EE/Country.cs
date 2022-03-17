namespace ISO3166EE
{
    public class Country
    {
        public CountryItem Us { get; set; }
        public CountryItem Kor { get; set; }

        public override string ToString()
        {
            CountryItem item = Kor;
            if(item == null)
            {
                item = Us;
            }
            //return string.Format("Country=[{0},{1},{2:000}] {3}, {4}]", Alpha2, Alpha3, Numeric, Name, Image);
            return $"Country=[{item.Alpha2},{item.Alpha3},{item.Numeric.ToString("000")}] {item.Name} : {item.Image}";
        }

        public string ToCSV()
        {
            return string.Join(",", 
                Us.Alpha2,
                Us.Alpha3, 
                Us.Numeric, 
                Us.Name != null ? "\"" + Us.Name + "\"" : "",
                Kor?.Name != null ? "\"" +Kor?.Name+"\"" : "",
                Us.Image != null ? "\"" +Us.Image+"\"" : ""
            );
        }
    }
}
