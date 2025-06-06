using UnityEngine;

namespace Retronia.Utils.UI
{
  public class MeshLayer : MonoBehaviour
  {
    [SerializeField] [GetSet(nameof(Mesh))]
    private MeshRenderer mesh;

    [SerializeField] [GetSet(nameof(Layer))]
    private int layer;

    [SerializeField] [GetSet(nameof(LayerName))]
    private string layerName;

    public MeshRenderer Mesh
    {
      get => mesh;
      set
      {
        mesh = value;

        if (mesh)
        {
          mesh.sortingOrder = layer;
          mesh.sortingLayerName = layerName;
        }
      }
    }

    public int Layer
    {
      get => mesh ? mesh.sortingOrder : layer;
      set
      {
        if (mesh) mesh.sortingOrder = value;
        layer = value;
      }
    }

    public string LayerName
    {
      get => mesh ? mesh.sortingLayerName : layerName;
      set
      {
        if (mesh) mesh.sortingLayerName = value;
        layerName = value;
      }
    }
  }
}