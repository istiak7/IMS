using IMS.Core.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMS.Application.Helpers.AppConstants;

namespace IMS.Application.Helpers
{
    public static class Utilities
    {
        public static ResponseModel GetSuccessMsg(string message, object data = null)
        {
            return new ResponseModel
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Status = ResultStatus.Success.ToString(),
                Message = message,
                Data = data
            };
        }

        public static ResponseModel GetAlreadyExistMsg(string message, object data = null)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status409Conflict,
                Status = ResultStatus.Canceled.ToString(),
                Message = message,
                Data = data
            };
        }

        public static ResponseModel GetErrorMsg(string message, object data = null)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Status = ResultStatus.Error.ToString(),
                Message = message,
                Data = data
            };
        }

        public static ResponseModel GetInternalServerErrorMsg(Exception ex)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status500InternalServerError,
                Status = ResultStatus.Error.ToString(),
                Message = ex.Message,
                Data = null
            };
        }

        public static ResponseModel GetInternalServerErrorMsg(string message)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status500InternalServerError,
                Status = ResultStatus.Error.ToString(),
                Message = message,
                Data = null
            };
        }

        public static ResponseModel GetNoDataFoundMsg(string? msg = "No Data is Found")
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Status = ResultStatus.Error.ToString(),
                Message = msg,
                Data = null
            };
        }

    }
}
