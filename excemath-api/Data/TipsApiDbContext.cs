// Зробити XML-документацію.

#region Usings-частина

using Microsoft.EntityFrameworkCore;
using excemathApi.Models;

#endregion

namespace excemathApi.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class TipsApiDbContext : DbContext
    {
        #region Конструктори

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public TipsApiDbContext(DbContextOptions<TipsApiDbContext> options) : base(options)
        {            
        }

        #endregion

        #region Властивості

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Tip> Tips { get; set; }

        #endregion
    }
}
