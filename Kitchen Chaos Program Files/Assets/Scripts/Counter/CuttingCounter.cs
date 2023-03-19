using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.playerRightHand.Deactivate();
                    player.playerLeftHand.Deactivate();
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress= 0;

                    CuttingRecipeSO cuttingRecipe = GetCuttingResipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipe.cuttingProgressMax
                    });
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();
                }
            }
            else
            {
                player.playerRightHand.Activate();
                player.playerLeftHand.Activate();
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipe = GetCuttingResipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipe.cuttingProgressMax
            });
            if (cuttingProgress >= cuttingRecipe.cuttingProgressMax)
            {
                KitchenObjectSO outputObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputObject, this);
            } 
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputObject)
    {
        CuttingRecipeSO cuttingRecipe = GetCuttingResipeSOWithInput(inputObject);
        if (cuttingRecipe)
            return true;
        else
            return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputObject)
    {
        CuttingRecipeSO cuttingRecipe = GetCuttingResipeSOWithInput(inputObject);
        if(cuttingRecipe) 
            return cuttingRecipe.output;
        else
            return null;
    }

    private CuttingRecipeSO GetCuttingResipeSOWithInput(KitchenObjectSO inputObject)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputObject)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}