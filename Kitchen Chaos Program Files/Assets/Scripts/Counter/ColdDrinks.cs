using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdDrinks : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
            player.playerRightHand.Activate();
            player.playerLeftHand.Activate();
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

            kitchenObject.SetKitchenObjectParent(player);

    }
}