using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BazaFilmów5
{
	public class Movie
	{

		public int Id { get; set; }
		public string Title { get; set; }
		public int Score { get; set; }
		public int YearofProduction { get; set; }
		public DateTime? RealeseDate { get; set; }
		public List<int> ActorsIds { get; set; } 
		
	}

	public class Actor
	{
		public int Id { get; set; }
		public string NameandSurname { get; set; }
		public int Yearofbirth { get; set; }
		public List<int> MoviesIds { get; set; }
	}



}







