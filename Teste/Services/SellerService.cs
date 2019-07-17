using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Models;
using Microsoft.EntityFrameworkCore;

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
            _testeContext.Add(obj);
            _testeContext.SaveChanges();
        }

        public Seller FindById(int id )
        {
            return _testeContext.Seller.Include(obj => obj.Department).FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _testeContext.Seller.Find(id);
            _testeContext.Remove(obj);
            _testeContext.SaveChanges();
        }



    }
}
