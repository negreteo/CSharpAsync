using System;
using System.Collections.Generic;

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
        Console.WriteLine ("You selected Normal Execute");
      } else if (key.ToString () == "D2") {
        Console.WriteLine ("You selected Async Execute");
      }

    }

  }
}
