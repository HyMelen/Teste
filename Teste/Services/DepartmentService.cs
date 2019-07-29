
using System.Collections.Generic;
using System.Linq;
using Teste.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Teste.Services
{
    public class DepartmentService
    {
        private readonly TesteContext _testeContext;

        public DepartmentService(TesteContext testeContext)
        {
            _testeContext = testeContext;
        }


        // Inserindo paralelismo com Task, async e await... 
        // Em todas as operações que acessam  bancos de dados.


        public async Task<List<Department>> FindAllAsync()
        {
            return await _testeContext.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
