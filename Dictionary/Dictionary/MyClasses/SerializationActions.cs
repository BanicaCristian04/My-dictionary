using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Dictionary.MyClasses
{
    internal class SerializationActions
    {
        string filePath = "C:\\Users\\CRISTI\\Desktop\\gfh\\My-dictionary\\Dictionary\\Dictionary\\words.json";
        public List<User> Users{ get; set; }
        public  SortedList<string,List<Word>> WordDictionary { get; set; }
        public SortedSet<Word> Words { get; set; }

        public void DeserializeObject()
        {

            FileStream file = new FileStream("C:\\Users\\CRISTI\\Desktop\\gfh\\My-dictionary\\Dictionary\\Dictionary\\users.json", FileMode.Open);
            Users = JsonSerializer.Deserialize<List<User>>(file);
            file.Dispose();
        }
        public void SerializeWords(SerializationActions action)
        {
            
                string jsonString = JsonSerializer.Serialize(action.WordDictionary);
                File.WriteAllText(filePath, jsonString);
                
            
        }
        public void DeserializeWords()
        {
            FileStream file = new FileStream("C:\\Users\\CRISTI\\Desktop\\gfh\\My-dictionary\\Dictionary\\Dictionary\\words.json", FileMode.Open);
            
            WordDictionary=JsonSerializer.Deserialize<SortedList<string,List<Word>>>(file);
            Words = new SortedSet<Word>();
            foreach(var pair in WordDictionary)
            {
                foreach(Word word in pair.Value)
                {
                    word.Category = pair.Key;
                    Words.Add(word);
                }
            }
            file.Dispose();

        }
    }
    
}
