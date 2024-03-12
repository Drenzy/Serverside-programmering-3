//using Bunit;
//using Bunit.TestDoubles;
//using Microsoft.AspNetCore.Identity.Data;
//using SoftwareTest.Components.Pages;

//namespace TestProject1
//{
//    public class UnitTest1
//    {
//        [Fact]
//        public void LoginViewTest()
//        {
//            // Arrange 
//            using var ctx = new TestContext();
//            var authContext = ctx.AddTestAuthorization();
//            authContext.SetAuthorized("");

//            // Act
//            var cut = ctx.RenderComponent<Home>();

//            // Assert
//            cut.MarkupMatches("<div> You are loggined in</div> Welcome to your new app.\r\n");
//        }

//        [Fact]
//        public void NotLoggedIn() 
//        {
//            // Arrange 
//            using var ctx = new TestContext();
//            var authContext = ctx.AddTestAuthorization();

//            // Act
//            var cut = ctx.RenderComponent<Home>();

//            // Assert
//            cut.MarkupMatches("<div> You are not loggined in</div> Welcome to your new app.\r\n");

//        }

//        [Fact]
//        public void LoginCodeTest() 
//        {
//            // Arrange 
//            using var ctx = new TestContext();
//            var authContext = ctx.AddTestAuthorization();
//            authContext.SetAuthorized("");

//            // Act
//            var cut = ctx.RenderComponent<Home>();
//            var MyInstance = cut.Instance;


//            // Assert
//            Assert.True(MyInstance._isAuthenticated);
//        }

//        [Fact]
//        public void NotLoginCodeTest()
//        {
//            // Arrange 
//            using var ctx = new TestContext();
//            var authContext = ctx.AddTestAuthorization();

//            // Act
//            var cut = ctx.RenderComponent<Home>();
//            var MyInstance = cut.Instance;


//            // Assert
//            Assert.False(MyInstance._isAuthenticated);
//        }


//        [Fact]
//        public void ButtonViewTest()
//        {
//            // Arrange 
//            using var ctx = new TestContext();
//            var authContext = ctx.AddTestAuthorization();
//            authContext.SetAuthorized("");

//            // Act
//            var cut = ctx.RenderComponent<Home>();

//            // Assert
//            cut.MarkupMatches("<div>\r\n  You are loggined in</div>\r\n<button >Create File</button>\r\nWelcome to your new app.");
//        }


//        [Fact]
//        public void ButtonNotViewTest()
//        {
//            // Arrange 
//            using var ctx = new TestContext();
//            var authContext = ctx.AddTestAuthorization();

//            // Act
//            var cut = ctx.RenderComponent<Home>();
//            var MyInstance = cut.Instance;

//            // Assert
//            cut.MarkupMatches("<div> You are not loggined in</div> Welcome to your new app.");
//        }

//        [Fact]
//        public void ButtonCodeTest()
//        {
//            // Arrange 
//            using var ctx = new TestContext();
//            var authContext = ctx.AddTestAuthorization();
//            authContext.SetAuthorized("");

//            // Act
//            var cut = ctx.RenderComponent<Home>();
//            var task = cut.Instance.CreateFile();
//            var MyInstance = task.Result;


//            // Assert
//            Assert.True(MyInstance);
//        }
//    }
//}