using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LumenWorks.Framework.IO.Csv;
using csvMovies = CS.ApplicationVariables.ApplicationVariables.DataIDs.CsvItems_Movies;
using cl = CS.ClassLayer.ClassLayer;
using System.IO;

namespace CS.DataLayer
{
    public class DataLayer : IDisposable
    {
        /*copy csv reader func
         *make a function that given a list of string saves it to a .sql file  
         */
        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }

        #region GetCsvData
        public cl.Films GetCsvData(string CsvPath)
        {
            cl.Films films = new cl.Films();

            //-- FilmID = csv[0];
            //-- FilmName = csv[1];
            //-- ImdbRating = csv[2];
            //-- FilmYear = csv[7];
            //-- DirectorID = csv[3];
            //-- DirectorName = csv[4];
            //-- ActorID = csv[5];
            //-- ActorName = csv[6];

            using (CsvReader csv = new CsvReader(new StreamReader(CsvPath), true))
            {
                int fieldCount = csv.FieldCount;
                long i = 0;
                string[] headers = csv.GetFieldHeaders();
                while (csv.ReadNextRecord())
                {
                    i = csv.CurrentRecordIndex;
                    if (films.Any(item => item.FilmID == csv[csvMovies.FilmID]))
                    {
                        cl.Film tmpFilm = films.Find(item => item.FilmID == csv[csvMovies.FilmID]);
                        if (tmpFilm.Directors.Any(item => item.PersonID == csv[csvMovies.DirectorID]))
                        { }
                        else
                        {
                            cl.Director director = getDirectorFromCSV(csv);
                            tmpFilm.Directors.Add(director);
                        }
                        if (tmpFilm.Actors.Any(item => item.PersonID == csv[csvMovies.ActorID]))
                        { }
                        else
                        {
                            cl.Actor actor = getActorFromCSV(csv);
                            tmpFilm.Actors.Add(actor);
                        }
                    }
                    else
                    {
                        cl.Film film = getFilmFromCSV(csv);
                        films.Add(film);
                    }
                }
            }
            //films = (cl.Films) films.OrderBy(x => x.FilmName).ToList();
            return films;
        }

        private cl.Director getDirectorFromCSV(CsvReader csv)
        {
            cl.Director director = new cl.Director(csv[csvMovies.DirectorID]
                                                    , csv[csvMovies.DirectorName]);
            return director;
        }

        private cl.Actor getActorFromCSV(CsvReader csv)
        {
            cl.Actor actor = new cl.Actor(csv[csvMovies.ActorID]
                                            , csv[csvMovies.ActorName]);
            return actor;
        }

        private cl.Film getFilmFromCSV(CsvReader csv)
        {
            cl.Director director = getDirectorFromCSV(csv);
            cl.Actor actor = getActorFromCSV(csv);
            cl.Film film = new cl.Film(csv[csvMovies.FilmID]
                                        , csv[csvMovies.FilmName]
                                        , csv[csvMovies.ImdbRating]
                                        , csv[csvMovies.FilmYear]);
            film.Directors.Add(director);
            film.Actors.Add(actor);
            return film;
        }
        #endregion

        #region Create File

        public void createFile(List<string> codeLines,string fileName)
        {
            //TODO add file directory
            File.WriteAllLines(fileName, codeLines);
        }

        #endregion

    }
}
