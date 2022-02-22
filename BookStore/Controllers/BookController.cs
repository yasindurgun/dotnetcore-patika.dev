using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBooks;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
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
        public Book GetById(int id)
        {
            //var book = books.Where(x => x.Id == id).SingleOrDefault();
            //return book;

            var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            return book;

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

            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
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

            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
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

            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
