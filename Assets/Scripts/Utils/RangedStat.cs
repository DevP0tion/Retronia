using System;
using UnityEngine;
using UnityEngine.Events;

namespace Retronia.Utils
{
  [Serializable]
  public class RangedStat
  {
    public UnityEvent<float> onChanged;

    [SerializeField] [GetSet("Value")] private float value;
    [SerializeField] private Stat max;

#if UNITY_EDITOR
    [SerializeField] [GetSet("Max")] private float maxValue;
#endif

    public RangedStat(float maxValue, float value, StatOperator<float> maxEffect = null)
    {
      max = new Stat(maxValue);
      Value = value;
      MaxEffect = maxEffect;
    }

    public RangedStat(float maxValue) : this(maxValue, maxValue)
    {
    }

    public float Value
    {
      get => value;
      set
      {
        var prevValue = this.value;
        this.value = Math.Max(0, Math.Min(value, max));
        onChanged?.Invoke(prevValue);
      }
    }

    public Stat Max
    {
      get => max;
      set
      {
        max.BaseValue = value;
        Value = this.value;
      }
    }

    public StatOperator<float> MaxEffect
    {
      get => max.effect;
      set
      {
        max.effect = value;
        Value = this.value;
      }
    }
  }
}