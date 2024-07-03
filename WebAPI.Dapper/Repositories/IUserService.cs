using WebAPI.Dapper.Models;
namespace WebAPI.Dapper.Repositories;
public interface IUserService
{
    public Task<List<User>> GetAll();
    public Task<User> GetById(int id);
    public Task<User> Create(User entity);
    public Task<User> Update(User entity);
    public Task<User> Delete(int id);
}
