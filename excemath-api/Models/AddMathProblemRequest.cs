namespace excemathApi.Models
{
    // Клас для відлагодження; перевіряє здатність контроллера проводити запит на додавання математичної проблеми до бази даних.
    public class AddMathProblemRequest
    {
        #region Властивості

        /// <inheritdoc cref="MathProblem.Kind"/>
        public MathProblemKinds Kind { get; set; }

        /// <inheritdoc cref="MathProblem.Question"/>
        public string Question { get; set; }

        /// <inheritdoc cref="MathProblem.Answer"/>
        public string Answer { get; set; }

        #endregion
    }
}
