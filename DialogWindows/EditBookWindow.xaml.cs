using System.ComponentModel;
using System.Windows;
using RocnikovkaODK_Zampach;

namespace RocnikovkaODK_Zampach.DialogWindows
{
    /// <summary>
    /// Interakční logika pro EditBook.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        public Book Book { get; set; }
        public EditBookWindow(Book book)
        {
            InitializeComponent();
            Book = book;
            DataContext = Book;
        }

        private void txtBoxBookName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void txtBoxAuthor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void cboxStatus_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Book.convertStatusIndexToStatus();
            Book.checkRating(); // Musi se rating nastavit na nula pokud kniha neni statusu "read"
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Book.convertStatusIndexToStatus(); // Ujistíme se, že Status je synchronizovaný
            Book.checkRating(); // Pokud není přečteno, nulujeme hodnocení
            this.Close();
        }

        private void txtBoxBookName_TextChanged_1(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void txtBoxGenre_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void txtBoxNote_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
