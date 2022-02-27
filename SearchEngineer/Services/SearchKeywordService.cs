using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SearchEngineer.Bll;
using SearchEngineer.Models;
using SearchEngineerProto;
using SearchEngineerUtility.DataValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineer.Services
{
    public class SearchKeywordService:ISearchKeyword.ISearchKeywordBase
    {
        private readonly ISearchKeywordBll _searchKeywordBll;
        private readonly IMapper _mapper;

        public SearchKeywordService(ISearchKeywordBll searchKeywordBll
                                ,IMapper mapper)
        {
            _searchKeywordBll = searchKeywordBll;
            _mapper = mapper;
        }

        /// <summary>
        /// Add a specific search keyword
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ADStatusReply> AddSearchTopKeywords(SADRequest request, ServerCallContext context)
        {
            var reply = new ADStatusReply();

            //Whether keyword is null or empty
            if (String.IsNullOrWhiteSpace(request.Keyword))
            {
                reply.StatusCode = (int)ReplyStatusCodes.Invalid_Data;
                return reply;
            }

            //Validation keyword
            if (!_searchKeywordBll.ValidationKeyword(request.Keyword))
            {
                reply.StatusCode = (int)ReplyStatusCodes.Invalid_Data;
                return reply;
            }

            var searchKeyword = _mapper.Map<SearchKeyword>(request);

            var result = await _searchKeywordBll.AddSearchKeyword(searchKeyword);

            //Add failed
            if (!result)
            {
                reply.StatusCode = (int)ReplyStatusCodes.Add_Failed;
                return reply;
            }

            //success return
            reply.StatusCode = (int)ReplyStatusCodes.Ok;

            return reply;
        }

        /// <summary>
        /// Delete a specific search Keyword
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ADStatusReply> DeleteSearchTopKeywords(SADRequest request, ServerCallContext context)
        {
            var reply = new ADStatusReply();

            //Whether keyword is null or empty
            if (String.IsNullOrWhiteSpace(request.Keyword))
            {
                reply.StatusCode = (int)ReplyStatusCodes.Invalid_Data;
                return reply;
            }

            //Validation keyword
            if (!_searchKeywordBll.ValidationKeyword(request.Keyword))
            {
                reply.StatusCode = (int)ReplyStatusCodes.Invalid_Data;
                return reply;
            }

            var result = await _searchKeywordBll.DeleteSearchKeyword(request.Keyword);

            //No content
            if (!result)
            {
                reply.StatusCode = (int)ReplyStatusCodes.No_Content;
                return reply;
            }

            //success return
            reply.StatusCode = (int)ReplyStatusCodes.Ok;

            return reply;
        }

        /// <summary>
        /// Getting an entity of SearchKeyword in terms of keyword
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<SearchBykeywordReply> SearchByKeyword(SADRequest request, ServerCallContext context)
        {
            var reply = new SearchBykeywordReply();

            //Whether keyword is null or empty
            if (String.IsNullOrWhiteSpace(request.Keyword))
            {
                reply.StatusCode = (int)ReplyStatusCodes.Invalid_Data;
                return reply;
            }

            //Validation keyword
            if (!_searchKeywordBll.ValidationKeyword(request.Keyword))
            {
                reply.StatusCode = (int)ReplyStatusCodes.Invalid_Data;
                return reply;
            }
            var resultForSearch = await _searchKeywordBll.GetSearchKeywordByKeyword(request.Keyword);

            //No data return
            if (resultForSearch == null)
            {
                reply.StatusCode = (int)ReplyStatusCodes.No_Content;
                return reply;
            }

            //success return
            reply.Keyword = resultForSearch.Keyword;
            reply.StatusCode = (int)ReplyStatusCodes.Ok;

            return reply;
        }

        /// <summary>
        /// Getting a list of top kewwords
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<SearchTopKeywordsReply> SearchTopKeywords(Empty request, ServerCallContext context)
        {
            var reply = new SearchTopKeywordsReply();

            var resultForTopWords = await _searchKeywordBll.GetTopSearchKeyword();

            //No data return
            if (resultForTopWords.Count() <= 0 || resultForTopWords == null)
            {
                reply.StatusCode = (int)ReplyStatusCodes.No_Content;
                return reply;
            }

            //success return
            var topKeywords = _mapper.Map<IEnumerable<TopKeywords>>(resultForTopWords);
            reply.StatusCode = (int)ReplyStatusCodes.Ok;
            reply.TopKeywords.AddRange(topKeywords);

            return reply;
        }
    }
}
