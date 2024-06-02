using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.MyClasses
{
    internal class Word : IComparable<Word>
    {
        public Word()
        {

        }
        public Word(string name, string description, string category,string imagePath= "C:\\Users\\CRISTI\\Desktop\\gfh\\My-dictionary\\Dictionary\\Dictionary\\Images\\default")
        {
            Name = name;
            Description = description;
            Category = category;
            ImagePath = imagePath;
        }

        public string Name { set; get; }
        public string Description { set; get; }
        public string Category { set; get; }
        public string ImagePath {  set; get; }

        public int CompareTo(Word other)
        {
           return string.Compare(this.Name,other.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
