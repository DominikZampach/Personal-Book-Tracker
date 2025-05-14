using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace RocnikovkaODK_Zampach
{
    internal class FileReader
    {
        public ObservableCollection<Book> Books;
        public FileReader()
        {
            Books = new ObservableCollection<Book>();
        }

        public ObservableCollection<Book> readFile (string fileName) 
        {
            string fullFileName = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, fileName);
            if (!File.Exists(fullFileName))
            {
                Trace.WriteLine($"Aplikace nenašla soubor {fileName}");
                return new ObservableCollection<Book>();
            }
            try
            {
                string JsonRead;
                using (StreamReader sr = new StreamReader(fullFileName))
                {
                    JsonRead = sr.ReadToEnd();
                    Books = System.Text.Json.JsonSerializer.Deserialize<ObservableCollection<Book>>(JsonRead);
                }
                if (Books != null && Books.Count > 0)
                {
                    // Vše proběhlo úspešně, knihy byly načteny
                    return Books;
                }
                return new ObservableCollection<Book>(); // Vrácení prázdného listu knih
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"CHYBA: {ex}");
                // Vrátit nějakou formou chybu (napr. napsat do nejakeho textboxu nebo dialogoveOkno)
                return new ObservableCollection<Book>();
            }
        }
        public void writeFile(string fileName, ObservableCollection<Book> books)
        {
            string fullFileName = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, fileName);
            Trace.WriteLine("Cesta: " + fullFileName);
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(books, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Výpis JSON do Debug okna
                Debug.WriteLine(json); // Zkontroluj, jestli se správně serializuje

                File.WriteAllText(fullFileName, json); // Uložení do souboru
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Chyba při ukládání: {ex.Message}");
            }
        }
    }
}
