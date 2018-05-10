using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quizy
{
    public partial class TestPage : System.Web.UI.Page
    {
        private Quiz quiz //przechowuje zawarość całego quizu
        {
            get { return (Quiz)Session["Quiz"]; }
            set { Session["Quiz"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
        }
    }
}