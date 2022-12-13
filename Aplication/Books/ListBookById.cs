using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persitense;

namespace Aplication.Books;

public class ListBookById
{
    public class ListBookByIdQuery : IRequest<Book>
    {
        public int Id { get; set; }
    }

    public class ListBookByAuthorQueryHandler : IRequestHandler<ListBookByIdQuery, Book>
    {
        private readonly DataContext _context;

        public ListBookByAuthorQueryHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<Book> Handle(ListBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == request.Id);
            if (book is null)
                throw new Exception("Author not found");
            
            return book;
        }
    }
}