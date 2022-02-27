using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineer.Models
{
    public class SearchKeyword
    {
        public Guid Id { get; set; }
        public string Keyword { get; set; }
        public int SearchTimes { get; set; } = 1;
    }
}
