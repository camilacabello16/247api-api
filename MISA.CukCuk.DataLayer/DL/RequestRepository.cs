using API.Common.Models;
using API.DataLayer.interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace API.DataLayer.DL
{
    public class RequestRepository : DbContext<request>, IRequestRepository
    {
        public IEnumerable<request> GetRequestByArticle(Guid articleId)
        {
            var data = _dbConnection.Query<request>($"SELECT * FROM request WHERE ArticleID = '{articleId}'", commandType: CommandType.Text);
            return data;
        }

        public int DeleteRequestByArticleID(Guid id)
        {
            var res = _dbConnection.Execute($"DELETE FROM request WHERE ArticleID = @id", new { id=id.ToString()}, commandType: CommandType.Text);
            return res;
        }

        public bool FindRequestByID(string requestID)
        {
            var data = _dbConnection.Query<request>($"SELECT * FROM request WHERE RequestID = '{requestID}'", commandType: CommandType.Text);
            var res = data as List<request>;
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
