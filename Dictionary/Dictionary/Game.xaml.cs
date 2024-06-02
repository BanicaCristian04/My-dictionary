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
using System.Windows.Shapes;

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private List<Word> randomWords = new List<Word>(); 
        private int correctAnswers = 0;
        Word currentWord;
        SerializationActions actionGame;
        private string defaultImage="C:\\Users\\CRISTI\\Desktop\\gfh\\My-dictionary\\Dictionary\\Dictionary\\Images\\default.jpg";
        public Game()
        {
            InitializeComponent();
            actionGame = new SerializationActions();
            actionGame.DeserializeWords();
        }
        private void Game_Closed(object sender, EventArgs e)
        {
            if (Owner is MainWindow mainWindow)
            {
                mainWindow.Show();
            }
        }
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            var random= new Random();
            List<Word> wordList = actionGame.Words.ToList();
            while (randomWords.Count < 5 && wordList.Count > 0)
            {
                
                int randomIndex = random.Next(0, wordList.Count);
                if (!randomWords.Contains(wordList[randomIndex]))
                {
                    randomWords.Add(wordList[randomIndex]);
                    wordList.RemoveAt(randomIndex);

                }
            }
            Round.Visibility= Visibility.Visible;
            Start.Visibility= Visibility.Collapsed;
            currentWord = randomWords[0];
            DataContext = randomWords[0];

            
                int randomAtribute = random.Next(0, 2);

                if (randomAtribute == 1 && currentWord.ImagePath!=defaultImage)
                {
                    ImageBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    DescriptionBlock.Visibility = Visibility.Visible;
                }
            
        }
        private void NextRound_Click(object sender, RoutedEventArgs e)
        {
            if (currentWord.Name == Point.Text)
                correctAnswers++;
            Point.Text = string.Empty;
            if(randomWords.Count > 1)
            {
                randomWords.RemoveAt(0);
                currentWord = randomWords[0];
                DataContext = currentWord;
            }
            else
            {
                Round.Visibility=Visibility.Collapsed;
                Final.Visibility=Visibility.Visible;
                if(correctAnswers==5)
                {
                    ResultTextBlock.Text = "Felicitari! Ati obtinut 5/5 puncte";
                }
                else
                {
                    ResultTextBlock.Text = " Ati obtinut " + correctAnswers.ToString() + " puncte. Mai incearca!";

                }
                return;
            }
                var random = new Random();
                int randomAtribute = random.Next(0, 2);
                if (randomAtribute == 1 && currentWord.ImagePath!=defaultImage)
                {
                    ImageBlock.Visibility = Visibility.Visible;
                    DescriptionBlock.Visibility = Visibility.Collapsed;
                }
                else
                {
                    DescriptionBlock.Visibility = Visibility.Visible;
                    ImageBlock.Visibility = Visibility.Collapsed;
                }
            


        }
        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            Final.Visibility=Visibility.Collapsed;
            Start.Visibility=Visibility.Visible;
        }
    }
}
