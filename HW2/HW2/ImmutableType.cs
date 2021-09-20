using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{
    class ImmutableType
    {
        public string Name { get; private set; }
        public string Article { get; }

        // Private constructor.       
        private ImmutableType(string authorName, string articleName)
        {
            Name = authorName;
            Article = articleName;
        }

        // Public factory method.       
        public static ImmutableType ConstructArticle(string name, string article)
        {
            return new ImmutableType(name, article);
        }
        // creates a copy of an object
        public ImmutableType ChangeArticleTo(string newArticle)
        {
            return new ImmutableType(Name, newArticle); ;
        }
    }
}
