using CoreHoney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreHoney.Business.Abstract
{
    public interface IHoneyService
    {
        Honey GetById(int id);
        Honey GetHoneyDetails(int id);
        List<Honey> GetAll();
        
        void Create(Honey entity);
        void Update(Honey entity);
        void Delete(Honey entity);
    }
}
