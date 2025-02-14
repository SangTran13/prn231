using BusinessObject.Models;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository
{
    public class PublisherRepository : RepositoryBase<Publisher, int>, IPublisherRepository
    {
        public PublisherRepository(EBookStoreDBContext context) : base(context)
        {
        }
    }
}
