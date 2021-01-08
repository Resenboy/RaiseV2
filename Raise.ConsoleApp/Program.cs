using Raise.Model.Models;
using Raise.Services;
using Raise.Utils;
using System;

namespace RaiseConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var userClient = new UserClient();

                var userInfo1 = new User() { Email = "k@k.com", IsFacebookUser = false, AcceptedTerms = true, Password = PasswordGenerate.Encryption("321") };
                userInfo1.GuidKey = GuidGenerate.USER_ID;
                var apiResponse = userClient.Login(userInfo1);
                if (!apiResponse.IsSuccess)
                    return;

                // user insert
                var userInfo = new User() { Email = "teste@gmail.com", IsFacebookUser = false, AcceptedTerms = true, Name = "alvin", Password = PasswordGenerate.Encryption("123456") };
                apiResponse = userClient.Add(userInfo);
                if (!apiResponse.IsSuccess)
                    return;

                // teste get
                apiResponse = userClient.GetByObj(userInfo);
                var userGet = apiResponse.Data;

                userGet.Name = "Alvin Rezende";
                apiResponse = userClient.Update(userGet);

                // account add
                var accountClient = new AccountClient();
                var accountInfo = new Account() { User = userInfo, Birthday = DateTime.Now, City = "Patrocínio", GamerTag = "@rezenboy", State = "SP", StartingDate = DateTime.Now };
                var apiResponseAccount = accountClient.Add(accountInfo);
                if (!apiResponseAccount.IsSuccess)
                    return;

                // teste update
                if (apiResponseAccount.IsSuccess)
                {
                    accountInfo.FavorityGame = Raise.Enums.Games.COD;
                    apiResponseAccount = accountClient.Update(accountInfo);
                }

                // teste get feed
                //if (apiResponseAccount.IsSuccess)
                //{
                //    var feedClient = new FeedClient();
                //    var feedInfo = new Feed() { GuidKey = GuidGenerate.USER_ID };
                //    var lst = feedClient.GetByObj(feedInfo);
                //}

                apiResponse = userClient.GetAll();
            }
            catch (Exception exc)
            {
                
            }
        }
    }
}
