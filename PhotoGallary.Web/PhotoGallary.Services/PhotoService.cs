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
        public PhotoViewModel GetPhoto(int id = 1)
        {
            var photo = new PhotoViewModel();

            using (var DB = new PhotoGallaryEntities())
            {
                photo = DB.Photos.Where(x => x.Id == id).Select(p => new PhotoViewModel { Id = p.Id, Name = p.Name, Url = p.Url, Description = p.Description }).Single();
            }
            return photo;
        }

        public List<PhotoViewModel> GetPhotos()
        {
            var photos = new List<PhotoViewModel>();

            using (var DB = new PhotoGallaryEntities())
            {
                photos = DB.Photos.Select(p => new PhotoViewModel { Id = p.Id, Name = p.Name, Description = p.Description, Url = p.Url }).ToList();
            }
            return photos;
        }

        //get photos of genres
        public List<PhotoViewModel> GetPhotos(int id)
        {
            var photos = new List<PhotoViewModel>();
            using (var DB = new PhotoGallaryEntities())
            {
                photos = DB.Genres.First(x => x.Id == id).Photos.Select(p => new PhotoViewModel { Id = p.Id, Description = p.Description, Name = p.Name, Url = p.Url }).ToList();
            }

            return photos;
        }

        //get last 5 photo of genre

        public List<PhotoViewModel> GetLastPhotos(int id)
        {
            var photos = new List<PhotoViewModel>();

            using (var DB = new PhotoGallaryEntities())
            {
                photos = DB.Genres.First(x => x.Id == id).Photos.OrderByDescending(x => x.DateAdded).Take(5).Select(p => new PhotoViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Url = p.Url
                }).ToList();
            }

            return photos;
        }


        public List<GenresViewModel> GetGenres()
        {
            var _genres = new List<GenresViewModel>();

            using (var DB = new PhotoGallaryEntities())
            {
                _genres = DB.Genres.Select(g => new GenresViewModel { Id = g.Id, Name = g.Name, Description = g.Description }).ToList();
            }
            return _genres;
        }


    }
}
