using SearchEngineer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineer.Repositorys
{
    public interface ISearchKeywordRepository
    {
        Task<IEnumerable<SearchKeyword>> GetTopSearchKeyword(int topNumber);
        Task<SearchKeyword> GetSearchKeywordByKeyword(string keyword);

        Task<bool> IsKeywordExisting(string keyword);

        Task<bool> AddSearchKeyword(SearchKeyword searchKeyword);
        Task<bool> UpdateSearchKeyword(SearchKeyword searchKeyword);
        Task<bool> DeleteSearchKeyword(SearchKeyword searchKeyword);
    }
}
