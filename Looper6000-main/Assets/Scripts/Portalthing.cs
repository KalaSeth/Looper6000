using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portalthing : MonoBehaviour
{
    public Animator PortalState;
    public float CloseTime;
    public float StartTime;

    private void Start()
    {
        PortalState = GetComponent<Animator>();
    }

    public void Openportal()
    {
        PortalState.SetTrigger("Open");
    }

    public void ClosePortal()
    {
        Invoke("Openportal", CloseTime);
        PortalState.SetTrigger("Close");
    }

    public void AccessPortal()
    {
        Invoke("ClosePortal", StartTime);
    }
}
