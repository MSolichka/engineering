using Microsoft.AspNetCore.Identity;

namespace EShop.Domain.Models;

public class User : IdentityUser<Guid>, IBaseEntity<Guid>
{
    public ICollection<ProductLike> ProductLikes { get; set; }
}