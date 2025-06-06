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
    public static async void LoadSave()
    {
      var save = await SAVE.Load("Test Save Data");
      
      Debug.Log(save.ToString());
    }
  }
}