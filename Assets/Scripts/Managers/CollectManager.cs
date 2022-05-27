using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CollectManager : Singleton<CollectManager>
{
    public int CollectedCoin;


    public List<GameObject> CollectedObjects = new List<GameObject>();

    public List<GameObject> ObstacleObjects = new List<GameObject>();

    public void AddCoin(int amount)
    {
        CollectedCoin += amount;
    }

    private void Update()
    {
        if (CollectedCoin < 0)
        {
            CollectedCoin = 0;
        }
    }

    public async void ActiveCollectedObject()
    {
        for (int i = 0; i < CollectedObjects.Count; i++)
        {
            CollectedObjects[i].SetActive(true);
        }
        await Task.Delay(100);
        for (int i = CollectedObjects.Count - 1; i >= 0; i--)
        {
            CollectedObjects.RemoveAt(i);
        }
    }

    public async void ActiveObstacleObject()
    {
        for (int i = 0; i < ObstacleObjects.Count; i++)
        {
            ObstacleObjects[i].SetActive(true);
        }
        await Task.Delay(100);
        for (int i = ObstacleObjects.Count - 1; i >= 0; i--)
        {
            ObstacleObjects.RemoveAt(i);
        }
    }
}
