using System.Collections.Generic;

namespace Lab_9.Models
{
    public class RequestOptions
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Sort { get; set; }
        public string Like { get; set; }
        public string GlobalLike { get; set; }
        public int MinId { get; set; }
        public int MaxId { get; set; }
        public string Columns { get; set; }

        public RequestOptions()
        {
            Limit = 2;
            Offset = 0;
            MinId = 0;
            MaxId = 100;
            Sort = "ID";
        }
    }
}