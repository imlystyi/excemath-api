namespace excemathApi.Models
{
    /// <summary>
    /// Представляє модель математичної проблеми, яка має унікальний ідентифікатор, тип, умову і розв'язок.
    /// </summary>
    public class MathProblem
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Повертає або встановлює вид математичної проблеми, представлений елементом перерахування <see cref="ProblemKinds"/>.
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// Повертає або встановлює умову математичної проблеми.
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// Повертає або встановлює правильний розв'язок математичної проблеми.
        /// </summary>
        public string RightAnswer { get; set; }
    }
}
