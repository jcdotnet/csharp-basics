using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphism
{
    interface ILoanable
    {
        int LoanPeriod { get; }
        string Borrower { get; set; }
        void Borrow(string borrower);
        void Return();
    }

    interface IPrintable
    {
        void Print();
    }

    class Book : ILoanable, IPrintable
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int LoanPeriod { get { return 21; } }
        public string Borrower { get; set; }

        public void Borrow(string borrower)
        {
            if (Borrower == null)
            {
                Borrower = borrower;
                Console.WriteLine($"{Title} by {Author} has been borrowed by {Borrower}");
            }
            else
            {
                Console.WriteLine($"{Title} by {Author} is already borrowed by {Borrower}");
            }
        }

        public void Return()
        {
            if (Borrower != null)
            {
                Console.WriteLine($"{Title} by {Author} has been returned");
                Borrower = null;
            }
            else
            {
                Console.WriteLine($"{Title} by {Author} is not borrowed");
            }
        }

        public void Print()
        {
            Console.WriteLine($"Book: {Title} by {Author} (ISBN: {ISBN})");
        }
    }

    class DVD : ILoanable, IPrintable
    {
        public string Director { get; set; }
        public string Title { get; set; }
        public int LengthInMinutes { get; set; }
        public int LoanPeriod { get { return 7; } }
        public string Borrower { get; set; }

        public void Borrow(string borrower)
        {
            if (Borrower == null)
            {
                Borrower = borrower;
                Console.WriteLine($"{Title} directed by {Director} has been borrowed by {Borrower}");
            }
            else
            {
                Console.WriteLine($"{Title} directed by {Director} is already borrowed by {Borrower}");
            }
        }

        public void Return()
        {
            if (Borrower != null)
            {
                Console.WriteLine($"{Title} directed by {Director} has been returned");
                Borrower = null;
            }
            else
            {
                Console.WriteLine($"{Title} directed by {Director} is not borrowed");
            }
        }

        public void Print()
        {
            Console.WriteLine($"DVD: {Title} directed by {Director} ({LengthInMinutes} min)");
        }
    }

    class CD : ILoanable, IPrintable
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public int LoanPeriod { get { return 14; } }
        public string Borrower { get; set; }

        public void Borrow(string borrower)
        {
            Borrower = borrower;
            Console.WriteLine($"CD {Album} by {Artist} has been borrowed by {borrower}.");
        }

        public void Return()
        {
            Console.WriteLine($"CD {Album} by {Artist} has been returned.");
            Borrower = null;
        }

        public void Print()
        {
            Console.WriteLine($"CD: {Album} by {Artist} ({(Borrower == null ? "available" : "borrowed by " + Borrower)})");
        }
    }

    public class Test
    {
        public Test()
        {
            CD cd = new CD
            {
                Artist = "The Beatles",
                Album = "Abbey Road",
                Borrower = "John Doe"
            };
            cd.Print();

            DVD dvd = new DVD
            {
                Title = "The Shawshank Redemption",
                Director = "Frank Darabont",
                Borrower = "Jane Doe"
            };
            dvd.Print();

            Book book = new Book
            {
                Author = "J.K. Rowling",
                Title = "Harry Potter and the Philosopher's Stone",
                ISBN = "9780747532743",
                Borrower = "Alice Smith"
            };
            book.Print();
        }
    }
}
