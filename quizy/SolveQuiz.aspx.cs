using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.OleDb;

namespace quizy
{
    public partial class SolveQuiz : System.Web.UI.Page
    {
        private OleDbConnection connection //połączenie z bazą danych
        {
            get { return (OleDbConnection)Session["connection"]; }
            set { Session["connection"] = value; }
        }

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

        private string currentQuestionNumber //
        {
            get { return (string)Session["currentQuestionNumber"]; }
            set { Session["currentQuestionNumber"] = value; }
        }

        private string selectedAnswer //record_number wybranej odpowiedzi
        {
            get { return (string)ViewState["selectedAnswer"]; }
            set { ViewState["selectedAnswer"] = value; }
        }

        private int questionNumber //
        {
            get { return (int)Session["questionNumber"]; }
            set { Session["questionNumber"] = value; }
        }

        private bool isLastQuestion //record_number wybranej odpowiedzi
        {
            get { return (bool)ViewState["isLastQuestion"]; }
            set { ViewState["isLastQuestion"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            questionNumber = Int32.Parse(currentQuestionNumber);
            

            if (!IsPostBack)
            {
                titleLabel.Text = quiz.Name;
                questionLabel.Text = quiz.Questions[questionNumber].Text;
                loadAnswerList();
               
                if (questionNumber == quiz.Questions.Count - 1)
                {
                    isLastQuestion = true;
                    NextNextButton.Text = "Wynik";
                }
                else isLastQuestion = false;

            }
            

            warningLabel1.Text = "";

            
        }

        protected void NextNextButton_Click(object sender, EventArgs e)
        {
            if (selectedAnswer == null)
            {
                warningLabel1.Text = "Zaznacz odpowiedź";
                return;
            }
            selectedAnswers[questionNumber] = selectedAnswer;

            currentQuestionNumber = (questionNumber + 1).ToString();

            if (questionNumber<quiz.Questions.Count-1)
            {
                Server.Transfer("SolveQuiz.aspx");
            }
            else Server.Transfer("QuizResult.aspx");
        }

        protected void answerRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem item = answerRadioButtonList.SelectedItem;
            selectedAnswer = (answerRadioButtonList.Items.IndexOf(item)).ToString();

            
        }

        private void loadAnswerList()
        {
           
            //wypełnienie listy
            // int questionNumber = Int32.Parse(currentQuestionNumber) -1 ;
            answerRadioButtonList.Items.Clear();

            for (int i = 0; i < quiz.Questions[questionNumber].Answers.Count; i++)
            {
                answerRadioButtonList.Items.Add(quiz.Questions[questionNumber].Answers[i].Text);
                answerRadioButtonList.Items[i].Value = quiz.Questions[questionNumber].Answers[i].Text;
            }

            if (selectedAnswers[questionNumber] != null)
            {
                string str_index = selectedAnswers[questionNumber];
                int index = Int32.Parse(str_index);
                answerRadioButtonList.Items[index].Selected=true;
                ListItem item = answerRadioButtonList.SelectedItem;
                selectedAnswer = (answerRadioButtonList.Items.IndexOf(item)).ToString();
            }


        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            if (currentQuestionNumber == "0")
            {
                Server.Transfer("QuizBase.aspx");
                quiz = null;
                selectedAnswers = null;
            }
            else
            {
                currentQuestionNumber = (Int32.Parse(currentQuestionNumber) - 1).ToString();
                Server.Transfer("SolveQuiz.aspx");
            }
        }
    }
}