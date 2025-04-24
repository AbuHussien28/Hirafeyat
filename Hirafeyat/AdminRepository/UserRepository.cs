using Dapper;
using Hirafeyat.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using X.PagedList;

namespace Hirafeyat.AdminRepository
{
    public class UserRepository:IUserRepository
    {
        private readonly HirafeyatContext _context;

        public UserRepository(HirafeyatContext context)
        {
            _context = context;
        }

        public async Task<IPagedList<ApplicationUser>> GetCustomersAsync(int page, int pageSize)
        {
            var totalCount = await _context.UserRoles.Join(_context.Roles,
                ur => ur.RoleId,
                r => r.Id,
                (ur, r) => new { ur.UserId, RoleName = r.Name })
                .Where(x => x.RoleName == "Customer").CountAsync();
            var usersDataCustomer = await _context.Users.Join(_context.UserRoles,
                u => u.Id,
                ur => ur.UserId,
                (u, ur) => new { User = u, ur.RoleId }).Join(_context.Roles,
                ur => ur.RoleId,
                r => r.Id,
                (ur, r) => new { ur.User, RoleName = r.Name }).Where(x => x.RoleName == "Customer")
                .OrderBy(x => x.User.UserName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                   .Select(x => x.User).ToListAsync();

            return new StaticPagedList<ApplicationUser>(usersDataCustomer, page, pageSize, totalCount);
        }

        public async Task<IPagedList<ApplicationUser>> GetSellersAsync(int page, int pageSize)
        {
            var totalCount = await _context.UserRoles
                   .Join(_context.Roles,
                       ur => ur.RoleId,
                       r => r.Id,
                       (ur, r) => new { ur.UserId, RoleName = r.Name })
                   .Where(x => x.RoleName == "Seller")
                   .CountAsync();

            // Get the paginated data
            var users = await _context.Users
                .Join(_context.UserRoles,
                    u => u.Id,
                    ur => ur.UserId,
                    (u, ur) => new { User = u, ur.RoleId })
                .Join(_context.Roles,
                    ur => ur.RoleId,
                    r => r.Id,
                    (ur, r) => new { ur.User, RoleName = r.Name })
                .Where(x => x.RoleName == "Seller")
                .OrderBy(x => x.User.UserName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => x.User)
                .ToListAsync();

            return new StaticPagedList<ApplicationUser>(users, page, pageSize, totalCount);
        }
    }
}
