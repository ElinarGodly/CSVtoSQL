using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.ApplicationVariables
{
    public class ApplicationVariables
    {
        public ApplicationVariables() { }

        public struct CsvPaths
        {
            //public static string MoviesCSV = @"C:\Users\Novus\Desktop\Lari_C#\Project\NovusMovieProject\WebMovies\ExtendedTestData.csv";  //work
            public static string MoviesCSV = @"D:\Programming\Repos\CSVtoSQL\CS.DataLayer\ExtendedTestData.csv";  //home
        }

        public struct DataIDs
        {
            public struct CsvItems_Movies
            {
                public static int FilmID = 0;
                public static int FilmName = 1;
                public static int ImdbRating = 2;
                public static int FilmYear = 7;
                public static int DirectorID = 3;
                public static int DirectorName = 4;
                public static int ActorID = 5;
                public static int ActorName = 6;
            }
        }

        public struct TextBox
        {
            public static string baseText = "FileName";
        }

        public struct SQLTemplates
        {
            public static string actorsInsert = "insert into actors (ActorImdbID, ActorName)";
            public static string directorsInsert = "insert into directors (DirectorImdbID, DirectorName)";
            public static string filmsInsert = "insert into films (FilmImdbID, FilmName, ImdbRating, FilmYear)";
            public static string filmActorInsert = "insert into film_actor (FilmID, ActorID)";
            public static string filmDirectorInsert = "insert into film_director (FilmID, DirectorID)";
            public static string valueLine = "values ('{0}', '{1}'),";
            public static string line = "('{0}', '{1}'),";
            public static string lastLine = "('{0}', '{1}');";
            public static string valueLineFilms = "values ('{0}', '{1}', '{2}', '{3}'),";
            public static string lineFilms = "('{0}', '{1}', '{2}', '{3}'),";
            public static string lastLineFilms = "('{0}', '{1}', '{2}', '{3}');";
        }

        
    }
}
