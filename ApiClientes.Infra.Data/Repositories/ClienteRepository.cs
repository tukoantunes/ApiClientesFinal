using ApiClientes.Infra.Data.Contexts;
using ApiClientes.Infra.Data.Entities;
using ApiClientes.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientes.Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly SqlServerContext _sqlServerContext;

        public ClienteRepository(SqlServerContext sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }

        public void Create(Cliente entity)
        {
            _sqlServerContext.Cliente.Add(entity);
            _sqlServerContext.SaveChanges();
        }

        public void Update(Cliente entity)
        {
            _sqlServerContext.Entry(entity).State = EntityState.Modified;
            _sqlServerContext.SaveChanges();
        }

        public void Delete(Cliente entity)
        {
            _sqlServerContext.Cliente.Remove(entity);
            _sqlServerContext.SaveChanges();
        }

        public List<Cliente> GetAll()
        {
            return _sqlServerContext.Cliente
              .OrderBy(e => e.Nome)
              .ToList();
        }

        public Cliente GetByCpf(string cpf)
        {
            return _sqlServerContext.Cliente
               .AsNoTracking()
               .FirstOrDefault(e => e.Cpf == cpf);
        }

        public Cliente GetByEmail(string email)
        {
            return _sqlServerContext.Cliente
               .AsNoTracking()
               .FirstOrDefault(e => e.Email == email);
        }

        public Cliente GetById(Guid id)
        {
            return _sqlServerContext.Cliente
               .AsNoTracking()
               .FirstOrDefault(e => e.IdCliente == id);
        }
    }
}
