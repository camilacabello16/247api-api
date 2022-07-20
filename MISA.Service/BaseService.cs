using API.Common.Models;
using API.DataLayer;
using API.DataLayer.interfaces;
using API.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Service
{
    public class BaseService<T> : IBaseService<T>
    {
        #region DECLARE
        IDbContext<T> _dbContext;
        #endregion

        #region CONSTRUCTOR 
        public BaseService(IDbContext<T> dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region METHOD
        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="id">id của bản ghi cần xóa</param>
        /// <returns>ServiceResult</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        public ServiceResult DeleteData(Guid id)
        {
            var serviceResult = new ServiceResult();
            serviceResult.Data = _dbContext.DeleteData(id);
            return serviceResult;
        }
        
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Toàn bộ dữ liệu</returns>
        /// CreatedBy: BDHIEU (09/02/2021)
        public virtual ServiceResult GetData()
        {
            var serviceResult = new ServiceResult();
            serviceResult.Data = _dbContext.GetAll();
            return serviceResult;
        }

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity">Dữ liệu thêm mới</param>
        /// <returns>Service Result</returns>
        /// CreatedBy: BDHIEU (09/02/2021)
        public ServiceResult InsertData(T entity)
        {
            var serviceResult = new ServiceResult();
            var errorMsg = new ErrorMsg();

            // Validate dữ liệu
            var isValid = ValidateData(entity, errorMsg);

            // validate sai thì ko cho thêm
            if (isValid == false)
            {
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
            }

            // validate đúng => thực hiện thêm mới
            else
            {
                serviceResult.Success = true;
                serviceResult.Data = _dbContext.InsertData(entity);
            }
            return serviceResult;
        }

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity">Dữ liệu cần cập nhật</param>
        /// <returns>ServiceResult</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        public ServiceResult UpdateData(T entity)
        {
            var serviceResult = new ServiceResult();
            var errorMsg = new ErrorMsg();

            // Validate dữ liệu
            var isValid = true;

            // validate sai thì ko cho thêm
            if (isValid == false)
            {
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
            }

            // validate đúng => thực hiện thêm mới
            else
            {
                serviceResult.Success = true;
                serviceResult.Data = _dbContext.UpdateData(entity);
            }
            return serviceResult;
        }

        public ServiceResult Update(T entity)
        {
            var serviceResult = new ServiceResult();
            var errorMsg = new ErrorMsg();

            // Validate dữ liệu
            var isValid = true;

            // validate sai thì ko cho thêm
            if (isValid == false)
            {
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
            }

            // validate đúng => thực hiện thêm mới
            else
            {
                serviceResult.Success = true;
                serviceResult.Data = _dbContext.Update(entity);
            }
            return serviceResult;
        }

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="entity">Dữ liệu truyền vào</param>
        /// <param name="errorMsg">Câu thông báo</param>
        /// <returns>true - validate đúng, false - validate sai</returns>
        /// CreatedBy: BDHIEU (09/02/2021)
        protected virtual bool ValidateData(T entity, ErrorMsg errorMsg = null)
        {
            return true;
        }

        /// <summary>
        /// Lấy danh sách theo vị trí bắt đầu và số lượng
        /// </summary>
        /// <param name="positionStart">Vị trí bắt đầu</param>
        /// <param name="offset">Số lượng cần lấy</param>
        /// <returns>ServiceResult</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        public ServiceResult GetDataByIndexAndOffset(int positionStart, int offset)
        {
            var serviceResult = new ServiceResult();
            serviceResult.Data = _dbContext.GetDataByIndexAndOffset(positionStart, offset);
            return serviceResult;
        }

        /// <summary>
        /// Lấy số lượng bản ghi trong database
        /// </summary>
        /// <returns>Số lượng bản ghi trong database</returns>
        /// CreatedBy: BDHIEU (10/02/2021)
        public ServiceResult GetCountData()
        {
            var serviceResult = new ServiceResult();
            serviceResult.Data = _dbContext.GetCountData();
            return serviceResult;
        }
        #endregion
    }
}
