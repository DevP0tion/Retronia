using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Retronia.Contents;
using Retronia.Contents.Items;
using Retronia.Contents.Properties;
using UnityEngine;

namespace Retronia.IO.Formats
{
  [Serializable]
  public class Inventory : IJsonSerializable, IList<ItemStack>
  {
    public static Inventory Current => PlayerInfo.Current.inventory;
    
    [SerializeField] protected ItemStack[] items = new ItemStack[10];

    public void LoadJson(JObject json)
    {
      if (json.TryGetValue(nameof(Size), out var sizeToken) && sizeToken is JValue size)
      {
        items = new ItemStack[size.Value<int>()];

        if (json.TryGetValue("contents", out var contentsToken) && contentsToken is JArray contents)
        {
          for (var i = 0; i < contents.Count && i < items.Length; i++)
          {
            var token = contents[i];

            if (token.Type == JTokenType.Integer)
              items[i] = null;
            else
            {
              var item = ItemStack.Load(token as JObject);
              items[i] = item;
            }
          }
        }
      }
    }

    public JObject ToJson()
    {
      var contents = new JArray();
      foreach (var item in items)
      {
        contents.Add(item != null ? item.ToJson() : 0);
      }
      
      return new JObject
      {
        [nameof(Size)] = Size, // 10
        ["contents"] = contents // [ { type: "item", amount: 10 }, { type: "item", amount: 10 }]
      };
    }

    public Inventory(JObject json = null)
    {
      if(json != null)
        LoadJson(json);
    }

    // Claude로 임시로 생성한 코드
    // IList 인터페이스와 유틸리티 기능을 구현하고 있습니다.
    #region List implementation
    
    /// <summary>
    /// 현재 보관중인 아이템의 종류 개수를 반환합니다.
    /// </summary>
    public int Count => items.Count(t => t != null);
    public bool IsReadOnly { get; protected set; }

    public int Size
    {
      get => items.Length;
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof(value));
        
        var newItems = new ItemStack[value];
        Array.Copy(items, newItems, Math.Min(items.Length, value));
        items = newItems;
      }
    }
    
    /// <summary>
    /// 인벤토리중 비어있는 가장 앞의 인덱스를 찾아서 반환합니다. <br/>
    /// 비어있는 칸이 없을 경우 -1를 반환합니다.
    /// </summary>
    public int FirstEmptyIndex => Array.FindIndex(items, item => item == null);
    
    public IEnumerator<ItemStack> GetEnumerator()
    {
      for (var i = 0; i < Count; i++)
      {
        yield return items[i];
      }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// 비어있는 칸이 있다면 추가합니다.<br/>
    /// TODO 동일한 아이템이 있다면 추가하고, 남은 양만큼 따로 추가하게 구현해야합니다. 
    ///
    /// </summary>
    /// <param name="item">추가할 아이템입니다.</param>
    public void Add(ItemStack item)
    {
      var index = FirstEmptyIndex;
      if (index == -1)
        throw new InvalidOperationException("인벤토리가 가득 찼습니다.");
        
      items[index] = item;
    }

    public void AddItem(ItemProperties type, int amount = 1) => Add(new ItemStack(type, amount));

    public void AddItem(string itemName, int amount = 1)
    {
      if(ItemProperties.items.TryGetValue(itemName, out var item))
        Add(new ItemStack(item, amount));
      else
        throw new ArgumentException($"Item {itemName} is not found.");
    }
    
    public ItemStack GetItem(ItemProperties type) => items.FirstOrDefault(item => item?.type == type);
    public ItemStack GetItem(string itemName) => items.FirstOrDefault(item => item?.type.name == itemName);
    public ItemStack GetItem(int index) => items[index];
    public bool TryGetItem(int index, out ItemStack itemStack)
    {
      itemStack = items[index];
      return itemStack != null;
    }

    public void Clear() => Array.Clear(items, 0, Size);
    public bool Contains(ItemStack item) => IndexOf(item) != -1;

    public void CopyTo(ItemStack[] array, int arrayIndex) => Array.Copy(items, 0, array, arrayIndex, Count);

    public bool Remove(ItemStack item)
    {
      var index = IndexOf(item);
      if (index == -1)
        return false;
        
      RemoveAt(index);
      return true;
    }

    public int IndexOf(ItemStack item)
    {
      for (var i = 0; i < Count; i++)
      {
        if (items[i] == item)
          return i;
      }
      return -1;
    }

    /// <summary>
    /// index 위치에 ItemStack을 넣습니다.
    /// 내부 요소의 위치를 변동시키지 않고, 요소가 이미 있었을 경우 대체시킵니다.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="item"></param>
    public void Insert(int index, ItemStack item)
    {
      if (index < 0 || index > Count)
        throw new ArgumentOutOfRangeException(nameof(index));
        
      if (Count >= items.Length)
        throw new InvalidOperationException("인벤토리가 가득 찼습니다.");
    
      items[index] = item;
    }

    /// <summary>
    /// index 위치의 아이템을 제거합니다. <br/>
    /// 내부 요소의 위치를 변동시키지 않습니다.
    /// </summary>
    /// <param name="index">제거할 요소의 인덱스입니다.</param>
    public void RemoveAt(int index)
    {
      if (index < 0 || index >= Size)
        throw new ArgumentOutOfRangeException(nameof(index));
    
      items[Count - 1] = null;
    }

    public ItemStack this[int index]
    {
      get => items[index];
      set => items[index] = value;
    }
    
    #endregion
  }
}