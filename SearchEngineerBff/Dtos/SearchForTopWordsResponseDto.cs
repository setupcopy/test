using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineer.Dtos
{
    public class SearchForTopWordsResponseDto
    {
        public string Keyword { get; set; }
        public int SearchTimes { get; set; }
    }
}
