using Estoque.Domain.Models;

namespace EstoqueApi.Interface
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
