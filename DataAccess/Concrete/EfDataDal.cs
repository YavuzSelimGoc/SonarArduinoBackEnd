using System;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace DataAccess.Concrete
{

    public class EfDataDal : EfRepositoryBase<Data, FonContext>, IDataDal
    {


    }

}

