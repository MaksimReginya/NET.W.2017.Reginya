using System;

namespace LibraryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var video1 = new Video.Video("Top 5 fails 2017!", "Comedy");
            Console.WriteLine(video1.ToString());
            Console.ReadKey();
        }
    }
}
