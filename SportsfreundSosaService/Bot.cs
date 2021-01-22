using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Serilog;
using Tweetinvi.Exceptions;

namespace SportsfreundSosaService
{
    public static class Bot
    {
        private static string CONSUMER_KEY = "s5zDLQeYsyPSTuLJnfPl668LM";
        private static string CONSUMER_SECRET = "katCNpDFT4244x2cXb10RxUJSjSMPOGG2wBFBMVjXqzjyqaTiG";
        private static string ACCESS_TOKEN = "1349037503114858498-mOHbb2YZpi2AeBGVOMBM138nYXhOSo";
        private static string ACCESS_TOKEN_SECRET = "tU0qbRrZoBTzgfAiZDwMIW9KmEhVpXB18Gbw7J6TBflFz";

        private static TwitterClient userClient;

        static Bot()
        {
            userClient = new TwitterClient(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            Log.Information("Twitter Client erstellt");
        }


        public static async Task Say(string text)
        {
            await userClient.Tweets.PublishTweetAsync(text);
            Log.Information($"Published: {text}");
        }

        public static async Task Retweet()
        {
            try
            {
                var tweets = (await userClient.Search.SearchTweetsAsync("Sportsfreund Sosa"))
                    .OrderByDescending(tweet => tweet.CreatedAt)
                    .ToList();

                foreach (var tweet in tweets)
                {
                    await userClient.Tweets.PublishRetweetAsync(tweet.Id);
                    Log.Information($"Retweeted: {{ {tweet.FullText} }}");
                }
            }
            catch (TwitterException ex)
            {
                Log.Error(ex.Message);
                return;
            }
        }          
    }
}
