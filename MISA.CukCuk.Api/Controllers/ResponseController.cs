using API.Common.Models;
using API.DataLayer.interfaces;
using API.Service.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : BaseController<response>
    {
        IBaseService<response> _baseService;
        IResponseRepository _responseRepository;
        public ResponseController(IBaseService<response> baseService, IResponseRepository responseRepository) : base(baseService)
        {
            _baseService = baseService;
            _responseRepository = responseRepository;
        }

        [HttpGet("{articleId}")]
        public IActionResult GetResponseByArticle([FromRoute] Guid articleId)
        {
            var res = _responseRepository.GetResponseByArticle(articleId);
            var data = res as List<response>;
            if (data.Count == 0)
            {
                return StatusCode(204, res);
            }
            else
            {
                return StatusCode(200, res);
            }
        }

        [HttpDelete("delete-response/{id}")]
        public IActionResult DeleteRequest(Guid id)
        {
            if (_responseRepository.FindResponseByID(id.ToString()) == true)
            {
                var res = _responseRepository.DeleteData(id);
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(200, res);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
