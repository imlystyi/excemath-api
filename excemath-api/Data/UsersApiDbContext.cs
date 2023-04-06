#region Usings-частина

using Microsoft.EntityFrameworkCore;
using excemathApi.Models;

#endregion

namespace excemathApi.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersApiDbContext : DbContext
    {
        #region Конструктори

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextOptions"></param>
        public UsersApiDbContext(DbContextOptions<UsersApiDbContext> options) : base(options)
        {            
        }

        #endregion

        #region Властивості

        /// <summary>
        /// 
        /// </summary>
        public DbSet<User> Users { get; set; }

        #endregion
    }
}
