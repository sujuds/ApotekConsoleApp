using ApotekConsoleApp.DbContexts;
using ApotekConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApotekConsoleApp.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly ApotekDbContextFactory _contextFactory;
        public GenericDataService(ApotekDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (ApotekDbContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(string kode)
        {
            using (ApotekDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Kode == kode);

                if (entity == null)
                {
                    return false;
                }
                
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
            
        }

        public async Task<T> Get(int id)
        {
            using (ApotekDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FindAsync(id);
                return entity;
            }
        }


        public async Task<T> GetByKode(string kode)
        {
            using (ApotekDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Kode == kode);

                return (entity == null) ? null : entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (ApotekDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }


        public async Task<T> Update(int id, T entity)
        {
            using (ApotekDbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
