using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;

namespace Editor.Scripts
{
    [CustomEditor(typeof(GenerateGrid))]
    public class GridEditor : UnityEditor.Editor
    {
        private VisualElement root;
        private Button button;
        
        [SerializeField] 
        private VisualTreeAsset visualTree;

        public override VisualElement CreateInspectorGUI()
        {
            root = new VisualElement();

            root.Add(visualTree.Instantiate());

            Bindings();
        
            return root;
        }


        private void Bindings()
        {
            button = root.Q<Button>("button");
            button.clicked += ()=> { target.GetComponent<GenerateGrid>().MakeGrid(); };
        }
    }
}
