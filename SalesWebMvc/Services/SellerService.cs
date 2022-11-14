using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;

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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        //Metodo que retorna um Seller com um determinado Id
        public Seller FindById(int id)
        {
            return _context.Seller.Include(y => y.Department).FirstOrDefault(x => x.Id == id);
        }

        //Metodo que remove do meu DbSet esse objeto 
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if(!_context.Seller.Any(sr => sr.Id == seller.Id))
            {
                throw new NotFoundException("Id not found!");
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }catch(DbConcurrencyException e)
            {
                //Esta menssagem proveim da Base de dados
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
