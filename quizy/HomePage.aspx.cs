using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace quizy
{
   

    public partial class MainPage1 : System.Web.UI.Page
    {

        private OleDbConnection connection //połączenie z bazą danych
        {
            get { return (OleDbConnection)Session["connection"]; }
            set { Session["connection"] = value; }
        }

        
        private bool AddQuizFlag //czy quiz był dodawany
        {
            get { return (bool)Session["AddQuizFlag"]; }
            set { Session["AddQuizFlag"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (AddQuizFlag)
                {
                    warningLabel1.Text = "Quiz dodano pomyślnie!";
                }

            }
            else
            {
                warningLabel1.Text = "";
                AddQuizFlag = false;
            }
           
        
        }

        protected void AddQuizButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("AddQuiz.aspx");
        }

        protected void QuizDBButton_Click(object sender, EventArgs e)
        {
            Server.Transfer("QuizBase.aspx");
        }
    }
}