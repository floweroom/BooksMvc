﻿using BooksMvc.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DbBooks;
using System.Collections.Generic;

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



        public Book GetId(int Id)
        {

            Book IdBook = _dbBooks.Books.FirstOrDefault(x => x.Id == Id);



            return IdBook;
        }
        public void Add(BookDto book)
        {
            

            Book bookdb = new Book()
            {
                Name = book.Name,
                Author = book.Author,
                Pages = book.Pages
               
            };

            _dbBooks.Add(book);
            _dbBooks.SaveChanges();
        }

    
        public void Delete(int Id) 
        {
            Book bookdb = _dbBooks.Books.FirstOrDefault(x => x.Id == Id);
            if(bookdb!=null)  
           _dbBooks.Books.Remove(bookdb);
            _dbBooks.SaveChanges();
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

