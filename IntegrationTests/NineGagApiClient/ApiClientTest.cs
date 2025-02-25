﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NineGagApiClient;
using NineGagApiClient.Models;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTest.My9GAG.NineGagApiClient
{
    [TestClass]
    public class ApiClientTest : TestBase
    {
        [TestMethod]
        public async Task GetPostsAsync_HappyFlow_10Posts()
        {
            using var apiClient = new ApiClient();
            await apiClient.LoginWithCredentialsAsync(Settings.UserName, Settings.UserPassword);

            //Act
            var posts = await apiClient.GetPostsAsync(PostCategory.Hot, 10);

            //Assert
            Assert.IsNotNull(posts);
            Assert.AreEqual(10, posts.Count());
            Assert.AreNotEqual(string.Empty, posts.First().Title);
        }

        [TestMethod]
        public async Task GetPostsAsync_PostsSince_OnlyPostsSince()
        {
            using var apiClient = new ApiClient();
            await apiClient.LoginWithCredentialsAsync(Settings.UserName, Settings.UserPassword);
            var top10posts = await apiClient.GetPostsAsync(PostCategory.Hot, 10);
            var post5 = top10posts[4];
            var post6 = top10posts[5];

            //Act
            var postsSince = await apiClient.GetPostsAsync(PostCategory.Hot, 10,olderThanPostId: post5.Id);

            //Assert
            Assert.IsNotNull(postsSince);
            Assert.AreEqual(10, postsSince.Count());
            Assert.AreNotEqual(string.Empty, postsSince.First().Title);
            Assert.AreEqual(post6.Id, postsSince[0].Id);
        }
    }
}
