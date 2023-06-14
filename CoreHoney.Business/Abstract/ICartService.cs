using CoreHoney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreHoney.Business.Abstract
{
    public interface ICartService 
    {
        void InitializeCart(string userId);
        Cart GetCartByUserId(string userId);
        void AddToCart(string userId, int honeyId, int quantity);
        void DeleteFromCart(string userId, int honeyId);
       
    }
}
