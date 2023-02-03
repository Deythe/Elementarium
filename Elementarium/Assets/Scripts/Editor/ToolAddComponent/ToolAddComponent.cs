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
    private ObjectField _script;
    private LayerMaskField _layerMaskField;
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
        _script = root.Q<ObjectField>("script");
        _layerMaskField = root.Q<LayerMaskField>("layer");
        _add = root.Q<Button>("addWithLayer");
        _add.clicked += AddWithLayer;
        _script.objectType = typeof(MonoScript);
    }

    private void AddWithLayer()
    {
        //Debug.Log();
        Debug.Log(Type.GetType("UnityEngine.Rigidbody, UnityEngine"));
        GameObject[] all = FindObjectsOfType<GameObject>();

        foreach (GameObject go in all)
        {
            if (((1<<go.layer) & _layerMaskField.value) != 0)
            {
                
                //go.AddComponent(_script.value.GetType());
            }
        }
    }
}