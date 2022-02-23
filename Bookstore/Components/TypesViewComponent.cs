using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models; 

namespace Bookstore.Components
{
    public class TypesViewComponent : ViewComponent
    {
        private IBookstoreRepository repo { get; set; } // i could have named repo anything

        public TypesViewComponent (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke() // when this view component is invoked then I want to do something
        {
            // question mark (?) means that this is nullable --> it is okay if this is null
            ViewBag.SelectedType = RouteData?.Values["Category"]; // this will look in the URL for book Category which we passed in on Default.cshtml page

            var types = repo.Books
                .Select(x => x.Category) // we want to get the book Category
                .Distinct() // don't want duplicates
                .OrderBy(x => x);

            return View(types);
        }

    }
}
