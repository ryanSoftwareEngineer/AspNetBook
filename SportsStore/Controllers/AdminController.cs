﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public IActionResult Index()
        {
            return View(repository.Products);
        }
        public AdminController(IProductRepository repository) {
            this.repository = repository;
        }
        public ViewResult Edit(int productId) => View(repository.Products.FirstOrDefault(product => product.ProductId == productId));

        [HttpPost]
        public IActionResult Edit(ProductModel product) {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = product.Name + "has been saved";
                return RedirectToAction("Index");
            }
            else {
                return View(product);
            }
        }
        public ViewResult Create() => View("Edit", new ProductModel());
        [HttpPost]
        public IActionResult Delete(int productId) {
            string deletedProductName = repository.DeleteProduct(productId);
            if (deletedProductName != null) {
                TempData["message"] = $"{deletedProductName} was deleted";
            }
            return RedirectToAction("Index");

        }
    }
}