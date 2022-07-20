using Dapper;
using API.Common.Models;
using API.DataLayer.interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace API.DataLayer
{
    public class DbContext<T> : IDbContext<T>
    {
        #region DECLARE
        protected string _sqlConnector = "Host=localhost; User Id=root; password=buiduchieu16; Database=247api; Character Set=utf8";
        protected IDbConnection _dbConnection;
        #endregion

        #region CONSTRUCTOR
        public DbContext()
        {
            _dbConnection = new MySqlConnection(_sqlConnector);
        }
        #endregion

        #region METHOD
        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <returns>Toàn bộ danh sách</returns>
        /// CreatedBy: BDHIEU (09/02/2021)
        public IEnumerable<T> GetAll()
        {
            var className = typeof(T).Name;
            var data = _dbConnection.Query<T>($"SELECT * FROM {className}", commandType: CommandType.Text);
            return data;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <param name="sqlCommand">Câu lệnh sql</param>
        /// <param name="commandType"></param>
        /// <returns>Toàn bộ danh sách</returns>
        /// CreatedBy: BDHIEU (09/02/2021)
        public IEnumerable<T> GetAll(string sqlCommand, CommandType commandType = CommandType.Text)
        {
            var data = _dbConnection.Query<T>(sqlCommand, commandType: CommandType.Text);
            return data;
        }

        /// <summary>
        /// Lấy danh sách theo dữ liệu truyền vào
        /// </summary>
        /// <param name="sqlCommand">Câu lệnh sql</param>
        /// <param name="parameters">Những dữ liệu truyền vào</param>
        /// <param name="commandType">Kiểu truy vấn</param>
        /// <returns>Danh sách</returns>
        /// CreatedBy: BDHIEU (09/02/2021)
        public IEnumerable<T> GetData(string sqlCommand = null, object parameters = null, CommandType commandType = CommandType.Text)
        {
            var className = typeof(T).Name;
            if (sqlCommand == null)
            {
                sqlCommand = $"SELECT * FROM {className}";
            }
            var data = _dbConnection.Query<T>(sqlCommand, param: parameters , commandType: commandType);
            return data;
        }

        /// <summary>
        /// Tạo chuỗi json của dữ liệu truyền vào 
        /// </summary>
        /// <param name="entity">Dữ liệu truyền vào</param>
        /// <returns>Chuỗi json của dữ liệu truyền vào</returns>
        /// CreatedBy: BDHIEU (09/02/2021)
        public DynamicParameters MappingDbType(T entity)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }
            return parameters;
        }

        /// <summary>
        /// Thực hiện thêm mới object vào database
        /// </summary>
        /// <param name="entity">Object cần thêm mới</param>
        /// <returns>Số lượng bản ghi thêm thành công</returns>
        /// CreatedBy: BDHIEU (09/02/2021)
        public int InsertData(T entity)
        {
            var className = typeof(T).Name;
            var parameters = MappingDbType(entity);
            var res = _dbConnection.Execute($"Proc_Insert{className}", parameters, commandType: CommandType.StoredProcedure);
            return res;
        }

        /// <summary>
        /// Thực hiện cập nhật dữ liệu
        /// </summary>
        /// <param name="entity">Object cần cập nhật</param>
        /// <returns>Số lượng bản ghi cập nhật thành công</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        public int UpdateData(T entity)
        {
            var className = typeof(T).Name;
            var parameters = MappingDbType(entity);
            var res = _dbConnection.Execute($"Proc_Update{className}", parameters, commandType: CommandType.StoredProcedure);
            return res;
        }

        public int Update(T entity)
        {
            string className = typeof(T).Name;
            var properties = typeof(T).GetProperties();
            var parameters = new DynamicParameters();
            var sqlBuilder = string.Empty;
            var sqlParamBuilder = string.Empty;
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                parameters.Add($"@{propertyName}", propertyValue);
                sqlBuilder += $",{propertyName} = @{propertyName}";
            }
            sqlBuilder = sqlBuilder.Substring(1);
            int position = sqlBuilder.IndexOf(",");
            var sqlCompare = sqlBuilder.Substring(0, position);
            sqlBuilder = sqlBuilder.Substring(position + 1);
            var sql = $"UPDATE {className} SET {sqlBuilder} WHERE {sqlCompare}";
            var effectRows = _dbConnection.Execute(sql, parameters);
            return effectRows;
        }

        /// <summary>
        /// Thực hiện xóa dữ liệu
        /// </summary>
        /// <param name="id">id của dữ liệu cần xóa</param>
        /// <returns>Số bản ghi xóa thành công</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        public int DeleteData(Guid id)
        {
            var className = typeof(T).Name;
            var res = _dbConnection.Execute($"DELETE FROM {className} WHERE {char.ToUpper(className[0]) + className.Substring(1)}ID = @id", new { id = id.ToString() }, commandType: CommandType.Text);
            return res;
        }

        /// <summary>
        /// Lấy danh sách theo vị trí bắt đầu và số lượng
        /// </summary>
        /// <param name="positionStart">Vị trí bắt đầu</param>
        /// <param name="offset">Số lượng cần lấy</param>
        /// <returns>Danh sách theo vị trí bắt đầu và số lượng</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        public IEnumerable<T> GetDataByIndexAndOffset(int positionStart, int offset)
        {
            var className = typeof(T).Name;
            var data = _dbConnection.Query<T>($"Proc_Get{className}ByIndexAndOffset", new { positionStart = positionStart, offset = offset }, commandType: CommandType.StoredProcedure);
            return data;
        }

        /// <summary>
        /// Lấy số lượng bản ghi trong database
        /// </summary>
        /// <returns>Số lượng bản ghi trong database</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        public int GetCountData()
        {
            var className = typeof(T).Name;
            var numberOfData = _dbConnection.Query<int>($"SELECT COUNT(*) FROM {className}", commandType: CommandType.Text).FirstOrDefault();
            return numberOfData;
        }

        /// <summary>
        /// Lấy danh sách dữ liệu bằng từ khóa tìm kiếm
        /// </summary>
        /// <param name="searchText">Từ khóa</param>
        /// <returns>Danh sách dữ liệu bằng từ khóa tìm kiếm</returns>
        /// CreatedBy: BDHIEU (19/02/2021)
        public IEnumerable<T> GetDataBySearchText(string searchText)
        {
            var className = typeof(T).Name;
            var data = _dbConnection.Query<T>($"Proc_Get{className}BySearchText", new { SearchText = searchText}, commandType: CommandType.StoredProcedure);
            return data;
        }
        #endregion
    }
}
