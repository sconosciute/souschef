using souschef_core.Model;

namespace souschef_core.Services;

public interface IUserSvc
{
    Task<User?> GetUserAsync(string username);

    Task<User?> SendUserAsync(User? user);

}