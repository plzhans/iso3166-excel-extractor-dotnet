namespace ISO3166EE
{
    public class CountryItem
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public int Numeric { get; set; }

        public override string ToString()
        {
            //return string.Format("Country=[{0},{1},{2:000}] {3}, {4}]", Alpha2, Alpha3, Numeric, Name, Image);
            return $"Country=[{Alpha2},{Alpha3},{Numeric.ToString("000")}] {Name} : {Image}";
        }
    }
}
