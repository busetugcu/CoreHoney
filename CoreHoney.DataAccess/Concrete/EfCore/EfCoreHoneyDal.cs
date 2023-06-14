using CoreHoney.DataAccess.Abstract;
using CoreHoney.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreHoney.DataAccess.Concrete.EfCore
{
   
    public class EfCoreHoneyDal : IHoneyDal
    {
        HoneyContext db = new HoneyContext();
        public void Create(Honey entity)
        {
            db.Honeys.Add(entity);  
            db.SaveChanges();   
            
        }

        public void Delete(Honey entity)
        {
            db.Honeys.Remove(entity);
            db.SaveChanges();
        }

        public IEnumerable<Honey> GetAll(Expression<Func<Honey, bool>> filter = null)
        {


            return filter == null
                      ? db.Set<Honey>().ToList()
                      : db.Set<Honey>().Where(filter).ToList();
        }

        public  Honey GetbyId(int id)
        {
            using (var db = new HoneyContext())
            {
                return db.Set<Honey>().Find(id);
            }
        }

        public Honey GetHoneyDetails(int id)
        {
           using(var db = new HoneyContext()) 
            {
                return db.Honeys.FirstOrDefault(i => i.Id == id);
            }

        }

        public Honey GetOne(Expression<Func<Honey, bool>> filter)
        {
            return db.Set<Honey>().Where(filter).FirstOrDefault();
        }

        public void Update(Honey entity)
        {
            //db.Entry(entity).State = EntityState.Modified;
            //db.SaveChanges();
            db.Honeys.Update(entity);
            db.SaveChanges();
        }
    }
}
