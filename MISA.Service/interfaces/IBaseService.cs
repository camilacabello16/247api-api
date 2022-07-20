using API.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Service.interfaces
{
    public interface IBaseService<T>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Toàn bộ dữ liệu</returns>
        /// CreatedBy: BDHIEU (09/02/2021)
        ServiceResult GetData();
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity">Dữ liệu thêm mới</param>
        /// <returns>Service Result</returns>
        /// CreatedBy: BDHIEU (09/02/2021)
        ServiceResult InsertData(T entity);

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity">Dữ liệu mới cần cập nhật</param>
        /// <returns>Service Result</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        ServiceResult UpdateData(T entity);
        ServiceResult Update(T entity);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// <returns>ServiceResult</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        ServiceResult DeleteData(Guid id);

        /// <summary>
        /// Lấy danh sách theo vị trí bắt đầu và số lượng
        /// </summary>
        /// <param name="positionStart">Vị trí bắt đầu</param>
        /// <param name="offset">Số lượng cần lấy</param>
        /// <returns>ServiceResult</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        ServiceResult GetDataByIndexAndOffset(int positionStart, int Offset);

        /// <summary>
        /// Lấy số lượng bản ghi trong database
        /// </summary>
        /// <returns>Số lượng bản ghi trong database</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        ServiceResult GetCountData();
    }
}
