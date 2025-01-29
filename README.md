# bsky.net
.Net library for interacting with the BlueSky API
Inspired by from  [bluesky-net](https://github.com/dariogriffo/bluesky-net) . 


# About the target framework

To simplify the development, first versions will target only NET8.0+

Eventually the library will support NET standard 2.0

# About the results

The api implements a basic discriminated union pattern for all the results.

The first option of the result is what you expect, the second an error if the HTTP status code is > 2xx

# How to use it

1- Install the nuget package

`Install-Package Bsky.Net`


2- Add the SDK to your services collection

```csharp
services.AddBluesky();
```

3- The SDK can be configured

```csharp
services
    .AddBluesky(options => {
        options.TrackSession = false;
    });
```

4- Inject the IBlueskyApi in your code

```csharp
public class MyClass
{
    private readonly IBlueskyApi _bluesky;
    public MyClass(IBlueskyApi bluesky)
    {
        _bluesky = bluesky;
    } 
}
```

5 - Call the api
```csharp
Login command = new("YOUR_HANDLE", "YOUR_PASSWORD");
Multiple<Session, Error> result = await _bluesky.Login(command, CancellationToken.None);

result
    .Switch(
        session => Console.WriteLine(session.Email),
        error => Console.WriteLine(JsonSerializer.Serialize(error)
    );    
```
6 - Create a post automatically resolving mentions and links
```csharp

Multiple<Did, Error> resolvedHandle = await api.ResolveHandle(session.Handle, CancellationToken.None);
        
string text =
        @"Link to Google https://google.com  This post is created with Bsky.Net. A library to interact with Bluesky. A mention to @myself and an emoji '🌅'";
    CreatePost post = new( text);

    //Create a post
    Multiple<CreatePostResponse, Error> created = await api.CreatePost(post, CancellationToken.None);    
```

7 - Create a post with images automatically resolving mentions and links and uploading images and remove the metadata form the image(s) max 4 images
```csharp

Multiple<Did, Error> resolvedHandle = await api.ResolveHandle(session.Handle, CancellationToken.None);
        
string text =
        @"Link to Google https://google.com  This post is created with Bsky.Net. A library to interact with Bluesky. A mention to @myself and an emoji '🌅'";
(string, string?)[] images = new (string, string?)[] {($"data:image/jpeg;base64,{base64String}", "Wijkertoren - Beverwijk") };
         CreatePost post = new(message, images);

    //Create a post
    Multiple<CreatePostResponse, Error> created = await api.CreatePost(post, CancellationToken.None);    
```
# Retries

You can easily plug retries with [Polly](https://github.com/App-vNext/Polly) when registering the api.
Install the nuget package

`Install-Package Microsoft.Extensions.Http.Polly`

Configure your retries:
```csharp
services
  .AddBluesky()
  .AddPolicyHandler(
      HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,retryAttempt))));
```
