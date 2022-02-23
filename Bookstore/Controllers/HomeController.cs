using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bookstore.Models;
using Bookstore.Models.ViewModels;

//this is the home controller page

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(string Category, int pageNum = 1) // this sets the default of pageNum to 1
        {
            int pageSize = 10;

            var x = new BooksViewModel
            {
                Books = repo.Books // pass data to the index file
                .Where(b => b.Category == Category || Category == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks =  // if we have not selected a category then use count of all books - otherwise only count books from selected book category
                        (Category == null
                            ? repo.Books.Count()
                            : repo.Books.Where(x => x.Category == Category).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum // pageNum comes from parameter being passed in 
                }
            };

            return View(x); // DO NOT FORGET TO PASS THE x RIGHT HERE!!!!!!!!!!!!!
        }

        public IActionResult About()
        {
            return View(); 
        }

    }
}
