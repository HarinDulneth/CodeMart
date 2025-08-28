using CodeMart.CodeMart.Server.Models;
using CodeMart.Server.Data;
using CodeMart.Server.Interfaces;
using CodeMart.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeMart.Server.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserService> _logger;

        public async Task<User?> GetUserByIdAsync(int id)
        {
            try
            {
                return await _context.Users
                    .Select(u => new User
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        Occupation = u.Occupation,
                        CompanyName = u.CompanyName,
                        ProfilePicture = u.ProfilePicture,
                        IsAdmin = u.IsAdmin
                        // Don't load navigation properties unless needed
                    })
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user {UserId}", id);
                throw;
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _context.Users
                    .Select(u => new User
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        Occupation = u.Occupation,
                        CompanyName = u.CompanyName,
                        ProfilePicture = u.ProfilePicture,
                        IsAdmin = u.IsAdmin
                        // Don't load navigation properties unless needed
                    })
                    .FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user with {Email}", email);
                throw;
            }
        }

        public async Task<List<Project>> GetSellingProjectsAsync(int userId)
        {
            var user = await _context.Users
            .Include(u => u.SellingProjects)
            .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.SellingProjects;
        }
    }
}
