using System;
using Rhino;
using Rhino.Runtime.InProcess;

namespace TestRhinoInside
{
  class Program
  {
    static Program()
    {
      RhinoInside.Resolver.Initialize();
    }


    static string OpenRhinoFile(string path)
    {
      try
      {
        var doc = RhinoDoc.Open(path, out bool wasAlreadyOpen);
        if (doc == null)
        {
          Console.WriteLine("error, failed to load file");
          return "failed...";
        }
        else
        {
          Console.WriteLine("file loaded successfully");
          System.Threading.Thread.Sleep(2000);
          doc.Dispose();
          return "done...";
        }
      }
      catch (Exception ex)
      {
        return "failed: " + ex.Message;
      }
    }

    static void Main(string[] args)
    {
      using (new RhinoCore())
      {
        //if (args.Length < 1)
        //{
        //  Console.WriteLine("please, select a .3dm file...");
        //}
        //else
        //{
        //  var path = args[0];
        //  Console.WriteLine("input file : {0}", path);
        //  var result = OpenRhinoFile(path);
        //  Console.WriteLine(result);
        //}

        string[] paths = new string[] {@"C:\dev\github\sbaer\archi.3dm",
      @"C:\dev\github\sbaer\simple-shape.3dm",
      @"C:\dev\github\sbaer\City_NURBS.3dm" };

        foreach (var path in paths)
        {
          Console.WriteLine("input file : {0}", path);
          var result = OpenRhinoFile(path);
          Console.WriteLine(result);
        }

      }
    }
  }
}
