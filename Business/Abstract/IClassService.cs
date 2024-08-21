using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IClassService 
    {
        IResult Add(ClassDto nclass);
        IResult Delete(int classID);
        IResult Update(ClassDto nclass);
        IDataResult<List<Class>> GetAll();
    }
}
