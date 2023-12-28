using System;
using System.ComponentModel.Design;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Business.Concrete
{
    public class DataManager : IDataService
    {
        IDataDal _dataDal;

        public DataManager(IDataDal dataDal)
        {
            _dataDal = dataDal;
        }

        public IResult Add(Data data)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _dataDal.Add(data);
            return new SuccesResult(Messages.UserNotFound);
        }

        public IResult Delete(Data data)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _dataDal.Delete(data);
            return new SuccesResult(Messages.UserNotFound);
        }

        public IDataResult<Data> GetById(int dataId)
        {
            return new SuccesDataResult<Data>(_dataDal.Get(x => x.DataId == dataId), Messages.UserNotFound);
        }

        public IDataResult<List<Data>> GetList()
        {
            return new SuccesDataResult<List<Data>>(_dataDal.GetAll().ToList());
        }


   

        public IResult Update(Data data)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _dataDal.Update(data);
            return new SuccesResult(Messages.UserRegistered);
        }
    }
}

