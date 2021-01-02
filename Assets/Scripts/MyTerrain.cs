using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTerrain : MonoBehaviour
{
    Mesh mesh;
    Vector3[] dots;
    int[] triangles;
    Color[] colors;
    float height;

    public int width=3;
    public int length=3;
    public float xnoise;
    public float znoise;
    [Range(0,15)] 
    public float ynoise;
    public Gradient gradient;

    public void CreateMesh()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        MakeDotes();
        MakeTriangles();
        MakeColors();
        UpdateMesh();
    }

 

    void MakeDotes()
    {
        dots = new Vector3[(width + 1) * (length + 1)];
        for(int i=0, z=0; z<=length; z++)
        {
            for(int x=0; x <= width; x++)
            {
                float y = Mathf.PerlinNoise(x * xnoise, z * znoise) * ynoise;

                if (y > height)
                    height = y;

                dots[i] = new Vector3(x, y, z);
                i++;
            }
            
        }

    }


    void MakeTriangles()
    {
        triangles = new int[length * width * 6];
        int i = 0;
        int j = 0;
        for(int z=0; z<length; z++)
        {
            for(int x=0; x<width; x++)
            {
               // Debug.Log(i);
                triangles[i + 0] = j + 0;
                triangles[i + 1] = j + width+1;
                triangles[i + 2] = j + 1;
                triangles[i + 3] = j + 1;
                triangles[i + 4] = j + width+1;
                triangles[i + 5] = j + width+2;

                i += 6;
                j++;
               
            }
            j++;
            
        }
    }


    void MakeColors()
    {

        colors = new Color[(width + 1) * (length + 1)];
        for (int i = 0, z = 0; z <= length; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                colors[i] = gradient.Evaluate(dots[i].y/height);
               
                i++;
            }

        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = dots;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
    }
}
