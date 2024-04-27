using WebAPI.Dapper.Models;

namespace WebAPI.Dapper.Service;
public interface IUserService
{
    Task<List<User>> GetAll();
    Task<User> GetById(int id);
    Task<User> Create(User entity);
    Task<User> Update(User entity);
    Task<User> Delete(int id);
}
