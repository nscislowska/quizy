using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace quizy
{
    public partial class AddAnswers : System.Web.UI.Page
    {
        private Quiz Quiz //przechowuje zawartość całego quizu
        {
            get { return (Quiz)Session["Quiz"]; }
            set { Session["Quiz"] = value; }
        }
        private string AddQuizError //zawiera błąd dodawania quizu
        {
            get { return (string)Session["AddQuizError"]; }
            set { Session["AddQuizError"] = value; }
        }
        private int ThisQuestionRecordNumber //numer porządkowy aktualnie wyświetlanego pytania
        {
            get { return (int)Session["ThisQuestionRecordNumber"]; }
            set { Session["ThisQuestionRecordNumber"] = value; }
        }

        private string editedAnswer //treść edytowanego pytania
        {
            get { return (string)ViewState["editedAnswer"]; }
            set { ViewState["editedAnswer"] = value; }
        }

        private bool editExistingAnswer //czy edytujemy pytanie z listy
        {
            get { return (bool)ViewState["editExistingAnswer"]; }
            set { ViewState["editExistingAnswer"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.editedAnswer = null;
                this.editExistingAnswer = false;

                if (AddQuizError != null)
                {
                    warningLabel2.Text = "Błąd dodania quizu: " + AddQuizError;
                }

                if (Quiz.Questions[ThisQuestionRecordNumber - 1].Answers.Count > 0) loadAnswers();


            }
            else AddQuizError = null;

            QuizNameLabel.Text = Quiz.Name;
            QuestionTextLabel.Text = "Pytanie "+ThisQuestionRecordNumber+": " + Quiz.Questions[ThisQuestionRecordNumber-1].Text;
            warningLabel0.Text = warningLabel1.Text =warningLabel3.Text= "";

            if (AddQuizError == null) warningLabel2.Text = ""; ;
        }

        protected void addAnswerButton_Click(object sender, EventArgs e)
        {
            //pobranie wpisanego pytania
            string newAnswer = AnswerTextBox.Text;
            if (newAnswer == "")
            {
                warningLabel0.Text = "nie podano pytania";
                return;
            }
            //sprawdzenie czy pytanie się powtarza
            if (AnswerListBox.Items.FindByValue(newAnswer) != null)
            {
                warningLabel0.Text = "podane pytanie już zostało dodane";
                return;
            }

            //zapisanie zmian lub dodanie pytania do listy
            if (editExistingAnswer)
            {
                if (AnswerListBox.Items.FindByValue(editedAnswer) != null)
                {
                    ListItem item = AnswerListBox.Items.FindByValue(editedAnswer);
                    item.Value = newAnswer;
                    item.Text = newAnswer;
                }
                else warningLabel0.Text = "Wybranego do edycji pytania nie ma już na liście";

                disableEditMode();
            }
            else
            {
                AnswerListBox.Items.Add(newAnswer);
                AnswerTextBox.Text = "";
            }
        }

        protected void editAnswerButton_Click(object sender, EventArgs e)
        {
            if (AnswerListBox.SelectedItem != null)
            {
                enableEditMode();
            }
            else warningLabel1.Text = "Wybierz pytanie z listy";
        }

        protected void deleteAnswerButton_Click(object sender, EventArgs e)
        {
            disableEditMode();

            //sprawdzenie czy wybrano obiekt z listy
            if (AnswerListBox.SelectedItem != null)
            {
                //usunięcie wybranego pytania
                string selectedAnswer = AnswerListBox.SelectedItem.ToString();
                AnswerListBox.Items.Remove(selectedAnswer);
            }
            else warningLabel1.Text = "Wybierz pytanie z listy";
        }

        protected void sendAnswersButton_Click(object sender, EventArgs e)
        {
            disableEditMode();

            //sprawdzenie czy wypełniono wymagane pola
            bool isCorrect = true;
            if (AnswerListBox.Items.Count == 0)
            {
                warningLabel1.Text = "Dodaj przynajmniej jedno pytanie";
                isCorrect = false;
            }
            if (correctAnswerLabel.Text == "")
            {
                warningLabel3.Text = "Wybierz prawidłową odpowiedź";
                isCorrect = false;
            }

            if (isCorrect)
            {
                saveQuizInSession(); //zapisanie quizu w sesji 
                ThisQuestionRecordNumber++; //ustawienie numeru pytania (dla okna odpowiedzi)

                if(ThisQuestionRecordNumber>Quiz.Questions.Count) Server.Transfer("TestPage.aspx"); //przekierowanie do wyjścia
                else Server.Transfer("AddAnswers.aspx"); //przekierowanie do dodania odpowiedzi
            }

            warningLabel2.Text = "Nie można przejść dalej z powodu błędów";
        }

        //włączenie trybu edycji pytania
        protected void enableEditMode()
        {
            editExistingAnswer = true;
            editedAnswer = AnswerListBox.SelectedItem.Value;
            AnswerTextBox.Text = editedAnswer;
            addAnswerButton.Text = "Zapisz";
        }

        //wyłączenie trybu edycji pytania
        protected void disableEditMode()
        {
            editedAnswer = null;
            editExistingAnswer = false;
            AnswerTextBox.Text = "";
            addAnswerButton.Text = "Dodaj";
        }

        //zapisanie odpowiedzi do quizu
        private void saveQuizInSession()
        {
            string answerText;
            bool correct;

            Quiz.Questions[ThisQuestionRecordNumber - 1].Answers.Clear();

            for (int i = 0; i < AnswerListBox.Items.Count; i++)
            {
                answerText = AnswerListBox.Items[i].Value;
                if (answerText == correctAnswerLabel.Text) correct = true;
                else correct = false;

                Answer answer = new Answer(answerText, correct);
                answer.RecordNumber = i + 1;
                Quiz.Questions[ThisQuestionRecordNumber - 1].Answers.Add(answer);
            }
        }

        protected void markCorrectButton_Click(object sender, EventArgs e)
        {
            disableEditMode();

            //sprawdzenie czy wybrano obiekt z listy
            if (AnswerListBox.SelectedItem != null)
            {
                string selectedAnswer = AnswerListBox.SelectedItem.ToString();
                correctAnswerLabel.Text = selectedAnswer;//oznaczenie odpowiedzi jako poprawnej
            }
            else
            {
                warningLabel1.Text = "Wybierz pytanie z listy";
                correctAnswerLabel.Text = "";
            }
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            if (ThisQuestionRecordNumber == 1)
            {
                Server.Transfer("AddQuiz.aspx");
            }
            else
            {
                ThisQuestionRecordNumber = ThisQuestionRecordNumber - 1;
                Server.Transfer("AddAnswers.aspx");
            }
            
        }

        private void loadAnswers()
        {
            disableEditMode();

            AnswerListBox.Items.Clear();

            foreach (Answer answer in Quiz.Questions[ThisQuestionRecordNumber - 1].Answers)
            {
                AnswerListBox.Items.Add(answer.Text);
                if (answer.IsCorrect == true)
                {
                    correctAnswerLabel.Text = answer.Text;
                }
            }
            return;
        }
    }
}