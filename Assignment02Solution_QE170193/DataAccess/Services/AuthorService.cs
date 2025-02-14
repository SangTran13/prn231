using BusinessObject.Models;
using DataAccess.Repository.Interface;
using DataAccess.Services.Interface;

namespace DataAccess.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository repository;

        public AuthorService(IAuthorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IReadOnlyList<Author>> GetAllAsync()
        {
            try
            {
                return await repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Author> GetAsync(int id)
        {
            try
            {
                var author = await repository.GetByIdAsync(id);
                if (author != null)
                {
                    return author;
                }
                throw new Exception("Author Not Found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Author> AddAsync(Author author)
        {
            try
            {
                var check = CheckValidation(author);
                return await repository.AddAsync(check);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(int id, Author author)
        {
            try
            {
                var authorDB = await repository.GetByIdAsync(id);
                if (authorDB != null)
                {
                    var check = CheckValidation(author);
                    authorDB.last_name = check.last_name;
                    authorDB.first_name = check.first_name;
                    authorDB.phone = check.phone;
                    authorDB.address = check.address;
                    authorDB.city = check.city;
                    authorDB.state = check.state;
                    authorDB.zip = check.zip;
                    authorDB.email_address = check.email_address;

                    await repository.UpdateAsync(authorDB);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<Author> GetAll()
        {
            return repository.GetAll();  
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var authorDB = await repository.GetByIdAsync(id);
                if (authorDB != null)
                {
                    await repository.DeleteAsync(authorDB);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private Author CheckValidation(Author author)
        {
            if (string.IsNullOrEmpty(author.last_name))
                throw new Exception("Last Name cannot be empty!!!");
            if (string.IsNullOrEmpty(author.first_name))
                throw new Exception("First Name cannot be empty!!!");
            if (string.IsNullOrEmpty(author.phone))
                throw new Exception("Phone cannot be empty!!!");
            if (string.IsNullOrEmpty(author.address))
                throw new Exception("Address cannot be empty!!!");
            if (string.IsNullOrEmpty(author.city))
                throw new Exception("City cannot be empty!!!");
            if (string.IsNullOrEmpty(author.state))
                throw new Exception("State cannot be empty!!!");
            if (string.IsNullOrEmpty(author.zip))
                throw new Exception("Zip cannot be empty!!!");
            if (string.IsNullOrEmpty(author.email_address))
                throw new Exception("EmailAddress cannot be empty!!!");

            return author;
        }

    }
}
