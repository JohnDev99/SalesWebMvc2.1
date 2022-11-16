using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)//Argumentos opcionais
        {
            var result = from obj in _context.Sales select obj;//Defeniçao da primeira parte da query

            if (minDate.HasValue)
            {
                //Adicionar informaçao adicional a query principal se o parametro receber um argumento
                result = result.Where(sr => sr.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                //Adicionar tambem esta informaçao na query principal
                result = result.Where(sr => sr.Date <= maxDate.Value);
            }

            return await result.Include(sr => sr.Seller)//Incluir todos os dados do objeto Seller
                .Include(sr => sr.Seller.Department)//Incluir o departamento(Classe associada ao objeto Seller)
                .OrderByDescending(sr => sr.Date)//Ordenar por uma regra(data, ordem decrescente)
                .ToListAsync();//Retorno resultado da query em uma tabela

        }
    }
}
