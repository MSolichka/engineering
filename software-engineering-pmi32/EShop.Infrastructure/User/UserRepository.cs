using EShop.Application.User;
using EShop.Infrastructure.Database;

namespace EShop.Infrastructure.User;

public class UserRepository : BaseRepository<Guid,Domain.Models.User,ApplicationDbContext>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}