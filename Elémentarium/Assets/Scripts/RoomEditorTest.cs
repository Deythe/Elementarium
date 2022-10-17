using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEditorTest : MonoBehaviour
{
    [SerializeField] private int xSizeRoom, ySizeRoom, zSizeRoom;
    [SerializeField] private GameObject prefab;
    private GameObject transformParent, cubePivot;
    private int x, y, z;
    
    [ContextMenu("Create Room Structure")]
    void CreatureRoomStructure()
    {
        if (xSizeRoom<4 || ySizeRoom<4 || zSizeRoom<4)
        {
            Debug.LogError("Une des 3 valeur rentrée est incorrect car inférieur à la limite minimum");   
            return;
        }
        
        transformParent = new GameObject();
        transformParent.name = "Room";
        for (x = 0; x < xSizeRoom; x++)
        {
            for (y = 0; y < ySizeRoom; y++)
            {
                for (z = 0; z < zSizeRoom; z++)
                {
                    if (y == 0 || y == ySizeRoom-1 || x == 0 || x == xSizeRoom-1 || z == 0 || z == zSizeRoom-1)
                    {
                        cubePivot = Instantiate(prefab, transformParent.transform);
                        cubePivot.transform.position = new Vector3(x, y, z);   
                    }
                }
            }
        }
    }
}
