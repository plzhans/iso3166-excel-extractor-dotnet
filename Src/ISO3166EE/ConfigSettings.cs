namespace ISO3166EE
{
    public class ConfigSettings
    {
        public class HeadSection
        {
            public string columns { get; set; }
            public string columns2 { get; set; }
        }
        public HeadSection Head { get; set; }

        public class LangSection
        {
            public string url { get; set; }
            public class TableSection
            {
                public class BodySection
                {
                    public string selector { get; set; }
                    public string name { get; set; }
                    public string image { get; set; }
                    public string alpha2 { get; set; }
                    public string alpha3 { get; set; }
                    public string numeric { get; set; }
                }
                public BodySection body { get; set; }
            }
            public TableSection table { get; set; }

        }
        public LangSection Us { get; set; }
        public LangSection Ko { get; set; }
    }
}
