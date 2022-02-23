using System;
using System.Collections.Generic;
using System.Linq;


namespace Bookstore.Models
{
    public class Basket
    {
        // we are creating a List of type BasketLineItem
        // declare List and then give it a value
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();

        // basket items must consist of a property and a quantity
        public void AddItem (Book book, int qty)
        {
            BasketLineItem line = Items
                .Where(b => b.Book.BookId == book.BookId)
                .FirstOrDefault();

            if (line == null) // if book has not been selected yet, then we will add a new entry in the BasketLine Item
            {
                Items.Add(new BasketLineItem
                {
                    Book = book,
                    Quantity = qty

                });
            }
            else
            {
                line.Quantity += qty; 
            }
        }

        public double CalculateTotal()
        {
            // ********** check this part!!!!! I AM GUESSING **********
            //figure out how to multiple by the price!!
            double sum = Items.Sum(x => x.Quantity * x.Book.Price);

            return sum;
        }
    }

    public class BasketLineItem
    {
        public int LineId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
