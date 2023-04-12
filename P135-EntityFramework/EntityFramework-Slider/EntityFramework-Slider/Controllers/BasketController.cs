﻿using EntityFramework_Slider.Data;
using EntityFramework_Slider.Models;
using EntityFramework_Slider.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EntityFramework_Slider.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        public BasketController(AppDbContext context)
        {
        
            _context = context;

        }


        public async Task <IActionResult>Index()
        {
            List<BasketVM> basket;
            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            foreach (var basketVM in basket)
            {
                Product dbProduct =  _context.Products.Include(m => m.Images).FirstOrDefault(m => m.Id == basketVM.Id);
                basketVM.Product = dbProduct;
            }
            return View(basket);
        }
    }
}
