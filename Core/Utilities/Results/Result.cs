using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string strmessage):this(success)
        {
            Message= strmessage;
            IsSuccess= success;
        }

        public Result(bool success)
        {
            IsSuccess = success;
        }

        public bool IsSuccess { get; }

        public string Message { get; }
    }
}
