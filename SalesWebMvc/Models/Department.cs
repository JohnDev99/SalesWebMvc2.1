using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private ICollection<Seller> _sellers = new List<Seller>();

        public Department() { }
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            _sellers.Add(seller);
        }

        public double ToTalSales(DateTime initial, DateTime final)
        {
            return _sellers.Sum(sr => sr.TotalSales(initial, final));
        }
    }
}
