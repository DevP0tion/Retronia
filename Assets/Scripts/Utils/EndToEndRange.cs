using System;
using UnityEngine;

namespace Retronia.Utils
{
  /// <summary>
  ///   숫자의 범위를 입력하고, 입력된 범위 내에서 값을 변경해주는 유틸리티
  ///   목록에서 선택 영역을 옮길 떄 사용
  /// </summary>
  [Serializable]
  public class EndToEndRange
  {
    public int min, max, interval;

    [SerializeField] [GetSet(nameof(Value))]
    protected int value;

    public EndToEndRange(int max, int min, int interval, int value)
    {
      this.min = min;
      this.max = max;
      this.interval = interval;
      this.value = value;
      Range = max - min;
    }

    public EndToEndRange(int max, int min = 0) : this(max, min, 1, min)
    {
      this.min = min;
      this.max = max;
    }

    public int Range { get; protected set; }

    /// <summary>
    ///   값을 대입할 시 min, max 범위 내 끝에서 끝으로 설정하고,
    ///   interval이 1이 아닐시 interval에 맞게 재설정함.
    /// </summary>
    public virtual int Value
    {
      get => value;
      set
      {
        if (value > max)
          for (this.value = value; this.value > max; this.value -= Range)
          {
          }
        else if (value < min)
          for (this.value = value; this.value < min; this.value += Range)
          {
          }

        if (interval != 1)
          this.value = min + (this.value - min) % interval;
        else this.value = value;
      }
    }

    public virtual int Next(bool reverse = false)
    {
      return reverse ? Value -= interval : Value += interval;
    }
  }
}