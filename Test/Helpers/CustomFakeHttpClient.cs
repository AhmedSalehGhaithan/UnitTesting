using FakeItEasy;
using System.Text.Json;

namespace Test.Helpers
{
    //his line declares a static class named CustomFakeHttpClient.Static classes cannot be instantiated and usually contain static methods that provide utility functions.
    public static class CustomFakeHttpClient
    {
        //This line declares a static method FakeHttpClient that takes a generic parameter T representing the content to be serialized into the HTTP response body. It returns an HttpClient instance.
        public static HttpClient FakeHttpClient<T>(T content)
        {
            //This line creates a new instance of HttpResponseMessage and sets its Content property to a StringContent object. The StringContent is created by serializing the content parameter to JSON format using JsonSerializer.Serialize.
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(content))
            };

            //This line uses FakeItEasy to create a fake instance of FakeableHttpMessageHandler. FakeItEasy is a library for creating fake objects for unit testing.
            var handler = A.Fake<FakeableHttpMessageHandler>();
            //This line sets up a fake call to handler.FakeSendAsync using FakeItEasy. It specifies that whenever FakeSendAsync is called with any HttpRequestMessage and CancellationToken , it should return the response object created earlier.
            A.CallTo(() => handler.FakeSendAsync(
                A<HttpRequestMessage>.Ignored,A<CancellationToken>.Ignored)).Returns(response);
            return new HttpClient(handler);
        }
        
    }
}
