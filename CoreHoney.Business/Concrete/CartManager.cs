using CoreHoney.Business.Abstract;
using CoreHoney.DataAccess.Abstract;
using CoreHoney.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreHoney.Business.Concrete
{
    public class CartManager : ICartService
    {
        private ICartDal _cartDal;
        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public void AddToCart(string userId, int honeyId, int quantity)
        {

            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                var index = cart.CartItems.FindIndex(i => i.HoneyId == honeyId);

                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        HoneyId = honeyId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }

                _cartDal.Update(cart);
            }





            //_cartDal.AddToCart(userId, honeyId, quantity);
        }

        

        

        public void DeleteFromCart(string userId, int honeyId)
        {
            var cart = GetCartByUserId(userId);

            if (cart != null)
            {
                _cartDal.DeleteFromCart(cart.Id, honeyId);
            }
        }

        

        public Cart GetCartByUserId(string userId)
        {
            return _cartDal.GetCartByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
           _cartDal.Create(new Entities.Cart() { UserId = userId});  
        }
    }
}
