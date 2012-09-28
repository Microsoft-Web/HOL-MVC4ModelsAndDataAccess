namespace MvcMusicStore.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using MvcMusicStore.Models;

    public class StoreController : Controller
    {
        // GET: /Store/
        public ActionResult Details(int id)
        {
            var album = new Album
            {
                Title = "Sample Album",
                Genre = new Genre { Name = "Sample Genre" },
                Artist = new Artist { Name = "Sample Artist" },
                AlbumArtUrl = "~/Content/Images/placeholder.gif",
                Price = 9.99M
            };

            if (album == null)
            {
                return this.HttpNotFound();
            }

            return this.View(album);
        }

        public ActionResult Browse(string genre)
        {
            // Retrieve Genre and its Associated Albums from database
            var genreModel = new Genre
            {
                Name = genre,
                Albums = new List<Album>
                {
                    new Album { Title = "Album 1" },
                    new Album { Title = "Album 2" },
                    new Album { Title = "Album 3" }
                }
            };

            return this.View(genreModel);
        }

        public ActionResult Index()
        {
            var genres = new List<Genre>
            {
                new Genre { Name = "Rock" },
                new Genre { Name = "Jazz" },
                new Genre { Name = "Metal" }
            };

            return this.View(genres);
        }

        // GET: /Store/GenreMenu
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = new List<Genre>
            {
                new Genre { Name = "Rock" },
                new Genre { Name = "Jazz" },
                new Genre { Name = "Metal" }
            };

            return this.PartialView(genres);
        }
    }
}
