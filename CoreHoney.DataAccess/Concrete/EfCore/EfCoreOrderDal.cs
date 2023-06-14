using CoreHoney.DataAccess.Abstract;
using CoreHoney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreHoney.DataAccess.Concrete.EfCore
{
    public class EfCoreOrderDal : IOrderDal
    {
        public void Create(Honey entity)
        {
            throw new NotImplementedException();
        }

        public void Create(Order entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Honey entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Honey> GetAll(Expression<Func<Honey, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Honey GetbyId(int id)
        {
            throw new NotImplementedException();
        }

        public Honey GetHoneyDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Honey GetOne(Expression<Func<Honey, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(Honey entity)
        {
            throw new NotImplementedException();
        }
    }
}
