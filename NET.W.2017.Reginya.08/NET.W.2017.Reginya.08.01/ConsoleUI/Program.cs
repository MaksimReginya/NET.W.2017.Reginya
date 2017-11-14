using System;
using System.Collections.Generic;
using System.IO;

using BookLogic;
using BookLogic.Comparers;
using BookLogic.Loggers;
using BookLogic.Predicates;
using Storage;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {            
            try
            {
                if (File.Exists("books"))
                {
                    LoadFromStorageTest("books");
                }
                else
                {
                    BookListServiceTest();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static void BookListServiceTest()
        {
            var books = new List<Book>
            {
                new Book("999-9-99-999999-0", "Тепляков С.", "Паттерны проектирования", "publisher1", 2015, 312, 15.20d),
                new Book("999-9-99-999999-1", "Bart De Smet", "C# 4.0 Unleashed", "publisher2", 2010, 1605, 35.99d)
            };            

            var bookListService = new BookListService(new NLogger(nameof(BookListService)), books);
            PrintBooks(bookListService.GetBooks());

            bookListService.AddBook(new Book("999-9-99-999999-2", "Jon Skeet", "C# in Depth. Third edition", "publisher3", 2014, 582, 25.99d));            
            PrintBooks(bookListService.GetBooks());           

            bookListService.SortBooksByTag(new TitleComparer());
            PrintBooks(bookListService.GetBooks()); 

            bookListService.RemoveBook(new Book("999-9-99-999999-1", "Bart De Smet", "C# 4.0 Unleashed", "publisher2", 2010, 1605, 35.99d));
            PrintBooks(bookListService.GetBooks());

            Console.WriteLine(bookListService.FindBookByTag(new IsTitleValid("C#")));
            Console.WriteLine();

            bookListService.Save(new BinaryFileStorage("books"));
        }

        private static void LoadFromStorageTest(string storageName)
        {
            var bookListService = new BookListService(new NLogger(nameof(BookListService)));
            bookListService.Load(new BinaryFileStorage(storageName));

            PrintBooks(bookListService.GetBooks());
        }

        private static void PrintBooks(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }

            Console.WriteLine();
        }
    }
}
