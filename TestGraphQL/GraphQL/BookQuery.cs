using HotChocolate.Data;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Models;

namespace TestGraphQL.GraphQL
{
    //https://blexin.com/en/blog-en/creating-our-api-with-graphql-and-hot-chocolate/
    //https://github.com/AARNOLD87/GraphQLWithHotChocolate
    [ExtendObjectType(Name = "Query")]
    public class BookQuery
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public BookQuery(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        [UsePaging(SchemaType = typeof(AuthorType))]
        [UseFiltering]
        public IQueryable<Author> Authors => _authorService.GetAll();

        [UsePaging(SchemaType = typeof(BookType))]
        [UseFiltering]
        public IQueryable<Book> Books => _bookService.GetAll();
    }
}
