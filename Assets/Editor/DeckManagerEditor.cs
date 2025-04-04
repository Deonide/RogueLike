using UnityEngine;



#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(DrawPileManager))]
public class DrawPileManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DrawPileManager drawPileManager = (DrawPileManager)target;
        if(GUILayout.Button("Draw Next Card"))
        {
            HandManager handManager = FindFirstObjectByType<HandManager>();
            if(handManager != null)
            {
                drawPileManager.DrawCard(handManager);
            }
        }
    }
}
#endif