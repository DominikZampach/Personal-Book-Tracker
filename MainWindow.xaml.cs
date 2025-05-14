using System.Diagnostics;
using System.Windows;
using RocnikovkaODK_Zampach.DialogWindows;

namespace RocnikovkaODK_Zampach
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LogicData logic = new LogicData();
        public int Book_status {  get; set; }
        public int Sort_status {  get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = logic;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Nastavit výchozí hodnotu pro radio buttons
            radioRead.IsChecked = true;
            radioPlan.IsChecked = false;
            radioReading.IsChecked = false;

            radioByName.IsChecked = true;
            radioByRating.IsChecked = false;
            radioByAuthor.IsChecked = false;
        }

        private void btn_addBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBook = new AddBookWindow();
            addBook.ShowDialog();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Zavolá se při checknutí jakéhokoliv RadioButtonu (potřeba vyvolat změnu)

            if (radioByName == null && radioByRating == null && radioByAuthor == null)
            {
                logic.LoadData(1, 1);
                return;
            }
            
            if (radioRead.IsChecked == true) { Book_status = 1; }
            else if (radioPlan.IsChecked == true) { Book_status = 2; }
            else if (radioReading.IsChecked == true) { Book_status = 3; }
            else { Book_status = 0; }

            if (radioByName.IsChecked == true) { Sort_status = 1; }
            else if (radioByRating.IsChecked == true) { Sort_status = 2; }
            else if (radioByAuthor.IsChecked == true) { Sort_status = 3; }
            else { Sort_status = 0; }

            logic.LoadData(Book_status, Sort_status);
            logic.UpdateLabel(Book_status, Sort_status);
        }

        private void listViewBooks_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Zavolá se, když člověk klikne na ListBoxItem
            if (listViewBooks.SelectedItem is Book selectBook)
            {
                listViewBooks.SelectedItem = null;
                EditBookWindow editBook = new EditBookWindow(selectBook);
                editBook.ShowDialog();

                //DEBUG ONLY
                foreach (var book in logic.ListVsechKnih)
                {
                    Trace.WriteLine($"{book.BookName}, {book.Author}, {book.Status}, {book.Rating}");
                }
                //END OF DEBUG ONLY

                logic.FileReader.writeFile("books.json", logic.ListVsechKnih);
                logic.LoadData(Book_status, Sort_status);
            }
        }
    }
}
