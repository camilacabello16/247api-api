using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Service;
using API.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controller.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region DECLARE
        IBaseService<T> _baseService;
        #endregion

        #region CONSTRUCTOR
        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region METHOD
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        /// CreatedBy : BDHIEU (09/02/2021)
        [HttpGet]
        public IActionResult Get()
        {
            var res = _baseService.GetData();
            var data = res.Data as List<T>;
            if (data.Count == 0)
            {
                return StatusCode(204, res.Data);
            }
            else
            {
                return StatusCode(200, res.Data);
            }
        }

        /// <summary>
        /// Lấy danh sách theo vị trí bắt đầu và số lượng
        /// </summary>
        /// <param name="positionstart">Vị trí bắt đầu</param>
        /// <param name="offset">Số lượng cần lấy</param>
        /// <returns>Danh sách theo vị trí và số lượng cần lấy</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        [HttpGet("paging")]
        public IActionResult GetDataByIndexAndOffset([FromQuery] int positionstart, [FromQuery] int offset)
        {
            var res = _baseService.GetDataByIndexAndOffset(positionstart, offset);
            var data = res.Data as List<T>;
            if (data.Count == 0)
            {
                return StatusCode(204, res.Data);
            }
            else
            {
                return StatusCode(200, res.Data);
            }
        }

        /// <summary>
        /// Lấy số lượng bản ghi trong database
        /// </summary>
        /// <returns>Số lượng bản ghi trong database</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        [HttpGet("number-data")]
        public int GetCountData()
        {
            var res = _baseService.GetCountData();
            return (int)res.Data;
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Tên bản ghi cần thêm mới</param>
        /// <returns>Kết quả thêm thành công hay không</returns>
        /// CreatedBy : BDHIEU (09/02/2021)
        [HttpPost]
        public IActionResult Post(T entity)
        {
            var res = _baseService.InsertData(entity);
            if (res.Success == false)
            {
                return StatusCode(400, res.Data);
            }
            else if (res.Success == true && (int)res.Data > 0)
            {
                return StatusCode(201, res.Data);
            }
            else
            {
                return StatusCode(200, res.Data);
            }
        }

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity">Dữ liệu cập nhật</param>
        /// <returns>Dữ liệu cập nhật</returns>
        /// CreatedBy : BDHIEU (10/02/2021)
        [HttpPut]
        public IActionResult Put(T entity)
        {
            var res = _baseService.Update(entity);
            if (res.Success == false)
            {
                return StatusCode(400, res.Data);
            }
            else if (res.Success == true && (int)res.Data > 0)
            {
                return StatusCode(201, res.Data);
            }
            else
            {
                return StatusCode(200, res.Data);
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="id">Id của dữ liệu cần xóa</param>
        /// CreatedBy : BDHIEU (10/02/2021)
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _baseService.DeleteData(id);
            if((int)res.Data > 0)
            {
                return StatusCode(201, res.Data);
            }
            else
            {
                return StatusCode(200, res.Data);
            }
        }
        #endregion
    }
}
