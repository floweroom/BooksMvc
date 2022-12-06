using BooksMvc.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DbBooks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BooksMvc.Repository
{
    public class BookRepository
    {
        protected DbBook _dbBooks;
        public BookRepository(DbBook dbBooks)
        {
            _dbBooks = dbBooks;
        }

        public async Task<IEnumerable<BookDto>> GetAll()
        {
            List<BookDto> result = new List<BookDto>();
            foreach (Book book in _dbBooks.Books.ToList())
            {
                result.Add(new BookDto
                {
                    Id = book.Id,
                    Name = book.Name,
                    Author = book.Author,
                    Pages = book.Pages,

                });
            }
            return result;




        }


        public async Task GetId(int Id)
        {
            if (Id != null)
            {
                Book IdBook = _dbBooks.Books.FirstOrDefault(x => x.Id == Id);



            }
            _dbBooks.SaveChanges();
        }


        public async Task Add(AddBook addBook)
        {


            Book bookdb = new Book()
            {
                Name = addBook.Name,
                Author = addBook.Author,
                Pages = addBook.Pages

            };

            _dbBooks.Add(bookdb);
            _dbBooks.SaveChanges();
        }

        public async Task Update(Update up)
        {
            Book book = new Book()
            {
                Name = up.Name,
                Author = up.Author,
                Pages = up.Pages
            };

            _dbBooks.Books.Update(book);
            _dbBooks.SaveChanges();
        }

        public async Task Delete(int id)
        {
            if (id != null)
            {
                Book book = new Book
                {
                    Id = id
                };
                _dbBooks.Entry(book).State = EntityState.Deleted;

                _dbBooks.SaveChanges();


            }
        }
        public Book[] Find(FindBookDto findBookDto)
        {
            IQueryable<Book> query = _dbBooks.Books;
            if (findBookDto.Name != null)
                query = query.Where(Book => Book.Name == findBookDto.Name);
            if (findBookDto.Author != null)
                query = query.Where(Book => Book.Author == findBookDto.Author);
            if (findBookDto.Pages != null)
                query = query.Where(Book => Book.Pages == findBookDto.Pages);

            return query.ToArray();
        }

    }
    
}

