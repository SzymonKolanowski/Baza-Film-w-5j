using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace BazaFilmów5
{
	class MoviesDatabase
	{

		ActorsandMovies actorsandMovies = new ActorsandMovies();

		private const string databasePath = "D://Szymon//Programowanie//BazaFilmów5//BazaFilmów5//Movies.json";

		public MoviesDatabase()
		{
			string json = string.Empty;
			try
			{
				json = File.ReadAllText(databasePath);
			}
			catch { }
			actorsandMovies = JsonConvert.DeserializeObject<ActorsandMovies>(json) ?? new ActorsandMovies();


		}

		public IEnumerable<Movie> MoviesList()
		{
			return actorsandMovies.Movies;
		}

		public IEnumerable<Actor> ActorsList()
		{
			return actorsandMovies.Actors;
		}

		public void AddMovies(Movie movie)
		{
			movie.Id = actorsandMovies.Movies.Select(m => m.Id).DefaultIfEmpty().Max() + 1;
			actorsandMovies.Movies.Add(movie);
		}

		public void AddActors(Actor actor)
		{
			actor.Id = actorsandMovies.Actors.Select(m => m.Id).DefaultIfEmpty().Max() + 1;
			actorsandMovies.Actors.Add(actor);
		}

		public void SaveMovies()
		{
			var json = JsonConvert.SerializeObject(actorsandMovies, Formatting.Indented);
			if (File.Exists(databasePath))
			{
				File.Delete(databasePath);
			}
			File.WriteAllText(databasePath, json);
		}

		
		public void RemoveMovies(int id)
		{
			actorsandMovies.Movies.RemoveAll(r => id == r.Id);
		}

		public void RemoveActors(int idactor)
		{

			actorsandMovies.Actors.RemoveAll(r => idactor == r.Id);
		}
				

		public Actor GetActorById(int id)
		{
			return actorsandMovies.Actors.FirstOrDefault(a => id == a.Id);
		}

		public Movie GetMovieById(int id)
		{
			return actorsandMovies.Movies.FirstOrDefault(a => id == a.Id);
		}

		public IEnumerable<string> GetMoviesNames(IEnumerable<int> idMovies)
		{
			if (idMovies == null)
			{
				return Enumerable.Empty<string>();
			}
			return actorsandMovies.Movies.Where(m => idMovies.Any(id => id == m.Id)).
			Select(m=>m.Title) ;
			
		}

		public IEnumerable<string> GetActorNames(IEnumerable<int> idActors)
		{
			if (idActors == null)
			{
				return Enumerable.Empty<string>();
			}
			return actorsandMovies.Actors.Where(a => idActors.Any(id => id == a.Id)).
				Select(a => a.NameandSurname);
		}
			
	}
}
