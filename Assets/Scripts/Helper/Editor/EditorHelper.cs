using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;

public static class EditorHelper {

    public static bool BigButton(this Editor editor, string content, params GUILayoutOption[] options) {
        return BigButton(content, options);
    }
    public static bool BigButton(this EditorWindow editor, string content, params GUILayoutOption[] options) {
        return BigButton(content, options);
    }
    public static bool BigButton(string content, params GUILayoutOption[] options) {
        List<GUILayoutOption> optionsList = new List<GUILayoutOption>(options);
        optionsList.Add(GUILayout.Height(EditorGUIUtility.singleLineHeight * 3));
        return GUILayout.Button(content, optionsList.ToArray());
    }

    public static void DrawHeader(this Editor editor, string title) {
        EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
    }

	public static void DrawProperty(this Editor editor, string name, string tooltip = null) {
        string prettyName = char.ToUpper(name[0]) + name.Substring(1);
        SerializedProperty property = editor.serializedObject.FindProperty(name);
        if (property == null) {
            Debug.LogError("Property name '" + name + "' does not exist on " + editor.target.GetType().Name + ".");
            return;
        }
        EditorGUILayout.PropertyField(property, new GUIContent(prettyName, tooltip));
    }

    public static void DrawReferencesList<T>(this Editor editor, string title, IEnumerable<T> references, string warningMessage = null, MessageType messageType = MessageType.Info, bool disabled = false) where T : Object {
        if (references == null) { return; }
        DrawHeader(editor, title);
        if (disabled) {
            EditorGUI.BeginDisabledGroup(true);
        }
        if (references.Count() == 0) {
            if (!string.IsNullOrEmpty(warningMessage)) {
                EditorGUILayout.HelpBox(warningMessage, messageType);
            }
        } else {
            foreach (T obj in references) {
                if (obj == null) {
                    GUI.color = Color.red;
                    GUILayout.Label("- Null");
                    GUI.color = Color.white;
                } else {
                    GUILayout.Label("- " + obj.name);
                }
            }
        }
        if (disabled) {
            EditorGUI.EndDisabledGroup();
        }
    }

    public static ReorderableList GenerateSortableList(this Editor editor, string name, bool sortable, bool showItemNames) {
        ReorderableList list = new ReorderableList(editor.serializedObject, editor.serializedObject.FindProperty(name), sortable, true, true, true);
        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, name);
        };
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            GUIContent itemName = showItemNames ? null : new GUIContent("");
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, itemName);
        };
        return list;
    }

    public static void AddHorizontalLayoutSpacing(this Editor editor, float width) {
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal(GUILayout.Width(width));
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    public static void SpaceLines(this Editor editor, int lineCount) {
        for (int i = 0; i < lineCount; i++) {
            EditorGUILayout.Space();
        }
    }

    public static bool EditorPrefFoldout(this EditorWindow editor, string editorPref, string title, string tooltip = null) {
        return EditorPrefFoldout(editorPref, title, tooltip);
    }
    public static bool EditorPrefFoldout(this Editor editor, string editorPref, string title, string tooltip = null) {
        return EditorPrefFoldout(editorPref, title, tooltip);
    }
    public static bool EditorPrefFoldout(string editorPref, string title, string tooltip = null) {
        bool isFoldedOut = EditorPrefs.GetBool(editorPref);
        isFoldedOut = EditorGUILayout.Foldout(isFoldedOut, new GUIContent(title, tooltip), true);
        EditorPrefs.SetBool(editorPref, isFoldedOut);
        return isFoldedOut;
    }

    public static void PingFolder(string path) {
        Object obj = AssetDatabase.LoadAssetAtPath("Assets/" + path, typeof(Object));
        EditorGUIUtility.PingObject(obj);
    }

    public static string GetAssetFolderName(Object asset) {
        string assetPath = AssetDatabase.GetAssetPath(asset);
        string[] segments = assetPath.Split('/');
        return segments[segments.Length - 2];
    }

    public static string GetAssetFolderPath(Object asset) {
        List<string> folderNames = GetAssetFolderNames(asset);
        string path = "";
        for (int i = 0; i < folderNames.Count; i++) {
            path += folderNames[i];
            if (i < folderNames.Count - 1) {
                path += '/';
            }
        }
        return path;
    }

    public static T GetAssetInParentResourceFolder<T>(Object asset) where T : Object {
        List<string> folderNames = GetAssetFolderNames(asset);
        folderNames.RemoveRange(0, 2);
        
        while (folderNames.Count > 0) {
            string path = "";
            for (int i = 0; i < folderNames.Count; i++) {
                path += folderNames[i];
                if (i < folderNames.Count - 1) {
                    path += '/';
                }
            }
            T[] results = Resources.LoadAll<T>(path);
            if (results.Length > 0) {
                return results[0];
            }
            folderNames.RemoveAt(folderNames.Count - 1);
        }

        return null;
    }

    public static List<string> GetAssetFolderNames(Object asset, string startFromFolder = null) {
        string assetPath = AssetDatabase.GetAssetPath(asset);
        List<string> segments = new List<string>(assetPath.Split('/'));
        segments.RemoveAt(segments.Count - 1);

        if (startFromFolder != null) {
            int index = segments.IndexOf(startFromFolder);
            if (index != -1) {
                segments.RemoveRange(0, index + 1);
            }
        }

        return segments;
    }

    public static string GetDirectoryPathOfAssetInResources(Object asset) {
        string assetPath = AssetDatabase.GetAssetPath(asset);
        string[] directories = assetPath.Split('/');
        string directoryPath = "";
        for (int i = 2; i < directories.Length - 1; i++) {
            directoryPath += directories[i];
            if (i < directories.Length - 2) {
                directoryPath += "/";
            }
        }
        return directoryPath;
    }

    public static bool IsTargetOrChildSelected(this Editor editor) {
        // Target is destroyed
        if (editor.target == null) { return false; }

        // Target is not a MonoBehaviour
        if (!(editor.target is MonoBehaviour)) { return false; }

        // Nothing is selected
        if (Selection.activeGameObject == null) { return false; }

        // Check parent of selection until target is found
        Transform targetTransform = (editor.target as MonoBehaviour).transform;
        Transform iterateTransform = Selection.activeGameObject.transform;
        while (iterateTransform != targetTransform) {
            iterateTransform = iterateTransform.parent;
            
            // No parent, target not found as parent
            if (iterateTransform == null) {
                return false;
            }
        }

        // Target is found as parent
        return true;
    }

    public static Vector3 GetEditorCameraPosition(bool zToZero = false) {
        if (SceneView.GetAllSceneCameras().Length == 0) { return Vector3.zero; }
        Camera camera = SceneView.GetAllSceneCameras()[0];
        Vector3 position = camera.ViewportToWorldPoint(Vector2.one * .5f);
        if (zToZero) {
            position.z = 0;
        }
        return position;
    }

}