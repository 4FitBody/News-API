using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Api.Infrastructure.Services;

public class BlobContainerService
{
    private readonly BlobServiceClient blobServiceClient;
    private readonly BlobContainerClient blobContainerClient;

    public BlobContainerService(string url, string containerName)
    {
        this.blobServiceClient = new BlobServiceClient
        (
            new Uri(url),
            new DefaultAzureCredential()
        );

        this.blobContainerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
    }

    public async Task UploadAsync(Stream stream, string path)
    {
        BlobClient blobClient = this.blobContainerClient.GetBlobClient(path);

        using (Stream file = stream)
        {
            await blobClient.UploadAsync(file);
        }
    }

}
