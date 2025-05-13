using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

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

        myOrder = CoffeType.Espresso;//(CoffeType)Random.Range(0, 12);
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
        if (Input.GetKeyDown(KeyCode.P)) { }

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
            Debug.Log(currentState);
            switch (currentState)
            {
                case CustomerState.Waiting:
                    SetDuration();
                    break;
                case CustomerState.Sitting:
                    TakeCoffe();
                    break;
                case CustomerState.Leaving:
                    LeaveFromCafe();
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
        if (currentState != CustomerState.InQueue) return;
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
            cup = cup1;
            cup_cs = cup.GetComponent<Cup>();
            score = score1;
            RateCoffe();
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
            currentState = CustomerState.Sitting;
        }

    }
    Transform selectedChair;
    float hangingOutTime;
    public void TakeCoffe()
    {
        isCoffeTaked = true;
        animator.SetBool("CoffeTaked", true);
        Payement();

        cup.transform.parent = cupPlace;
        cup.transform.localScale = cup.transform.localScale / 2f;
        cup.transform.localPosition = Vector3.zero;
        cup.transform.localRotation = Quaternion.Euler(0, 0, 0);
        cup.GetComponent<Product>().enabled = false;

        cup.GetComponent<Rigidbody>().isKinematic = true;

        selectedChair = ChairManager.instance.RandomChair();
        targetPosition = selectedChair.position;

        Move();
        Queue.instance.coffeTakePosition_busy = false;


        hangingOutTime = Random.Range(15f, 30f);
        InvokeRepeating(nameof(HangingOut), 0f, 1f);
    }


    void HangingOut()
    {
        hangingOutTime -= 1;
        if (hangingOutTime <= 0)
        {
            currentState = CustomerState.Leaving;
            CancelInvoke(nameof(HangingOut));
        }
    }

    #endregion

    #region Leaving
    void LeaveFromCafe()
    {
        animator.SetBool("Sitting", false);
        animator.SetBool("CoffeTaked", false);

        cup.transform.parent = null;
        cup.transform.localScale = cup.transform.localScale * 2f;
        cup.transform.localPosition = Vector3.zero;
        cup.transform.localRotation = Quaternion.Euler(0, 0, 0);
        cup.GetComponent<Product>().enabled = true;

        targetPosition = quitShopPosition;
        Move();
    }



    #endregion

    int price;
    void Payement()
    {

        switch (cup.GetComponent<Cup>().myCoffe)
        {
            case CoffeType.Espresso:
                price = 8;
                break;
            case CoffeType.Cappuccino:
                price = 12;
                break;
            case CoffeType.Latte:
                price = 12;
                break;
            case CoffeType.Mocha:
                price = 13;
                break;
            case CoffeType.Macchiato:
                price = 12;
                break;
            case CoffeType.FlatWhite:
                price = 12;
                break;
            case CoffeType.Cortado:
                price = 12;
                break;
            case CoffeType.Americano:
                price = 11;
                break;
            case CoffeType.IcedAmericano:
                price = 12;
                break;
            case CoffeType.IcedLatte:
                price = 13;
                break;
            case CoffeType.IcedCappuccino:
                price = 13;
                break;
            case CoffeType.IcedMocha:
                price = 14;
                break;
            default:
                break;
        }
        switch (cup.GetComponent<Cup>().myCoffeSize)
        {
            case CoffeeSize.Small:
                price += 0;
                break;
            case CoffeeSize.Medium:
                price += 3;
                break;
            case CoffeeSize.Large:
                price += 5;
                break;
            default:
                break;
        }
        CoinSystem.instance.AddCoin(price);
    }





    void WaitMoveComplete()
    {
        if (targetPosition == quitShopPosition)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {

                Destroy(gameObject);
            }
        }
        else if (selectedChair != null)
        {
            if (targetPosition != selectedChair?.position) return;
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {

                animator.SetBool("Sitting", true);
                transform.forward = selectedChair.forward;
                transform.position = selectedChair.position + new Vector3(0, -0.5f, 0);


            }
        }
    }

}