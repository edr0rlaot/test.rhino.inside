using System;
using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Runtime.InProcess;

namespace TestRhinoInside
{
  class Program
  {
    static Rhino.Geometry.Mesh GetMesh(Rhino.RhinoDoc doc)
    {
      var mesh = new Rhino.Geometry.Mesh();

      var count = 0;
      foreach (var obj in doc.Objects)
      {
        if (!obj.IsNormal)
          continue;

        Console.WriteLine("[C#] {0} - {1} : {2}", ++count, obj.Id, obj.ObjectType.ToString());

        if (obj.ObjectType == ObjectType.Brep)
        {
          var brep = Rhino.Geometry.Brep.TryConvertBrep(obj.Geometry);
          if (brep != null)
          {
            Console.WriteLine("[C#] converting brep to mesh...");
            var aggregate = new Rhino.Geometry.Mesh();
            aggregate.Append(Rhino.Geometry.Mesh.CreateFromBrep(brep, Rhino.Geometry.MeshingParameters.Default));
            mesh.Append(aggregate);
            brep.Dispose();
          }
        }
        else if (obj.ObjectType == ObjectType.Mesh)
        {
          Console.WriteLine("[C#] extracting mesh...");
          mesh.Append(obj.GetMeshes(MeshType.Any));
        }
      }

      Console.WriteLine("[C#] found {0} objects, generated mesh", doc.Objects.Count);
      return mesh;
    }

    static string OpenRhinoFile(string path)
    {
      try
      {
        using (new RhinoCore(new string[] { "/NOSPLASH" }, WindowStyle.NoWindow))
        {
          Console.WriteLine("[C#] opening file at " + path);
          var doc = RhinoDoc.Open(path, out bool wasAlreadyOpen);
          if (doc == null)
          {
            Console.WriteLine("[C#] error, failed to load file");
            return "'failed to load file'";
          }
          else
          {
            Console.WriteLine("[C#] file loaded successfully");
            var mesh = GetMesh(doc);
            doc.Dispose();
            return "done...";
          }
        }
      }
      catch (Exception ex)
      {
        return "failed";
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
        var result = OpenRhinoFile(args[0]);
        Console.WriteLine(result);
      }
    }
  }
}
