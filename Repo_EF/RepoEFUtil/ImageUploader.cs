using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;

namespace Repo_EF.RepoEFUtil
{
    public class ImageUploader
    {
        private readonly GoogleCredential _credential;
        private readonly StorageClient _storageClient;
        private static ImageUploader? _instance;
        private const string directory = "/upload/images/";

        private ImageUploader()
        {
            _credential = GoogleCredential.FromFile("space-rover-941b5-firebase-adminsdk-weh4t-81170f5526.json");
            _storageClient = StorageClient.Create(_credential);
        }

        public static ImageUploader GetInstance()
        {
            return _instance ??= new ImageUploader();
        }

        public async Task<string> UploadImage(string path, IFormFile file)
        {
            // Upload an image file to the bucket.
            var destination = directory + path + "/" + file.FileName;
            const string bucketName = "space-rover-941b5.appspot.com";

            await _storageClient.UploadObjectAsync(
                bucketName, destination, file.ContentType, file.OpenReadStream()
            );

            // Make the object publicly readable for download via HTTP GET Request only for 7 days.
            var urlSigner = UrlSigner.FromCredential(_credential);
            var signedUrl = await urlSigner.SignAsync(
                bucketName, destination, TimeSpan.FromDays(7), HttpMethod.Get
                );

            return signedUrl;
        }

    }
}
