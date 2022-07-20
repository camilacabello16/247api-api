using API.Common.Models;
using API.DataLayer.interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace API.DataLayer.DL
{
    public class ArticleRepository : DbContext<article>, IArticleRepository
    {
        public IEnumerable<article> GetArticleById(Guid articleId)
        {
            var data = _dbConnection.Query<article>($"SELECT * FROM article WHERE ArticleID = '{articleId}'", commandType: CommandType.Text);
            return data;
        }

        public IEnumerable<article> GetArticleByCategoryID(Guid categoryID)
        {
            var data = _dbConnection.Query<article>($"SELECT * FROM article WHERE CategoryID = '{categoryID}'", commandType: CommandType.Text);
            return data;
        }

        public IEnumerable<article> GetArticleBySearchText(string searchText)
        {
            var data = _dbConnection.Query<article>($"SELECT * FROM article WHERE ArticleName LIKE CONCAT('%','{searchText}','%')", commandType: CommandType.Text);
            return data;
        }
    }
}
