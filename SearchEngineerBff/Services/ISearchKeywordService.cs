using SearchEngineerProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineerBackendForFrontend.Services
{
    public interface ISearchKeywordService
    {
        Task<SearchBykeywordReply> GetSearchKeywordByKeyword(string keyword);
        Task<SearchTopKeywordsReply> GetTopSearchKeyword();

        Task<ADStatusReply> AddSearchTopKeywords(string keyword);
        Task<ADStatusReply> DeleteSearchTopKeywords(string keyword);

        bool ValidationKeyword(string keyword);
    }
}
