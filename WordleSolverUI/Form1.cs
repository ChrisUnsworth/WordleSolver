using WordleSolver.common;
using WordleSolver.Data;
using WordleSolver.Search;

namespace WordleSolverUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            _guessButtons = new Button[][]
                {
                    new Button[] { BT_G_1_1, BT_G_1_2, BT_G_1_3, BT_G_1_4, BT_G_1_5 },
                    new Button[] { BT_G_2_1, BT_G_2_2, BT_G_2_3, BT_G_2_4, BT_G_2_5 },
                    new Button[] { BT_G_3_1, BT_G_3_2, BT_G_3_3, BT_G_3_4, BT_G_3_5 },
                    new Button[] { BT_G_4_1, BT_G_4_2, BT_G_4_3, BT_G_4_4, BT_G_4_5 },
                    new Button[] { BT_G_5_1, BT_G_5_2, BT_G_5_3, BT_G_5_4, BT_G_5_5 },
                    new Button[] { BT_G_6_1, BT_G_6_2, BT_G_6_3, BT_G_6_4, BT_G_6_5 },
                    new Button[] { BT_G_7_1, BT_G_7_2, BT_G_7_3, BT_G_7_4, BT_G_7_5 }
                };

            _strategy = Widest.GetStrategy(new Word("APPLE"));
        }

        private Random _rand = new();
        private readonly Button[][] _guessButtons;
        private IStrategy _strategy;

        private void TB_Answer_TextChanged(object sender, EventArgs e)
        {
            string text = TB_Answer.Text?.Trim() ?? string.Empty;
            if (text.Length == 5)
            {
                var word = new Word(text);
                if (WordList.Answers.Contains(word))
                {
                    Solve(word);
                }
            }
        }

        private void Solve(Word word)
        {
            var idx = 0;
            var strategy = _strategy;
            Match match;
            bool allGood = true;

            do
            {
                match = word.Compare(strategy.Guess);
                foreach (var c in Enumerable.Range(0, 5))
                {
                    _guessButtons[idx][c].Text = strategy.Guess[c].ToString();
                    _guessButtons[idx][c].ForeColor = match[c] switch
                    {
                        2 => Color.DarkGreen,
                        1 => Color.DarkGoldenrod,
                        _ => Color.DarkGray
                    };
                    _guessButtons[idx][c].BackColor = match[c] switch
                    {
                        2 => Color.LightGreen,
                        1 => Color.LightYellow,
                        _ => Color.LightGray
                    };
                }

                try
                {
                    strategy = strategy.Next(match);
                }
                catch (ArgumentException)
                {
                    allGood = false;
                }

                idx++;
            } while (!match.AreEqual && idx < 7 && allGood);

            while (idx < 7)
            {
                foreach (var c in Enumerable.Range(0, 5))
                {
                    _guessButtons[idx][c].Text = string.Empty;
                    _guessButtons[idx][c].ForeColor = Color.DarkGray;
                    _guessButtons[idx][c].BackColor = Color.LightGray;
                }

                idx++;
            }
        }

        private void BT_Random_Answer_Click(object sender, EventArgs e)
        {
            var word = WordList.Answers[_rand.Next(WordList.Answers.Length)];
            TB_Answer.Text = word.ToString();
        }
    }
}