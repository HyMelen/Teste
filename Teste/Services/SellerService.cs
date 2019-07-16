using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Models;

namespace Teste.Services
{
    public class SellerService
    {
        private readonly TesteContext _testeContext;

        public SellerService(TesteContext testeContext)
        {
            _testeContext = testeContext;
        }

        public List<Seller> FindAll()
        {
            return _testeContext.Seller.ToList(); 
        }

        public void Insert(Seller obj)
        {
            obj.Department = _testeContext.Department.First();
            _testeContext.Add(obj);
            _testeContext.SaveChanges();
        }


    }
}
