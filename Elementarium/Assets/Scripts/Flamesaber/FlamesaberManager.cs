using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlamesaberManager : MonoBehaviour
{
    public FlamesaberData[] pillarsArray;

    bool isPuzzleRunning;

    public Transform leftSpawnTransform;
    public Transform rightSpawnTransform;

    public AnimationCurve pillarSpeedOverTime;
    public float maxDistance;

    private int score;

    public UnityEvent winEvent;

    public void StartPuzzle()
    {
        if (!isPuzzleRunning)
        {
            isPuzzleRunning = true;
            StartCoroutine(PuzzleCoroutine());
        }
    }


    public void StopPuzzle()
    {
        //print("puzzle stopped");
        isPuzzleRunning = false;
        score = 0;
    }

    private void WinPuzzle()
    {
        winEvent.Invoke();
    }

    IEnumerator PuzzleCoroutine()
    {
        int i = 0;
        while (i<pillarsArray.Length && isPuzzleRunning)
        {
            switch (pillarsArray[i].pillarPosition)
            {
                case FlamesaberData.position.Left:
                    SpawnPillar(i, leftSpawnTransform);
                    break;

                case FlamesaberData.position.Right:
                    SpawnPillar(i, rightSpawnTransform);
                    break;

            }

            if (i+1 < pillarsArray.Length) yield return new WaitForSeconds(pillarsArray[i+1].delay);

            i++;
        }
    }


    private void SpawnPillar(int i, Transform spawnTransform)
    {
        GameObject pillar = Pooler.instance.Pop(pillarsArray[i].pillarPrefabName, spawnTransform.position, spawnTransform.rotation);
        FlamesaberPillar pillarComponent = pillar.GetComponent<FlamesaberPillar>();
        pillarComponent.speed = pillarsArray[i].speed;
        pillarComponent.speedOverTime = pillarSpeedOverTime;
        pillarComponent.FS_Manager = gameObject.GetComponent<FlamesaberManager>();
        pillarComponent.maxDistance = maxDistance;
        pillarComponent.prefabName = pillarsArray[i].pillarPrefabName;
    }


    public void AddScore()
    {
        score++;
        if (score >= pillarsArray.Length) WinPuzzle();
    }


    [ContextMenu("Debug Puzzle")]
    public void DebugPuzzle()
    {
        StartPuzzle();
    }
}
