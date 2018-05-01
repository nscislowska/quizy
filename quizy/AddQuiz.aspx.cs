using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//System.Diagnostics.Debug.WriteLine();
namespace quizy
{
    public partial class MainPage : System.Web.UI.Page
    {
       
        private string editedQuestion //treść edytowanego pytania
        {
            get { return (string) ViewState["editedQuestion"]; }
            set { ViewState["editedQuestion"] = value; }
        }

        private bool editExistingQuestion //czy edytujemy pytanie z listy
        {
            get { return (bool) ViewState["editExistingQuestion"]; }
            set { ViewState["editExistingQuestion"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.editExistingQuestion = false;
                this.editedQuestion = null;
            }
            warningLabel3.Text = "";
            warningLabel1.Text = "";
            warningLabel2.Text = "";
            warningLabel0.Text = "";
        }

        //dodanie pytania do listy/edycja pytania listy
        protected void addButton_Click(object sender, EventArgs e)
        {
            //pobranie wpisanego pytania
            string newQuestion = questionTextBox.Text;

            //sprawdzenie czy pytanie się powtarza
            if (questionListBox.Items.FindByValue(newQuestion) != null) { 
                warningLabel1.Text = "podane pytanie już zostało dodane";
                return; 
            }

            //zapisanie zmian lub dodanie pytania do listy
            if (editExistingQuestion) 
            {
                if (questionListBox.Items.FindByValue(editedQuestion) != null)
                {
                    ListItem item = questionListBox.Items.FindByValue(editedQuestion);
                    item.Value = newQuestion;
                    item.Text = newQuestion;
                }
                else warningLabel1.Text = "Wybranego do edycji pytania nie ma już na liście";

                disableEditMode();
            }
            else questionListBox.Items.Add(newQuestion);

        }

        //usunięcie pytania z listy
        protected void deleteButton_Click(object sender, EventArgs e)
        {
            disableEditMode();

            //sprawdzenie czy wybrano obiekt z listy
            if (questionListBox.SelectedItem != null)
            {    
                //usunięcie wybranego pytania
                string selectedQuestion = questionListBox.SelectedItem.ToString();
                questionListBox.Items.Remove(selectedQuestion);
            }
            else warningLabel2.Text = "Wybierz pytanie z listy";
            
        }

        //włączenie trybu edycji pytania z listy
        protected void editButton_Click(object sender, EventArgs e)
        {
            if (questionListBox.SelectedItem != null)
            {
                enableEditMode();
            }
            else warningLabel2.Text = "Wybierz pytanie z listy";
        }

        //wysłanie pytań do formularza dodania do nich odpowiedzi
        protected void sendButton_Click(object sender, EventArgs e)
        {
            disableEditMode();

            if (questionListBox.Items.Count == 0)
            {
                warningLabel3.Text = "Dodaj jakieś pytanie, ćwoku";
            }
            else warningLabel3.Text = "Przejdź dalej jest nieopracowane: przekazanie pytań do formularza dodania odpowiedzi";
        }

        //włączenie trybu edycji pytania
        protected void enableEditMode()
        {
            editExistingQuestion = true;
            editedQuestion = questionListBox.SelectedItem.Value;
            questionTextBox.Text = editedQuestion;
            addButton.Text = "Zapisz";
        }

        //wyłączenie trybu edycji pytania
        protected void disableEditMode()
        {
            editedQuestion = null;
            editExistingQuestion = false;
            questionTextBox.Text = "";
           addButton.Text = "Dodaj";
        }

        //ustawienie nazwy quizu
        protected void setQuizNameButton_Click(object sender, EventArgs e)
        {
            disableEditMode();

            if (quizNameTextBox.Text == "")
            {
                warningLabel0.Text = "Nazwa quizu jest pusta";
                quizNameLabel.Text = "Podaj nazwę quizu";
            }
            else quizNameLabel.Text = quizNameTextBox.Text;
        }
    }
}