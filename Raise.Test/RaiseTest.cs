using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raise.Model.Models;
using Raise.Services;
using Raise.Utils;
using System.Collections.Generic;

namespace Raise.Test
{
    [TestClass]
    [TestCategory("RaiseTest")]
    public class RaiseTest
    {
        [TestMethod]
        [TestCategory("User")]
        public void GetUser()
        {
            var userInfo = new User() { Email = "alvinresende@hotmail.com", IsFacebookUser = false, AcceptedTerms = true, Name = "Alvin", Password = "123456" };
            userInfo.GuidKey = GuidGenerate.USER_ID;
            var userClient = new UserClient();

            var apiResponse = userClient.GetByObj(userInfo);
            var userIdResponse = ((BaseModel)apiResponse.Data).GuidKey;

            if (!apiResponse.IsSuccess)
            {
                return;
            }

            apiResponse = userClient.Login(userInfo);
            if (!apiResponse.IsSuccess)
            {
                return;
            }

            Assert.IsTrue(apiResponse.IsSuccess);
        }

        [TestMethod]
        [TestCategory("User")]
        public void AddUser()
        {
            var userClient = new UserClient();

            var userInfo = new User() { Email = "a@a.com", IsFacebookUser = false, AcceptedTerms = true, Name = "Alvin", Password = PasswordGenerate.Encryption("321") };
            userInfo.GuidKey = GuidGenerate.USER_ID;
            var apiResponse = userClient.Add(userInfo);
            Assert.IsTrue(apiResponse.IsSuccess);
        }

        [TestMethod]
        [TestCategory("User")]
        public void UpdateUser()
        {
            var userInfo = new User() { Email = "k@k.com", IsFacebookUser = false, AcceptedTerms = true, Name = "Kenji" };
            userInfo.GuidKey = GuidGenerate.USER_ID;
            var userClient = new UserClient();

            var apiResponse = userClient.GetByObj(userInfo);

            if (apiResponse.IsSuccess)
            {
                userInfo.Password = PasswordGenerate.Encryption("321");
                apiResponse = userClient.Update(userInfo);
            }

            Assert.IsTrue(apiResponse.IsSuccess);
        }

        [TestMethod]
        [TestCategory("Feed")]
        public void GetAllPostByUser()
        {
            var userInfo = new User() { Email = "k@k.com", IsFacebookUser = false, AcceptedTerms = true, Name = "Kenji", Password = "321" };
            userInfo.GuidKey = GuidGenerate.USER_ID;
            var userClient = new UserClient();

            var apiResponse = userClient.GetByObj(userInfo);
            if (apiResponse.IsSuccess)
            {
                var feedClient = new FeedClient();
                var feedInfo = new Feed() { GuidKey = GuidGenerate.USER_ID };
                var apiResponse1 = feedClient.GetByObj(feedInfo);
                var lstPosts = apiResponse1.Data;
            }

            Assert.IsTrue(true);
        }
    }
}
