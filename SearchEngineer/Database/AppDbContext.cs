using Microsoft.EntityFrameworkCore;
using SearchEngineer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineer.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            if (SearchKeywords.Count() == 0)
            {
                InitialDefaultData();
            }           
        }

        public DbSet<SearchKeyword> SearchKeywords { get; set; }

        private void InitialDefaultData()
        {
            List<string> defaultData = new List<string>()
            {
                new string ("hello"),
                new string ("goodbye"),
                new string ("simple"),
                new string ("list"),
                new string ("search"),
                new string ("filter"),
                new string ("yes"),
                new string ("no"),
            };

            foreach (var data in defaultData)
            {
                SearchKeyword searchKeyword = new SearchKeyword();
                searchKeyword.Id = Guid.NewGuid();
                searchKeyword.Keyword = data;

                SearchKeywords.Add(searchKeyword);
            }

            base.SaveChanges();
        }
    }
}
