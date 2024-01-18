using EvoSys.Models;
using System.ComponentModel.DataAnnotations;

namespace EvoSys.InputModels
{
    public class FuncionarioInputModel
    {
        public string Nome { get; set; }
        public IFormFile? Foto { get; set; }
        [MinLength(3, ErrorMessage = "Números insuficientes, tente novamente.")]
        public string RG { get; set; }
        public int DepartamentoId { get; set; }
    }
}
