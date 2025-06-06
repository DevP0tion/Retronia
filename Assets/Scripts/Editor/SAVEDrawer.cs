using Newtonsoft.Json.Linq;
using Retronia.IO;
using Retronia.Utils.Editor;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SAVE))]
public class SAVEDrawer : PropertyDrawer
{
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    EditorGUI.BeginProperty(position, label, property);

	  var data = property?.Get<SAVE>();
    
    if (data is not null)
    {
      EditorGUI.LabelField(EditorGUILayout.GetControlRect(), $"{label} - {data.name}");

      foreach (var (key, token) in data)
      {
        if(token == null) continue;

        data[key] = token.Type switch
        {
          JTokenType.Float => EditorGUI.FloatField(EditorGUILayout.GetControlRect(), key, token.Value<float>()),
          JTokenType.Integer => EditorGUI.IntField(EditorGUILayout.GetControlRect(), key, token.Value<int>()),
          JTokenType.String => EditorGUI.TextField(EditorGUILayout.GetControlRect(), key, token.Value<string>()),
          JTokenType.Boolean => EditorGUI.Toggle(EditorGUILayout.GetControlRect(), key, token.Value<bool>()),
          _ => data[key]
        };
      }
    }
    else
    {
      EditorGUI.LabelField(position, $"{label} - No data");
    }
    
    EditorGUI.EndProperty();
  }
}