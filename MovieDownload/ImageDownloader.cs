using System;
using System.Collections.Generic;
using System.Text;
using MovieSearch.Models;

namespace MovieDownload
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class ImageDownloader
    {
        private IImageStorage _imageStorage;

        public ImageDownloader(IImageStorage imageStorage)
        {
            this._imageStorage = imageStorage;
        }

        public string LocalPathForFilename(string remoteFilePath)
        {
            if (remoteFilePath == null)
            {
                return string.Empty;
            }

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string localPath = Path.Combine(documentsPath, remoteFilePath.TrimStart('/'));
            return localPath;
        }

        public async Task DownloadImage(string remoteFilePath, string localFilePath, CancellationToken token)
        {
            if(remoteFilePath == string.Empty || localFilePath == string.Empty){
                return;
            }
            var fileStream = new FileStream(
                                 localFilePath,
                                 FileMode.Create,
                                 FileAccess.Write,
                                 FileShare.None,
                                 short.MaxValue,
                                 true);
            try
            {
                await this._imageStorage.DownloadAsync(remoteFilePath, fileStream, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task DownloadImagesInList(List<MovieListViewModel> movies)
        {
            foreach (var movie in movies)
            {
                movie.LocalImageUrl = LocalPathForFilename(movie.RemoteImageUrl);
                if (movie.LocalImageUrl != string.Empty && !File.Exists(movie.LocalImageUrl))
                {
                    await this.DownloadImage(movie.RemoteImageUrl, movie.LocalImageUrl, new CancellationToken());
                }
            }
        }
    }
}
