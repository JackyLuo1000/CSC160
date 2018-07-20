using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateDemo_A
{
    public delegate void PrintDelegate(string input);

    public static class NewsPublisher
    {
        public static PrintDelegate newsDel;

        private static string currentHeadLine = "Nothing to report.";

        public static void UpdateHeadline(string headline)
        {
            currentHeadLine = headline;
            if(newsDel != null)
            {
                newsDel.Invoke(currentHeadLine);
            }
        }
    }

    public static class TVAnchor
    {
        public static void ReportTheNews(string news)
        {
            Console.WriteLine($"This just in: {news}");
        }
    }

    public static class Blogger
    {
        public static void HashtagPost(string statePropaganda)
        {
            Console.WriteLine($"The Establishment just said, \"{statePropaganda}\", but that's not the ENTIRE truth, is it.");
        }
    }

    public static class Burnout
    {
        public static void HearStuff(string stuff)
        {
            Console.WriteLine($"Dude, did you just say soomething?");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            //Subscribe our listeners to the delegate
            NewsPublisher.newsDel += TVAnchor.ReportTheNews;
            NewsPublisher.newsDel += Blogger.HashtagPost;
            NewsPublisher.newsDel += Burnout.HearStuff;
            NewsPublisher.newsDel += TVAnchor.ReportTheNews;

            //Unsubscribe
            NewsPublisher.newsDel -= Burnout.HearStuff;

            string news = "Playstation Nation is now a sovereign country. XBots swears to invade.";
            NewsPublisher.UpdateHeadline(news);
        }
    }
}
