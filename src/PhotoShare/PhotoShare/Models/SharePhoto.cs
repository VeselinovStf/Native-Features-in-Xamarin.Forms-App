using System.IO;

namespace PhotoShare.Services.Models
{
    public class SharePhoto
    {
        public string ImageName { get; set; }

        public Stream ImageData { get; set; }
    }
}