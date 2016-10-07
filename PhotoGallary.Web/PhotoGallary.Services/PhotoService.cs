using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoGallary.ViewModel;
using PhotoGallary.DAL;
using PhotoGallary.DalDF;

namespace PhotoGallary.Services
{
    public class PhotoService : IPhotoService
    {
        public PhotoViewModel GetPhoto(int id)
        {
            throw new NotImplementedException();
        }

        public List<PhotoViewModel> GetPhotos()
        {
            var res = DB.Photos.Select(x => new PhotoViewModel { Id = x.Id, Description = x.Description, Name = x.Name, Url = x.Url }).ToList();
            return res;
        }

        public List<GenresViewModel> GetGenres()
        {
           // var _genres = new List<GenresViewModel>();

            var _genres = DB.Genres.Select(x => new GenresViewModel { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();

            return _genres;
        }

        public List<PhotoViewModel> GetPhotos(int id)
        {
            var photos = DB.Photos.Select(x => new PhotoViewModel { Id = x.Id, Description = x.Description, Name = x.Name, Url = x.Url }).ToList();

            var res = new List<PhotoViewModel>();
            int count = 0;
            foreach(var item in photos)
            {
                if (count < 3)
                    res.Add(item);
                else break;
            }
            return res;
        }


        public List<GenresViewModel> GetGenresDB()
        {
            var _genres = new List<GenresViewModel>();

            // var _genres = DB.Genres.Select(x => new GenresViewModel { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();

            using (var DB = new PhotoGallaryEntities())
            {
                _genres = DB.Genres.Select(g => new GenresViewModel { Id = g.Id, Name = g.Name, Description = g.Description }).ToList();
            }
                return _genres;
        }


    }
}
