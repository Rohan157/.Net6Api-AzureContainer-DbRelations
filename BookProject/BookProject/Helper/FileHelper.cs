using Azure.Storage.Blobs;

namespace BookProject.Helper
{
    public static class FileHelper
    {
        public static async Task<string> UploadImage(IFormFile file)
        {
            string filename = Guid.NewGuid().ToString();
            string connectionString = @"connectionString";
            string containerName = "containerName";
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = containerClient.GetBlobClient(filename+ file.FileName);
            MemoryStream ms = new MemoryStream();
            await file.CopyToAsync(ms);
            ms.Position = 0;
            await blobClient.UploadAsync(ms);
            return blobClient.Uri.AbsoluteUri;
        }

        public static async Task<string> UploadUrl(IFormFile file)
        {
            string filename = Guid.NewGuid().ToString();
            string connectionString = @"connectionString";
            string containerName = "containerName";
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = containerClient.GetBlobClient(filename+ file.FileName);
            MemoryStream ms = new MemoryStream();
            await file.CopyToAsync(ms);
            ms.Position = 0;
            await blobClient.UploadAsync(ms);
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
