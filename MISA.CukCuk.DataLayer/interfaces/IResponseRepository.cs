using API.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.DataLayer.interfaces
{
    public interface IResponseRepository : IDbContext<response>
    {
        IEnumerable<response> GetResponseByArticle(Guid articleId);
        int DeleteResponseByArticleID(Guid id);
        bool FindResponseByID(string responseID);
    }
}
