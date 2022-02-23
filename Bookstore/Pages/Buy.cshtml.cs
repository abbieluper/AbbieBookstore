using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bookstore.Infrastructure;
using Bookstore.Models; 

namespace Bookstore.Pages
{
    public class BuyModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }

        public BuyModel (IBookstoreRepository temp)
        {
            repo = temp; 
        }

        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/"; // the / will take us back to the index page
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }

        public IActionResult OnPost(int bookId, string returnUrl) // we are getting passed the bookId from the ProjectSummary.cshtml page
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            // ?? returns the value on the left if it isn’t null otherwise it evaluates the value on the right and returns its results
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();

            // basket items must consist of a book and a quantity bc this is how we set it in Basket.cs page
            basket.AddItem(b, 1); // we just pulled b (Book) on the line above and we are setting the quantity to 1

            HttpContext.Session.SetJson("basket", basket);

            return RedirectToPage(new { ReturnUrl = returnUrl }); 
        }


    }
}
