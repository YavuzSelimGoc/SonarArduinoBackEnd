using System;
using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.Abstract
{
    public interface IDataDal : IEntityRepository<Data>
    {
    }
}

