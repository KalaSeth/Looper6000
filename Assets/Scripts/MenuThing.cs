using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuThing : MonoBehaviour
{
    float Zdistance;
    bool back;

    public Text BestL;
    public Text Kill;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Coins", 1000);
        PlayerPrefs.SetInt("Kills", 999);
        PlayerPrefs.SetInt("Level", 40);


        BestL.text = "Best Loops : " + PlayerPrefs.GetInt("BestLoop",0).ToString();
        Kill.text = "Kills : " + PlayerPrefs.GetInt("Kills",0).ToString();

        Zdistance = -15;
        back = false;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Zdistance <= -9.8f)
        {
            if (back == false)
            {
                Zdistance = Mathf.Lerp(Zdistance, -10.8f, 0.5f * Time.deltaTime);
                if (Zdistance <= -10.7f)
                {
                    back = true;
                }
            }

            if (back == true)
            {
                Zdistance = Mathf.Lerp(Zdistance, -9.9f, 0.5f * Time.deltaTime);
                if (Zdistance >= -10)
                {
                    back = false;
                }
            }
        }
       
        transform.position = new Vector3(transform.position.x, transform.position.y, Zdistance);

    }
}
