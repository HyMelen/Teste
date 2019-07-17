
using System.Collections.Generic;
using System.Linq;
using Teste.Models;

namespace Teste.Services
{
    public class DepartmentService
    {
        private readonly TesteContext _testeContext;

        public DepartmentService(TesteContext testeContext)
        {
            _testeContext = testeContext;
        }

        public List<Department> FindAll()
        {
            return _testeContext.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
