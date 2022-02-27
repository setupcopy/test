using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using SearchEngineer.Dtos;
using SearchEngineerBackendForFrontend.Dtos;
using SearchEngineerBackendForFrontend.Services;
using SearchEngineerProto;
using SearchEngineerUtility.DataValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineerBackendForFrontend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchKeywordController: ControllerBase
    {
        private readonly ISearchKeywordService _searchKeywordService;

        public SearchKeywordController(ISearchKeywordService searchKeywordService)
        {
            _searchKeywordService = searchKeywordService;
        }

        [HttpGet("{keyword}")]
        public async Task<IActionResult> SearchByKeyword([FromRoute] string keyword)
        {
            //Whether keyword is null or empty
            if (String.IsNullOrWhiteSpace(keyword))
            {
                return StatusCode((int)ReplyStatusCodes.Invalid_Data);
            }

            //Validation keyword
            if (!_searchKeywordService.ValidationKeyword(keyword))
            {
                return StatusCode((int)ReplyStatusCodes.Invalid_Data);
            }

            var result = await _searchKeywordService.GetSearchKeywordByKeyword(keyword);

            if (result == null)
            {   
                return StatusCode((int)ReplyStatusCodes.Internal_Serice_Error);
            }

            //return dto
            var searchForSingleWordResponseDto = new SearchForSingleWordResponseDto();
            searchForSingleWordResponseDto.Keyword = result.Keyword;

            return StatusCode(result.StatusCode, searchForSingleWordResponseDto);
        }

        [HttpGet]
        public async Task<IActionResult> SearchTopKeywords()
        {
            var result = await _searchKeywordService.GetTopSearchKeyword();

            if (result == null)
            {
                return StatusCode((int)ReplyStatusCodes.Internal_Serice_Error);
            }

            return StatusCode(result.StatusCode, result.TopKeywords);
        }

        [HttpPost]
        public async Task<IActionResult> AddSearchTopKeywords([FromBody] SearchKeywordAddRequestDto searchKeywordAddDto)
        {
            //Whether keyword is null or empty
            if (String.IsNullOrWhiteSpace(searchKeywordAddDto.Keyword))
            {
                return StatusCode((int)ReplyStatusCodes.Invalid_Data);
            }

            //Validation keyword
            if (!_searchKeywordService.ValidationKeyword(searchKeywordAddDto.Keyword))
            {
                return StatusCode((int)ReplyStatusCodes.Invalid_Data);
            }

            var result = await _searchKeywordService.AddSearchTopKeywords(searchKeywordAddDto.Keyword);

            if (result == null)
            {
                return StatusCode((int)ReplyStatusCodes.Internal_Serice_Error);
            }

            return StatusCode(result.StatusCode);
        }

        [HttpDelete("{keyword}")]
        public async Task<IActionResult> DeleteSearchTopKeywords([FromRoute] string keyword)
        {
            //Whether keyword is null or empty
            if (String.IsNullOrWhiteSpace(keyword))
            {
                return StatusCode((int)ReplyStatusCodes.Invalid_Data);
            }

            //Validation keyword
            if (!_searchKeywordService.ValidationKeyword(keyword))
            {
                return StatusCode((int)ReplyStatusCodes.Invalid_Data);
            }

            var result = await _searchKeywordService.DeleteSearchTopKeywords(keyword);

            if (result == null)
            {
                return StatusCode((int)ReplyStatusCodes.Internal_Serice_Error);
            }
            
            return StatusCode(result.StatusCode);
        }
    }
}
