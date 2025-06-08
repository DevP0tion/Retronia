using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Retronia.IO.Bson
{
  [Serializable]
  public class BObject : ISerializable
  {
    private readonly Dictionary<string, object> data = new();
    
    public void Serialize()
    {
      var formatter = new BinaryFormatter();
      var write = new FileStream("Test.bin", FileMode.Create, FileAccess.Write, FileShare.None);
      
      formatter.Serialize(write, data);
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotImplementedException();
    }
  }
}