using AutoMapper;
using SearchEngineer.Models;
using SearchEngineerProto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineer.Profiles
{
    public class SearchKeywordProfile:Profile
    {
        public SearchKeywordProfile()
        {
            CreateMap<SearchKeyword, TopKeywords>();

            //automaticlly generate Id and SearchTimes
            CreateMap<SADRequest, SearchKeyword>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                )
                .ForMember(
                    dest => dest.SearchTimes,
                    opt => opt.MapFrom(src => 1)
                );
        }
    }
}
