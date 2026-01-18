using System;
using System.Collections.Generic;
using System.Linq;

namespace Magazyn
{
    class Produkt
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public decimal Cena { get; set; }
        public int Ilosc { get; set; }
    }

    class Program
    {
        static List<Produkt> produkty = new List<Produkt>();
        static int id = 1;

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Dodaj przykładowe produkty
            DodajPrzykladowe();

            while (true)
            {
                Menu();
                string opcja = Console.ReadLine();

                if (opcja == "1") PokazProdukty();
                else if (opcja == "2") Dodaj();
                else if (opcja == "3") Edytuj();
                else if (opcja == "4") Usun();
                else if (opcja == "0") break;
                else Console.WriteLine("Zła opcja!");

                Console.WriteLine("\nWciśnij Enter...");
                Console.ReadLine();
            }
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1. Pokaż produkty");
            Console.WriteLine("2. Dodaj produkt");
            Console.WriteLine("3. Edytuj produkt");
            Console.WriteLine("4. Usuń produkt");
            Console.WriteLine("0. Wyjście");
            Console.Write("Wybierz: ");
        }

        static void DodajPrzykladowe()
        {
            produkty.Add(new Produkt { Id = id++, Nazwa = "Laptop", Cena = 3500, Ilosc = 5 });
            produkty.Add(new Produkt { Id = id++, Nazwa = "Myszka", Cena = 120, Ilosc = 20 });
            produkty.Add(new Produkt { Id = id++, Nazwa = "Klawiatura", Cena = 250, Ilosc = 15 });
        }

        static void PokazProdukty()
        {
            Console.WriteLine("\nID | Nazwa | Cena (zł) | Ilość");
            Console.WriteLine("-------------------------------");

            decimal lacznaWartosc = 0;
            foreach (var p in produkty)
            {
                decimal wartosc = p.Cena * p.Ilosc;
                lacznaWartosc += wartosc;
                Console.WriteLine($"{p.Id} | {p.Nazwa} | {p.Cena}zł | {p.Ilosc}");
            }

            Console.WriteLine($"\nŁączna wartość: {lacznaWartosc}zł");
            Console.WriteLine($"Ilość produktów: {produkty.Count}");
        }

        static void Dodaj()
        {
            Console.Write("Nazwa: ");
            string nazwa = Console.ReadLine();

            Console.Write("Cena (zł): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal cena) || cena <= 0)
            {
                Console.WriteLine("Zła cena!");
                return;
            }

            Console.Write("Ilość: ");
            if (!int.TryParse(Console.ReadLine(), out int ilosc) || ilosc < 0)
            {
                Console.WriteLine("Zła ilość!");
                return;
            }

            produkty.Add(new Produkt { Id = id++, Nazwa = nazwa, Cena = cena, Ilosc = ilosc });
            Console.WriteLine("Dodano!");
        }

        static void Edytuj()
        {
            PokazProdukty();
            Console.Write("\nID do edycji: ");

            if (!int.TryParse(Console.ReadLine(), out int szukaneId))
            {
                Console.WriteLine("Złe ID!");
                return;
            }

            var produkt = produkty.FirstOrDefault(p => p.Id == szukaneId);
            if (produkt == null)
            {
                Console.WriteLine("Nie znaleziono!");
                return;
            }

            Console.Write($"Nowa nazwa [{produkt.Nazwa}]: ");
            string nowaNazwa = Console.ReadLine();
            if (!string.IsNullOrEmpty(nowaNazwa)) produkt.Nazwa = nowaNazwa;

            Console.Write($"Nowa cena [{produkt.Cena}]: ");
            string cenaStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(cenaStr) && decimal.TryParse(cenaStr, out decimal nowaCena) && nowaCena > 0)
                produkt.Cena = nowaCena;

            Console.Write($"Nowa ilość [{produkt.Ilosc}]: ");
            string iloscStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(iloscStr) && int.TryParse(iloscStr, out int nowaIlosc) && nowaIlosc >= 0)
                produkt.Ilosc = nowaIlosc;

            Console.WriteLine("Zaktualizowano!");
        }

        static void Usun()
        {
            PokazProdukty();
            Console.Write("\nID do usunięcia: ");

            if (!int.TryParse(Console.ReadLine(), out int szukaneId))
            {
                Console.WriteLine("Złe ID!");
                return;
            }

            var produkt = produkty.FirstOrDefault(p => p.Id == szukaneId);
            if (produkt == null)
            {
                Console.WriteLine("Nie znaleziono!");
                return;
            }

            produkty.Remove(produkt);
            Console.WriteLine("Usunięto!");
        }
    }
}