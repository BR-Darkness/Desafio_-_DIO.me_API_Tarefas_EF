using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [Tags("c.R.u.d - Ler/Buscar")]
        public IActionResult ObterPorId(int id)
        {
            Tarefa tarefa = _context.Tarefas.Where(tarefa => tarefa.Id == id).FirstOrDefault();
            
            if (tarefa is null) return NotFound();

            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        [Tags("c.R.u.d - Ler/Buscar")]
        public IActionResult ObterTodos()
        {
            return Ok(_context.Tarefas.ToList());
        }

        [HttpGet("ObterPorTitulo")]
        [Tags("c.R.u.d - Ler/Buscar")]
        public IActionResult ObterPorTitulo(string titulo)
        {            
            IQueryable<Tarefa> listaTarefa = _context.Tarefas.Where(
                x => x.Titulo.Contains(titulo)
            );

            if (listaTarefa is null) return NotFound();

            return Ok(listaTarefa);
        }

        [HttpGet("ObterPorData")]
        [Tags("c.R.u.d - Ler/Buscar")]
        public IActionResult ObterPorData(DateTime data)
        {
            IQueryable<Tarefa> listaTarefa = _context.Tarefas.Where(
                x => x.Data.Date == data.Date
            );

            if (listaTarefa is null) return NotFound();

            return Ok(listaTarefa);
        }

        [HttpGet("ObterPorStatus")]
        [Tags("c.R.u.d - Ler/Buscar")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            IQueryable<Tarefa> listaTarefa = _context.Tarefas.Where(
                x => x.Status == status
            );

            if (listaTarefa is null) return NotFound();

            return Ok(listaTarefa);
        }

        [HttpPost]
        [Tags("C.r.u.d - Criar/Adicionar")]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
            {
                return BadRequest(
                    new { Erro = "A data da tarefa não pode ser vazia" }
                );
            }

            if (string.IsNullOrWhiteSpace(tarefa.Titulo))
            {
                return BadRequest(
                    new { Erro = "O titulo da tarefa não pode ser vazio" }
                );
            }

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        [Tags("c.r.U.d - Editar/Atualizar")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            Tarefa tarefaBanco = _context.Tarefas.Where(tarefa => tarefa.Id == id).FirstOrDefault();
            if (tarefaBanco == null) return NotFound();

            if (tarefa.Data == DateTime.MinValue)
            {
                return BadRequest(
                    new { Erro = "A data da tarefa não pode ser vazia" }
                );
            }

            tarefaBanco.Titulo      = tarefa.Titulo;
            tarefaBanco.Descricao   = tarefa.Descricao;
            tarefaBanco.Data        = tarefa.Data;
            tarefaBanco.Status      = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Tags("c.r.u.D - Remover/Deletar")]
        public IActionResult Deletar(int id)
        {
            Tarefa tarefaBanco = _context.Tarefas.Where(tarefa => tarefa.Id == id).FirstOrDefault();
            if (tarefaBanco == null) return NotFound();

            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
