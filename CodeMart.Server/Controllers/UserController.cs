using CodeMart.CodeMart.Server.Models;
using CodeMart.Server.DTOs.User;
using CodeMart.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeMart.Server.Utils;
using CodeMart.Server.DTOs.Project;
using CodeMart.Server.DTOs;

namespace CodeMart.Server.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

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
                IsAdmin = user.IsAdmin,
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

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDtoIn dtoIn)
        {
            if (dtoIn == null)
            {
                return BadRequest("Bad Request.");
            }

            var currentUserId = ControllerHelpers.GetCurrentUserId(User);
            if (currentUserId == null)
            {
                return Unauthorized("Invalid token.");
            }

            var isAdmin = ControllerHelpers.IsCurrentUserAdmin(User);
            if (currentUserId != id && !isAdmin)
            {
                return Forbid("You can only update your own profile.");
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
                IsAdmin = false
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
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!ControllerHelpers.IsCurrentUserAdmin(User))
            {
                return Forbid("Only administrators can delete users.");
            }

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
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var projects = await _userService.GetSellingProjectsAsync(id);

            if (projects == null || projects.Count == 0)
            {
                return Ok(new List<ProjectDto>());
            }
            var projectsDtos = projects.Select(p => new ProjectDto
            {
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                Price = p.Price,
                ProjectUrl = p.ProjectUrl,
                VideoUrl = p.VideoUrl,
                UploadDate = p.UploadDate,
                ImageUrls = p.ImageUrls,
                PrimaryLanguages = p.PrimaryLanguages,
                SecondaryLanguages = p.SecondaryLanguages
            }).ToList();
            return Ok(projectsDtos);
        }

        [HttpPut("addtowishlist")]
        [Authorize]
        public async Task<IActionResult> AddtoWishList([FromQuery] int userId, [FromQuery] int projectId)
        {
            var currentUserId = ControllerHelpers.GetCurrentUserId(User);
            if (currentUserId == null)
            {
                return Unauthorized("Invalid token.");
            }

            if (currentUserId != userId)
            {
                return Forbid("You can only add items to your own wishlist.");
            }

            var result = await _userService.AddProjectToWishlistAsync(userId, projectId);

            if (!result)
            {
                return StatusCode(500, "Internal Server Error.");
            }

            return Ok("Project added to WishList");
        }

        [HttpPut("removefromwishlist")]
        [Authorize]
        public async Task<IActionResult> RemoveFromWishList([FromQuery] int userId, [FromQuery] int projectId)
        {
            var currentUserId = ControllerHelpers.GetCurrentUserId(User);
            if (currentUserId == null)
            {
                return Unauthorized("Invalid token.");
            }

            if (currentUserId != userId)
            {
                return Forbid("You can only remove items from your own wishlist.");
            }

            var result = await _userService.RemoveProjectFromWishlistAsync(userId, projectId);

            if (!result)
            {
                return StatusCode(500, "Internal Server Error.");
            }

            return Ok("Project removed from WishList");
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
                OwnerId = p.OwnerId,
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                Price = p.Price,
                ProjectUrl = p.ProjectUrl,
                VideoUrl = p.VideoUrl,
                UploadDate = p.UploadDate,
                ImageUrls = p.ImageUrls,
                PrimaryLanguages = p.PrimaryLanguages,
                SecondaryLanguages = p.SecondaryLanguages,
                Permission = p.Permission
            }).ToList();

            return Ok(wishListDto);
        }


        [HttpPut("addtocart")]
        [Authorize]
        public async Task<IActionResult> AddtoCart([FromQuery] int userId, [FromQuery] int projectId)
        {
            var currentUserId = ControllerHelpers.GetCurrentUserId(User);
            if (currentUserId == null)
            {
                return Unauthorized("Invalid token.");
            }

            if (currentUserId != userId)
            {
                return Forbid("You can only add items to your own Cart.");
            }

            var result = await _userService.AddToCartAsync(userId, projectId);

            if (!result)
            {
                return StatusCode(500, "Internal Server Error.");
            }

            return Ok("Project added to Cart");
        }

        [HttpPut("removefromcart")]
        [Authorize]
        public async Task<IActionResult> RemoveFromCart([FromQuery] int userId, [FromQuery] int projectId)
        {
            var currentUserId = ControllerHelpers.GetCurrentUserId(User);
            if (currentUserId == null)
            {
                return Unauthorized("Invalid token.");
            }

            if (currentUserId != userId)
            {
                return Forbid("You can only remove items from your own cart.");
            }

            var result = await _userService.RemoveProjectFromCartAsync(userId, projectId);

            if (!result)
            {
                return StatusCode(500, "Internal Server Error.");
            }

            return Ok("Project removed from Cart");
        }

        [HttpGet("{id}/cart")]
        public async Task<IActionResult> GetCart(int id)
        {
            var cart = await _userService.GetCartAsync(id);
            if (cart == null || cart.Count == 0)
            {
                return Ok(new List<ProjectDto>());
            }

            var cartDto = cart.Select(p => new ProjectDto
            {
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                Price = p.Price,
                ProjectUrl = p.ProjectUrl,
                VideoUrl = p.VideoUrl,
                UploadDate = p.UploadDate,
                ImageUrls = p.ImageUrls,
                PrimaryLanguages = p.PrimaryLanguages,
                SecondaryLanguages = p.SecondaryLanguages
            }).ToList();

            return Ok(cartDto);
        }


        [HttpGet("{id}/boughtprojects")]
        [Authorize]
        public async Task<IActionResult> GetPurchasedProjects(int id)
        {
            var currentUserId = ControllerHelpers.GetCurrentUserId(User);
            if (currentUserId == null)
            {
                return Unauthorized("Invalid token.");
            }

            if (currentUserId != id && !ControllerHelpers.IsCurrentUserAdmin(User))
            {
                return Forbid("You can only view your own purchased projects.");
            }

            var purchased = await _userService.GetPurchasedProjectsAsync(id);
            if (purchased == null || purchased.Count == 0)
            {
                return Ok(new List<ProjectDto>());
            }

            var purchasedListDto = purchased.Select(p => new ProjectDto
            {
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                Price = p.Price,
                ProjectUrl = p.ProjectUrl,
                VideoUrl = p.VideoUrl,
                UploadDate = p.UploadDate,
                ImageUrls = p.ImageUrls,
                PrimaryLanguages = p.PrimaryLanguages,
                SecondaryLanguages = p.SecondaryLanguages
            }).ToList();

            return Ok(purchasedListDto);
        }

    }
}
