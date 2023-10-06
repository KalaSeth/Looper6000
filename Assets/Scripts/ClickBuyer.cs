using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickBuyer : MonoBehaviour
{
    public int State;
    public Text BuyText;
    int ActiveShip;

    int coin;
    int kill;
    int Level;
    public Text PLevel;
    public Text pKill;
    public Text pCoin;
    public GameObject Equippedth;
    public Text Price;

    private void Start()
    {
        ActiveShip = PlayerPrefs.GetInt("Chip", 0);
        Cursor.visible = true;
    }

    public void Update()
    {
        coin = PlayerPrefs.GetInt("Coins", 0);
        kill = PlayerPrefs.GetInt("Kills",0);
        Level = PlayerPrefs.GetInt("Level",1);

        PLevel.text = "x" + Level.ToString() + " Level";
        pCoin.text = "x" + coin.ToString();
        pKill.text = "x" + kill.ToString();

        ActiveShip = PlayerPrefs.GetInt("Chip", 0);
        State = ShopThing.instance.Index;

            ShowBuyop();
        
    }

    void ShowBuyop()
    {
        if (State == ActiveShip)
        {
            Equippedth.SetActive(true);
            Price.text = " ";
            BuyText.text = "Equipped";
        }
        else if (State != ActiveShip)
        {
            if (ShopThing.instance.BuyIndex[State] == 0)
            {
                Equippedth.SetActive(false);
                BuyText.text = "Unlock";
                if (State == 0) { Price.text = " "; }
                else if (State == 1) { Price.text = "-500 Coins-"; }
                else if (State == 2) { Price.text = "-1000 Kills-"; }
                else if (State == 3) { Price.text = "-50 Level-"; }
                else if (State == 4) { Price.text = "-1000 Coins-"; }
            }
            else if (ShopThing.instance.BuyIndex[State] == 1)
            {
                Equippedth.SetActive(false);
                Price.text = " ";
                BuyText.text = "Equip";
            }
        }
    }

    void Equipsh()
    {
        ShopThing.instance.BuyIndex[ShopThing.instance.Index] = 1;
        PlayerPrefs.SetInt("Ship" + ShopThing.instance.Index.ToString(), 1);
       
    }

    public void OnClickBuyThing()
    {
        if (State == ActiveShip)
        {

        }
        else if (State != ActiveShip)
        {
            if (ShopThing.instance.BuyIndex[State] == 0)
            {
                if (State == 1)
                {
                    if (coin >= 500)
                    {
                        coin -= 500;
                        PlayerPrefs.SetInt("Coins", coin);
                        Equipsh();
                    }
                }
                else if (State == 2)
                {
                    if (kill >= 1000)
                    {
                        Equipsh();
                    }
                }
                else if (State == 3)
                {
                    if (Level >= 50)
                    {
                        Equipsh();
                    }
                }
                else if (State == 4)
                {
                    if (coin >= 1000)
                    {
                        coin -= 1000;
                        PlayerPrefs.SetInt("Coins", coin);
                        Equipsh();
                    }
                }
            }
            else if (ShopThing.instance.BuyIndex[State] == 1)
            {
                PlayerPrefs.SetInt("Chip", State);
            }
        }                  
    }
}
