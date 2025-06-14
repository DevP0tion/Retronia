using UnityEngine;

namespace Retronia.Utils
{
  public static class ArrayUtil
  {
    public static T GetRandom<T>(this T[] array)
    {
      return array[Random.Range(0, array.Length)];
    }
  }
}