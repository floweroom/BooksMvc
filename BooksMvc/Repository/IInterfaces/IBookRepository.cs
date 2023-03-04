using BooksMvc.Models;
using DbBooks;

namespace BooksMvc.Repository.IInterfaces
{
    public interface IBookRepository
    {
        public  Task<IEnumerable<BookDto>> GetAll();


        public Task<Book> GetId(int Id);
        


        public  Task Add(AddBook addBook);


        public  Task Update(int Id, Update up);


        public  Task Delete(int id);

        public Book[] Find(FindBookDto findBookDto);
       

    }

}
    
