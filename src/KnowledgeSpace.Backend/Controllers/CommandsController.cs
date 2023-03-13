using KnowledgeSpace.Backend.Data;
using KnowledgeSpace.ViewModels.Systems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeSpace.Backend.Controllers
{
    public class CommandsController : BaseController
    {
        private readonly ApplicationDBContext _context;

        public CommandsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCommants()
        {
            var commands = _context.Commands;

            var commandVms = await commands.Select(u => new CommandVm()
            {
                Id = u.Id,
                Name = u.Name,
            }).ToListAsync();

            return Ok(commandVms);
        }
    }
}
