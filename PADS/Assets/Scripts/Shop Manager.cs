using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int giftIndexMax;
    public List<int> giftList = new List<int>();
    public int giftShopCount = 2;

    public int matIndexMax;
    public List<int> matList = new List<int>();
    public int matShopCount = 2;

    public List<int> shopIDList;
    public ShopItem[] shopButtons;
    public int shopPageNum = 0;

    public ShopPageArrow[] shopArrowList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        giftIndexMax = PlayerInventory.giftDictionary.Count;
        matIndexMax = PlayerInventory.matDictionary.Count;
        SetShopLists();

        shopIDList = new List<int>(giftShopCount + matShopCount);
        shopButtons = this.gameObject.GetComponentsInChildren<ShopItem>();
    }

    public void SetShopLists()
    {

        for (int i  = 0; i < giftIndexMax; i++)
        {
            giftList.Add(i);
        }

        for (int i = 0; i < matIndexMax; i++)
        {
            matList.Add(i);
        }
    }

    public void OpenShop()
    {
        shopPageNum = 0;

        int[] assignedValues = RandomizeItem(giftShopCount, giftList);
        for (int i = 0; i < giftShopCount; i++)
        {
            shopIDList[i] = assignedValues[i];
        }

        assignedValues = RandomizeItem(matShopCount, matList);
        for (int i = 0; i < matShopCount; i++)
        {
            shopIDList[i + giftShopCount] = assignedValues[i];
        }

        RefreshShop();
        shopArrowList[0].uniqueShopItems = shopIDList.Count;
    }

    public int[] RandomizeItem(int indices, List<int> listRange)
    {
        int[] returnVar = new int[indices];
        List<int> intPool = new List<int>(listRange);
        int randomValue = 0;

        for (int i = 0; i < indices; i++)
        {
            randomValue = Random.Range(0, intPool.Count);
            listRange.RemoveAt(randomValue);
            returnVar[i] = randomValue;
        }

        return returnVar;
    }

    public void ChangePage(int pageChange)
    {
        shopPageNum += pageChange;
    }

    public void RefreshShop()
    {
        for (int i = 0; i < shopButtons.Length; i++)
        {
            if (i + shopPageNum > shopIDList.Count)
            {
                shopButtons[i].itemIndex = 0;
            }
            else
            {
                shopButtons[i].itemIndex = i + shopPageNum;
            }
            
        }
    }
}
