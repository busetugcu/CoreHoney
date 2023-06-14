using CoreHoney.Business.Abstract;
using CoreHoney.WEBUI.Identity;
using CoreHoney.WEBUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreHoney.WEBUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
            
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    HoneyId = i.Honey.Id,
                    Name = i.Honey.Name,
                    Price = i.Honey.Price,
                    Image = i.Honey.Image,
                    Quantity = i.Quantity
                }).ToList()
            });
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int honeyId, int quantity)
        {
            _cartService.AddToCart(_userManager.GetUserId(User), honeyId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int honeyId)
        {
            _cartService.DeleteFromCart(_userManager.GetUserId(User), honeyId);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            var orderModel = new OrderModel();

            orderModel.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    HoneyId = i.Honey.Id,
                    Name = i.Honey.Name,
                    Price = i.Honey.Price,
                    Image = i.Honey.Image,
                    Quantity = i.Quantity
                }).ToList()
            };

            return View(orderModel);

        }
        
     }
}
