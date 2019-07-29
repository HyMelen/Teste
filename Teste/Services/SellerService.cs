using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Models;
using Microsoft.EntityFrameworkCore;
using Teste.Services.Exceptions;

namespace Teste.Services
{
    // Inserindo paralelismo com Task, async e await... 
    // Em todas as operações que acessam  bancos de dados.
    public class SellerService
    {
        private readonly TesteContext _testeContext;

        public SellerService(TesteContext testeContext)
        {
            _testeContext = testeContext;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _testeContext.Seller.ToListAsync(); 
        }

        public async Task InsertAsync(Seller obj)
        {
            _testeContext.Add(obj);
            await _testeContext.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id )
        {
            return await _testeContext.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = _testeContext.Seller.Find(id);
            _testeContext.Remove(obj);
            await _testeContext.SaveChangesAsync();
        }

        public async Task updateAsync(Seller obj)
        {

            bool hasAny = await _testeContext.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _testeContext.Seller.Update(obj);
                await _testeContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }
    }
}
