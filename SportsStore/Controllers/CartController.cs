using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    namespace SportsStore.Controllers
    {

        public class CartController : Controller
        {
            private IProductRepository repository;
            private Cart cart; 

            public CartController(IProductRepository repo, Cart cartService)
            {
                repository = repo;
                cart = cartService;
            }
            public ViewResult Index(string returnUrl) {
                var model = new CartIndexViewModel {
                    Cart = cart,
                    ReturnUrl = returnUrl
                };
                return View(model);
            }
            public RedirectToActionResult AddToCart(int ProductId, string returnUrl)
            {
                ProductModel product = repository.Products
                .FirstOrDefault(p => p.ProductId == ProductId);
                if (product != null)
                {
                    
                    cart.AddItem(product, 1);
                   
                }
                return RedirectToAction("Index", new { returnUrl });
            }
            public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
            {
                ProductModel product = repository.Products
                .FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                   
                    cart.RemoveLine(product);
                    
                }
                return RedirectToAction("Index", new { returnUrl });
            }
            private Cart GetCart()
            {
                //// if cart exists then take left... if null then take right new cart
                //Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
                //var test = HttpContext.Session.Keys;
                //string test2 = "";
                //foreach (var a in test) {
                //    test2 = a; 
                //}

                //var able = HttpContext.Session.GetString(test2);
                //return cart;
                return null;
            }
            private void SaveCart(Cart cart)
            {
                // you can add custom objects to the Session by session.setString(this Isession session, name of the key, serialize the object into json string)
                HttpContext.Session.SetJson("Cart", cart);
            }
        }
    }
}