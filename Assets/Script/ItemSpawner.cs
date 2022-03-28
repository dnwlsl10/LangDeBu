using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<Transform> targets;
    public List<ITem> items;
    public GameObject spawnerField;

    void Start()
    {
     
        for (int i = 0; i < targets.Count; i++)
        {
            int itemsCount =Random.Range(0, 3);
            Instantiate<ITem>(items[itemsCount], targets[i]);
        }
    }
}
