namespace excemathApiUnitTests
{
    public class MathProblemTests   // Tests the MathProblem class.
    {
        #region  Fields

        private readonly Guid _id = Guid.NewGuid();

        private const MathProblemTypes _TYPE = MathProblemTypes.TableIntegral;

        private const int _DIFFICULTY = 150;

        private const string _QUESTION_NORMAL_TEXT = "This is question normal text.";
        private const string _QUESTION_LATEX = "This is question LaTeX.";

        private const bool _FIRST_OPTION_RENDER_AS_LATEX = true;
        private const bool _SECOND_OPTION_RENDER_AS_LATEX = false;
        private const bool _THIRD_OPTION_RENDER_AS_LATEX = false;
        private const bool _FOURTH_OPTION_RENDER_AS_LATEX = true;

        private const int _FIRST_OPTION_INDEX = 0;
        private const int _SECOND_OPTION_INDEX = 1;
        private const int _THIRD_OPTION_INDEX = 2;
        private const int _FOURTH_OPTION_INDEX = 3;

        private const string _FIRST_OPTION_CONTENT = "First option (LaTeX).";
        private const string _SECOND_OPTION_CONTENT = "Second option (normal text).";
        private const string _THIRD_OPTION_CONTENT = "Third option (normal text, answer).";
        private const string _FOURTH_OPTION_CONTENT = "Fourth option (LaTeX).";

        private const int _ANSWER_INDEX = 2;

        private const string? _FIRST_STEP_NORMAL_TEXT = null;
        private const string _SECOND_STEP_NORMAL_TEXT = "Second step normal text, LaTeX is null.";
        private const string _THIRD_STEP_NORMAL_TEXT = "Third step normal text, nothing is null.";
        private const string? _FOURTH_STEP_NORMAL_TEXT = null;

        private const string _FIRST_STEP_LATEX = "First step LaTeX, normal text is null.";
        private const string? _SECOND_STEP_LATEX = null;
        private const string _THIRD_STEP_LATEX = "Third step LaTeX, nothing is null.";
        private const string? _FOURTH_STEP_LATEX = null;

        #endregion

        #region Tests

        [Test]
        public void ToDtoTest()     // Tests the ToDto() method.
        {
            MathProblem mathProblem = new()                 // Initial math problem.
            {
                Id = _id,
                Type = _TYPE,
                Difficulty = _DIFFICULTY,
                Question = new(normalText: _QUESTION_NORMAL_TEXT, latex: _QUESTION_LATEX),
                Options = new()
                {
                    new(renderAsLatex: _FIRST_OPTION_RENDER_AS_LATEX, index: _FIRST_OPTION_INDEX, content: _FIRST_OPTION_CONTENT),
                    new(renderAsLatex: _SECOND_OPTION_RENDER_AS_LATEX, index: _SECOND_OPTION_INDEX, content: _SECOND_OPTION_CONTENT),
                    new(renderAsLatex: _THIRD_OPTION_RENDER_AS_LATEX, index: _THIRD_OPTION_INDEX, content: _THIRD_OPTION_CONTENT),
                    new(renderAsLatex: _FOURTH_OPTION_RENDER_AS_LATEX, index: _FOURTH_OPTION_INDEX, content: _FOURTH_OPTION_CONTENT),
                },
                AnswerIndex = _ANSWER_INDEX,
                Solution = new()
                {
                    new(normalText: _FIRST_STEP_NORMAL_TEXT, latex: _FIRST_STEP_LATEX),
                    new(normalText: _SECOND_STEP_NORMAL_TEXT, latex: _SECOND_STEP_LATEX),
                    new(normalText: _THIRD_STEP_NORMAL_TEXT, latex: _THIRD_STEP_LATEX),
                    new(normalText: _FOURTH_STEP_NORMAL_TEXT, latex: _FOURTH_STEP_LATEX),
                }
            };

            MathProblemDto expectedDto = new()              // Expected Data Transfer Object.
            {
                Id = _id,
                Type = _TYPE,
                Difficulty = _DIFFICULTY,
                QuestionNormalText = _QUESTION_NORMAL_TEXT,
                QuestionLatex = _QUESTION_LATEX,
                OptionsRenderAsLatexOrder = new()
                    { _FIRST_OPTION_RENDER_AS_LATEX, _SECOND_OPTION_RENDER_AS_LATEX, _THIRD_OPTION_RENDER_AS_LATEX, _FOURTH_OPTION_RENDER_AS_LATEX },
                OptionsIndexOrder = new() { _FIRST_OPTION_INDEX, _SECOND_OPTION_INDEX, _THIRD_OPTION_INDEX, _FOURTH_OPTION_INDEX },
                OptionsContentOrder = new() { _FIRST_OPTION_CONTENT, _SECOND_OPTION_CONTENT, _THIRD_OPTION_CONTENT, _FOURTH_OPTION_CONTENT },
                AnswerIndex = _ANSWER_INDEX,
                SolutionNormalTextsOrder = new()
                    { _FIRST_STEP_NORMAL_TEXT, _SECOND_STEP_NORMAL_TEXT, _THIRD_STEP_NORMAL_TEXT, _FOURTH_STEP_NORMAL_TEXT },
                SolutionLatexOrder = new() { _FIRST_STEP_LATEX, _SECOND_STEP_LATEX, _THIRD_STEP_LATEX, _FOURTH_STEP_LATEX }
            };

            MathProblemDto actualDto = mathProblem.ToDto(); // Actual Data Transfer Object.

            actualDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void FromDtoTest()   // Tests the MathProblem(MathProblemDto dto) constructor.
        {
            MathProblemDto dto = new()                      // Initial Data Transfer Object.
            {
                Id = _id,
                Type = _TYPE,
                Difficulty = _DIFFICULTY,
                QuestionNormalText = _QUESTION_NORMAL_TEXT,
                QuestionLatex = _QUESTION_LATEX,
                OptionsRenderAsLatexOrder = new()
                    { _FIRST_OPTION_RENDER_AS_LATEX, _SECOND_OPTION_RENDER_AS_LATEX, _THIRD_OPTION_RENDER_AS_LATEX, _FOURTH_OPTION_RENDER_AS_LATEX },
                OptionsIndexOrder = new() { _FIRST_OPTION_INDEX, _SECOND_OPTION_INDEX, _THIRD_OPTION_INDEX, _FOURTH_OPTION_INDEX },
                OptionsContentOrder = new() { _FIRST_OPTION_CONTENT, _SECOND_OPTION_CONTENT, _THIRD_OPTION_CONTENT, _FOURTH_OPTION_CONTENT },
                AnswerIndex = _ANSWER_INDEX,
                SolutionNormalTextsOrder = new()
                    { _FIRST_STEP_NORMAL_TEXT, _SECOND_STEP_NORMAL_TEXT, _THIRD_STEP_NORMAL_TEXT, _FOURTH_STEP_NORMAL_TEXT },
                SolutionLatexOrder = new() { _FIRST_STEP_LATEX, _SECOND_STEP_LATEX, _THIRD_STEP_LATEX, _FOURTH_STEP_LATEX }
            };

            MathProblem expectedMathProblem = new()         // Expected math problem.
            {
                Id = _id,
                Type = _TYPE,
                Difficulty = _DIFFICULTY,
                Question = new(normalText: _QUESTION_NORMAL_TEXT, latex: _QUESTION_LATEX),
                Options = new()
                {
                    new(renderAsLatex: _FIRST_OPTION_RENDER_AS_LATEX, index: _FIRST_OPTION_INDEX, content: _FIRST_OPTION_CONTENT),
                    new(renderAsLatex: _SECOND_OPTION_RENDER_AS_LATEX, index: _SECOND_OPTION_INDEX, content: _SECOND_OPTION_CONTENT),
                    new(renderAsLatex: _THIRD_OPTION_RENDER_AS_LATEX, index: _THIRD_OPTION_INDEX, content: _THIRD_OPTION_CONTENT),
                    new(renderAsLatex: _FOURTH_OPTION_RENDER_AS_LATEX, index: _FOURTH_OPTION_INDEX, content: _FOURTH_OPTION_CONTENT),
                },
                AnswerIndex = _ANSWER_INDEX,
                Solution = new()
                {
                    new(normalText: _FIRST_STEP_NORMAL_TEXT, latex: _FIRST_STEP_LATEX),
                    new(normalText: _SECOND_STEP_NORMAL_TEXT, latex: _SECOND_STEP_LATEX),
                    new(normalText: _THIRD_STEP_NORMAL_TEXT, latex: _THIRD_STEP_LATEX),
                    new(normalText: _FOURTH_STEP_NORMAL_TEXT, latex: _FOURTH_STEP_LATEX),
                }
            };

            MathProblem actualMathProblem = new(dto);       // Actual math problem.

            actualMathProblem.Should().BeEquivalentTo(expectedMathProblem);
        }

        #endregion
    }
}