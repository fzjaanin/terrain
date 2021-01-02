using System.Collections;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MyTerrain))]
public class MyTerrainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MyTerrain myTerrain = (MyTerrain)target;
        if (GUILayout.Button("update terrain"))
        {
            myTerrain.CreateMesh();
       }


    }

}
