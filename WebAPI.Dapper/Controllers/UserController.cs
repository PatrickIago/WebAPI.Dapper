using Microsoft.AspNetCore.Mvc;
using WebAPI.Dapper.Models;
using WebAPI.Dapper.Repositories;

namespace WebAPI.Dapper.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("Buscar por todos os usuarios")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter usuários: {ex.Message}");
        }
    }

    [HttpGet("Buscar usuario por Id especifico{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var user = await _userService.GetById(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter usuário: {ex.Message}");
        }
    }

    [HttpPost("Adicionar novo usuario")]
    public async Task<IActionResult> CreateUser(User user)
    {
        try
        {
            var createUser = await _userService.Create(user);
            return Ok(createUser);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar usuário: {ex.Message}");
        }
    }

    [HttpPut("Atualizar um usuario")]
    public async Task<IActionResult> UpdateUser(User user)
    {
        try
        {
            if (user.Id != user.Id)
            {
                return BadRequest("ID do usuário não corresponde ao ID fornecido na requisição.");
            }

            var updatedUser = await _userService.Update(user);
            if (updatedUser != null)
            {
                return Ok(updatedUser);
            }
            else
            {
                return NotFound($"Usuário com ID {user.Id} não encontrado.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar usuário: {ex.Message}");
        }
    }

    [HttpDelete("Deletar um usuario: {id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var deletedUser = await _userService.Delete(id);
            if (deletedUser != null)
            {
                return Ok(deletedUser);
            }
            else
            {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao excluir usuário: {ex.Message}");
        }
    }
}


