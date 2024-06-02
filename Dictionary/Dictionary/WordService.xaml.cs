using Dictionary.MyClasses;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for WordService.xaml
    /// </summary>
    public partial class WordService : Window
    {
        SerializationActions actionService;
        Word addWord, updateWord;
        internal WordService()
        {
            InitializeComponent();
            actionService= new SerializationActions();
            actionService.DeserializeWords();
            Category category = new Category(actionService.WordDictionary);
            category.Categories.RemoveAt(0);
            category.Categories.Add("Categorie noua");
            addWord = new Word();   
            DataContext = category;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void AddWord_Click(object sender, RoutedEventArgs e)
        {
            WordServices.Visibility = Visibility.Visible;
            StackService.Visibility = Visibility.Collapsed;
            Add.Visibility = Visibility.Visible;
        }

        private void UpdateWord_Click(object sender, RoutedEventArgs e)
        {
            WordServices.Visibility = Visibility.Visible;
            StackService.Visibility = Visibility.Collapsed;
            Update.Visibility = Visibility.Visible;
        }
        private void DeleteWord_Click(Object sender, RoutedEventArgs e) 
        {
            WordServices.Visibility = Visibility.Visible;
            StackService.Visibility = Visibility.Collapsed;
            Delete.Visibility = Visibility.Visible;
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                
                string imagePath = openFileDialog.FileName;

                addWord.ImagePath = imagePath;
            }
        }
        private void SelectCategory_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            
            string selectedCategory = (string)addCategory.SelectedItem;
            if(!selectedCategory.Equals("Categorie noua"))
                addWord.Category= selectedCategory;
            else
            {
                NewCategoryTextBox.Visibility = Visibility.Visible;
                addCategory.Visibility = Visibility.Collapsed;
            }
        }
        private void AddWord_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.Key == Key.Enter)
            {
                string newWord = NewCategoryTextBox.Text;
                if (!string.IsNullOrEmpty(newWord))
                {
                    var wordsList = addCategory.ItemsSource as List<string>;

                    if (wordsList == null)
                    {
                        wordsList = new List<string>();
                       addCategory.ItemsSource = wordsList;
                    }
                    wordsList.Add(newWord);

                    NewCategoryTextBox.Text = string.Empty;
                    addCategory.SelectedIndex = wordsList.Count - 1;
                    addWord.Category = newWord;
                }

                addCategory.Visibility = Visibility.Visible;
                NewCategoryTextBox.Visibility = Visibility.Collapsed;
            }
        }
        private void SerializeWord_Click(object sender, RoutedEventArgs e)
        {
            string selectedCategory = addCategory.SelectedItem as string;
            if (WordName.Text == "" || WordDescription.Text == "" || object.Equals(selectedCategory,null) || object.Equals(selectedCategory,"Categorie noua"))
            {
                MessageBox.Show("Datele nu au fost introduse corect");
                return;
            }
            addWord.Name = WordName.Text;
            addWord.Description = WordDescription.Text;
            if(addWord.ImagePath==null)
            {
                addWord.ImagePath = "C:\\Users\\CRISTI\\Desktop\\gfh\\My-dictionary\\Dictionary\\Dictionary\\Images\\default.jpg";
            }
            if(actionService.WordDictionary.ContainsKey(addWord.Category))
            {
                if (!actionService.WordDictionary[addWord.Category].Contains(addWord))
                {
                    actionService.WordDictionary[addWord.Category].Add(addWord);
                }
            }
            else
            {
                List<Word> newList = new List<Word> { addWord };
                actionService.WordDictionary.Add(addWord.Category, newList);
            }
            WordName.Text = string.Empty;
            WordDescription.Text = string.Empty;
            addCategory.Text = string.Empty;

            WordName.Text = string.Empty;
            WordName.Text = string.Empty;
            Serialize();
            WordServices.Visibility = Visibility.Collapsed;
            StackService.Visibility = Visibility.Visible;
            Add.Visibility= Visibility.Collapsed; 
        }
        private void Serialize()
        {
            try
            {
                actionService.SerializeWords(actionService);
                MessageBox.Show("Operatie cu succes");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Serialization error" + ex.Message);
            }
        }
        private void DeleteSerializeWord_Click(object sender, EventArgs e)
        {
            foreach (var pair in actionService.WordDictionary)
                foreach (Word word in pair.Value)
                    if (word.Name.Equals(deleteBox.Text))
                    {
                        if (pair.Value.Count == 1)
                            actionService.WordDictionary.Remove(pair.Key);
                        else
                        {
                            actionService.WordDictionary[pair.Key].Remove(word);
                        }
                        Serialize();
                        WordServices.Visibility = Visibility.Collapsed;
                        StackService.Visibility = Visibility.Visible;
                        Delete.Visibility = Visibility.Collapsed;
                        return;
                    }
            MessageBox.Show("Cuvantul introdus nu se afla in tastatura! Introdu alt cuvant");
        }
        private void FindWord_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.Key == Key.Enter)
            {

                foreach (var pair in actionService.WordDictionary)
                    foreach (Word word in pair.Value)
                        if (word.Name.Equals(UpdateBox.Text))
                        {
                            Add.Visibility = Visibility.Visible;
                            UpdateWordSerialize.Visibility = Visibility.Visible;
                            AddWordSerialize.Visibility = Visibility.Collapsed;
                            Update.Visibility = Visibility.Collapsed;
                            WordName.Text= word.Name;
                            WordDescription.Text= word.Description;
                            addCategory.Text= word.Category;
                            updateWord = word;
                            UpdateBox.Text = string.Empty;

                        }                        
            }
                
        }
        private void UpdateSerializeWord_Click(object sender, RoutedEventArgs e)
        { 
            string selectedCategory = addCategory.SelectedItem as string;
            if (WordName.Text == updateWord.Name
                && WordDescription.Text == updateWord.Description
                && (selectedCategory == null || selectedCategory == "Categorie noua")
                && (addWord.ImagePath == null || addWord.ImagePath == updateWord.ImagePath)
                )
            {
                MessageBox.Show("Datele nu au fost modificate");
                return;
            }
            else
            {
                addWord.Name = WordName.Text;
                addWord.Description = WordDescription.Text;
                if(addWord.ImagePath==null)
                    addWord.ImagePath= updateWord.ImagePath; 
                WordName.Text = string.Empty;
                WordDescription.Text = string.Empty;
                addCategory.Text = string.Empty;

                WordName.Text = string.Empty;
                WordName.Text = string.Empty;
                if (!(selectedCategory == null || selectedCategory == updateWord.Category))
                {
                    if (actionService.WordDictionary[updateWord.Category].Count == 1)
                    {
                        actionService.WordDictionary.Remove(updateWord.Category);

                        
                    }
                    else
                    {
                        actionService.WordDictionary[updateWord.Category].Remove(updateWord);
                    }
                    if (actionService.WordDictionary.ContainsKey(selectedCategory))
                            actionService.WordDictionary[selectedCategory].Add(addWord);
                        else
                        {
                        List<Word> newList = new List<Word> { addWord };
                        actionService.WordDictionary.Add(selectedCategory, newList);
                            
                        }
                }  
                }
                Serialize();
                UpdateWordSerialize.Visibility = Visibility.Collapsed;
                AddWordSerialize.Visibility = Visibility.Visible;
                Add.Visibility = Visibility.Collapsed;
                WordServices.Visibility = Visibility.Collapsed;
                StackService.Visibility = Visibility.Visible;
            }

        }
    }


