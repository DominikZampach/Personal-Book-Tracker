using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocnikovkaODK_Zampach
{
    internal class LogicData : INotifyPropertyChanged
    {
        public ObservableCollection<Book> SeznamKnihNaZobrazeni {  get; set; }
        public ObservableCollection<Book> ListVsechKnih {  get; set; }
        public FileReader FileReader { get; set; }
        public string CurrentLabel { get; set; }
        public LogicData()
        {
            SeznamKnihNaZobrazeni = new ObservableCollection<Book>();
            FileReader = new FileReader();
            LoadData();
        }

        public void LoadData(int bookType=0, int sort=0)
        {
            Trace.WriteLine("Loading started");
            ListVsechKnih = FileReader.readFile("books.json");
            SeznamKnihNaZobrazeni.Clear();
            foreach (Book b in ListVsechKnih)
            {
                b.initializeData();
                if (bookType == 1) //Read books
                {
                    if (b.Status == "read") { SeznamKnihNaZobrazeni.Add(b); }
                }else if (bookType == 2) //Plan to read
                {
                    if (b.Status == "plan") { SeznamKnihNaZobrazeni.Add(b); }
                }
                else if (bookType == 3) //Reading
                {
                    if (b.Status == "reading") { SeznamKnihNaZobrazeni.Add(b); }
                }
            }
            if (sort == 1) // Sort by BookName
            {
                SeznamKnihNaZobrazeni = new ObservableCollection<Book>(
                    SeznamKnihNaZobrazeni.OrderBy(b => b.BookName)
                );
            }
            else if (sort == 2 && bookType == 1) // Sort by Rating
            {
                SeznamKnihNaZobrazeni = new ObservableCollection<Book>(
                    SeznamKnihNaZobrazeni.OrderByDescending(b => b.Rating)
                );
            }
            else if (sort == 3) // Sort by Author
            {
                SeznamKnihNaZobrazeni = new ObservableCollection<Book>(
                    SeznamKnihNaZobrazeni.OrderBy(b => b.Author)
                );
            }
            VyvolejZmenu("SeznamKnihNaZobrazeni");
        }
        public void UpdateLabel(int bookType, int sortType)
        {
            string finalString = "";
            switch (bookType)
            {
                case 1:
                    finalString += "Read books ";
                    break;
                case 2:
                    finalString += "Plan to read books ";
                    break;
                case 3:
                    finalString += "Currently read books ";
                    break;
                default:
                    finalString += "Error ";
                    break;
            }
            finalString += "sorted by ";
            switch (sortType)
            {
                case 1:
                    finalString += "book name";
                    break;
                case 2:
                    finalString += "book rating";
                    break;
                case 3:
                    finalString += "author name";
                    break;
            }
            finalString += ":";
            CurrentLabel = finalString;
            VyvolejZmenu("CurrentLabel");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void VyvolejZmenu(string vlastnost)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(vlastnost));
            }
        }
    }
}
