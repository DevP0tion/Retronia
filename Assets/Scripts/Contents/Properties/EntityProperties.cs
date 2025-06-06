using UnityEngine;

namespace Retronia.Contents.Properties
{
  [CreateAssetMenu(fileName = "new Entity Properties", menuName = "Retronia/Entity Properties")]
  public class EntityProperties : ScriptableObject
  {
    public float speed = 1;
    public float rotateSpeed = 1;
    public float healthPoint = 100;
    public float maxHealthPoint = 100;
  }
}