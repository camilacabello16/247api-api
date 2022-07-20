using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Common.Models;
using API.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataLayer.interfaces;
using Newtonsoft.Json.Linq;

namespace API.Controller.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArticleController : BaseController<article>
    {
        IBaseService<article> _baseService;
        IArticleRepository _articleRepository;
        IRequestRepository _requestRepository;
        IResponseRepository _responseRepository;
        public ArticleController(IBaseService<article> baseService, IArticleRepository articleRepository, IRequestRepository requestRepository, IResponseRepository responseRepository) : base(baseService)
        {
            _baseService = baseService;
            _articleRepository = articleRepository;
            _requestRepository = requestRepository;
            _responseRepository = responseRepository;
        }

        [HttpPost("insert-article")]
        public IActionResult PostArticle([FromBody] ArticleModel articleCreated)
        {
            var articleContent = articleCreated.articleApi;
            var articleRequest = articleCreated.requestApi;
            var articleResponse = articleCreated.responseApi;
            articleContent.ArticleID = Guid.NewGuid();
            var requestLength = articleCreated.requestApi.Count;
            for (int i = 0;i< requestLength; i++)
            {
                articleRequest[i].ArticleID = articleContent.ArticleID;
            }
            for(int i = 0; i < requestLength; i++){
                articleRequest[i].RequestID = Guid.NewGuid();
                _requestRepository.InsertData(articleRequest[i]);
            }
            var responseLength = articleCreated.responseApi.Count;
            for (int i = 0; i < responseLength; i++)
            {
                articleResponse[i].ArticleID = articleContent.ArticleID;
            }
            for (int i = 0; i < responseLength; i++)
            {
                articleResponse[i].ResponseID = Guid.NewGuid();
                _responseRepository.InsertData(articleResponse[i]);
            }
            _articleRepository.InsertData(articleContent);
            return StatusCode(201, "Created success");
        }

        [HttpGet("{articleId}")]
        public IActionResult GetArticleById([FromRoute] Guid articleId)
        {
            var res = _articleRepository.GetArticleById(articleId);
            var data = res as List<article>;
            if (data.Count == 0)
            {
                return StatusCode(204, res);
            }
            else
            {
                return StatusCode(200, res);
            }
        }

        [HttpGet("category/{categoryID}")]
        public IActionResult GetArticleByCategoryID([FromRoute] Guid categoryID)
        {
            var res = _articleRepository.GetArticleByCategoryID(categoryID);
            var data = res as List<article>;
            if (data.Count == 0)
            {
                return StatusCode(204, res);
            }
            else
            {
                return StatusCode(200, res);
            }
        }

        [HttpDelete("delete-article/{id}")]
        public IActionResult DeleteArticle(Guid id)
        {
            _requestRepository.DeleteRequestByArticleID(id);
            _responseRepository.DeleteResponseByArticleID(id);
            var res = _articleRepository.DeleteData(id);
            if (res > 0)
            {
                return StatusCode(201, res);
            }
            else
            {
                return StatusCode(200, res);
            }
        }

        [HttpPut("update-article")]
        public IActionResult PutArticle(ArticleModel articleModel)
        {
            var articleContent = articleModel.articleApi;
            var articleRequest = articleModel.requestApi;
            var articleResponse = articleModel.responseApi;
            var requestLength = articleModel.requestApi.Count;
            for (int i = 0; i < requestLength; i++)
            {
                if(_requestRepository.FindRequestByID(articleRequest[i].RequestID.ToString()) == true)
                {
                    _requestRepository.Update(articleRequest[i]);
                }
                else
                {
                    _requestRepository.InsertData(articleRequest[i]);
                }
            }
            var responseLength = articleModel.responseApi.Count;
            for (int i = 0; i < responseLength; i++)
            {
                if (_responseRepository.FindResponseByID(articleResponse[i].ResponseID.ToString()) == true)
                {
                    _responseRepository.Update(articleResponse[i]);
                }
                else
                {
                    _responseRepository.InsertData(articleResponse[i]);
                }
            }
            _articleRepository.Update(articleContent);
            return StatusCode(201, "Updated success");
        }

        [HttpGet("search/{searchText}")]
        public IActionResult GetArticleBySearchText(string searchText)
        {
            var res = _articleRepository.GetArticleBySearchText(searchText);
            var data = res as List<article>;
            if (data.Count == 0)
            {
                return StatusCode(204, res);
            }
            else
            {
                return StatusCode(200, res);
            }
        }
    }
}
