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
    public class EfCoreCartDal : ICartDal
    {
        HoneyContext db = new HoneyContext();

       

        public void Create(Cart entity)
        {
            db.Carts.Add(entity);
            db.SaveChanges();

        }

        public void Create(Honey entity)
        {
            throw new NotImplementedException();
        }


        public void Delete(Honey entity)
        {
            db.Honeys.Remove(entity);
            db.SaveChanges();
        }

        public void DeleteFromCart(int cartId, int honeyId)
        {
            using (var context = new HoneyContext())
            {
                var cmd = @"delete from cartItem where honeyId=@h0 and cartId=@h1";
                context.Database.ExecuteSqlRaw(cmd, honeyId, cartId);
            }
        }

        public IEnumerable<Honey> GetAll(Expression<Func<Honey, bool>> filter = null)
        {

            return filter == null
                      ? db.Set<Honey>().ToList()
                      : db.Set<Honey>().Where(filter).ToList();
        }

        public Honey GetbyId(int id)
        {
            using (var db = new HoneyContext())
            {
                return db.Set<Honey>().Find(id);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            using (var context = new HoneyContext())
            {
                return context
                    .Carts
                    .Include(i => i.CartItems)
                    .ThenInclude(i => i.Honey)
                    
                   
                    .FirstOrDefault(i => i.UserId == userId);
            }
        }

        public Honey GetHoneyDetails(int id)
        {
            using (var db = new HoneyContext())
            {
                return db.Honeys.FirstOrDefault(i => i.Id == id);
            }
        }

        public Honey GetOne(Expression<Func<Honey, bool>> filter)
        {
            return db.Set<Honey>().Where(filter).FirstOrDefault();
        }

      



        public void Update(Cart entity)
        {
            db.Carts.Update(entity);
            db.SaveChanges();
        }

       

        public void Update(Honey entity)
        {
            throw new NotImplementedException();
        }
    }
}
