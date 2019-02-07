using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AssetDropdown))]
public class AssetDropdownDrawer : PropertyDrawer {
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        AssetDropdown assetDropdown = attribute as AssetDropdown;

        List<Object> objects;

        if (assetDropdown.ResourceType == null) {
            objects = new List<Object>(Resources.LoadAll(assetDropdown.ResourcePath));
        } else {
            objects = new List<Object>(Resources.LoadAll(assetDropdown.ResourcePath, assetDropdown.ResourceType));
        }

        objects.Insert(0, null);

        List<string> options = new List<string>();
        int index = 0;
        int selectedIndex = -1;

        string currentSelectedName = property.objectReferenceValue != null ? property.objectReferenceValue.name : string.Empty;

        foreach (Object obj in objects) {
            string name = obj == null ? "None" : obj.name;
            options.Add(name);
            
            if ((obj == null && string.IsNullOrEmpty(currentSelectedName)) ||
                (obj != null && obj.name == currentSelectedName)) {
                selectedIndex = index;
            }

            index++;
        }

        string propertyName = property.name.CapitalizeFirstChar().AddSpaceBetweenUpperChars();
        int newSelectedIndex;

        if (assetDropdown.ShowTitle) {
             newSelectedIndex = EditorGUI.Popup(position, propertyName, selectedIndex, options.ToArray());
        } else {
            newSelectedIndex = EditorGUI.Popup(position, selectedIndex, options.ToArray());
        }

        if (newSelectedIndex != selectedIndex) {
            Object newSelected = objects[newSelectedIndex];
            property.objectReferenceValue = newSelected;
        }
    }

}