using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private bool sceneFound = false;

    public void LoadLevelAsync(int sceneIdx)
    {
        sceneFound = false;

        FindScene(sceneIdx);

        if (!sceneFound) SceneManager.LoadSceneAsync(sceneIdx, LoadSceneMode.Additive);
    }

    public void UnloadLevelAsync(int sceneIdx) 
    {
        sceneFound = false;

        FindScene(sceneIdx);

        if(sceneFound) SceneManager.UnloadSceneAsync(sceneIdx);
        //Resources.UnloadUnusedAssets();
    }

    private void FindScene(int sceneIdx)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).buildIndex == sceneIdx) sceneFound = true;
        }
    }

}
