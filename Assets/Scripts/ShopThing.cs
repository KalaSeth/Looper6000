using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopThing : MonoBehaviour
{
    public GameObject Scrollthing;
    Vector3 NewPos;
    public int Index;

    public int[] BuyIndex;

    public GameObject Lockedobj;

    // Start is called before the first frame update
    void Start()
    {
        Index = 0;
        Scrollthing.transform.position = new Vector3(0, 0, 0);
        NewPos = new Vector3(0, 0, 0);

        for (int i = 1; i < 5; i++)
        {
            BuyIndex[i] = PlayerPrefs.GetInt("Ship" + i.ToString(), 0);
        }
        BuyIndex[0] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (BuyIndex[Index] == 1)
        {
            Lockedobj.SetActive(false);
        }
        else Lockedobj.SetActive(true);

        Scrollthing.transform.position = new Vector3(Mathf.Lerp(transform.position.x, NewPos.x, 0.05f), 0, transform.position.z);
    }

    public void OnClickBuyThing()
    {
        if (BuyIndex[Index] == 0)
        {
            BuyIndex[Index] = 1;
            PlayerPrefs.SetInt("Ship"+Index.ToString(),BuyIndex[Index]);
        }
    }


    public void onClickLeft()
    {
        if (Index != 4)
        {
            NewPos = new Vector3(NewPos.x - 20, 0, NewPos.z);
            Index++;
        }
    }
    public void onClickRight()
    {
        if (Index != 0)
        {
            NewPos = new Vector3(NewPos.x + 20, 0, NewPos.z);
            Index--;
        }
    }
}
