using DentistApp.DAL.Models;
using DentistApp.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace DentistApp.DAL.Queries;

public class UserQueries: IUserQueries
{
    private readonly ApplicationDbContext _context;

    public UserQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}