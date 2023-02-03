using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateGrid : MonoBehaviour
{
    public GameObject blockGameObject;
    
    [SerializeField]
    private int worldSizeX = 10;
    
    [SerializeField]
    private int worldSizeZ = 10;

    [SerializeField]
    private float gridOffset = 2f;
    
    private float noiseHeight = 3;
    
    [SerializeField] [Range(0, 10)]
    private float noiseSmoothness = 8f;

    private List<Vector3> _blockPositions = new List<Vector3>();

    [ContextMenu("Make Grid")]
    public void MakeGrid()
    {
        if (transform.childCount > 0)
        {
            ClearGrid();
        }
        for (int x = 0; x < worldSizeX; x++)
        {
            for (int z = 0; z < worldSizeZ; z++)
            {
                
                float degX = Mathf.Rad2Deg * transform.rotation.eulerAngles.x;
                float degY = Mathf.Rad2Deg * transform.rotation.eulerAngles.y;
                float degZ = Mathf.Rad2Deg * transform.rotation.eulerAngles.z;

                Debug.Log(degZ);
                
                Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

                Vector3 pos = new Vector3(
                    transform.position.x + x * gridOffset,
                    transform.position.y + GenerateNoise(x, z, 102 - noiseSmoothness * 10) * noiseHeight,
                    transform.position.z + z * gridOffset);

                pos = rotationMatrix.MultiplyVector(pos);
                
                GameObject block = Instantiate(blockGameObject, pos, transform.rotation) as GameObject;
                _blockPositions.Add(block.transform.position);
                block.transform.SetParent(this.transform);
            }
        }
    }

    private Vector3 ObjectSpawnLocation()
    {
        int rndIndex = Random.Range(0, _blockPositions.Count);

        Vector3 newPos = new Vector3(
            _blockPositions[rndIndex].x,
            _blockPositions[rndIndex].y + 1f,
            _blockPositions[rndIndex].z
        );
        
        _blockPositions.RemoveAt(rndIndex);
        return newPos;
    }

    //[ContextMenu("Spawn Objects")]
    // private void SpawnObject()
    // {
    //     if (transform.childCount > 0)
    //     {
    //         ClearGrid();
    //         MakeGrid();
    //     }
    //     for (int c = 0; c < objectNumber; c++)
    //     {
    //         GameObject toPlaceObject = Instantiate(
    //             objectToSpawn,
    //             ObjectSpawnLocation(),
    //             Quaternion.identity);
    //         toPlaceObject.transform.SetParent(this.transform);
    //     }
    // }

    private float GenerateNoise(int x, int z, float detailScale)
    {
        if (detailScale == 102)
        {
            return .5f;
            //test
        }
        int randomOffset = Random.Range(0, 10000);
        
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.y) / detailScale;

        return Mathf.PerlinNoise(xNoise + randomOffset, zNoise + randomOffset);
    }

    private void ClearGrid()
    {
        List<Transform> cubes = new List<Transform>();
        foreach (Transform cube in transform)
        {
            cubes.Add(cube);
        }

        var size = cubes.Count;
        for (int x = 0; x < size; x++)
        {
            DestroyImmediate(cubes[x].gameObject);
        }
        cubes.Clear();
        _blockPositions.Clear();
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = Color.blue;
        Matrix4x4 handleMatrix = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.lossyScale);
        Handles.matrix = handleMatrix;
        
        Handles.DrawWireCube(
            transform.position + 
            new Vector3(
                worldSizeX * gridOffset - gridOffset ,
                noiseHeight,
                worldSizeZ * gridOffset  - gridOffset)/2,
            
            new Vector3(
                worldSizeX * gridOffset - (gridOffset-1),
                1,
                worldSizeZ * gridOffset - (gridOffset-1)));
#endif
    }
}
