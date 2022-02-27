using SearchEngineerProto;
using SearchEngineerUtility.DataValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SearchEngineerProto.ISearchKeyword;

namespace SearchEngineerBackendForFrontend.Services
{
    public class SearchKeywordService : ISearchKeywordService
    {
        private readonly ISearchKeywordClient _searchKeywordClient;
        public SearchKeywordService(ISearchKeywordClient searchKeywordClient)
        {
            _searchKeywordClient = searchKeywordClient;
        }

        /// <summary>
        /// Add a specific search keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<ADStatusReply> AddSearchTopKeywords(string keyword)
        {
            try
            {
                var result = await _searchKeywordClient.AddSearchTopKeywordsAsync(new SADRequest
                {
                    Keyword = keyword
                });
                
                return result;
            }
            catch (Exception ex)
            {
                //If I have enough time,I will do exception filters to handle errors.
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete a specific search Keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<ADStatusReply> DeleteSearchTopKeywords(string keyword)
        {
            try
            {
                var result = await _searchKeywordClient.DeleteSearchTopKeywordsAsync(new SADRequest
                {
                    Keyword = keyword
                });

                return result;
            }
            catch (Exception ex)
            {
                //If I have enough time,I will do exception filters to handle errors.
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Getting an entity of SearchKeyword in terms of keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<SearchBykeywordReply> GetSearchKeywordByKeyword(string keyword)
        {
            try
            {
                var result = await _searchKeywordClient.SearchByKeywordAsync(new SADRequest
                {
                    Keyword = keyword
                });

                return result;
            }
            catch (Exception ex)
            {
                //If I have enough time,I will do exception filters to handle errors.
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Getting a list of top kewwords
        /// </summary>
        /// <returns></returns>
        public async Task<SearchTopKeywordsReply> GetTopSearchKeyword()
        {
            try
            {
                //Empty param
                var request = new Google.Protobuf.WellKnownTypes.Empty();
                var result = await _searchKeywordClient.SearchTopKeywordsAsync(request);

                return result;
            }
            catch (Exception ex)
            {
                //If I have enough time,I will do exception filters to handle errors.
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// validation data
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public bool ValidationKeyword(string keyword)
        {
            return InputValidation.KeywordValidation(keyword);
        }
    }
}
