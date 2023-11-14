using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Policy;

namespace ZH_Gyakorlas2
{
    internal class Program
    {
        class ZH
        {
            string title;
            string genre;
            string publisher;
            DateTime stadiaRelease;
            DateTime originalRelease;
            public ZH(string input, string genre)
            {
                string[] temp = input.Split(';');
                title = temp[0];
                this.genre = genre;
                publisher = temp[2];
                stadiaRelease = DateTime.Parse(temp[3]);
                originalRelease = DateTime.Parse(temp[4]);
            }
            public bool publisherTrue(string publisher)
            {
                return this.publisher == publisher;
            }
            public bool withinTheSameYear()
            {
                return originalRelease.Year == stadiaRelease.Year;
            }
            public void gamesaAvailableFromReleaseDate()
            {
                Console.WriteLine($"{title} {genre} {originalRelease}");
            }
            public bool genresTrue(string genre)
            {
                return this.genre == genre;
            }
        }
        static void Main(string[] args)
        {
            //1
            string[] genres = File.ReadAllText("genre.txt").Split(',');
            for (int i = 0; i < genres.Length; i++) genres[i] = genres[i].Split('=')[0];

            //2
            string[] allLines = File.ReadAllLines("stadia_dataset.csv");
            ZH[] games = new ZH[allLines.Length - 1];
            for (int i = 1; i < allLines.Length; i++)
            {
                games[i - 1] = new ZH(allLines[i], genres[int.Parse(allLines[i].Split(';')[1]) - 1]);
            }

            //3-4-5-6
            string answer = "";
            while (answer != "0")
            {
                Console.WriteLine("Adj meg egy számot 1-3 között és 0-t, ha ki akarsz lépni");
                answer = Console.ReadLine();
                if (answer == "1")
                {
                    string publisher = Console.ReadLine();
                    int count = 0;
                    for (int i = 0; i < games.Length; i++)
                    {
                        if (games[i].publisherTrue(publisher)) count++;
                    }
                    Console.WriteLine(count);
                }
                if (answer == "2")
                {
                    for (int i = 0; i < games.Length; i++)
                    {
                        if (games[i].withinTheSameYear()) games[i].gamesaAvailableFromReleaseDate();
                    }
                }
                if (answer == "3")
                {
                    int count = 0;
                    for (int i = 0; i < genres.Length; ++i)
                    {
                        Console.Write($"{genres[i]} ");
                        for (int j = 0; j < games.Length; ++j)
                        {
                            if (games[j].genresTrue(genres[i])) count++;
                        }
                        Console.WriteLine(count);
                        count = 0;
                    }
                }
            }
        }
    }
}
