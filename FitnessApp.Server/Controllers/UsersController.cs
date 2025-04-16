using FitnessApp.Server.Data;
using FitnessApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Server.Controllers
{
    [ApiController] // Indica que esta classe é um controlador de API.
    [Route("api/[controller]")] // Define a rota base para os endpoints deste controlador.
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Construtor que injeta o contexto do banco de dados.
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint para obter todos os usuários.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            // Retorna a lista de todos os usuários do banco de dados.
            return await _context.Users.ToListAsync();
        }

        // Endpoint para obter um usuário específico pelo ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            // Busca o usuário pelo ID no banco de dados.
            var user = await _context.Users.FindAsync(id);

            // Retorna 404 (Not Found) se o usuário não for encontrado.
            if (user == null) return NotFound();

            // Retorna o usuário encontrado.
            return user;
        }

        // Endpoint para criar um novo usuário.
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            // Adiciona o novo usuário ao banco de dados.
            _context.Users.Add(user);

            // Salva as alterações no banco de dados.
            await _context.SaveChangesAsync();

            // Retorna 201 (Created) com a localização do novo recurso.
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // Endpoint para atualizar um usuário existente.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            // Verifica se o ID fornecido corresponde ao ID do usuário.
            if (id != user.Id) return BadRequest();

            // Marca o usuário como modificado no contexto.
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                // Salva as alterações no banco de dados.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verifica se o usuário ainda existe no banco de dados.
                if (!_context.Users.Any(e => e.Id == id)) return NotFound();

                // Relança a exceção se for um erro de concorrência.
                throw;
            }

            // Retorna 204 (No Content) indicando sucesso sem conteúdo adicional.
            return NoContent();
        }

        // Endpoint para excluir um usuário existente.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Busca o usuário pelo ID no banco de dados.
            var user = await _context.Users.FindAsync(id);

            // Retorna 404 (Not Found) se o usuário não for encontrado.
            if (user == null) return NotFound();

            // Remove o usuário do banco de dados.
            _context.Users.Remove(user);

            // Salva as alterações no banco de dados.
            await _context.SaveChangesAsync();

            // Retorna 204 (No Content) indicando sucesso sem conteúdo adicional.
            return NoContent();
        }
    }
}
