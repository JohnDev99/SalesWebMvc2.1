using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;
using System;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        //Injeçao de dependencia no construtor
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        //Metodo que retorna um Seller com um determinado Id
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(y => y.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        //Metodo que remove do meu DbSet esse objeto 
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool any = await _context.Seller.AnyAsync(sr => sr.Id == seller.Id);

            if (!any)
            {
                throw new NotFoundException("Id not found!");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }catch(DbConcurrencyException e)
            {
                //Esta menssagem proveim da Base de dados
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
