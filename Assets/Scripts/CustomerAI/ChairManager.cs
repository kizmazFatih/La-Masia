using System.Collections.Generic;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    public static ChairManager instance;
    public Transform chairs;

    public List<Transform> emptyChairs = new List<Transform>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        for (int i = 0; i < chairs.childCount; i++)
        {
            emptyChairs.Add(chairs.GetChild(i));
        }
    }
    public void RemoveList(Transform chair)
    {
        emptyChairs.Remove(chair);
    }
    public void AddList(Transform chair)
    {
        emptyChairs.Add(chair);
    }
    public Transform RandomChair()
    {
        Transform selectedChair = emptyChairs[Random.Range(0, emptyChairs.Count)];
        RemoveList(selectedChair);
        return selectedChair;

    }
}
