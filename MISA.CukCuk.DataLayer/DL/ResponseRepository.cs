using API.Common.Models;
using API.DataLayer.interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace API.DataLayer.DL
{
    public class ResponseRepository : DbContext<response>, IResponseRepository
    {
        public IEnumerable<response> GetResponseByArticle(Guid articleId)
        {
            var data = _dbConnection.Query<response>($"SELECT * FROM response WHERE ArticleID = '{articleId}'", commandType: CommandType.Text);
            return data;
        }

        public int DeleteResponseByArticleID(Guid id)
        {
            var res = _dbConnection.Execute($"DELETE FROM response WHERE ArticleID = '{id}'", commandType: CommandType.Text);
            return res;
        }

        public bool FindResponseByID(string responseID)
        {
            var data = _dbConnection.Query<response>($"SELECT * FROM response WHERE ResponseID = '{responseID}'", commandType: CommandType.Text);
            var res = data as List<response>;
            if (res.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
