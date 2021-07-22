using System;
using Newtonsoft.Json;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace BazaFilmów5
{
    class Program
    {

        private static MoviesDatabase database = new MoviesDatabase();
   
        static void Main(string[] args)
        {
            string command = string.Empty;
            do
            {
                Console.WriteLine("Jeśli chcesz dodać film podaj kondę 'AddMovies'. Jeśli chcesz zobaczyć aktualną listę filmów podaj komendę 'MoviesList'.");
                Console.WriteLine("Jeśli chcesz usunąć film z listy podaj komendę 'RemoveMovies'. ");
                Console.WriteLine("Jeśli chcesz wyświetlić pojedyńczy film podaj komendę 'ShowMovie' ");
                Console.WriteLine("Jeśli chcesz dodać aktora do obsady filmu podaj komendę 'AddActorToMovie'");
                Console.WriteLine("Jeśli chcesz usunąc aktora z obsady danego filmu podaj komendę 'RemoveActorFromMovieCast'");
                Console.WriteLine("Jeśli chcesz dodać aktora podaj kondę 'AddActors'. Jeśli chcesz zobaczyć aktualną listę aktorów podaj komendę 'ActorsList'.");
                Console.WriteLine("Jeśli chcesz usunąć aktora z listy podaj komendę 'RemoveActors'. Jeśli chcesz zobaczyć pojedyńczego aktora podaj komendę 'ShowActor' ");
                Console.WriteLine("Jeśli chcesz dodać film do filmografii aktora podaj komendę 'AddMovieToActor' ");
                Console.WriteLine("Jeśli chcesz usunąć film z filmografii aktora podaj komendę 'RemoveMovieFromActorFilmography'");
                Console.WriteLine("Jeśli chcesz zakończyć działanie programu podaj komendę'Exit'");

                command = Console.ReadLine();

                switch (command)
                {
                   
                    case "MoviesList":
                        MoviesList();
                        break;
                    case "AddMovies":
                        AddMovies();
                        break;
                    case "RemoveMovies":
                        RemoveMovies();
                        break;
					case "ActorsList":
						ActorsList();
						break;
					case "AddActors":
                        AddActors();
                        break;
                   case "RemoveActors":
                        RemoveActors();
                        break;
                    case "ShowActor":
                        ShowActor();
                        break;
                    case "AddMovieToActor":
                        AddMovietoActor();
                        break;
                    case "ShowMovie":
                        ShowMovie();
                        break;
                    case "AddActorToMovie":
                        AddActorToMovie();
                        break;
                    case "RemoveMovieFromActorFilmography":
                        RemoveMovieFromActorFilmography();
                        break;
                    case "RemoveActorFromMovieCast":
                        RemoveActorFromMovieCast();
                        break;


                }
            } while (command != "Exit");

            Console.WriteLine("Exiting program");
            database.SaveMovies();
          
        }
        private static void MoviesList()
        {
            var movies = database.MoviesList();
            WriteJson(movies);
        }

        private static void AddMovies()
        {
            Console.WriteLine("Title:");
            var title = Console.ReadLine();

            Console.WriteLine("Year of production");

            var year = GetIntParameter();

            Console.WriteLine("Score:");

            var score = GetIntParameter();

            Console.WriteLine("Relase Date:");
            var dateInput = Console.ReadLine();
            var date = DateTime.TryParse(dateInput, out var parsedDate)
                ? parsedDate
                : default(DateTime?);

            var movie = new Movie
            {
                Title = title,
                YearofProduction = year,
                Score = score,
                RealeseDate = date
            };

            database.AddMovies(movie);
        }

        private static void RemoveMovies()
        {
			Console.WriteLine("Write the id of Movie to Deleted");

            var id = GetIntParameter();
            	
           database.RemoveMovies(id);
            
        }

		
		private static void ActorsList()
		{
			var actors = database.ActorsList();
            WriteJson(actors);
		}

		private static void AddActors()
        {
            Console.WriteLine("Name and Surname:");
            var nameandsurname = Console.ReadLine();

            Console.WriteLine("Year of birth");

            var yearofbirth = GetIntParameter();
            
            var actor = new Actor
            {
                NameandSurname = nameandsurname,
                Yearofbirth = yearofbirth,
            };

            database.AddActors(actor);
        }

        private static void RemoveActors()
        {
            Console.WriteLine("Write the id of Actor  to Deleted");

            var idactor = GetIntParameter();
          
            database.RemoveActors(idactor) ;
        }



        private static void ShowMovie()
        {
            Console.WriteLine("Choose movie which you want to see");

            var idmovie = GetIntParameter();

            Movie movie = database.GetMovieById(idmovie);

            var actors = database.GetActorNames(movie.ActorsIds);

            var movieViewModel = new
            {
                movie.Id,
                movie.Title,
                movie.Score,
                movie.RealeseDate,
                ActorNames = actors
            };

            WriteJson(movieViewModel);
        }
        
        private static void ShowActor()
        {
            Console.WriteLine("Choose actor which you want to see");
            
            var idactor = GetIntParameter();
           

            
            Actor actor = database.GetActorById(idactor) ;
          

            var movies = database.GetMoviesNames(actor.MoviesIds);
            

            var actorViewModel = new
            {
                actor.Id,
                actor.NameandSurname,
                actor.Yearofbirth,
                MoviesNames = movies

            };
            WriteJson(actorViewModel);

        }

        private static void WriteJson(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static int GetIntParameter()
        {
            var idInput = Console.ReadLine();
            var id = int.TryParse(idInput, out var parsedID)
                 ? parsedID
                 : 0;

            return id;
        }

        private static void AddMovietoActor()
        {
            Console.WriteLine("choose id of actor");
            var idActor = GetIntParameter();

            Console.WriteLine("choose id of movie");
            var idMovie = GetIntParameter();

            var actor = database.GetActorById(idActor) ;
            actor.MoviesIds ??= new List<int>();
            actor.MoviesIds.Add(idMovie);                  
            
        }

        private static void AddActorToMovie()
        {
            Console.WriteLine("choose id of movie");
            var idMovie = GetIntParameter();

            Console.WriteLine("choose id of actor");
            var idActor = GetIntParameter();

            var movie = database.GetMovieById(idMovie);
            movie.ActorsIds ??= new List<int>();
            movie.ActorsIds.Add(idActor);
        }


        private static void RemoveMovieFromActorFilmography()
        {
            Console.WriteLine("choose id of actor");
            var idActor = GetIntParameter();

            Console.WriteLine("choose id of movie to remove from actor filmography");
            var idMovie = GetIntParameter();

            var actor = database.GetActorById(idActor);
            actor.MoviesIds.Remove(idMovie);
            
        }

        private static void RemoveActorFromMovieCast()
        {
            Console.WriteLine("choose id of movie");
            var idMovie = GetIntParameter();

            Console.WriteLine("choose id of actor to remove from cast of movie");
            var idActor = GetIntParameter();

            var movie = database.GetMovieById(idMovie);
            movie.ActorsIds.Remove(idActor);
        }
    }
}


