using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quizy
{
    [Serializable]
    public class Question
    {
        /// <summary>
        /// Gets or sets the text content of a question.
        /// </summary>
        public string Text
        {
            get;
            set;
            //{
            //    if (value != null) this.Text = value;
            //    else throw new ArgumentNullException($"{nameof(value)} must not be empty.");
            //}

        }
        /// <summary>
        /// Gets the list of answers assigned to a question.
        /// </summary>
        public List<Answer> Answers //odpowiedzi do pytania
        {
            get;
        }

        public Question(string text, List<Answer> answers)
        {
            this.Text = text;
            for(int i=1; i<answers.Count; i++)
            {
                this.Answers.Add(answers[i]);
            }
        }

        public Question(string text)
        {
            this.Text = text;
            Answers = null;
        }

    }
}