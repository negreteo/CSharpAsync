using System;
using System.Collections.Generic;
using System.Net;
using CSharpAsync.models;

namespace CSharpAsync {
  class Program {
    static void Main (string[] args) {

      Console.WriteLine ();
      Console.WriteLine ("Simple Async Demo App");
      Console.WriteLine ();
      Console.WriteLine ("Press 1 for Normal Execute");
      Console.WriteLine ("Press 2 for Async Execute");

      var key = Console.ReadKey (true).Key;

      if (key.ToString () == "D1") {
        //Console.WriteLine ("You selected Normal Execute");
        NormalExecute ();
      } else if (key.ToString () == "D2") {
        Console.WriteLine ("You selected Async Execute");
      }
    }

    private static void NormalExecute () {
      var watch = System.Diagnostics.Stopwatch.StartNew ();

      RunDownloadSync ();

      watch.Stop ();
      var elapsedMs = watch.ElapsedMilliseconds;

      Console.WriteLine ($"Total execution time: { elapsedMs }");
    }

    private static void RunDownloadSync () {
      List<string> websites = PrepData ();

      foreach (string site in websites) {
        WebsiteDataModel results = DownloadWebsite (site);
        ReportWebsiteInfo (results);
      }
    }

    private static WebsiteDataModel DownloadWebsite (string websiteURL) {
      WebsiteDataModel output = new WebsiteDataModel ();
      WebClient client = new WebClient ();

      output.WebsiteUrl = websiteURL;
      output.WebsiteData = client.DownloadString (websiteURL);

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
