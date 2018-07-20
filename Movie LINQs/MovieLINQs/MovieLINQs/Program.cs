using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFITopMoviesDB;
using ExtMethods;

namespace MovieLINQs
{
    class Program
    {
        //private static Movie[] movies = {
        //    new Movie("Antman and the Wasp", 2018, MPAARating.PG13),
        //    new Movie("Antman", 2015, MPAARating.PG13),
        //    new Movie("Ironman", 2008, MPAARating.PG13),
        //    new Movie("Ironman 2", 2010, MPAARating.PG13),
        //    new Movie("Ironman 3", 2013, MPAARating.PG13),
        //    new Movie("The Incredible Hulk", 2008, MPAARating.PG13),
        //    new Movie("Thor", 2011, MPAARating.PG13),
        //    new Movie("Captain America: The First Avenger", 2011, MPAARating.PG13),
        //    new Movie("The Avengers", 2012, MPAARating.PG13),
        //    new Movie("Thor: The Dark World", 2013, MPAARating.PG13),
        //    new Movie("Captain America: The Winter Soldier", 2014, MPAARating.PG13),
        //    new Movie("The Guardians of the Galaxy", 2014, MPAARating.PG13),
        //    new Movie("Avengers: Age of Ultron", 2015, MPAARating.PG13),
        //    new Movie("Captain America: Civil War", 2016, MPAARating.PG13),
        //    new Movie("Doctor Strange", 2016, MPAARating.PG13),
        //    new Movie("Guardians of the Galaxy Vol. 2", 2017, MPAARating.PG13),
        //    new Movie("Spider-Man Homecoming", 2017, MPAARating.PG13),
        //    new Movie("Thor: Ragnarok", 2017, MPAARating.PG13),
        //    new Movie("Black Panther", 2018, MPAARating.PG13),
        //    new Movie("Avengers: Infinity War", 2018, MPAARating.PG13),
        //    new Movie("Deadpool 2", 2018, MPAARating.R),
        //    new Movie("Deadpool", 2016, MPAARating.R)
        //};

        public static List<Movie> movies = MovieLoader.AllMovies;
        
        static void Main(string[] args)
        {
            Query12();
        }
        
        //What are the years of the oldest and newest movies in the list?

        public static void Query1()
        {
            var newestMovie = movies.Aggregate((m1, m2) => m1.Year > m2.Year ? m1 : m2);
            Console.WriteLine($"The newest movie in the collection is {newestMovie.Title} released in {newestMovie.Year}.");
            var oldestMovie = movies.Aggregate((m1, m2) => m1.Year < m2.Year ? m1 : m2);
            Console.WriteLine($"The oldest movie in the collection is {oldestMovie.Title} released in {oldestMovie.Year}.");
        }

        //Split the total year range in half. Movies on the lower half are considered “classic” while those on the upper half are considered “new”. How many classic movies are there? How many new movies?    

        public static void Query2()
        {
            var newestMovie = movies.Aggregate((m1, m2) => m1.Year > m2.Year ? m1 : m2);
            var oldestMovie = movies.Aggregate((m1, m2) => m1.Year < m2.Year ? m1 : m2);
            int middleYear = ((newestMovie.Year - oldestMovie.Year) / 2) + oldestMovie.Year;
            var newMovies = movies.Where(movie => movie.Year >= middleYear);
            var classicMovies = movies.Where(movie => movie.Year < middleYear);
            Console.WriteLine($"There are {newMovies.Count()} new movies in this collection.");
            Console.WriteLine($"There are {classicMovies.Count()} classic movies in this collection.");
        }
        
        //Are there more odd years or even years in the list? Show the quantities of both, please.
        
        public static void Query3()
        {
            var oddMovies = movies.Where(movie => movie.Year % 2 == 1);
            var evenMovies = movies.Where(movie => movie.Year % 2 == 0);
            if(evenMovies.Count() > oddMovies.Count())
            {
                Console.WriteLine($"There are more movies released in even years than odd years in this collection.");
            }else if(evenMovies.Count() < oddMovies.Count())
            {
                Console.WriteLine($"There are more movies released in odd years than even years in this collection.");
            }else
            {
                Console.WriteLine($"The number of movies released in even and odd years are equal in this collection.");
            }
            Console.WriteLine($"Even Release Count: {evenMovies.Count()}");
            Console.WriteLine($"Odd Release Count: {oddMovies.Count()}");
        }

        //Which year contains the most titles? What is the quantity?

        public static void Query4()
        {
            //var releaseYears = from movie in movies
            //                   group movie by movie.Year into yearGroup
            //                   select yearGroup;
            var releaseYears = movies.GroupBy(movie => movie.Year);
            var bestRelease = releaseYears.Aggregate((g1, g2) => g1.Count() > g2.Count() ? g1 : g2);
            Console.WriteLine($"{bestRelease.Key} had the most releases in this collection.");
            Console.WriteLine($"{bestRelease.Count()} movies were released in {bestRelease.Key} in this collection.");
            Console.WriteLine($"{bestRelease.Key} with {bestRelease.Count()}");
        }

        //Which rating shows up the most? What is the quantity?

        public static void Query5()
        {
            //var ratingsByMovies = from movie in movies
            //                      group movie by movie.Rating into rateGroup
            //                      select rateGroup;
            var ratingsByMovies = movies.GroupBy(movie => movie.Rating);
            var bestRating = ratingsByMovies.Aggregate((m1, m2) => m1.Count() > m2.Count() ? m1 : m2);
            Console.WriteLine($"{bestRating.Key} is the most popular rating in this collection.");
            Console.WriteLine($"{bestRating.Count()} movies are rated {bestRating.Key} in this collection.");
            Console.WriteLine($"{bestRating.Count()} {bestRating.Key} movies");

        }

        //Which title is the shortest(in character length)? Which is the longest? Your answer should be the titles themselves(but no other data)

        public static void Query6()
        {
            var longestestMovie = movies.Aggregate((m1, m2) => m1.Title.Length > m2.Title.Length ? m1 : m2);
            Console.WriteLine($"The longest movie title in the collection is {longestestMovie.Title}.");
            var shortestMovie = movies.Aggregate((m1, m2) => m1.Title.Length < m2.Title.Length ? m1 : m2);
            Console.WriteLine($"The shortest movie title in the collection is {shortestMovie.Title}.");
        }

        //Which rating has the widest range in years?

        public static void Query7()
        {
            var ratings = movies.GroupBy(movie => movie.Rating);
            int highestCount = 0;
            MPAARating popularRating = MPAARating.Unknown;
            var newestYear = 0;
            var oldestYear = 0;
            foreach( var rating in ratings)
            {
                var newestMovie = rating.Aggregate((m1, m2) => m1.Year > m2.Year ? m1 : m2);
                var oldestMovie = rating.Aggregate((m1, m2) => m1.Year < m2.Year ? m1 : m2);
                int range = newestMovie.Year - oldestMovie.Year;
                if(range > highestCount)
                {
                    highestCount = range;
                    popularRating = rating.Key;
                    newestYear = newestMovie.Year;
                    oldestYear = oldestMovie.Year;
                }
            }
            Console.WriteLine($"Rating '{popularRating}' has the widest range of movies.");
            Console.WriteLine($"{popularRating}: {oldestYear}-{newestYear}");
        }

        //How many films are in each decade of the total year range?

        public static void Query8()
        {
            //var newestMovie = movies.Aggregate((m1, m2) => m1.Year > m2.Year ? m1 : m2);
            //var oldestMovie = movies.Aggregate((m1, m2) => m1.Year < m2.Year ? m1 : m2);
            //int year = (oldestMovie.Year / 10) * 10;
            //int oldYear = year+1;
            var decadeMovies = movies.GroupBy(movies => (movies.Year / 10) * 10);
            decadeMovies = decadeMovies.OrderBy(decade => decade.Key);
            foreach(var decade in decadeMovies)
            {
                Console.WriteLine($"{decade.Key}-{decade.Key + 9} has {decade.Count()} movies.");
            }
            //do
            //{
            //    year += 10;
            //    var decadeMovie = movies.Where(movie => movie.Year <= year && movie.Year >= oldYear);
            //    Console.WriteLine($"From {oldYear}-{year} has {decadeMovie.Count()} movies.");
            //    oldYear = year+1;
            //} while (year < newestMovie.Year);
        }

        //Which ratings in the MPAARating enum are never used?

        public static void Query9()
        {
            //var ratingsByMovies = from movie in movies
            //                      group movie by movie.Rating into rateGroup
            //                      select rateGroup;
            var ratingsByMovies = movies.GroupBy(movie => movie.Rating);
            List<MPAARating> ratings = Enum.GetValues(typeof(MPAARating)).Cast<MPAARating>().ToList<MPAARating>();
            
            foreach (var rating in ratingsByMovies)
            {
                if (ratings.Contains(rating.Key))
                {
                    ratings.Remove(rating.Key);
                }
            }
            Console.Write($"This collection has no movies with these ratings: ");
            ratings.Print();

            //ratings.Add(MPAARating.G);
            //ratings.Add(MPAARating.NC17);
            //ratings.Add(MPAARating.NotRated);
            //ratings.Add(MPAARating.PG);
            //ratings.Add(MPAARating.PG13);
            //ratings.Add(MPAARating.R);
            //ratings.Add(MPAARating.Unknown);
            //ratings.Add(MPAARating.Unrated);
        }

        //Display all films, first by rating(lowest to highest enum value), then by title(alphabetically ascending).

        public static void Query10()
        {
            var ratings = movies.GroupBy(movie => movie.Rating);
            ratings = ratings.OrderBy(movie => movie.Key);
            foreach(var rating in ratings)
            {
                var titles = rating.OrderByDescending(movie => movie.Title);
                foreach(var movie in titles)
                {
                    Console.WriteLine(movie);
                }
            }
        }

        //Group the movies by number of words in the title.List the results by group (meaning the number of words in the title) and the number of films in that group.Clearly label your results, please.

        public static void Query11()
        {
            //var groupTitleLength = from movie in movies
            //                       group movie by movie.Title.Length into groupLength
            //                      select groupLength;
            var groupTitleLength = movies.GroupBy(movie => movie.Title.Split(' ').Count());
            groupTitleLength = groupTitleLength.OrderBy(movie => movie.Key);
            foreach(var length in groupTitleLength)
            {
                Console.WriteLine($"{length.Key} word long titles has {length.Count()} movies.");
            }
        }

        //My teenager is 15 and may watch any film that is rated G, PG, or PG-13. How many films from the master list could they watch? Which films are they?

        public static void Query12()
        {
            
            var kidsMovies = movies.Where(movie => movie.Rating == MPAARating.PG13 || movie.Rating == MPAARating.PG || movie.Rating == MPAARating.G ).Select(m => m.Title);
            Console.WriteLine($"There are {kidsMovies.Count()} movies that are non-adult friendly.");
            Console.Write("These films are: ");
            kidsMovies.Print();
            //StringBuilder build = new StringBuilder();
            //foreach(var movie in kidsMovies)
            //{
            //    build.Append($"{movie.Title}, ");
            //}
            //build.Length -= 2;
            //Console.WriteLine($"These films are: {build}");

        }

    }
}
