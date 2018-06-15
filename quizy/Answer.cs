using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quizy
{
    [Serializable]
    public class Answer
    {
        /// <summary>
        /// Gets or sets the text content of an answer.
        /// </summary>
        public string Text
        {
            get; set;    
        }
        /// <summary>
        /// Gets or sets value determining whether an answer is correct.
        /// </summary>
        public bool IsCorrect
        {
            get; set;
        }
        /// <summary>
        /// record number of an answer in a question
        /// </summary>
        public int RecordNumber
        {
            get;
            set;
        }
        public Answer(string text, bool isCorrect)
        {
            this.Text = text;
            this.IsCorrect = isCorrect;
        }
        public Answer(string text, bool isCorrect, int recordNumber)
        {
            this.Text = text;
            this.IsCorrect = isCorrect;
            this.RecordNumber = recordNumber;
        }
    }
}