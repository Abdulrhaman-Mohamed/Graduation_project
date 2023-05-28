using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;

namespace Repo_EF.RepoEFUtil
{
    public class ImageUploader
    {
        private static ImageUploader instance = null;
        private ImageUploader() { }

        public static ImageUploader getInstance()
        {
            if (instance == null)
                instance = new ImageUploader();

            return instance;
        }

        public async Task<string> UploadImage(string path, IFormFile file)
        {
            var storage = StorageClient.Create();
            var bucketName = $"gs://space-rover-941b5.appspot.com/{path}";
            var image = await storage.UploadObjectAsync(
                bucketName, file.FileName, file.ContentType, file.OpenReadStream()
                );
            return image.MediaLink;
        }

    }
}
