using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCubes : MonoBehaviour
{
    //PolygonalizeCube(float isoLevel, float size, Vector3 position, ref List<float> pointValues, ref List<Vector3> vertices, ref List<int> triangles)
    public static GameObject GenerateChunk(float isoLevel, float size, Vector3 position, int dimension, List<List<List<float>>> densityValues, Material material)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        Vector3 cubeOffsetForChunk;
        //Find the densities for the points of this cube
        List<float> tempDensities = new List<float>(8);
        for (int i = 0; i < 8; i++)
            tempDensities.Add(0);
        for (int xi = 0; xi < dimension - 1; xi++)
        {
            for (int yi = 0; yi < dimension - 1; yi++)
            {
                for (int zi = 0; zi < dimension - 1; zi++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        tempDensities[i] = densityValues[xi + (int)tbl.points[i].x][ yi + (int)tbl.points[i].y][zi + (int)tbl.points[i].z];
                    }
                    cubeOffsetForChunk.x = xi;
                    cubeOffsetForChunk.y = yi;
                    cubeOffsetForChunk.z = zi;
                    Polygonalizer.PolygonalizeCube(isoLevel, size, (position + cubeOffsetForChunk), ref tempDensities, ref vertices, ref triangles);
                }
            }
        }
        GameObject returnGameObject = new GameObject();
        returnGameObject.AddComponent<MeshRenderer>().material = material;
        returnGameObject.AddComponent<MeshFilter>();
        Mesh mesh = returnGameObject.GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        return returnGameObject;
    }
}
