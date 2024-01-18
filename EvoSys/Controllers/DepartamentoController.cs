using EvoSys.Data;
using EvoSys.InputModels;
using EvoSys.Models;
using EvoSys.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EvoSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly DepartamentoRepository _repository;

        public DepartamentoController()
        {
            var context = new MainContext();
            _repository = new DepartamentoRepository(context);
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar(DepartamentoInputModel departamento)
        {
            var model = new Departamento();
            model.Nome = departamento.Nome;
            model.Sigla = departamento.Sigla;
            await _repository.AdicionarAsync(model);
            return Ok();
        }

        [HttpGet("todos")]
        public async Task<IEnumerable<Departamento>> ObterTodos()
        {
            return await _repository.ObterTodosAsync();
        }

        [HttpGet("obter/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var departamento = _repository.ObterPorId(id);

            if (departamento == null)
            {
                return NotFound();
            }

            return Ok(departamento);
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> Atualizar(DepartamentoInputModel departamento, int id)
        {
            var model = new Departamento();
            model.Id = id;
            model.Nome = departamento.Nome;
            model.Sigla = departamento.Sigla;
            await _repository.AtualizarAsync(model);
            return Ok();
        }

        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            await _repository.ExcluirAsync(id);
            return Ok();
        }
    }
}