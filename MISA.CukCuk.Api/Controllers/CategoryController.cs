using API.Common.Models;
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
    public class CategoryController : BaseController<category>
    {
        public CategoryController(IBaseService<category> baseService): base(baseService)
        {

        }
    }
}
