using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace quizy
{
    public partial class QuizBase : System.Web.UI.Page
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
        private string currentQuestionNumber //nr wyświetlanego pytania
        {
            get { return (string)Session["currentQuestionNumber"]; }
            set { Session["currentQuestionNumber"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                quiz = new Quiz();
                currentQuestionNumber = "0";
                

                loadQuizList();
            }
        }

        private void loadQuizList()
        {
            //zczytanie tytułów z bazy danych
            String[] names = SQL.SimpleSelect(connection, "quiz", "name", null);

            if (names == null) return;

            //wypełnienie listy

            QuizRadioButtonList.Items.Clear();

            for (int i=0; i<names.Length; i++)
            {
                QuizRadioButtonList.Items.Add(names[i]);
                QuizRadioButtonList.Items[i].Value = names[i];
            }
        }

        protected void QuizRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {  
            quiz.Name = QuizRadioButtonList.SelectedValue;
        }

        protected void SolveButton_Click(object sender, EventArgs e)
        {
            getQuizFromDatabase();
            selectedAnswers = new string[quiz.Questions.Count];
            Server.Transfer("SolveQuiz.aspx");
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            //pobranie id quizu i id pytań
            try { 
            string query = "SELECT id FROM quiz WHERE name='" + quiz.Name + "' ;";
            string quizID = SQL.SingleCellSelect(connection, query);

            string whereClause = "WHERE quiz_id=" + quizID + ";";
            string[] questionsID = SQL.SimpleSelect(connection, "question", "id", whereClause);

            //usunięcie quizu
            //odpowiedzi
            foreach (string questionID in questionsID) {
                SQL.SimpleDelete(connection, "answer", "question_id", questionID);
            }

            //pytania
            SQL.SimpleDelete(connection, "question", "quiz_id", quizID);
            //quiz
            SQL.SimpleDelete(connection, "quiz", "id", quizID);
            }
            catch(Exception exc)
            {
                warningLabel1.Text = "Błąd usuwania: " + exc.ToString();
            }

            loadQuizList();
            quiz.Name = null;
            return;
        }

        private void getQuizFromDatabase()
        {
            
            string query = "SELECT id FROM quiz WHERE name='" + quiz.Name + "' ;";
            string quizID = SQL.SingleCellSelect(connection, query);

            string whereClause = "WHERE quiz_id=" + quizID + " ORDER BY record_number;";
            string[] questionsID = SQL.SimpleSelect(connection, "question", "id", whereClause);
            string[] questions = SQL.SimpleSelect(connection, "question", "question", whereClause);

            List<Answer> answerList = new List<Answer>();

            for (int i = 0; i < questionsID.Length; i++)
            {
               

                whereClause = "WHERE question_id=" + questionsID[i] + " ORDER BY record_number";
                string[] answers = SQL.SimpleSelect(connection, "answer", "answer", whereClause);
                string[] isCorrect = SQL.SimpleSelect(connection, "answer", "is_correct", whereClause);

                for (int j = 0; j < answers.Length; j++)
                {
                    if (isCorrect[j] == "False")
                    {
                        answerList.Add(new Answer(answers[j], false, j+1));
                    }
                    else
                    {
                        answerList.Add(new Answer(answers[j], true, j+1));
                    }
                }

                quiz.Questions.Add(new Question(questions[i], answerList, i + 1));

                answerList.Clear();
            }

            return;
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("HomePage.aspx");
        }
    }
}