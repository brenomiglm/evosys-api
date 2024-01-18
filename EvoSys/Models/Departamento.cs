namespace EvoSys.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set;}
        public IEnumerable<Funcionario> Funcionarios { get; set; }
    }
}
