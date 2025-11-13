using CodeMart.CodeMart.Server.Models;
using CodeMart.Server.DTOs;
using CodeMart.Server.DTOs.User;
using CodeMart.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeMart.Server.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var dto = new UserDtoOut
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Occupation = user.Occupation,
                CompanyName = user.CompanyName,
                ProfilePicture = user.ProfilePicture,
                IsAdmin = user.IsAdmin
            };
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            var dto = new UserDtoOut
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Occupation = user.Occupation,
                CompanyName = user.CompanyName,
                ProfilePicture = user.ProfilePicture,
                IsAdmin = user.IsAdmin
            };
            return Ok(dto);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users == null || users.Count == 0)
            {
                return Ok(new List<UserDtoOut>());
            }
            var dtos = users.Select(u => new UserDtoOut
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Occupation = u.Occupation,
                CompanyName = u.CompanyName,
                ProfilePicture = u.ProfilePicture,
                IsAdmin = u.IsAdmin
            }).ToList();
            return Ok(dtos);
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateUser([FromBody] UserDtoIn dtoIn)
        {
            if (dtoIn == null)
            {
                return BadRequest("Bad Request.");
            }

            var user = new User
            {
                FirstName = dtoIn.FirstName,
                LastName = dtoIn.LastName,
                Email = dtoIn.Email,
                Password = dtoIn.Password,
                Occupation = dtoIn.Occupation,
                CompanyName = dtoIn.CompanyName,
                ProfilePicture = dtoIn.ProfilePicture,
                IsAdmin = dtoIn.IsAdmin
            };

            var createdUser = await _userService.CreateUserAsync(user);
            if (createdUser == null)
            {
                return StatusCode(500, "Internal Server Error.");
            }

            var dtoOut = new UserDtoOut
            {
                Id = createdUser.Id,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email,
                Occupation = createdUser.Occupation,
                CompanyName = createdUser.CompanyName,
                ProfilePicture = createdUser.ProfilePicture,
                IsAdmin = createdUser.IsAdmin
            };
            return CreatedAtAction(
                nameof(GetUserById),
                new { id = createdUser.Id},
                dtoOut
            );
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDtoIn dtoIn)
        {
            if (dtoIn == null)
            {
                return BadRequest("Bad Request.");
            }

            var updatedUser = await _userService.UpdateUserAsync(id, new User
            {
                FirstName = dtoIn.FirstName,
                LastName = dtoIn.LastName,
                Email = dtoIn.Email,
                Password = dtoIn.Password,
                Occupation = dtoIn.Occupation,
                CompanyName = dtoIn.CompanyName,
                ProfilePicture = dtoIn.ProfilePicture,
                IsAdmin = dtoIn.IsAdmin
            });

            if (updatedUser == null)
            {
                return StatusCode(500, "Internal Server Error.");
            }

            var dtoOut = new UserDtoOut
            {
                Id = updatedUser.Id,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Email = updatedUser.Email,
                Occupation = updatedUser.Occupation,
                CompanyName = updatedUser.CompanyName,
                ProfilePicture = updatedUser.ProfilePicture,
                IsAdmin = updatedUser.IsAdmin
            };
            return Ok(dtoOut);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return StatusCode(500, "Internal Server Error.");
            }
            return Ok($"Deleted User {id}");
        }

        [HttpGet("{id}/selling")]
        public async Task<IActionResult> GetSellingProjects(int id)
        {
            var projects = await _userService.GetSellingProjectsAsync(id);

            if (projects == null || projects.Count == 0)
            {
                return Ok(new List<ProjectDto>());
            }
            var projectsDtos = projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                Price = p.Price,
                ProjectUrl = p.ProjectUrl,
                VideoUrl = p.VideoUrl,
                UploadDate = p.UploadDate,
                ImageUrls = p.ImageUrls,
                PrimaryLanguage = p.PrimaryLanguage,
                SecondaryLanguages = p.SecondaryLanguages
            }).ToList();
            return Ok(projectsDtos);
        }

        [HttpPut("addtowishlist")]
        public async Task<IActionResult> AddtoWishList([FromQuery] int userId, [FromQuery] int projectId)
        {
            var result = await _userService.AddProjectToWishlistAsync(userId, projectId);

            if (!result)
            {
                return StatusCode(500, "Internal Server Error.");
            }

            return Ok("Project added to WishList");
        }

        [HttpPut("removefromwishlist")]
        public async Task<IActionResult> RemoveFromWishList([FromQuery] int userId, [FromQuery] int projectId)
        {
            var result = await _userService.RemoveProjectFromWishlistAsync(userId, projectId);

            if (!result)
            {
                return StatusCode(500, "Internal Server Error.");
            }

            return Ok("Project added to WishList");
        }

        [HttpGet("{id}/wishlist")]
        public async Task<IActionResult> GetWishList(int id)
        {
            var wishlist = await _userService.GetWishlistAsync(id);
            if (wishlist == null || wishlist.Count == 0)
            {
                return Ok(new List<ProjectDto>());
            }

            var wishListDto = wishlist.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                Price = p.Price,
                ProjectUrl = p.ProjectUrl,
                VideoUrl = p.VideoUrl,
                UploadDate = p.UploadDate,
                ImageUrls = p.ImageUrls,
                PrimaryLanguage = p.PrimaryLanguage,
                SecondaryLanguages = p.SecondaryLanguages
            }).ToList();

            return Ok(wishListDto);
        }

        [HttpGet("{id}/boughtprojects")]
        public async Task<IActionResult> GetPurchasedProjects(int id)
        {
            var purchased = await _userService.GetPurchasedProjectsAsync(id);
            if (purchased == null || purchased.Count == 0)
            {
                return Ok(new List<ProjectDto>());
            }

            var purchasedListDto = purchased.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                Price = p.Price,
                ProjectUrl = p.ProjectUrl,
                VideoUrl = p.VideoUrl,
                UploadDate = p.UploadDate,
                ImageUrls = p.ImageUrls,
                PrimaryLanguage = p.PrimaryLanguage,
                SecondaryLanguages = p.SecondaryLanguages
            }).ToList();

            return Ok(purchasedListDto);
        }
    }
}
