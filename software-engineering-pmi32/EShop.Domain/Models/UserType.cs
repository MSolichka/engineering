using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace EShop.Domain.Models;

public class UserType : IdentityRole<Guid>, IBaseEntity<Guid>
{
}