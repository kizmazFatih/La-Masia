using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{

    void Interact(Transform handle);

    void Release(Transform handle);

    Canvas ShowMyUI();


}
