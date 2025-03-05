using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour, IInteractable
{
    private Canvas myCanvas;

    private Transform targetTransform;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private int placeNumber;

    void Start()
    {
        EventDispatcher.RegisterFunction("GoNextPosition", GoNextPosition);

        myCanvas = transform.GetChild(0).GetComponent<Canvas>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        placeNumber = Queue.instance.queueSize;
        targetTransform = Queue.instance.positions[placeNumber];
        navMeshAgent.SetDestination(targetTransform.position);
        Queue.instance.queueSize++;
    }


    void QuitQueue()
    {

        if (placeNumber == 0)
        {
            //Kahve alındığında çalışacak aşağısı
            Queue.instance.queueSize--;
            navMeshAgent.SetDestination(new Vector3(5, 1, -6));
            placeNumber = -1;
            Queue.instance.RemoveCustomerQueue(gameObject);
        }

    }
    void GoNextPosition()
    {
        if (placeNumber > 0)
        {
            navMeshAgent.SetDestination(Queue.instance.positions[placeNumber - 1].position);
            placeNumber--;
        }
    }


    public void Interact(Transform handle)
    {
        QuitQueue();
    }

    public void Release(Transform handle)
    {
        throw new System.NotImplementedException();
    }

    public Canvas ShowMyUI()
    {
        if (placeNumber == 0)
        { myCanvas.gameObject.SetActive(true); }
        return myCanvas;
    }


}