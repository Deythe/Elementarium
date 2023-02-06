using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateGrid : MonoBehaviour
{
    public GameObject blockGameObject;

    public GameObject planeGameObject;

    public bool makePlaneBehind;
    
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
    private List<float> _blockHeights = new List<float>();

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
                
                Matrix4x4 gridMatrix = this.transform.localToWorldMatrix;
                Vector3 pos = new Vector3(
                     x * gridOffset,
                     (float)GenerateNoise(x, z, 102 - noiseSmoothness * 10) * noiseHeight,
                     z * gridOffset);

                //pos = gridMatrix.MultiplyVector(pos + transform.position);
                pos -= Vector3.up * noiseHeight/2;

                GameObject block = Instantiate(blockGameObject, Vector3.zero, transform.rotation) as GameObject;
                block.transform.SetParent(this.transform);
                block.transform.localPosition = pos;
                _blockHeights.Add(block.transform.localPosition.y);
                
            }
        }

        if (!makePlaneBehind) return;
        MakePlaneBehind();
    }

    private void MakePlaneBehind()
    {
        _blockHeights.Sort();
        GameObject plane = Instantiate(planeGameObject, Vector3.zero, transform.rotation);
        
        float posX = (worldSizeX - 1) / 2f;
        float posY = _blockHeights[0] - blockGameObject.transform.lossyScale.y/2f - 0.01f;
        float posZ = (worldSizeZ - 1) / 2f;
        
        plane.transform.SetParent(this.transform);
        plane.transform.localPosition = new Vector3(posX, posY, posZ);
        plane.transform.localScale = new Vector3(worldSizeX/10f, 0, worldSizeZ/10f);
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
        _blockHeights.Clear();
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = Color.blue;
        Matrix4x4 handleMatrix = this.transform.localToWorldMatrix;
        Handles.matrix = handleMatrix;

        Handles.DrawWireCube(
            new Vector3(
                worldSizeX * gridOffset - gridOffset ,
                0,
                worldSizeZ * gridOffset  - gridOffset)/2,
            
            new Vector3(
                worldSizeX * gridOffset - (gridOffset-1),
                1,
                worldSizeZ * gridOffset - (gridOffset-1)));
#endif
    }
}
