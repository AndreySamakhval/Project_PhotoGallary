using PhotoGallary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallary.Services
{
     public interface IPhotoService
    {
        List<PhotoViewModel> GetPhotos();
        List<PhotoViewModel> GetPhotos(int id);
        PhotoViewModel GetPhoto(int id);

        List<GenresViewModel> GetGenres();
        
    }
}
