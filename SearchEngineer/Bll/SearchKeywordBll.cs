using SearchEngineer.Models;
using SearchEngineer.Repositorys;
using SearchEngineer.Utilities;
using SearchEngineerUtility.DataValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineer.Bll
{
    public class SearchKeywordBll : ISearchKeywordBll
    {
        private readonly ISearchKeywordRepository _searchKeywordRepository;

        public SearchKeywordBll(ISearchKeywordRepository searchKeywordRepository)
        {
            _searchKeywordRepository = searchKeywordRepository;
        }

        private async Task<bool> UpdateSearchKeyword(string keyword)
        {
            try
            {
                //initial entity of searchkeyword
                var searchKeyword = await _searchKeywordRepository.GetSearchKeywordByKeyword(keyword);

                if (searchKeyword == null)
                {
                    return false;
                }

                searchKeyword.SearchTimes++;

                return await _searchKeywordRepository.UpdateSearchKeyword(searchKeyword);
            }
            catch (Exception ex)
            {
                //If I have enough time,I will do exception filters to handle errors.
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add a specific search keyword
        /// </summary>
        /// <param name="searchKeyword">an entity of SearchKeyword</param>
        /// <returns>true:success flase:failed</returns>
        public async Task<bool> AddSearchKeyword(SearchKeyword searchKeyword)
        {
            try
            {
                //Keyword has been existing
                if (await _searchKeywordRepository.IsKeywordExisting(searchKeyword.Keyword))
                {
                    return false;
                }

                return await _searchKeywordRepository.AddSearchKeyword(searchKeyword);
            }
            catch (Exception ex)
            {
                //If I have enough time,I will do exception filters to handle errors.
                return false;
            }
        }

        /// <summary>
        /// Getting an entity of SearchKeyword in terms of keyword
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <returns>an entity of SearchKeyword</returns>
        public async Task<SearchKeyword> GetSearchKeywordByKeyword(string keyword)
        {
            try
            {
                bool reuslt = true;
                //if keyword has existed,updating times. otherwise adding
                if (await _searchKeywordRepository.IsKeywordExisting(keyword))
                {
                    reuslt = await UpdateSearchKeyword(keyword);
                }

                if (!reuslt)
                {
                    return null;
                }

                return await _searchKeywordRepository.GetSearchKeywordByKeyword(keyword);
            }
            catch (Exception ex)
            {
                //If I have enough time,I will do exception filters to handle errors.
                return null;
            }
        }

        /// <summary>
        /// Getting a list of top kewwords
        /// </summary>
        /// <returns>list</returns>
        public async Task<IEnumerable<SearchKeyword>> GetTopSearchKeyword()
        {
            try
            {
                return await _searchKeywordRepository.GetTopSearchKeyword(SearchGlobalStates.TopWordsNumber);
            }
            catch (Exception ex)
            {
                //If I have enough time,I will do exception filters to handle errors.
                return null;
            }
        }

        /// <summary>
        /// Delete a specific search Keyword
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <returns>true:success false:failed</returns>
        public async Task<bool> DeleteSearchKeyword(string keyword)
        {
            try
            {
                //initial entity of searchkeyword
                var searchKeyword = await _searchKeywordRepository.GetSearchKeywordByKeyword(keyword);

                if (searchKeyword == null)
                {
                    return false;
                }

                return await _searchKeywordRepository.DeleteSearchKeyword(searchKeyword);
            }
            catch (Exception ex)
            {
                //If I have enough time,I will do exception filters to handle errors.
                return false;
            }
        }

        /// <summary>
        /// Validation data
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public bool ValidationKeyword(string keyword)
        {
            return InputValidation.KeywordValidation(keyword);
        }
    }
}
