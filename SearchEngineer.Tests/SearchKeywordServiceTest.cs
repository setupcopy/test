using AutoMapper;
using Grpc.Core;
using Moq;
using SearchEngineer.Bll;
using SearchEngineer.Models;
using SearchEngineer.Repositorys;
using SearchEngineer.Services;
using SearchEngineer.Tests.Utilities;
using SearchEngineerProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SearchEngineer.Tests
{
    public class SearchKeywordServiceTest
    {
        private SearchKeywordService MockSearchKeywordService()
        {
            Mock<ISearchKeywordBll> mockBll = new Mock<ISearchKeywordBll>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            return new SearchKeywordService(mockBll.Object, mockMapper.Object);
        }

        private ADStatusReply InvalidData (string keyword)
        {
            ServerCallContext context = new MockServerCallContext();
            var request = new SADRequest();
            request.Keyword = keyword;

            Mock<ISearchKeywordBll> mockBll = new Mock<ISearchKeywordBll>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockBll.Setup(x => x.ValidationKeyword(It.IsAny<string>())).Returns(false);

            var searchKeywordService = new SearchKeywordService(mockBll.Object, mockMapper.Object);
            var result = searchKeywordService.AddSearchTopKeywords(request, context);

            return result.Result;
        }

        private ADStatusReply InitialMockAddSearchTopKeywords(bool resultValue)
        {
            ServerCallContext context = new MockServerCallContext();
            var request = new SADRequest();
            request.Keyword = "hello";
            var searchKeyword = new SearchKeyword();
            searchKeyword.Keyword = request.Keyword;

            Mock<ISearchKeywordBll> mockBll = new Mock<ISearchKeywordBll>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            mockBll.Setup(x => x.ValidationKeyword(It.IsAny<string>())).Returns(true);
            mockBll.Setup(x => x.AddSearchKeyword(It.IsAny<SearchKeyword>())).Returns(Task.FromResult(resultValue));
            mockMapper.Setup(x => x.Map<SearchKeyword>(It.IsAny<SADRequest>())).Returns(searchKeyword);

            var searchKeywordService = new SearchKeywordService(mockBll.Object, mockMapper.Object);
            var result = searchKeywordService.AddSearchTopKeywords(request, context);

            return result.Result;
        }

        [Fact]
        public void ShouldReturnOkAddSearchTopKeywords()
        {
            var result = InitialMockAddSearchTopKeywords(true);

            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void ShouldReturnFailedAddSearchTopKeywords()
        {
            var result = InitialMockAddSearchTopKeywords(false);

            Assert.Equal(423, result.StatusCode);
        }

        [Fact]
        public void ShouldReturnErrorWithInvalidDataAddSearchTopKeywords()
        {
            var result = InvalidData("H1");

            Assert.Equal(422, result.StatusCode);
        }

        [Fact]
        public void ShouldReturnErrorWithNullAddSearchTopKeywords()
        {
            var result = InvalidData("");

            Assert.Equal(422, result.StatusCode);
        }
    }
}
