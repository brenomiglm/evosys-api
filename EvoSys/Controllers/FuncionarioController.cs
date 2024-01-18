using Microsoft.AspNetCore.Mvc;
using EvoSys.Models;
using EvoSys.InputModels;
using EvoSys.Repositories;
using EvoSys.Data;

namespace EvoSys.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioRepository funcionarioRepository;
        private readonly string uploadFolderPath = "wwwroot/uploads";

        public FuncionarioController()
        {
            var dbContext = new MainContext();
            funcionarioRepository = new FuncionarioRepository(dbContext);
        }

        [HttpGet("todos")]
        public IActionResult ObterTodos(int? departamentoId = null)
        {
            var funcionarios = funcionarioRepository.ObterTodos(departamentoId);
            return Ok(funcionarios);
        }

        [HttpGet("obter/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var funcionario = funcionarioRepository.ObterPorId(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        [HttpPost("adicionar")]
        public IActionResult AdicionarFuncionario([FromForm] FuncionarioInputModel inputModel)
        {
            if (inputModel == null)
            {
                return BadRequest("Dados inválidos");
            }

            var novoFuncionario = new Funcionario
            {
                Nome = inputModel.Nome,
                RG = inputModel.RG,
                DepartamentoId = inputModel.DepartamentoId
            };

            if (inputModel.Foto != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(inputModel.Foto.FileName);
                var filePath = Path.Combine(uploadFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    inputModel.Foto.CopyTo(stream);
                }

                novoFuncionario.Foto = fileName;
            }

            funcionarioRepository.Adicionar(novoFuncionario);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoFuncionario.Id }, novoFuncionario);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult AtualizarFuncionario(int id, [FromForm] FuncionarioInputModel inputModel)
        {
            var funcionarioExistente = funcionarioRepository.ObterPorId(id);

            if (funcionarioExistente == null)
            {
                return NotFound();
            }

            funcionarioExistente.Nome = inputModel.Nome;
            funcionarioExistente.RG = inputModel.RG;
            funcionarioExistente.DepartamentoId = inputModel.DepartamentoId;

            if (inputModel.Foto != null)
            {
                if (!string.IsNullOrEmpty(funcionarioExistente.Foto))
                {
                    var fotoExistentePath = Path.Combine(uploadFolderPath, funcionarioExistente.Foto);
                    if (System.IO.File.Exists(fotoExistentePath))
                    {
                        System.IO.File.Delete(fotoExistentePath);
                    }
                }

                var novoNomeFoto = Guid.NewGuid().ToString() + Path.GetExtension(inputModel.Foto.FileName);
                var novoCaminhoFoto = Path.Combine(uploadFolderPath, novoNomeFoto);

                using (var stream = new FileStream(novoCaminhoFoto, FileMode.Create))
                {
                    inputModel.Foto.CopyTo(stream);
                }

                funcionarioExistente.Foto = novoNomeFoto;
            }

            funcionarioRepository.Atualizar(funcionarioExistente);

            return Ok(funcionarioExistente);
        }

        [HttpGet("verfoto/{id}")]
        public IActionResult VerFoto(int id)
        {
            var funcionario = funcionarioRepository.ObterPorId(id);

            if (funcionario == null || string.IsNullOrEmpty(funcionario.Foto))
            {
                return NotFound("Foto não encontrada");
            }

            var fotoPath = Path.Combine(uploadFolderPath, funcionario.Foto);


            if (System.IO.File.Exists(fotoPath))
            {
                byte[] fotoBytes = System.IO.File.ReadAllBytes(fotoPath);
                return File(fotoBytes, "image/jpeg");
            }
            else
            {
                return NotFound("Foto não encontrada");
            }
        }


        [HttpDelete("excluir/{id}")]
        public IActionResult ExcluirFuncionario(int id)
        {
            var funcionarioExistente = funcionarioRepository.ObterPorId(id);

            if (funcionarioExistente == null)
            {
                return NotFound();
            }

            if (funcionarioExistente.Foto != null)
            {
                var filePath = Path.Combine(uploadFolderPath, funcionarioExistente.Foto);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            funcionarioRepository.Excluir(id);
            return NoContent();
        }
    }
}
