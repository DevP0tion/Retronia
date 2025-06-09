using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Retronia.Utils.UI
{
  public class UILinePoint : MonoBehaviour
  {
    public Image lineImage;
    public UILinePoint next;
    public bool update = false;

    private void Start()
    {
      Draw();
    }

    private void LateUpdate()
    {
      if(update) Draw();
    }

    [Tooltip( "Draw Line" )]
    [Button]
    public void Draw()
    {
      if(lineImage == null || next == null) return;
      
      var lineRect = lineImage.rectTransform;
      Vector3 pos = transform.position, nextPos = next.transform.position;
      
      lineRect.SetParent(transform);
      lineRect.anchoredPosition = new Vector2(1, 1);
      lineImage.rectTransform.sizeDelta = new Vector2(Vector2.Distance(pos, nextPos) / 2, lineImage.rectTransform.sizeDelta.y);
      
      var vec = pos - nextPos;
      lineRect.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg);
      next.Draw();
    }
  }
}