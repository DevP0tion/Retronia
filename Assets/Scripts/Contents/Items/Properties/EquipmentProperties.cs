using System;
using System.Collections.Generic;
using Retronia.Contents.Items;
using Retronia.Utils;
using UnityEngine;
using CharacterInfo = Retronia.IO.Formats.CharacterInfo;

namespace Retronia.Contents.Properties
{
  [CreateAssetMenu(fileName = "new Equipment Properties", menuName = "Properties/Equipment Item")]
  public class EquipmentProperties : ItemProperties, IEquipable
  {
    public EquipmentPart part;
    public EquipmentPart Part => part;
    public string Name => name;
    public SerializableDictionary<State, float> statOperator = new();
    
    public Action Equip(CharacterInfo character)
    {
      var opers = new List<(State, StatOperator<float>)>();
      
      foreach (var pair in statOperator)
      {
        switch (pair.Key)
        {
          case State.Hp:
          {
            StatOperator<float> oper = v => v + pair.Value;
            character.hpEffect.Add(oper);
            opers.Add((State.Hp, oper));
            break;
          }

          case State.Atk:
          {
            StatOperator<float> oper = v => v + pair.Value;
            character.atkEffect.Add(oper);
            opers.Add((State.Atk, oper));
            break;
          }

          case State.Def:
          {
            StatOperator<float> oper = v => v + pair.Value;
            character.defEffect.Add(oper);
            opers.Add((State.Def, oper));
            break;
          }

          case State.CritChance:
          {
            StatOperator<float> oper = v => v + pair.Value;
            character.critChanceEffect.Add(oper);
            opers.Add((State.CritChance, oper));
            break;
          }
        }
      }
      
      return () =>
      {
        foreach (var (state, oper) in opers)
        {
          switch (state)
          {
            case State.Hp:
              character.hpEffect.Remove(oper);
              break;
            case State.Atk:
              character.atkEffect.Remove(oper);
              break;
            case State.Def:
              character.defEffect.Remove(oper);
              break;
            case State.CritChance:
              character.critChanceEffect.Remove(oper);
              break;
            default: continue;
          }
        }
      };
    }

    public void UnEquip(CharacterInfo character)
    {
    }
    
    public enum State
    {
      Hp, Atk, Def, CritChance
    }
  }
}