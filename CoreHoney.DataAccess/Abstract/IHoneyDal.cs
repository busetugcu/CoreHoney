using CoreHoney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreHoney.DataAccess.Abstract
{
    public interface IHoneyDal
    {

        Honey GetbyId(int id);
        Honey GetOne(Expression<Func<Honey, bool>> filter);
        IEnumerable<Honey> GetAll(Expression<Func<Honey, bool>> filter = null);

        Honey GetHoneyDetails(int id);

        void Create(Honey entity);
        void Update(Honey entity);
        void Delete(Honey entity);


    }
}
