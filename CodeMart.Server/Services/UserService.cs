using CodeMart.CodeMart.Server.Models;
using CodeMart.Server.Data;
using CodeMart.Server.Interfaces;
using CodeMart.Server.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace CodeMart.Server.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
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
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user with {Email}", email);
                throw;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");
                throw;
            }
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(id);

                if (existingUser == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found for update", id);
                    return null;
                }

                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Occupation = user.Occupation;
                existingUser.CompanyName = user.CompanyName;
                existingUser.ProfilePicture = user.ProfilePicture;
                existingUser.IsAdmin = user.IsAdmin;

                await _context.SaveChangesAsync();

                return existingUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user {UserId}", user.Id);
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found for deletion", id);
                    throw new Exception("User not found");
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user {UserId}", id);
                throw;
            }
        }

        public async Task<List<Project>> GetSellingProjectsAsync(int userId)
        {
            try
            {
                var user = await _context.Users
                .Include(u => u.SellingProjects)
                .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);
                    return new List<Project>();
                }

                return user?.SellingProjects;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "cant get selling projects");
                throw;
            }

        }

        public async Task<bool> AddProjectToWishlistAsync(int userId, int projectId)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.WishlistedProjects) // load wishlist
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);
                    return false;
                }

                var project = await _context.Projects.FindAsync(projectId);
                if (project == null)
                {
                    _logger.LogWarning("Project with ID {ProjectId} not found", projectId);
                    return false;
                }

                if (!user.WishlistedProjects.Any(p => p.Id == projectId))
                {
                    user.WishlistedProjects.Add(project);
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding project {ProjectId} to wishlist for user {UserId}", projectId, userId);
                throw;
            }
        }


        public async Task<bool> RemoveProjectFromWishlistAsync(int userId, int projectId)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.WishlistedProjects)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);
                    return false;
                }

                var project = user.WishlistedProjects.FirstOrDefault(p => p.Id == projectId);
                if (project == null)
                {
                    _logger.LogWarning("Project with ID {ProjectId} not found in wishlist for user {UserId}", projectId, userId);
                    return false;
                }

                user.WishlistedProjects.Remove(project);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Removed project {ProjectId} from wishlist for user {UserId}", projectId, userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing project {ProjectId} from wishlist for user {UserId}", projectId, userId);
                throw;
            }
        }

        public async Task<List<Project>> GetWishlistAsync(int userId)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.WishlistedProjects)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);
                    return new List<Project>();
                }
                return user.WishlistedProjects;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting wishlist from user {UserId}", userId);
                throw;
            }
        }

        public async Task<List<Project>> GetPurchasedProjectsAsync(int userId)
        {
            try
            {
                var user = await _context.Users
                      .Include(u => u.BoughtProjects)
                      .FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);
                    return new List<Project>();
                }
                return user.BoughtProjects;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting wishlist from user {UserId}", userId);
                throw;
            }
        }
    }
}
