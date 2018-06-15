using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quizy
{
    public partial class QuizResult : System.Web.UI.Page
    {

        private Quiz quiz //quiz
        {
            get { return (Quiz)Session["Quiz"]; }
            set { Session["Quiz"] = value; }
        }

        private String[] selectedAnswers //indeks - nr pytania, warość - nr odpowiedzi
        {
            get { return (string[])Session["selectedAnswers"]; }
            set { Session["selectedAnswers"] = value; }
        }
        private int[] answers;


        protected void Page_Load(object sender, EventArgs e)
        {
            answers = new int[selectedAnswers.Length];

            for(int i = 0; i < selectedAnswers.Length; i++)
            {
                answers[i] = Int32.Parse(selectedAnswers[i]);
            }

            int points = countPoints();

            pointsLabel.Text = points.ToString();
            totalLabel.Text = quiz.Questions.Count.ToString();
        }

        private int countPoints()
        {
            int result = 0;
            for(int i=0; i < quiz.Questions.Count; i++)
            {
                int myAnswerIndex = answers[i];
                if (quiz.Questions[i].Answers[myAnswerIndex].IsCorrect) result++;
            }

            return result;
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            quiz = null;
            selectedAnswers = null;

            Server.Transfer("QuizBase.aspx");
        }
    }
}