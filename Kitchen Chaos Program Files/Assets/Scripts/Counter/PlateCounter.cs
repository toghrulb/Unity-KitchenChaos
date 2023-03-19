using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;


    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private int plateAmount;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if(spawnPlateTimer > 4f) 
        {
            spawnPlateTimer= 0f;
            if(plateAmount < 4)
            {
                plateAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if(plateAmount> 0) 
            {
                plateAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                player.playerRightHand.Activate();
                player.playerLeftHand.Activate();
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
