using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCore.Domain.Models;
using WebApplicationCore.Domain.Repositories;
using WebApplicationCore.Persistence.Contexts;

namespace WebApplicationCore.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            var querover = await _context.Categories.FindAsync(id);
            return querover;
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}
