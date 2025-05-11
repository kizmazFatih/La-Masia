using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour, IInteractable
{
    private Canvas myCanvas;
    private Animator animator;

    private NavMeshAgent navMeshAgent;
    [SerializeField] private int placeNumber;
    [SerializeField] private float duration;

    private Vector3 coffeTakePosition = new Vector3(-31f, 1, 6);
    private Vector3 quitShopPosition = new Vector3(-10f, 1, 2);

    private Vector3 targetPosition;

    public CoffeType myOrder;
    public CoffeeSize mySize;

    private bool isCoffeTaked = false;

    public enum CustomerState { InQueue, Waiting, Sitting, Leaving };
    private CustomerState currentState = CustomerState.InQueue;
    private CustomerState previousState;



    void Start()
    {
        animator = GetComponent<Animator>();

        myOrder = (CoffeType)Random.Range(0, 12);
        mySize = (CoffeeSize)Random.Range(0, 3);

        EventDispatcher.RegisterFunction("GoNextPosition", GoNextPosition);
        EventDispatcher.RegisterFunction<GameObject, int>("IsReadyMyOrder", IsReadyMyOrder);

        myCanvas = transform.GetChild(0).GetComponent<Canvas>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        placeNumber = Queue.instance.queueSize;
        targetPosition = Queue.instance.positions[placeNumber].position;
        navMeshAgent.SetDestination(targetPosition);
        Queue.instance.queueSize++;
    }
    void Update()
    {

        WaitMoveComplete();


        if (navMeshAgent.velocity.sqrMagnitude > 0.01f)
        {
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Idle", true);
        }


        if (currentState != previousState)
        {
            switch (currentState)
            {
                case CustomerState.Waiting:
                    SetDuration();
                    break;
                case CustomerState.Sitting:
                    TakeCoffe();
                    break;
                case CustomerState.Leaving:
                    break;
            }

            previousState = currentState;
        }


    }


    void QuitQueue()
    {

        if (placeNumber == 0 && Queue.instance.coffeTakePosition_busy == false)
        {
            //SetDuration();
            Queue.instance.queueSize--;
            placeNumber = -1;
            Queue.instance.RemoveCustomerQueue(gameObject);
            currentState = CustomerState.Waiting;
        }
    }

    void Move()
    {
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
        targetPosition = Queue.instance.coffeTakePosition_busy ? transform.position : coffeTakePosition;
        QuitQueue();
        Move();
        Queue.instance.coffeTakePosition_busy = true;
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


    #region  Waiting
    //Buradan aşağısı müşterinin süresiyle ilgili fonksiyonlar
    //duration: Müşterinin bekleme süresini tutan değişken
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
            Move();
            Queue.instance.coffeTakePosition_busy = false;
            CancelInvoke(nameof(DurationTimer));
            currentState = CustomerState.Leaving;
        }
    }

    [SerializeField] private Transform cupPlace;
    private GameObject cup;
    private Cup cup_cs;
    private int score;
    public void IsReadyMyOrder(GameObject cup1, int score1)
    {
        if (currentState == CustomerState.Waiting)
        {
            CancelInvoke(nameof(DurationTimer));
            RateCoffe();
            cup = cup1;
            cup_cs = cup.GetComponent<Cup>();
            score = score1;
        }
    }
    #endregion

    #region Take Coffe

    public void RateCoffe()
    {
        if (cup_cs.myCoffe == myOrder)
        {
            if (cup_cs.myCoffeSize != mySize)
            {
                score -= 15;
            }
            Debug.Log("Your coffe is perfect!" + score);
            currentState = CustomerState.Sitting;
        }
        else
        {
            Debug.Log("Your coffe is not perfect!");
        }

    }
    Transform selectedChair;
    public void TakeCoffe()
    {
        isCoffeTaked = true;
        cup.transform.parent = cupPlace;

        selectedChair = ChairManager.instance.RandomChair();
        targetPosition = selectedChair.position;

        Move();
        Queue.instance.coffeTakePosition_busy = false;
    }
    #endregion
    void WaitMoveComplete()
    {
        if (targetPosition == quitShopPosition)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {

                Destroy(gameObject);
            }
        }
        else if (targetPosition == selectedChair?.position)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                //Oturma Animasyonu
            }
        }
    }

}