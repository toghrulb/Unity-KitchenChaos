using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    public GameObject Hands;

    public void Activate()
    {
        Hands.SetActive(true);
    }
    
    public void Deactivate()
    {
        Hands.SetActive(false);
    }

}
