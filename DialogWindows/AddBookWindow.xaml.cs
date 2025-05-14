using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace RocnikovkaODK_Zampach.DialogWindows
{
    /// <summary>
    /// Interakční logika pro AddBook.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public ObservableCollection<Book> Books { get; set; }
        public AddBookWindow(ObservableCollection<Book> books)
        {
            InitializeComponent();
            Books = books;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string status = "";
            switch (cboxStatus.SelectedIndex)
            {
                case 0:
                    status = "read";
                    break;
                case 1:
                    status = "plan";
                    break;
                case 2:
                    status = "reading";
                    break;
                default:
                    break;
            }
            Book book = new Book(txtBoxBookName.Text, txtBoxAuthor.Text, status, cboxRating.SelectedIndex, txtBoxGenre.Text, txtBoxNote.Text);
            Books.Add(book);
            this.Close();
        }
    }
}
