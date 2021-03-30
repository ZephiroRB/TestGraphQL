using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Models;

namespace TestGraphQL.GraphQL
{
    public class InMemoryBookService : IBookService
    {
        private IList<Book> _books;

        public InMemoryBookService()
        {
            _books = new List<Book>()
            {
                new Book() { Id = 1, Title = "First Book", Price = 10, AuthorId = 1},
                new Book() { Id = 2, Title = "Second Book", Price = 11, AuthorId = 2},
                new Book() { Id = 3, Title = "Third Book", Price = 12, AuthorId = 3},
                new Book() { Id = 4, Title = "Fourth Book", Price = 15, AuthorId = 1},
            };
        }

        public IQueryable<Book> GetAll()
        {
            return _books.AsQueryable();
        }
    }
}
