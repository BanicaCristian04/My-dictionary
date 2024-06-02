using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.MyClasses
{
    internal class Category
    {
        public Category()
        {
            Categories = new List<string>();
        }
        public Category(SortedList<string,List<Word>> words) 
        {
           
            Categories =new List<string>( words.Keys);
            Categories.Insert(0, "Toate categoriile");
        }
        public List<string> Categories { get; set; }
    }
}
