using PhotoShare.Services.Models;
using System.Threading.Tasks;

namespace PhotoShare.Services
{
    public interface IPhotoPickerService
    {
        Task<SharePhoto> GetImageStreamAsync(); 
    }
}
