using Microsoft.EntityFrameworkCore;
using SearchEngineer.Database;
using SearchEngineer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineer.Repositorys
{
    public class SearchKeywordRepository : BaseRepository, ISearchKeywordRepository
    {
        public SearchKeywordRepository(AppDbContext context):base(context)
        {
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
                await base.Insert<SearchKeyword>(searchKeyword);
                var result = await base.CommitAsync();
                if (result <= 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            };
        }

        /// <summary>
        /// Getting an entity of SearchKeyword in terms of keyword
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <returns>SearchKeyword if nothing is found ,return null</returns>
        public async Task<SearchKeyword> GetSearchKeywordByKeyword(string keyword)
        {
            try
            {
                var result = base.Query<SearchKeyword>(s => s.Keyword.Equals(keyword, 
                                StringComparison.InvariantCultureIgnoreCase));
                
                if (result.Count() <= 0)
                {
                    return null;
                }

                return await result.FirstAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Getting a list of top search keywords
        /// </summary>
        /// <param name="topNumber">numbers of rows you want to get</param>
        /// <returns>list</returns>
        public async Task<IEnumerable<SearchKeyword>> GetTopSearchKeyword(int topNumber)
        {
            try
            {
                //SearchTimes descending order,and then get top numbers of row in terms of topNumber
                return await _context.SearchKeywords.OrderByDescending(s => s.SearchTimes).Take(topNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update a specific search Keyword
        /// </summary>
        /// <param name="searchKeyword">an entity of SearchKeyword</param>
        /// <returns>true:success false:failed</returns>
        public async Task<bool> UpdateSearchKeyword(SearchKeyword searchKeyword)
        {
            try
            {
                base.Update<SearchKeyword>(searchKeyword);
                var result = await base.CommitAsync();
                if (result <= 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Checking whether keyword is existing in list of searchkeyword
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <returns>true:existing  false: not existing</returns>
        public async Task<bool> IsKeywordExisting(string keyword)
        {
            try
            {
                return await base.Any<SearchKeyword>(s => s.Keyword.Equals(keyword,
                    StringComparison.InvariantCultureIgnoreCase));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete a specific search Keyword
        /// </summary>
        /// <param name="searchKeyword">an entity of SearchKeyword</param>
        /// <returns>true:success false:failed</returns>
        public async Task<bool> DeleteSearchKeyword(SearchKeyword searchKeyword)
        {
            try
            {
                base.Delete<SearchKeyword>(searchKeyword);
                var result = await base.CommitAsync();
                if (result <= 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
