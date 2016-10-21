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
        //get string[] genres of Photo
        public string[] GetGenresPhoto(int id)
        {
            string[] result;
            using(var DB = new PhotoGallaryEntities())
            {
                var genres = DB.Photos.First(x => x.Id == id).Genres;
                result = new string[genres.Count];
                int i = 0;
                foreach (var item in genres)
                {
                    result[i] = item.Name;
                    i++;
                }
            }
            return result;

        }
        //get one photo for id
        public PhotoViewModel GetPhoto(int id = 1)
        {
            var photo = new PhotoViewModel();

            using (var DB = new PhotoGallaryEntities())
            {                
                photo = DB.Photos.Where(x => x.Id == id).Select(p => new PhotoViewModel {
                    Id = p.Id,
                    Name = p.Name,
                    Url = p.Url,
                    Description = p.Description,
                    DateAdded = p.DateAdded.ToString()
                }).Single();                
            }
            photo.GenrePhoto = GetGenresPhoto(id);
            return photo;
        }

        // get all photo
        public List<PhotoViewModel> GetPhotos()
        {
            var photos = new List<PhotoViewModel>();

            using (var DB = new PhotoGallaryEntities())
            {
                photos = DB.Photos.Select(p => new PhotoViewModel {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Url = p.Url, DateAdded = p.DateAdded.ToString()
                }).ToList();
            }

            foreach(var item in photos)
            {
                item.GenrePhoto = GetGenresPhoto(item.Id);
            }
            return photos;
        }

        //get photos of genres
        public List<PhotoViewModel> GetPhotos(int id)
        {
            var photos = new List<PhotoViewModel>();
            using (var DB = new PhotoGallaryEntities())
            {
                photos = DB.Genres.First(x => x.Id == id).Photos.Select(p => new PhotoViewModel {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name, Url = p.Url,
                    DateAdded = p.DateAdded.ToString()
                }).ToList();
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
                    Url = p.Url,
                    DateAdded = p.DateAdded.ToString()
                }).ToList();
            }

            return photos;
        }

        //get all genres
        public List<GenresViewModel> GetGenres()
        {
            var _genres = new List<GenresViewModel>();

            using (var DB = new PhotoGallaryEntities())
            {
                _genres = DB.Genres.Select(g => new GenresViewModel {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description
                }).ToList();
            }
            return _genres;
        }

        // get one genre
        public GenresViewModel GetGenre(int id)
        {          
            GenresViewModel genre;

            using(var DB = new PhotoGallaryEntities())
            {
                var res = DB.Genres.First(x => x.Id == id);
                genre = new GenresViewModel {
                    Id = res.Id,
                    Description = res.Description,
                    Name = res.Name };
            }
            return genre;
        }

        //Add new genre
        public int AddGenre(string Name)
        {
            int result = 0;
            bool exist = false;
            using (var DB = new PhotoGallaryEntities())
            {
                var genres = DB.Genres.ToList();
                foreach(var item in genres)
                {
                    if (item.Name == Name)
                        exist = true;                 
                }
                if (!exist)
                {
                    var newGenre = DB.Genres.Add(new Genre { Name = Name });
                    DB.SaveChanges();
                    result = newGenre.Id;
                }
                return result;
            }
        }

        //Remove Genre for id
        public bool RemoveGenre(int id)
        {
            bool result = false;

            using (var DB = new PhotoGallaryEntities())
            {
                //переместить все фотографии жанра
                var genre = DB.Genres.First(x => x.Id == id);
                DB.Genres.Remove(genre);
                DB.SaveChanges();
                result = true;
            }

            return result;
        }


    }
}
