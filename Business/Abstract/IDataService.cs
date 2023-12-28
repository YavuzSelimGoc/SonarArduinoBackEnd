using System;
using Core.Utilities.Results;
using Entities.Concrete;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Business.Abstract
{
    public interface IDataService
    {
        IDataResult<Data> GetById(int dataId);
        IDataResult<List<Data>> GetList();
        IResult Add(Data data);
        IResult Delete(Data data);
        IResult Update(Data data);
    }
}

