using Moq;
using SearchEngineer.Bll;
using SearchEngineer.Models;
using SearchEngineer.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SearchEngineer.Tests
{
    public class SearchKeywordBllTest
    {
        private SearchKeyword InitialSearchKeyword()
        {
            var searchKeyword = new SearchKeyword();
            searchKeyword.Id = Guid.NewGuid();
            searchKeyword.Keyword = "TestAdd";
            searchKeyword.SearchTimes = 1;

            return searchKeyword;
        }

        private bool InitialMockAdd(bool returnValue)
        {
            Mock<ISearchKeywordRepository> mockRepository = new Mock<ISearchKeywordRepository>();
            mockRepository.Setup(x => x.AddSearchKeyword(It.IsAny<SearchKeyword>())).Returns(Task.FromResult(returnValue));

            var searchKeywordBll = new SearchKeywordBll(mockRepository.Object);
            var searchKeyword = InitialSearchKeyword();
            var result = searchKeywordBll.AddSearchKeyword(searchKeyword);

            return result.Result;
        }

        private bool InitialMockDelete(bool returnValue)
        {
            var searchKeyword = InitialSearchKeyword();

            Mock<ISearchKeywordRepository> mockRepository = new Mock<ISearchKeywordRepository>();
            mockRepository.Setup(x => x.GetSearchKeywordByKeyword(It.IsAny<string>())).Returns(Task.FromResult(searchKeyword));
            mockRepository.Setup(x => x.DeleteSearchKeyword(It.IsAny<SearchKeyword>())).Returns(Task.FromResult(returnValue));

            var searchKeywordBll = new SearchKeywordBll(mockRepository.Object);
            var result = searchKeywordBll.DeleteSearchKeyword("hello");

            return result.Result;
        }

        [Fact]
        public void ShouldReturnTrueAddSearchKeyword()
        {
            var result = InitialMockAdd(true);

            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalseAddSearchKeyword()
        {
            var result = InitialMockAdd(false);

            Assert.False(result);
        }

        [Fact]
        public void ShouldReturnTrueDeleteSearchKeyword()
        {
            var result = InitialMockDelete(true);

            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalseDeleteSearchKeyword()
        {
            var result = InitialMockDelete(false);

            Assert.False(result);
        }

        [Fact]
        public void ShouldReturnSearchKeywordGetSearchKeywordByKeyword()
        {
            var searchKeyword = InitialSearchKeyword();

            Mock<ISearchKeywordRepository> mockRepository = new Mock<ISearchKeywordRepository>();
            mockRepository.Setup(x => x.GetSearchKeywordByKeyword(It.IsAny<string>())).Returns(Task.FromResult(searchKeyword));

            var searchKeywordBll = new SearchKeywordBll(mockRepository.Object);
            var result = searchKeywordBll.GetSearchKeywordByKeyword("TestAdd");

            Assert.Equal("TestAdd", result.Result.Keyword);
        }

        [Fact]
        public void ShouldReturnNullGetSearchKeywordByKeyword()
        {
            var searchKeyword = InitialSearchKeyword();

            Mock<ISearchKeywordRepository> mockRepository = new Mock<ISearchKeywordRepository>();
            mockRepository.Setup(x => x.IsKeywordExisting(It.IsAny<string>())).Returns(Task.FromResult(true));

            var searchKeywordBll = new SearchKeywordBll(mockRepository.Object);
            var result = searchKeywordBll.GetSearchKeywordByKeyword("TestAdd");

            Assert.Null(result.Result);
        }

        [Fact]
        public void ShouldReturnListGetTopSearchKeyword()
        {
            var searchKeyword = InitialSearchKeyword();
            var tempList = new List<SearchKeyword>();
            IEnumerable<SearchKeyword> searchKeywordList = tempList;

            Mock<ISearchKeywordRepository> mockRepository = new Mock<ISearchKeywordRepository>();
            mockRepository.Setup(x => x.GetTopSearchKeyword(It.IsAny<int>())).Returns(Task.FromResult(searchKeywordList));

            var searchKeywordBll = new SearchKeywordBll(mockRepository.Object);
            var result = searchKeywordBll.GetTopSearchKeyword();

            Assert.NotNull(result.Result);
        }
    }
}
