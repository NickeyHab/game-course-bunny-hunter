using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    public GameObject arrowPrefab;
    public int poolSize = 20;
    private List<GameObject> arrowPool;

    void Awake()
    {
        arrowPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.SetActive(false);
            arrowPool.Add(arrow);
        }
    }

    public GameObject GetArrow()
    {
        foreach (GameObject arrow in arrowPool)
        {
            if (!arrow.activeInHierarchy)
            {
                arrow.SetActive(true);
                return arrow;
            }
        }
        arrowPool[0].SetActive(true);
        return arrowPool[0];
    }
}
