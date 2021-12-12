using System;
using System.IO;
using System.Threading.Tasks;

namespace WIS.Interfaces
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
