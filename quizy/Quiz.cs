using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quizy
{
    [Serializable]
    public class Quiz
    {
        /// <summary>
        /// Gets or sets the name of a quiz.
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the list of questions assigned to a quiz.
        /// </summary>
        public List<Question> Questions
        {
            get;
        }

        public Quiz()
        {
            this.Name = null;
            this.Questions=new List<Question>();
        }

        public Quiz(string Name, List<Question> questions)
        {
            this.Name = Name;
            this.Questions = questions;
        }

        public Quiz(string Name)
        {
            this.Name = Name;
            this.Questions = new List<Question>();
        }

    }
}