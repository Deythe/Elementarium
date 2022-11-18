using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveGloves : MonoBehaviour
{
    public void GivesGloves()
    {
        PlayerManager.instance.GiveGlove();
        Destroy(gameObject);
    }

    public void Test()
    {
        Destroy(gameObject);
    }
}
