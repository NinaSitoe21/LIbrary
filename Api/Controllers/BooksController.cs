using Aplication.Books;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


public class BooksController:BaseController
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpPost]
    public async Task<ActionResult<Book>> AddBook(AddBook.AddBooKCommand bookCommand)
    {
        return await _mediator.Send(bookCommand);
    }

    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetAllBooks()
    {
        return await _mediator.Send(new ListAllBooks.ListAllBooksQuery());
    }
    
    //[HttpGet("")]
    //public async Task<ActionResult<List<Book>>> SearchBook()
    //{
      //  return await _mediator.Send(new ListAllBooks.ListAllBooksQuery());
    //}

}