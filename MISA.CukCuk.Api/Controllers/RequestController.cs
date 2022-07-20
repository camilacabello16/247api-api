using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Common.Models;
using API.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataLayer.interfaces;

namespace API.Controller.Controllers
{
    [Route("api/v1/request")]
    [ApiController]
    public class RequestController : BaseController<request>
    {
        IBaseService<request> _baseService;
        IRequestRepository _requestRepository;

        public RequestController(IBaseService<request> baseService, IRequestRepository requestRepository) : base(baseService)
        {
            _baseService = baseService;
            _requestRepository = requestRepository;
        }

        [HttpGet("{articleId}")]
        public IActionResult GetArticleById([FromRoute] Guid articleId)
        {
            var res = _requestRepository.GetRequestByArticle(articleId);
            var data = res as List<request>;
            if (data.Count == 0)
            {
                return StatusCode(204, res);
            }
            else
            {
                return StatusCode(200, res);
            }
        }

        [HttpDelete("delete-request/{id}")]
        public IActionResult DeleteRequest(Guid id)
        {
            if(_requestRepository.FindRequestByID(id.ToString()) == true)
            {
                var res = _requestRepository.DeleteData(id);
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(200, res);
                }
            } else
            {
                return null;
            }
        }
    }
}
