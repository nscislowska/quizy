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
        }
        /// <summary>
        /// Gets the list of answers assigned to a question.
        /// </summary>
        public List<Answer> Answers
        {
            get;
        }
        /// <summary>
        /// record number of a question in a quiz
        /// </summary>
        public int RecordNumber
        {
            get;
            set;
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
            Answers = new List<Answer>(); ;
        }

    }
}