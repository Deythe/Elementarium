using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using MonoScript = UnityEditor.MonoScript;
using Object = UnityEngine.Object;


public class ToolAddComponent : EditorWindow
{
    [SerializeField] private VisualTreeAsset _visualTree;
    private VisualElement root;
    private Button _add;
    
    [MenuItem("Tool/AddComponentOnPrefabs")]
    public static void ShowWindow()
    {
        ToolAddComponent wnd = GetWindow<ToolAddComponent>();
        wnd.titleContent = new GUIContent("ToolAddComponent");
    }

    public void CreateGUI()
    {
        root = rootVisualElement;
        root.Add(_visualTree.Instantiate());
        Biding();
    }

    private void Biding()
    {
        _add = root.Q<Button>("addGround");
        _add.clicked += AddTPAreaToGround;
    }

    private void AddTPAreaToGround()
    {
        GameObject[] all = FindObjectsOfType<GameObject>();

        foreach (GameObject go in all)
        {
            if (go.layer.Equals(8))
            {
                go.AddComponent<TeleportationArea>();
            }
        }
    }
}