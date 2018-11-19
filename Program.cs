using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CSharpAsync.models;

namespace CSharpAsync {
  class Program {
    static void Main (string[] args) {

      Console.WriteLine ();
      Console.WriteLine ("Simple Async Demo App");
      Console.WriteLine ();
      Console.WriteLine ("Press 1 for Normal Execute");
      Console.WriteLine ("Press 2 for Async Execute");

      ConsoleKey key;
      do {
        // Key is available - read it
        key = Console.ReadKey (true).Key;

        if (key == ConsoleKey.D1) {
          NormalExecute ();
        } else if (key == ConsoleKey.D2) {
          AsyncExecute ();
        }

      } while (key != ConsoleKey.Escape);

      Console.WriteLine ();

    }

    private static void NormalExecute () {
      var watch = System.Diagnostics.Stopwatch.StartNew ();

      RunDownloadSync ();

      watch.Stop ();
      var elapsedMs = watch.ElapsedMilliseconds;

      Console.WriteLine ($"Total Normal execution time: { elapsedMs }");
    }

    private static async void AsyncExecute () {
      var watch = System.Diagnostics.Stopwatch.StartNew ();

      await RunDownloadParallelAsync ();

      watch.Stop ();
      var elapsedMs = watch.ElapsedMilliseconds;

      Console.WriteLine ($"Total Async execution time: { elapsedMs }");
    }

    private static void RunDownloadSync () {
      List<string> websites = PrepData ();

      foreach (string site in websites) {
        WebsiteDataModel results = DownloadWebsite (site);
        ReportWebsiteInfo (results);
      }
    }

    private static async Task RunDownloadAsync () {
      List<string> websites = PrepData ();

      foreach (string site in websites) {
        WebsiteDataModel results = await Task.Run (() => DownloadWebsite (site));
        ReportWebsiteInfo (results);
      }
    }

    private static async Task RunDownloadParallelAsync () {
      List<string> websites = PrepData ();
      List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>> ();

      foreach (string site in websites) {
        //tasks.Add (Task.Run (() => DownloadWebsite (site)));
        tasks.Add (DownloadWebsiteAsync (site));
      }

      var results = await Task.WhenAll (tasks);

      foreach (var item in results) {
        ReportWebsiteInfo (item);
      }
    }

    private static WebsiteDataModel DownloadWebsite (string websiteURL) {
      WebsiteDataModel output = new WebsiteDataModel ();
      WebClient client = new WebClient ();

      output.WebsiteUrl = websiteURL;
      output.WebsiteData = client.DownloadString (websiteURL);

      return output;
    }

    private static async Task<WebsiteDataModel> DownloadWebsiteAsync (string websiteURL) {
      WebsiteDataModel output = new WebsiteDataModel ();
      WebClient client = new WebClient ();

      output.WebsiteUrl = websiteURL;
      output.WebsiteData = await client.DownloadStringTaskAsync (websiteURL);

      return output;
    }

    private static void ReportWebsiteInfo (WebsiteDataModel data) {
      Console.WriteLine ($"{ data.WebsiteUrl } downloaded: { data.WebsiteData.Length } characters long.{ Environment.NewLine }");
    }

    private static List<string> PrepData () {
      List<string> output = new List<string> ();

      Console.WriteLine ();

      output.Add ("https://www.yahoo.com");
      output.Add ("https://www.google.com");
      output.Add ("https://www.microsoft.com");
      output.Add ("https://www.cnn.com");
      output.Add ("https://www.codeproject.com");
      output.Add ("https://www.stackoverflow.com");

      return output;
    }

  }
}
