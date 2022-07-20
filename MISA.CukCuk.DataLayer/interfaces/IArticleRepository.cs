using API.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.DataLayer.interfaces
{
    public interface IArticleRepository : IDbContext<article>
    {
        IEnumerable<article> GetArticleById(Guid articleId);
        IEnumerable<article> GetArticleByCategoryID(Guid categoryID);
        IEnumerable<article> GetArticleBySearchText(string searchText);
    }
}
