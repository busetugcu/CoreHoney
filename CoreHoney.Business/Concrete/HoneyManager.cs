using CoreHoney.Business.Abstract;
using CoreHoney.DataAccess.Abstract;
using CoreHoney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreHoney.Business.Concrete
{
    public class HoneyManager : IHoneyService
    {
        private IHoneyDal _honeyDal;


        public HoneyManager(IHoneyDal honeyDal)
        {
            _honeyDal = honeyDal;
        }


        public void Create(Honey entity)
        {
            _honeyDal.Create(entity);
        }

        public void Delete(Honey entity)
        {
            _honeyDal.Delete(entity);
        }

        public List<Honey> GetAll()
        {
            return _honeyDal.GetAll().ToList();
        }

        public Honey GetById(int id)
        {
            return _honeyDal.GetbyId(id);
        }

        public Honey GetHoneyDetails(int id)
        {
            return _honeyDal.GetHoneyDetails(id);
        }

        public void Update(Honey entity)
        {
            _honeyDal.Update(entity);
        }
    }
}
