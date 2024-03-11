namespace Test
{
    public class UnitTest1
    {
        private readonly HttpClient _client;

        public UnitTest1()
        {
            // Initialize HttpClient
            _client = new HttpClient();
        }

        public async Task LoginPage_ReturnsLoginPage()
        {
            // Arrange
            var response = await _client.GetAsync("http://localhost:5000/Account/Login"); // Change URL as per your application

            // Assert
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.Contains("<h1>Log in</h1>", body); // Assuming this is part of your login page HTML
        }

        [Fact]
        public async Task LoginPage_CanLogin()
        {
            // Arrange
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Input.Email", "user@example.com"),
                new KeyValuePair<string, string>("Input.Password", "password"),
                new KeyValuePair<string, string>("Input.RememberMe", "true"),
            });

            // Act
            var response = await _client.PostAsync("http://localhost:5000/Account/Login", content); // Change URL as per your application

            // Assert
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.Contains("User logged in.", body); // Assuming this message is displayed after successful login
        }
    }
}