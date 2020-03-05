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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user, ERole[] userRoles)
        {
            var roleNames = userRoles.Select(r => r.ToString()).ToList();
            var roles = await _context.Roles.Where(r => roleNames.Contains(r.Name)).ToListAsync();

            foreach (var role in roles)
            {
                user.UserRoles.Add(new UserRole { RoleId = role.Id });
            }

            _context.Users.Add(user);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.UserRoles)
                                       .ThenInclude(ur => ur.Role)
                                       .SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
