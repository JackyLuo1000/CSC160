using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchConsole.Models
{
    public class Movies
    {
        private string title;
        public string Director { get; private set; }
        public int Runtime { get; private set; }
        public int Year { get; private set; }

        public string Title
        {
            get { return title;  }
            set
            {
                if (!string.IsNullOrEmpty(value?.Trim()))
                {
                    title = value;
                }
            }
        }

        public Movies(string director, int runtime, int year, string title)
        {
            Director = director ?? throw new ArgumentNullException(nameof(director));
            Runtime = runtime;
            Year = year;
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }
    }
}
