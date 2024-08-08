namespace Test.Helpers
{
    public abstract class FakeableHttpMessageHandler : HttpMessageHandler
    {
        //Defines an abstract method that needs to be implemented by derived classes.This method is used to handle HTTP requests.

        //This defines an abstract method FakeSendAsync that must be implemented by any non-abstract subclass. This method is intended to handle HTTP requests asynchronously and return an HttpResponseMessage.
        public abstract Task<HttpResponseMessage> FakeSendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken);

        // sealed so FakeItEasy won't intercept calls to this method

        //Seals the SendAsync method to prevent FakeItEasy from intercepting calls to it.Calls FakeSendAsync to handle the actual request.
        //This overrides the SendAsync method from HttpMessageHandler and seals it, meaning no further overriding is allowed in subclasses. It calls the FakeSendAsync method to handle the actual HTTP request.
        protected sealed override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
            => this.FakeSendAsync(request, cancellationToken);
    }
}
