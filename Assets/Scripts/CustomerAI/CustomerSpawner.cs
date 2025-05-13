using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomerSpawner : MonoBehaviour
{
    private GameObject customerPrefab;
    [SerializeField] private GameObject[] customers;
    [SerializeField] private int customerCount;

    [SerializeField] private Vector3 spawnPosition;






    void Awake()
    {
        EventDispatcher.RegisterFunction<float>("SetCustomerCount", SetCustomerCount);

    }



    void SetCustomerCount(float popularity)
    {
        switch (popularity)
        {
            case > 80:
                customerCount = Random.Range(4, 6);
                break;
            case > 60:
                customerCount = Random.Range(3, 6);
                break;
            case > 50:
                customerCount = Random.Range(3, 5);
                break;
            case > 40:
                customerCount = Random.Range(2, 5);
                break;
            case > 30:
                customerCount = Random.Range(2, 4);
                break;
            case > 15:
                customerCount = Random.Range(1, 4);
                break;
            default:
                customerCount = 1;
                break;
        }

        StartCoroutine(SpawnCustomer());

    }

    IEnumerator SpawnCustomer()
    {

        yield return new WaitForSeconds(Random.Range(2f, 3f));

        if (customerCount > 0)
        {
            customerPrefab = customers[Random.Range(0, customers.Length)];
            GameObject newCustomer = Instantiate(customerPrefab, spawnPosition, Quaternion.identity);
            Queue.instance.AddCustomerQueue(newCustomer);
            customerCount--;
            StartCoroutine(SpawnCustomer());
        }

    }


}
