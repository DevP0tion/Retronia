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