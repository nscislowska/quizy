using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace quizy
{
    public partial class StartPage : System.Web.UI.Page
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

        private bool isFirstC //czy quiz był dodawany
        {
            get { return (bool)Session["AddQuizFlag"]; }
            set { Session["AddQuizFlag"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //utworzenie połączenia z bazą danych i przypisanie do zmiennej sesji
            string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Nats\Desktop\quizy\quizy_db.accdb";
            connection = new OleDbConnection(connString);

            AddQuizFlag = false;

            Server.Transfer("HomePage.aspx");
        }

        
    }
}