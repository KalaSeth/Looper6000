using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuThing : MonoBehaviour
{
    float Zdistance;
    bool back;
    // Start is called before the first frame update
    void Start()
    {
        Zdistance = -15;
        back = false;
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
