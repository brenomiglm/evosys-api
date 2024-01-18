using System.ComponentModel.DataAnnotations;

namespace EvoSys.InputModels
{
    public class DepartamentoInputModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [MaxLength(3, ErrorMessage = "A Sigla não pode conter mais de 3 Caracteres")]
        public string Sigla { get; set; }
    }
}

