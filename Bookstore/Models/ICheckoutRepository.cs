using System;
using System.Linq;

namespace Bookstore.Models
{
    public interface ICheckoutRepository
    {
        public IQueryable<Checkout> Checkouts { get; }

        public void SaveCheckout(Checkout checkout); 
    }
}
