using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace API.DataLayer.interfaces
{
    public interface IDbContext<T>
    {
        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <returns>Toàn bộ danh sách</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        IEnumerable<T> GetAll();
        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <param name="sqlCommand">Câu lệnh sql</param>
        /// <param name="commandType"></param>
        /// <returns>Toàn bộ danh sách</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        IEnumerable<T> GetAll(string sqlCommand, CommandType commandType = CommandType.Text);
        /// <summary>
        /// Lấy danh sách theo dữ liệu truyền vào
        /// </summary>
        /// <param name="sqlCommand">Câu lệnh sql</param>
        /// <param name="parameters">Những dữ liệu truyền vào</param>
        /// <param name="commandType">Kiểu truy vấn</param>
        /// <returns>Danh sách</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        IEnumerable<T> GetData(string sqlCommand = null, object parameters = null, CommandType commandType = CommandType.Text);
        /// <summary>
        /// Thực hiện thêm mới object vào database
        /// </summary>
        /// <param name="entity">Object cần thêm mới</param>
        /// <returns>Số lượng bản ghi thêm thành công</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        int InsertData(T entity);

        /// <summary>
        /// Thực hiện cập nhật dữ liệu
        /// </summary>
        /// <param name="entity">Object cần cập nhật</param>
        /// <returns>Số lượng bản ghi cập nhật thành công</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        int UpdateData(T entity);

        /// <summary>
        /// Thực hiện xóa dữ liệu
        /// </summary>
        /// <param name="id">id của dữ liệu cần xóa</param>
        /// <returns>Số bản ghi xóa thành công</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        int DeleteData(Guid id);

        /// <summary>
        /// Lấy danh sách theo vị trí bắt đầu và số lượng
        /// </summary>
        /// <param name="positionStart">Vị trí bắt đầu</param>
        /// <param name="offset">Số lượng cần lấy</param>
        /// <returns>Danh sách theo vị trí bắt đầu và số lượng</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        public IEnumerable<T> GetDataByIndexAndOffset(int positionStart, int offset);

        /// <summary>
        /// Lấy số lượng bản ghi trong database
        /// </summary>
        /// <returns>Số lượng bản ghi trong database</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        public int GetCountData();

        /// <summary>
        /// Lấy danh sách dữ liệu bằng từ khóa tìm kiếm
        /// </summary>
        /// <param name="searchText">Từ khóa</param>
        /// <returns>Danh sách dữ liệu bằng từ khóa tìm kiếm</returns>
        /// CreatedBy: BDHIEU (19/02/2021)
        IEnumerable<T> GetDataBySearchText(string searchText);

        int Update(T entity);
    }
}
