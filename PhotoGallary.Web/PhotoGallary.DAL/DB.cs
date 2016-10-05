using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallary.DAL
{
    public static class DB
    {
        public static List<PhotoModel> Photos = new List<PhotoModel>();
        public static List<GenreModel> Genres = new List<GenreModel>();

                static DB()
        {
            Photos.Add(new PhotoModel { Id = 1, Name = "Photo 1", Url = "/Photos/Photo1.jpg", Description = "Description photo 1" });
            Photos.Add(new PhotoModel { Id = 2, Name = "Photo 2", Url = "/Photos/Photo2.jpg", Description = "Description photo 2" });
            Photos.Add(new PhotoModel { Id = 3, Name = "Photo 3", Url = "/Photos/Photo3.jpg", Description = "Description photo 3" });
            Photos.Add(new PhotoModel { Id = 4, Name = "Photo 4", Url = "/Photos/Photo4.jpg", Description = "Description photo 4" });
            Photos.Add(new PhotoModel { Id = 5, Name = "Photo 5", Url = "/Photos/Photo5.jpg", Description = "Description photo 5" });

            Genres.Add(new GenreModel { Id = 1, Name = "Nature", Description = "Description genre Nature" });
            Genres.Add(new GenreModel { Id = 2, Name = "Auto", Description = "Description genre Auto" });
            Genres.Add(new GenreModel { Id = 3, Name = "Urbane", Description = "Description genre Urbane" });
        }

    }
}
