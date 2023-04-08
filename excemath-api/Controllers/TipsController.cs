// Зробити окрему базу даних для загальних підказок, чи зберігати їх як поля математичних проблем?
// Подумати над цим; моделі, контролери, контексти для Tip поки що не робити!

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
    public class TipsController : Controller
    {
        #region Поля

        /// <summary>
        /// 
        /// </summary>
        private readonly TipsApiDbContext _dbContext;

        #endregion

        #region Конструктори

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public TipsController(TipsApiDbContext dbContext) => _dbContext = dbContext;

        #endregion

        #region HttpGet-методи

        // Зробити HttpGet-методи для отримання підказки за видом математичної проблеми.

        #endregion
    }
}
