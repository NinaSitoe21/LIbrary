using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persitense;

namespace Aplication.Books;

public class AddBook
{
   public class AddBooKCommand:IRequest<Book>
   {
      public string Title  { get; set; }
      public string Author  { get; set; }
      public string Publishing_company  { get; set; }
      public int Year { get; set; }
      public string Description  { get; set; }
      public string Image  { get; set; }
      public string Url_Download  { get; set; }
   }
   
   public class AddBooKCommandHandle:IRequestHandler<AddBooKCommand,Book>
   {
      private readonly DataContext _context;

      public AddBooKCommandHandle(DataContext context)
      {
         _context = context;
      }

      public async Task<Book> Handle(AddBooKCommand request, CancellationToken cancellationToken)
      {
         var existedBook =
            await _context.Books.FirstOrDefaultAsync(book =>
               book.Title == request.Title && book.Author == request.Author, cancellationToken: cancellationToken);
         if (existedBook != null)
         {
            throw new Exception("Error, This book already exist, change the title");
         }

         var newBook = new Book
         {
            Title = request.Title,
            Author = request.Author,
            Publishing_company = request.Publishing_company,
            Year = request.Year,
            Description = request.Description,
            Image = request.Image,
            Url_Download = request.Url_Download
         };
         
         await _context.Set<Book>().AddAsync(newBook, cancellationToken);
         var result = await _context.SaveChangesAsync(cancellationToken)<0;
         if (result)
         {
            throw new Exception("AN ERROR OCCURRED");
         }
         return newBook;
      }
   }
}