using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Retronia.Contents.Properties;
using Retronia.Utils;
using UnityEngine;

namespace Retronia.IO.Formats
{
  [Serializable]
  public class PlayerInfo : IJsonSerializable
  {
    public static PlayerInfo Current => SAVE.Current?.player;
    
    #region State 
    
    public string name = "Player";
    public int gold = 1000;
    public Inventory inventory = new();
    public List<CharacterInfo> characters = new();
    
    #endregion
    
    #region Logic

    [SerializeField, GetSet(nameof(Index))] private int selectedCharacterIndex = 0;
    public int Index
    {
      get => selectedCharacterIndex;
      set
      {
        if (value < 0 || value >= characters.Count)
          throw new ArgumentOutOfRangeException(nameof(value));
        
        selectedCharacterIndex = value;
      }
    }
    public CharacterInfo SelectedCharacter => characters[selectedCharacterIndex];
    public void AddCharacter(CharacterProperties properties) => characters.Add(properties.Instantiate());
    public void AddCharacter(string characterName) => AddCharacter(CharacterProperties.Characters[characterName]);

    public void RemoveCharacter(int index)
    {
      if (index < 0 || index >= characters.Count)
        throw new ArgumentOutOfRangeException(nameof(index));
        
      characters.RemoveAt(index);
    }
    public CharacterInfo this[int index] => characters[index];
    
    #endregion

    public PlayerInfo(JObject json = null)
    {
      if(json != null)
        LoadJson(json);
    }

    public void LoadJson(JObject json)
    {
      name = json.TryGetValue(nameof(name), out var token) ? token.Value<string>() : name;
      gold = json.TryGetValue(nameof(gold), out token) ? token.Value<int>() : gold;
      
      if(json.TryGetValue(nameof(inventory), out token) && token is JObject inventoryJson)
        inventory.LoadJson(inventoryJson);
      
      if(json.TryGetValue(nameof(characters), out token) && token is JArray charactersJson)
      {
        foreach (var characterJson in charactersJson)
        {
          if (characterJson is JObject character)
            characters.Add(new CharacterInfo(character));
        }
      }
    }

    public JObject ToJson()
    {
      return new JObject
      {
        [nameof(name)] = name,
        [nameof(gold)] = gold,
        [nameof(inventory)] = inventory.ToJson(),
        [nameof(characters)] = new JArray(from obj in characters where obj != null select obj.ToJson())
      };
    }
  }
}