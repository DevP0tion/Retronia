using System.Threading.Tasks;
using NUnit.Framework;
using Retronia.IO;
using UnityEngine;
using UnityEngine.TestTools;

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
      task.Wait();
      var save = task.Result;
      Debug.Log(save.ToString());
    }
  }
}