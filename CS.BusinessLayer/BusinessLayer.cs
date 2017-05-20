using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS.ClassLayer;
using av = CS.ApplicationVariables.ApplicationVariables;
using cl = CS.ClassLayer.ClassLayer;
using dl = CS.DataLayer.DataLayer;
using System.Text.RegularExpressions;

namespace CS.BusinessLayer
{
    public class BusinessLayer : IDisposable
    {
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

        //--------------------------------------------------------------------- FILMS
        public cl.Films GetFilms(string csvPath)
        {
            using (dl dl1 = new dl())
            {
                return dl1.GetCsvData(csvPath);
            }
            //-- TODO: raise error if needed
        }

        public List<cl.Actor> GetDistinctActorsFromFilms(cl.Films films)
        {
            return (films == null) ? null : films.ToListDistinctActor();
        }

        public List<cl.Director> GetDistinctDirectorsFromFilms(cl.Films films)
        {
            return (films == null) ? null : films.ToListDistinctDirector();
        }


        //------------------------------------------------------------------------------------------------------SQL lines 
        public List<string> fileLines(cl.Films films, List<cl.Actor> actors, List<cl.Director> directors)
        {
            /* create a method that creates a list of string
             * every string is a line of SQL code
             * the method calls separate methods that give back a list of strings
             * the methods create SQL code for 
             * insert actor,
             * insert director,
             * insert films,
             * insert film and actor
             * insert film and director
             */
            List<string> lines = new List<string>();
            lines.AddRange(insertActors(actors));
            lines.Add(String.Empty);
            lines.AddRange(insertDirectors(directors));
            lines.Add(String.Empty);
            lines.AddRange(insertFilms(films));
            lines.Add(String.Empty);
            lines.AddRange(insertFilmActor(films, actors));
            lines.Add(String.Empty);
            lines.AddRange(insertFilmDirector(films, directors));

            return lines;
        }

        private List<string> insertActors(List<cl.Actor> actors)
        {
            List<string> code = new List<string>();
            
            code.Add(av.SQLTemplates.actorsInsert);

            for (int index = 0;index < actors.Count; index++) 
                if(index==0)
                    code.Add(String.Format(av.SQLTemplates.valueLine, actors[index].PersonID, actors[index].PersonName));
                else if (index == actors.Count - 1)
                    code.Add(String.Format(av.SQLTemplates.lastLine, actors[index].PersonID, actors[index].PersonName));
                else
                    code.Add(String.Format(av.SQLTemplates.line, actors[index].PersonID, actors[index].PersonName));
            
            return code;
        }

        private List<string> insertDirectors(List<cl.Director> directors)
        {
            List<string> code = new List<string>();
            
            code.Add(av.SQLTemplates.directorsInsert);

            for(int index = 0;index < directors.Count;index++)
                if(index==0)
                    code.Add(String.Format(av.SQLTemplates.valueLine, directors[index].PersonID, directors[index].PersonName));
                else if (index == directors.Count - 1)
                    code.Add(String.Format(av.SQLTemplates.lastLine, directors[index].PersonID, directors[index].PersonName));
                else
                    code.Add(String.Format(av.SQLTemplates.line, directors[index].PersonID, directors[index].PersonName));

            return code;
        }

        private List<string> insertFilms(cl.Films films)
        {
            List<string> code = new List<string>();

            code.Add(av.SQLTemplates.filmsInsert);
            

            for (int index = 0; index < films.Count; index++)
                if(index==0)
                    code.Add(String.Format(av.SQLTemplates.valueLineFilms, films[index].FilmID, films[index].FilmName,
                                                                films[index].ImdbRating, films[index].FilmYear));
                else if (index == films.Count - 1)
                    code.Add(String.Format(av.SQLTemplates.lastLineFilms, films[index].FilmID, films[index].FilmName,
                                                                films[index].ImdbRating, films[index].FilmYear));
                else
                    code.Add(String.Format(av.SQLTemplates.lineFilms, films[index].FilmID, films[index].FilmName,
                                                                films[index].ImdbRating, films[index].FilmYear));

            return code;
        }

        private List<string> insertFilmActor(cl.Films films, List<cl.Actor> actors)
        {
            List<string> code = new List<string>();

            code.Add(av.SQLTemplates.filmActorInsert);

            for(int index=0; index < films.Count; index++)
                if (index==0)
                    foreach (var actor in films[index].Actors)
                    {
                        code.Add(String.Format(av.SQLTemplates.valueLine, (index + 1).ToString(), actors.FindIndex(p=> p.PersonID==actor.PersonID)));
                    }
                else if(index == films.Count - 1)
                    foreach (var actor in films[index].Actors)
                    {
                        code.Add(String.Format(av.SQLTemplates.lastLine, (index + 1).ToString(), actors.FindIndex(p => p.PersonID == actor.PersonID)));
                    }
                else
                    foreach (var actor in films[index].Actors)
                    {
                        code.Add(String.Format(av.SQLTemplates.line, (index + 1).ToString(), actors.FindIndex(p => p.PersonID == actor.PersonID)));
                    }

            return code;
        }

        private List<string> insertFilmDirector(cl.Films films, List<cl.Director> directors)
        {
            List<string> code = new List<string>();

            code.Add(av.SQLTemplates.filmDirectorInsert);

            for (int index = 0;index<films.Count;index++)
                if(index==0)
                    foreach (var director in films[index].Directors)
                    {
                        string codeLine = String.Format(av.SQLTemplates.valueLine, (index + 1).ToString(), directors.FindIndex(p => p.PersonID == director.PersonID));
                        code.Add(Regex.Replace(codeLine, "[']", string.Empty));
                    }
                else if (index == films.Count - 1)
                    foreach (var director in films[index].Directors)
                    {
                        code.Add(String.Format(av.SQLTemplates.lastLine, (index + 1).ToString(), directors.FindIndex(p => p.PersonID == director.PersonID)));
                    }
                else
                    foreach (var director in films[index].Directors)
                    {
                        code.Add(String.Format(av.SQLTemplates.line, (index + 1).ToString(), directors.FindIndex(p => p.PersonID == director.PersonID)));
                    }

            return code;
        }

    }
}
