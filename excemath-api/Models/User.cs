#nullable disable

namespace excemathApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class User
    {
        #region Властивості 

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RightAnswers { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int WrongAnswers { get; set; } = 0;

        #endregion
    }
}
