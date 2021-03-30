using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQL.Models
{
    public interface IBookService
    {
        IQueryable<Book> GetAll();
    }
}
