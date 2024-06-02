using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        public Word(string name, string description, string category,string imagePath=null)
        {
            Name = name;
            Description = description;
            Category = category;
            ImagePath = GetAbsolutePath(imagePath);
        }
        public Word(Word word)
        {
            Name = word.Name;
            Description = word.Description;
            Category = word.Category;
            ImagePath = GetAbsolutePath(word.ImagePath);
        }

        private string GetAbsolutePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                relativePath = "Images/default.jpg";
            }
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string absolutePath = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..", "Dictionary", relativePath));
            return absolutePath;
        }
        
        public string Name { set; get; }
        public string Description { set; get; }
        public string Category { set; get; }
        public string ImagePath {  set; get; }

        public int CompareTo(Word other)
        {
           return string.Compare(Name,other.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
