using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.OleDb;

namespace quizy
{
    public partial class TestPage : System.Web.UI.Page
    {
        private OleDbConnection connection //połączenie z bazą danych
        {
            get { return (OleDbConnection)Session["connection"]; }
            set { Session["connection"] = value; }
        }

        private Quiz quiz //przechowuje zawarość całego quizu
        {
            get { return (Quiz)Session["Quiz"]; }
            set { Session["Quiz"] = value; }
        }

        private string AddQuizError //zawiera błąd dodawania quizu
        {
            get { return (string)Session["AddQuizError"]; }
            set { Session["AddQuizError"] = value; }
        }

        private bool AddQuizFlag //czy quiz był już dodany
        {
            get { return (bool)Session["AddQuizFlag"]; }
            set { Session["AddQuizFlag"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                addQuizToDatabase();
            }

            ListBox1.Items.Add("quiz: " + quiz.Name);
            ListBox1.Items.Add("-----------");
            foreach (Question question in quiz.Questions)
            {
                ListBox1.Items.Add("-- "+question.Text);

                foreach(Answer answ in question.Answers)
                {
                    if(answ.IsCorrect) ListBox1.Items.Add(answ.Text+" *");
                    else ListBox1.Items.Add(answ.Text);
                }
            }

            Server.Transfer("MainPage.aspx");
        }

        protected void addQuizToDatabase()
        {
            String username = "";

            try
            {

                string query = "INSERT INTO quiz(name, username, is_done) VALUES('" + quiz.Name + "', '" + username + "', 'True');";
                SQL.SimpleQuery(connection, query);

                query = "SELECT id FROM quiz WHERE name='" + quiz.Name + "' AND username='" + username + "';";
                string quizId = SQL.SingleCellSelect(connection, query);

                foreach (Question question in quiz.Questions)
                {
                    query = "INSERT INTO question(quiz_id, record_number, question) VALUES('" + quizId + "','" + question.RecordNumber + "','" + question.Text + "');";
                    SQL.SimpleQuery(connection, query);

                    query = "SELECT id FROM question WHERE record_number=" + question.RecordNumber + " AND quiz_id=" + quizId + ";";
                    string questionId = SQL.SingleCellSelect(connection, query);

                    foreach (Answer answer in question.Answers)
                    {
                        query = "INSERT INTO answer(question_id, answer, record_number, is_correct) VALUES('" + questionId + "', '" + answer.Text + "' ,'" + answer.RecordNumber + "', '" + answer.IsCorrect.ToString() + "');";
                        SQL.SimpleQuery(connection, query);
                    }
                }
            }
            catch (Exception e)
            {
                AddQuizError = e.ToString();
                Server.Transfer("AddAnswers.aspx");
            }


            AddQuizFlag = true;
            quiz = null;
            Server.Transfer("HomePage.aspx");
               
            

            return;
        }
    }
}