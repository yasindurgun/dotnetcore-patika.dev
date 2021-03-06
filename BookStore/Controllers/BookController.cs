using AutoMapper;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //private static List<Book> books = new List<Book>()
        //{
        //    new Book
        //    {
        //        Id = 1,
        //        Title = "Lean Startup",
        //        GenreId = 1, //Personal Growth
        //        PageCount = 200,
        //        PublishDate = new DateTime(2001,06,12)
        //    },
        //    new Book
        //    {
        //        Id = 2,
        //        Title = "Herland",
        //        GenreId = 2, //Science Fiction
        //        PageCount = 250,
        //        PublishDate = new DateTime(2010,05,12)
        //    },
        //    new Book
        //    {
        //        Id = 3,
        //        Title = "Dune",
        //        GenreId = 1, //Science Fiction
        //        PageCount = 540,
        //        PublishDate = new DateTime(2001,12,21)
        //    }
        //};

        [HttpGet]
        public IActionResult GetBooks()
        {
            //var bookList = books.OrderBy(x => x.Id).ToList<Book>();
            //return bookList;

            //var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            //return bookList;

            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //var book = books.Where(x => x.Id == id).SingleOrDefault();
            //return book;

            //var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            //return book;

            //BookDetailViewModel result;

                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
          
            return Ok(query);
        }

        //[HttpGet]
        //public Book GetById([FromQuery]string id)
        //{
        //    var book = books.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}

        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            //var book = books.SingleOrDefault(x=> x.Title == newBook.Title);
            //if (book is not null)
            //{
            //    return BadRequest();
            //}
            //books.Add(newBook);
            //return Ok();

            //var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);
            //if (book is not null)
            //{
            //    return BadRequest();
            //}
            //_context.Books.Add(newBook);
            //_context.SaveChanges();
            //return Ok();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            
                
                command.Model = newBook;

                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

                //ValidationResult result = validator.Validate(command);
                //if (!result.IsValid)
                //{
                //    foreach (var item in result.Errors)
                //    {
                //        Console.WriteLine("Özellik: " + item.PropertyName + "- Error Massage: " + item.ErrorMessage);
                //    }
                //}
                //else
                //    command.Handle();
            
            return Ok();
        }

        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
            //var book = books.SingleOrDefault(x=> x.Id == id);
            //if (book is null)
            //{
            //    return BadRequest();
            //}

            //book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            //book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            //book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            //book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            //return Ok();

            //var book = _context.Books.SingleOrDefault(x => x.Id == id);
            //if (book is null)
            //{
            //    return BadRequest();
            //}

            //book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            //book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            //book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            //book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            //_context.SaveChanges();
            //return Ok();
           
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
           
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            //var book = books.SingleOrDefault(x => x.Id == id);
            //if (book is null)
            //{
            //    return BadRequest();
            //}
            //books.Remove(book);
            //return Ok();

            //var book = _context.Books.SingleOrDefault(x => x.Id == id);
            //if (book is null)
            //{
            //    return BadRequest();
            //}
            //_context.Books.Remove(book);
            //_context.SaveChanges();
            //return Ok();

           
          
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;

                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
            
            return Ok();
        }
    }
}
