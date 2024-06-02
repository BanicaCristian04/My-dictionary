using Dictionary.MyClasses;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dictionary
{
    
    public partial class MainWindow : Window
    {
        internal SerializationActions action;
        public MainWindow()
        {
            InitializeComponent();
            action = new SerializationActions();
            action.DeserializeWords();
            DataContext = new Category(action.WordDictionary);
        }

        private void OpenNewWindow_Click(object sender, RoutedEventArgs e)
        {
           Login loginPage=new Login();
           loginPage.Owner = this;
           this.Hide();
           loginPage.ShowDialog();
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBar.Text.ToLower();
            string selectedCategory = comboBox.SelectedItem as string;
            List<string> filteredWords = new List<string>();

            if(!string.IsNullOrEmpty(selectedCategory) && searchText!="" && !selectedCategory.Equals("Toate categoriile"))
            {
                foreach (Word word in action.WordDictionary[selectedCategory])
                {
                    if(word.Name.StartsWith(searchText))
                    {
                        filteredWords.Add(word.Name);
                        if (filteredWords.Count >= 10)
                            break;
                    }
                }
            }
            else
            {
                if(object.Equals(selectedCategory,null) || selectedCategory.Equals("Toate categoriile") && searchText != "")
                    foreach(Word word in action.Words)
                    {
                        if(word.Name.StartsWith(searchText))
                        {
                            filteredWords.Add(word.Name);
                            if (filteredWords.Count >= 10)
                                break;
                        }
                    }
            }
            ListBoxWords.ItemsSource = filteredWords;
            ListBoxWords.Visibility = filteredWords.Count > 0 ? Visibility.Visible : Visibility.Collapsed;

        }
        internal Word SearchWord(string name)
        {
            foreach(Word word in action.Words)
                if (word.Name.Equals(name))
                {
                    return word;
                }
            return null;
        }
        private void ListBoxWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxWords.SelectedItem!=null)
            {
                SearchBar.Text=ListBoxWords.SelectedItem.ToString();
                Word word = SearchWord(ListBoxWords.SelectedItem.ToString());
                ListBoxWords.Visibility = Visibility.Collapsed;
                DetailWords wordDetails = new DetailWords(word);
                wordDetails.Owner = this;
                this.Hide();
                wordDetails.Show();

            }  
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            SearchBar.RaiseEvent(new TextChangedEventArgs(TextBox.TextChangedEvent, UndoAction.None));
        }
        private void OpenGame_Click(object sender, RoutedEventArgs e)
        {
            Game gamePage=new Game();
            gamePage.Owner = this;
            this.Hide();
            gamePage.ShowDialog();
           
        }



    }
}
