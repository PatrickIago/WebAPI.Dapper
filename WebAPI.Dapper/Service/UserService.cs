using WebAPI.Dapper.Data;
using WebAPI.Dapper.Models;
using System.Data;
using Dapper;
using static Dapper.SqlMapper;
using WebAPI.Dapper.Repositories;

namespace WebAPI.Dapper.Service;
public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IDbConnection _dbConnection;
    public UserService(AppDbContext context, IDbConnection dbConnection)
    {
        _context = context;
        _dbConnection = dbConnection;
    }

    public async Task<User> Create(User entity)
    {
        var sqlCreate = @"INSERT INTO Users (Name,Age) VALUES (@Name,@Age); SELECT CAST(SCOPE_IDENTITY() as int);"; // OBTER O ID NO NOVO USUARIO
        var id = await _dbConnection.QueryFirstAsync<int>(sqlCreate,entity); // VERIFICA SE A INSERÇÃO FOI BEM SUCEDIDA

        if (id > 0)
        {
            entity.Id = id;
            return entity;
        }
        else
        {
            throw new Exception("Falha ao criar o usuário.");
        }
    }

    public async Task<User> Delete(int id)
    {
       var sqlDelete = @"DELETE FROM Users WHERE Id = @Id";
       var affectedRows = await _dbConnection.ExecuteAsync(sqlDelete, new {Id = id}); // verifica se algum linha foi excluído com sucesso com base no número de linhas afetadas pela consulta

        if (affectedRows > 0) //Verifica se alguma linha foi afetada pela exclusão
        {
            // Retorna o usuário deletado
            return new User { Id = id };
        }
        else
        {
            // Retorna null se nenhum usuário foi deletado
            return null;
        } 
    }

    public async Task<List<User>> GetAll()
    {
        var sql = @"SELECT * FROM Users";
        var users =  (List<User>)await _dbConnection.QueryAsync<User>(sql);
        return users;
    }

    public async Task<User> GetById(int id)
    {
       var sql = @"SELECT * FROM Users WHERE Id = @Id";
       var user = await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
       return user;
    }

    public async Task<User> Update(User entity)
    {
        var sqlUpdate = @"UPDATE Users SET Name = @Name, Age = @Age WHERE Id = @Id";
        var affectedRows = await _dbConnection.ExecuteAsync(sqlUpdate,entity);

        if (affectedRows > 0) // Verifica se alguma linha foi afetada pela atualização
        {
            // Retorna o próprio objeto entity após a atualização bem-sucedida
            return entity;
        }
        else
        {
            // Retorna null se a atualização não foi bem-sucedida
            return null;
        }
    }
}
