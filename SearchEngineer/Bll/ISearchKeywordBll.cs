using SearchEngineer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineer.Bll
{
    public interface ISearchKeywordBll
    {
        Task<IEnumerable<SearchKeyword>> GetTopSearchKeyword();
        Task<SearchKeyword> GetSearchKeywordByKeyword(string keyword);
        Task<bool> AddSearchKeyword(SearchKeyword searchKeyword);
        Task<bool> DeleteSearchKeyword(string keyword);

        bool ValidationKeyword(string keyword);
    }
}
