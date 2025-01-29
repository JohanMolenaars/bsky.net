using Bsky.Net.Helper.Commands.Bsky.Feed;
using Bsky.Net.Helper.Img;
using Bsky.Net.Model;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static Bsky.Net.Helper.Constants;

namespace Bsky.Net.Helper.AtProto
{
    internal class Repo
    {
        private readonly HttpClient _client;

        public Repo(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Creates a new record in the repository.
        /// </summary>
        /// <param name="record">The record to be created.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response of the create post operation.</returns>
        public Task<Result<CreatePostResponse>> Create(CreateRecord record, CancellationToken cancellationToken)
        {
            return
                _client
                    .Post<CreateRecord, CreatePostResponse>(
                        Constants.Urls.AtProtoRepo.CreateRecord, record,
                        cancellationToken);
        }


        /// <summary>
        /// Creates a new record in the repository.
        /// </summary>
        /// <param name="record">The record to be created.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response of the create post operation.</returns>
        public Task<Result<CreatePostResponse>> CreatePostExternal(CreateRecord record, CancellationToken cancellationToken)
        {
            return
                _client
                    .Post<CreateRecord, CreatePostResponse>(
                        Constants.Urls.AtProtoRepo.CreateRecord, record,
                        cancellationToken);
        }

        /// <summary>
        /// Uploads an image to the server.
        /// </summary>
        /// <param name="image">The image data in Data URI format.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response of the upload operation.</returns>
        public Task<Result<UploadBlobResponse>> Upload(string image, CancellationToken cancellationToken)
        {
            BlobConverter blob = new BlobConverter();
            byte[] binaryData = Meteadata.Remove(blob.ConvertDataURIToByteArray(image));
            var content = new ByteArrayContent(binaryData);           
            return
                _client
                    .Post<ByteArrayContent, UploadBlobResponse>(
                        Constants.Urls.AtProtoRepo.UploadBlob, content,
                        cancellationToken);
        }


        /// <summary>
        /// Retrieves all follow records from a specified collection for a given DID (Decentralized Identifier).
        /// </summary>
        /// <param name="collection">The name of the collection from which to retrieve follow records.</param>
        /// <param name="did">The Decentralized Identifier (DID) of the user whose follow records are to be retrieved.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/> object with a list of follow records, or null if no records are found.</returns>
        public Task<Result<List<Record>?>> GetAllFollowRecords(string collection,Did did, CancellationToken cancellationToken)
        {
            List<Record>? records = new();
            string cursor = null;
            do
            {
                string url = $"{Urls.AtProtoRepo.listRecords}?repo={did}&collection={collection}";
                url += (cursor != null ? $"&cursor={cursor}" : "");

                var response = _client.Get<ListRecordsResponse>(url, cancellationToken).Result;
                response.Switch(response =>
                {
                    records.AddRange(response.Records);
                    cursor = response.Cursor;
                }, _ => { });

            } while (!string.IsNullOrEmpty(cursor));

            return Task.FromResult(new Result<List<Record>?>(records));
        }
    }
}
