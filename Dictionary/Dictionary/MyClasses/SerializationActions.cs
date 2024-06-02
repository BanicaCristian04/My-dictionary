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
        readonly string baseDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..", "Dictionary"));
        readonly string userFilePath;
        readonly string wordFilePath;
        public List<User> Users{ get; set; }
        public  SortedList<string,List<Word>> WordDictionary { get; set; }
        public SortedSet<Word> Words { get; set; }

        public SerializationActions()
        {
            userFilePath = Path.Combine(baseDirectory, "users.json");
            wordFilePath = Path.Combine(baseDirectory, "words.json");
        }

        public void DeserializeObject()
        {
            using (FileStream file = new FileStream(userFilePath, FileMode.Open))
            {
                Users = JsonSerializer.Deserialize<List<User>>(file);
            }
        }
        public void SerializeWords(SerializationActions action)
        {
            
                string jsonString = JsonSerializer.Serialize(action.WordDictionary);
                File.WriteAllText(wordFilePath, jsonString);
        }
        public void DeserializeWords()
        {
            using (FileStream file = new FileStream(wordFilePath, FileMode.Open))
            {
                WordDictionary = JsonSerializer.Deserialize<SortedList<string, List<Word>>>(file);
                Words = new SortedSet<Word>();
                foreach (var pair in WordDictionary)
                {
                    foreach (Word word in pair.Value)
                    {
                        Word deserializedWord = new Word(word);
                        deserializedWord.Category = pair.Key;
                        Words.Add(deserializedWord);
                    }
                }
            }

        }
    }
    
}
