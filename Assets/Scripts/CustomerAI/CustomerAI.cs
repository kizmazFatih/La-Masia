using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour, IInteractable
{
    private Canvas myCanvas;
    private Animator animator;

    private Transform targetTransform;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private int placeNumber;
    [SerializeField] private float duration;

    private Vector3 coffeTakePosition = new Vector3(-31f, 1, 6);
    private Vector3 quitShopPosition;

    private Vector3 targetPosition;

    public CoffeType myOrder;
    public CoffeeSize mySize;

    private bool isCoffeTaked = false;




    void Start()
    {
        animator = GetComponent<Animator>();

        myOrder = (CoffeType)Random.Range(0, 12);
        mySize = (CoffeeSize)Random.Range(0, 3);

        EventDispatcher.RegisterFunction("GoNextPosition", GoNextPosition);

        myCanvas = transform.GetChild(0).GetComponent<Canvas>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        placeNumber = Queue.instance.queueSize;
        targetTransform = Queue.instance.positions[placeNumber];
        navMeshAgent.SetDestination(targetTransform.position);
        Queue.instance.queueSize++;
    }
    void Update()
    {
        if (navMeshAgent.velocity.sqrMagnitude > 0.01f)
        {
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Idle", true);
        }

    }


    void QuitQueue()
    {

        if (placeNumber == 0)
        {
            //SetDuration();
            Queue.instance.queueSize--;
            placeNumber = -1;
            Queue.instance.RemoveCustomerQueue(gameObject);
        }
        navMeshAgent.SetDestination(targetPosition);

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
        targetPosition = coffeTakePosition;
        QuitQueue();
    }

    public void Release(Transform handle)
    {
        throw new System.NotImplementedException();
    }

    public Transform ShowMyUI()
    {
        if (placeNumber == 0)
        { myCanvas.gameObject.SetActive(true); }
        return myCanvas.transform.parent;
    }

    public void SetDuration()
    {
        InvokeRepeating(nameof(DurationTimer), 0, 1f);
    }

    void DurationTimer()
    {
        duration -= 1f;
        if (duration == 0 && !isCoffeTaked)
        {
            targetPosition = quitShopPosition;
            QuitQueue();
            CancelInvoke(nameof(DurationTimer));
        }
    }


}