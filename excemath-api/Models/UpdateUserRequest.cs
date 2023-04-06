#nullable disable

namespace excemathApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateUserRequest
    {
        /// <inheritdoc cref="User.Nickname"/>
        public string Nickname { get; set; }

        /// <inheritdoc cref="User.Password"/>
        public string Password { get; set; }

        /// <inheritdoc cref="User.RightAnswers"/>
        public int RightAnswers { get; set; }

        /// <inheritdoc cref="User.WrongAnswers"/>
        public int WrongAnswers { get; set; }
    }
}
