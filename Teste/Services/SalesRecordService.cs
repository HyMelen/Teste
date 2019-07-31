using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Models;
using Microsoft.EntityFrameworkCore;

namespace Teste.Services
{
    public class SalesRecordService
    {
        private readonly TesteContext _context;

        public SalesRecordService(TesteContext testeContext)
        {
            _context = testeContext;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) //Os parâmetros data min e max são opcionais
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue )
            {
                result = result.Where(obj => obj.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(obj => obj.Date <= maxDate.Value);
            }

            return await result              //Devolve uma lista limitada e ordenada por Data, juntamente com a propriedades Seller, Department. 
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }

    }
}
