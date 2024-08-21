using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data,string massage) : base(data, true,massage)
        {

        }

        public SuccessDataResult(T data):base(data,true) 
        {
            
        }

        public SuccessDataResult(string massage) : base(default, true, massage)
        {
            
        }

        public SuccessDataResult():base(default,true)
        {
            
        }
    }
}
