using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.CORE.Response
{
	public class ApiResponse<T>
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }

		//public ApiResponse(bool isSuccess)
		//{
		//	Success = isSuccess;
		//	Data = default;
		//	Message = isSuccess ? "Success" : "Error";
		//}
		public ApiResponse(T data)
		{
			Success = true;
			Data = data;
			Message = "Success";
		}
		public ApiResponse(string message)
		{
			Success = false;
			Data = default;
			Message = message;
		}
	}
}
