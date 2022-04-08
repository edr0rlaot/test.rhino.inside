using System;
using Rhino;
using Rhino.Runtime.InProcess;

namespace TestRhinoInside
{
  class Program
  {
    static string OpenRhinoFile(string path)
    {
      try
      {
        using (new RhinoCore(new string[] { "/NOSPLASH" }, WindowStyle.NoWindow))
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
      }
      catch (Exception ex)
      {
        return "failed: " + ex.Message;
      }
    }

    static void Main(string[] args)
    {
      if (args.Length < 1)
      {
        Console.WriteLine("please, select a .3dm file...");
      }
      else
      {
        var path = args[0];
        Console.WriteLine("input file : {0}", path);
        var result = OpenRhinoFile(path);
        Console.WriteLine(result);
      }
    }
  }
}
