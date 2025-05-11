using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{
    //public Dictionary<Transform, bool> positions = new Dictionary<Transform, bool>();
    public static Queue instance;

    public int queueSize = 0;
    public List<Transform> positions = new List<Transform>();

    public List<GameObject> customersInQueue = new List<GameObject>();

    public bool coffeTakePosition_busy = false;


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

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            positions.Add(transform.GetChild(i));
        }
    }




    public void AddCustomerQueue(GameObject customer)
    {
        customersInQueue.Add(customer);
    }

    public void RemoveCustomerQueue(GameObject customer)
    {
        customersInQueue.Remove(customer);
        EventDispatcher.SummonEvent("GoNextPosition");
    }








}
