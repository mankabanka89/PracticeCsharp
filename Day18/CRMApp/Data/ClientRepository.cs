using Microsoft.EntityFrameworkCore;
using CRMApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMApp.Data
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly AppDbContext _context;

        public ClientRepository()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task AddAsync(Client entity)
        {
            await _context.Clients.AddAsync(entity);
        }

        public Task UpdateAsync(Client entity)
        {
            _context.Clients.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Client entity)
        {
            _context.Clients.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}