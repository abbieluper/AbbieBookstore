using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public class EFCheckoutRepository : ICheckoutRepository
    {
        private BookstoreContext context;

        public EFCheckoutRepository (BookstoreContext temp)
        {
            context = temp; 
        }

        public IQueryable<Checkout> Checkouts => context.Checkouts.Include(x => x.Lines).ThenInclude(x => x.Book);

        //IQueryable<Checkout> ICheckoutRepository.Checkouts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void SaveCheckout(Checkout checkout)
        {
            context.AttachRange(checkout.Lines.Select(x => x.Book));

            if (checkout.CheckoutId == 0)
            {
                context.Checkouts.Add(checkout); 
            }

            context.SaveChanges(); 
        }
    }
}
