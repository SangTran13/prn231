using BusinessObject.Models;
using DataAccess.Repository.Interface;
using DataAccess.Services.Interface;

namespace DataAccess.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IRoleRepository roleRepository;
        private readonly IPublisherRepository publisherRepository;

        public UserService(IUserRepository repository, IRoleRepository roleRepository, IPublisherRepository publisherRepository)
        {
            this.repository = repository;
            this.roleRepository = roleRepository;
            this.publisherRepository = publisherRepository;
        }

        private void ValidateUser(User user)
        {
            if (string.IsNullOrEmpty(user.email_address)) throw new Exception("Email cannot be empty!");
            if (string.IsNullOrEmpty(user.password)) throw new Exception("Password cannot be empty!");
            if (string.IsNullOrEmpty(user.first_name)) throw new Exception("First name cannot be empty!");
            if (string.IsNullOrEmpty(user.last_name)) throw new Exception("Last name cannot be empty!");
            if (string.IsNullOrEmpty(user.source)) throw new Exception("Source cannot be empty!");
            if (user.role_id <= 0) throw new Exception("Invalid Role ID!");
            if (user.pub_id <= 0) throw new Exception("Invalid Publisher ID!");
            if (user.hire_date == default) throw new Exception("Hire date is required!");
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            try
            {
                var users = await repository.GetAllAsync();
                foreach (var user in users)
                {
                    user.Role = await roleRepository.GetByIdAsync(user.role_id);
                    user.Publisher = await publisherRepository.GetByIdAsync(user.pub_id);
                }
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                var user = await repository.GetByIdAsync(id);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                user.Role = await roleRepository.GetByIdAsync(user.role_id);
                user.Publisher = await publisherRepository.GetByIdAsync(user.pub_id);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> AddAsync(User user)
        {
            try
            {
                ValidateUser(user);

                user.Role = await roleRepository.GetByIdAsync(user.role_id)
                    ?? throw new Exception("Role not found!");
                user.Publisher = await publisherRepository.GetByIdAsync(user.pub_id)
                    ?? throw new Exception("Publisher not found!");

                return await repository.AddAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(int id, User user)
        {
            try
            {
                var userDB = await repository.GetByIdAsync(id);
                if (userDB == null)
                {
                    throw new Exception("User not found");
                }

                ValidateUser(user);

                userDB.email_address = user.email_address;
                userDB.password = user.password;
                userDB.source = user.source;
                userDB.first_name = user.first_name;
                userDB.middle_name = user.middle_name;
                userDB.last_name = user.last_name;
                userDB.role_id = user.role_id;
                userDB.pub_id = user.pub_id;
                userDB.hire_date = user.hire_date;

                await repository.UpdateAsync(userDB);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var user = await repository.GetByIdAsync(id);
                if (user == null)
                {
                    return false;
                }

                await repository.DeleteAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User?> CheckLoginAsync(string email)
        {
            try
            {
                var users = await repository.GetAllAsync();
                return users.FirstOrDefault(x => x.email_address == email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
