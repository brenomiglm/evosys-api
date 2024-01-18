using EvoSys.Data;
using EvoSys.Models;
using Microsoft.EntityFrameworkCore;

namespace EvoSys.Repositories
{
    public class FuncionarioRepository
    {
        private readonly MainContext context;

        public FuncionarioRepository(MainContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<Funcionario> ObterTodos(int? departamentoId)
        {
            return context.Funcionarios.Where(x=>departamentoId.HasValue ?x.DepartamentoId == departamentoId.Value : true).Include(x=> x.Departamento).ToList();
        }

        public Funcionario ObterPorId(int id)
        {
            return context.Funcionarios.FirstOrDefault(f => f.Id == id);
        }

        public void Adicionar(Funcionario funcionario)
        {
            context.Funcionarios.Add(funcionario);
            context.SaveChanges();
        }

        public void Atualizar(Funcionario funcionario)
        {
            context.Funcionarios.Update(funcionario);
            context.SaveChanges();
        }

        public void Excluir(int id)
        {
            var funcionario = context.Funcionarios.FirstOrDefault(f => f.Id == id);
            if (funcionario != null)
            {
                context.Funcionarios.Remove(funcionario);
                context.SaveChanges();
            }
        }
    }
}
