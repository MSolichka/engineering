using EShop.Application.Abstractions;

namespace EShop.Application.User;

public interface IUserRepository : IRepository<Guid,Domain.Models.User>
{
    
}