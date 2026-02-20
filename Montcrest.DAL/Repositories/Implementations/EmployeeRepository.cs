using Microsoft.EntityFrameworkCore;
using Montcrest.DAL.Context;
using Montcrest.DAL.Models;
using Montcrest.DAL.Repositories.Interfaces;

namespace Montcrest.DAL.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MontcrestDbContext _context;

        public EmployeeRepository(MontcrestDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByUserIdAsync(int userId)
        {
            return await _context.Employees
                .Include(e => e.User)
                .Include(e => e.Manager)
                    .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(e => e.UserId == userId);
        }
        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee?> GetByIdAsync(int employeeId)
        {
            return await _context.Employees
                .Include(e => e.User)
                .Include(e => e.Manager)
                    .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(e => e.Id == employeeId);
        }
    }
}
