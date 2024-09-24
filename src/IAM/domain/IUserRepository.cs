using tecnosor.cleanarchitecture.common.domain.match;

namespace iam.domain;

public interface IUserRepository : IMatch<User>
{
    void CreateUser(User user);
    Task CreateUserAsync(User user);

    User GetUserById(string id);
    Task<User> GetUserByIdAsync(string id);
    User GetUserByUserName(string userName);
    Task<User> GetUserByUserNameAsync(string id);
    User GetUserByEmail(string email);
    Task<User> GetUserByEmailAsync(string email);
}