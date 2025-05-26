using UnityEngine;

public class ChocolateSyrup : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }



    public void FillChocolateSyrup(Transform cup)
    {
        animator.SetTrigger("Fill");
        cup.GetComponent<Cup>().chocolate_syrup += 1f;
    }


}
