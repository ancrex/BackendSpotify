using System.Net;
using BackendSpotify.Core.Application.Configuration.Interfaces;
using BackendSpotify.Core.Domain.Exceptions;
using BackendSpotify.Core.Domain.Models.Token;
using BackendSpotify.Infraestructure.Services.Identity;
using Moq;
using Moq.Protected;

namespace Infraestructure.Test.Services.Identity
{
    public class AccessTokenGeneratorTest
    {
        private readonly Mock<HttpMessageHandler> mockMessageHandler = new();
        private readonly Mock<ISpotifyConfigService> spotifyConfigService = new();

        private readonly string tokenEndpoint = "http://spotify.count.com";
        private readonly string scopes = "scope1";
        private readonly string clientId = "1";
        private readonly string redirectUri = "http://callback";

        public AccessTokenGeneratorTest() {

            spotifyConfigService.Setup(a => a.TokenEndpoint).Returns(tokenEndpoint);
            spotifyConfigService.Setup(a => a.Scopes).Returns(scopes);
            spotifyConfigService.Setup(a => a.ClientId).Returns(clientId);
            spotifyConfigService.Setup(a => a.RedirectUri).Returns(redirectUri);
        }

        [Fact]
        public async Task WhenGenerateAccessToken_ApiCall_Is_Success_Response_Then_Return_Token()
        {

            const string code = "code";
            const string token = "BQDBKJ5eo5jxbtpWjVOj7ryS84khybFpP_lTqzV7uV-T_m0cTfwvdn5BnBSKPxKgEb11";
            const string tokenType = "Bearer";
            const string scope = "user-read-private user-read-email";
            const int expiresIn = 3600;
            const string refreshToken = "NgAagA...Um_SHo";



            TokenResponse tokenResponseExpected = new TokenResponse()
            {
                AccessToken = token,
                TokenType = tokenType,
                Scope = scope,
                ExpiresIn = expiresIn,
                RefreshToken = refreshToken
            };

            string expectedMessage = $@"{{
                   'access_token': '{token}',
                   'token_type': '{tokenType}',
                   'scope': '{scope}',
                   'expires_in': {expiresIn},
                   'refresh_token': '{refreshToken}'                   
                }}";

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedMessage)
                });


            AccessTokenGenerator accessTokenGenerator = new AccessTokenGenerator(new HttpClient(mockMessageHandler.Object),
                spotifyConfigService.Object);




            TokenResponse tokenResponse = await accessTokenGenerator.GenerateAccessToken(code);



            Assert.Equal(tokenResponseExpected.AccessToken, tokenResponse.AccessToken);
            Assert.Equal(tokenResponseExpected.TokenType, tokenResponse.TokenType);
            Assert.Equal(tokenResponseExpected.Scope, tokenResponse.Scope);
            Assert.Equal(tokenResponseExpected.ExpiresIn, tokenResponse.ExpiresIn);
            Assert.Equal(tokenResponseExpected.RefreshToken, tokenResponse.RefreshToken);
        }



        [Fact]
        public async Task WhenGenerateAccessToken_ApiCall_Is_Not_Success_Response_Then_ApiException()
        {   

            const string code = "code";
            const string expectedMessage = "Failed getting Token from Spotify";

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(String.Empty)
                });


            AccessTokenGenerator accessTokenGenerator = new AccessTokenGenerator(new HttpClient(mockMessageHandler.Object),
                spotifyConfigService.Object);

            


            ApiException exception = await Assert.ThrowsAsync<ApiException>(() =>
              accessTokenGenerator.GenerateAccessToken(code)
            );



            Assert.Equal(expectedMessage, exception.Message);
            Assert.Equal(HttpStatusCode.FailedDependency, exception.StatusCode);
        }
    }
}

