using BusinessObject.Models;
using DataAccess.Repository.Interface;
using DataAccess.Services.Interface;

namespace DataAccess.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository repository;

        public PublisherService(IPublisherRepository repository)
        {
            this.repository = repository;
        }

        private void ValidatePublisher(Publisher publisher)
        {
            if (string.IsNullOrEmpty(publisher.publisher_name)) throw new Exception("Publisher name cannot be empty!");
            if (string.IsNullOrEmpty(publisher.city)) throw new Exception("City cannot be empty!");
            if (string.IsNullOrEmpty(publisher.state)) throw new Exception("State cannot be empty!");
            if (string.IsNullOrEmpty(publisher.country)) throw new Exception("Country cannot be empty!");
        }

        public async Task<IReadOnlyList<Publisher>> GetAllAsync()
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

        public async Task<Publisher> GetByIdAsync(int id)
        {
            try
            {
                var publisher = await repository.GetByIdAsync(id);
                if (publisher == null)
                {
                    throw new Exception("Publisher not found");
                }
                return publisher;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Publisher> AddAsync(Publisher publisher)
        {
            try
            {
                ValidatePublisher(publisher);
                return await repository.AddAsync(publisher);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(int id, Publisher publisher)
        {
            try
            {
                var publisherDB = await repository.GetByIdAsync(id);
                if (publisherDB == null)
                {
                    throw new Exception("Publisher not found");
                }

                ValidatePublisher(publisher);

                publisherDB.publisher_name = publisher.publisher_name;
                publisherDB.city = publisher.city;
                publisherDB.state = publisher.state;
                publisherDB.country = publisher.country;

                await repository.UpdateAsync(publisherDB);
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
                var publisher = await repository.GetByIdAsync(id);
                if (publisher == null)
                {
                    return false;
                }

                await repository.DeleteAsync(publisher);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
