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
    /// Interaction logic for DetailWords.xaml
    /// </summary>
    public partial class DetailWords : Window
    {
        internal Word Word {  get; set; }

        internal DetailWords(Word word)
        {
            InitializeComponent();
            this.Word = word;
            DataContext = Word;
        }
        private void DetailsWord_Closed(object sender, EventArgs e)
        {
            if (Owner is MainWindow mainWindow)
            {
                mainWindow.Show();
            }
        }
       
    }
}
