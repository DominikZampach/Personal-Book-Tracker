using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RocnikovkaODK_Zampach
{
    public class Book : INotifyPropertyChanged
    {
        private string _bookName;
        private string _author;
        private string _status;
        private int _rating;
        private string _genre;
        private string _note;
        private string _ratingInStars;
        private int _statusIndex;

        [JsonPropertyName("bookName")]
        public string BookName
        {
            get => _bookName;
            set
            {
                _bookName = value;
                VyvolejZmenu(nameof(BookName));
            }
        }

        [JsonPropertyName("author")]
        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                VyvolejZmenu(nameof(Author));
            }
        }

        [JsonPropertyName("status")]
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                VyvolejZmenu(nameof(Status));
            }
        }

        [JsonPropertyName("rating")]
        public int Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                RatingInStars = convertRatingToStars();
                VyvolejZmenu(nameof(Rating));
            }
        }

        [JsonPropertyName("genre")]
        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                VyvolejZmenu(nameof(Genre));
            }
        }

        [JsonPropertyName("note")]
        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                VyvolejZmenu(nameof(Note));
            }
        }

        [JsonIgnore]
        public string RatingInStars
        {
            get => _ratingInStars;
            set
            {
                _ratingInStars = value;
                VyvolejZmenu(nameof(RatingInStars));
            }
        }

        [JsonIgnore]
        public int StatusIndex
        {
            get => _statusIndex;
            set
            {
                _statusIndex = value;
                VyvolejZmenu(nameof(StatusIndex));
            }
        }

        public Book(string bookName, string author, string status, int rating, string genre, string note)
        {
            BookName = bookName;
            Author = author;
            Status = status;
            Rating = rating;
            Genre = genre;
            Note = note;
            initializeData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void initializeData()
        {
            RatingInStars = convertRatingToStars();
            StatusIndex = getStatusIndex();
        }

        public int getStatusIndex()
        {
            switch (Status)
            {
                case "read":
                    return 0;
                case "plan":
                    return 1;
                case "reading":
                    return 2;
                default:
                    return 0;
            }
        }

        public void convertStatusIndexToStatus()
        {
            switch (StatusIndex)
            {
                case 0:
                    Status = "read";
                    break;
                case 1:
                    Status = "plan";
                    break;
                case 2:
                    Status = "reading";
                    break;
                default:
                    Status = "read";
                    break;
            }
        }

        public string convertRatingToStars()
        {
            switch (Rating)
            {
                case 0:
                    return "";
                case 1:
                    return "                  *";
                case 2:
                    return "                 **";
                case 3:
                    return "                ***";
                case 4:
                    return "               ****";
                case 5:
                    return "              *****";
                default:
                    return "";
            }
        }

        public void checkRating()
        {
            if (Status != "read")
            {
                Rating = 0; // setter už zajistí aktualizaci hvězdiček
            }
        }

        public override string ToString()
        {
            return $"{BookName} - {Author}\n{Genre}{convertRatingToStars()}";
        }

        public void VyvolejZmenu(string vlastnost)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(vlastnost));
        }
    }
}
