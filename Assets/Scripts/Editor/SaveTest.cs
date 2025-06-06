using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Retronia.IO;
using UnityEngine;

namespace Retronia.Editor
{
  [TestFixture]
  public class SaveTest
  {
    [Test]
    public static void TestSave()
    {
      var save = new SAVE("Test Save Data") { { "test", "test" } };
      save.Save().Wait();
    }

    [Test]
    public static void LoadSave()
    {
      var task = SAVE.Load("Test Save Data");
      task.ContinueWith((_, _) =>
      {
        var save = task.Result;
        Debug.Log(save.ToString());
        Debug.Log(save.player.ToString());
        
      }, TaskContinuationOptions.OnlyOnRanToCompletion);
    }

    [Test]
    public static void Test()
    {
      var dic = new Dictionary<string, int>
      {
        ["test1"] = 3,
        ["test2"] = 2,
        ["test3"] = 1,
      };
      var json = new JObject(dic);
      
      Debug.Log(json.ToString());
    }
  }
}