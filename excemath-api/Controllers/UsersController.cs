﻿#nullable enable

#region Usings-частина

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using excemathApi.Data;
using excemathApi.Models;

#endregion

namespace excemathApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        #region Поля

        /// <summary>
        /// 
        /// </summary>
        private readonly UsersApiDbContext _dbContext;

        #endregion

        #region Конструктори

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public UsersController(UsersApiDbContext dbContext) => _dbContext = dbContext;

        #endregion

        #region HttpGet-методи

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers() => Ok(await _dbContext.Users.ToListAsync()); // Привести до GetUserRequest!

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{nickname}")]
        public async Task<IActionResult> GetUser([FromRoute] string nickname)
        {
            User? user = await _dbContext.Users.FindAsync(nickname);

            if (user is null)
                return NotFound();

            return Ok((GetUserRequest)user);
        }

        #endregion

        #region HttpPost-методи

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addUserRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            User user = new()
            {
                //Id = Guid.NewGuid(),
                Nickname = addUserRequest.Nickname,
                Password = addUserRequest.Password
            };

            _ = await _dbContext.Users.AddAsync(user);
            _ = await _dbContext.SaveChangesAsync();

            return Ok(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateUserRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UpdateUserRequest updateUserRequest)
        {
            User? user = await _dbContext.Users.FindAsync(id);

            if (user is null)
                return NotFound();

            user.Nickname = updateUserRequest.Nickname;
            user.Password = updateUserRequest.Password;
            user.RightAnswers = updateUserRequest.RightAnswers;
            user.WrongAnswers = updateUserRequest.WrongAnswers;

            _ = await _dbContext.SaveChangesAsync();

            return Ok(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            User? user = await _dbContext.Users.FindAsync(id);

            if (user is null)
                return NotFound();

            _ = _dbContext.Users.Remove(user);

            _ = await _dbContext.SaveChangesAsync();

            return Ok(user);
        }

        #endregion
    }
}
