using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ActionS : MonoBehaviour
{
    public VRInputModule m_InputModule;
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean click;
    Paused pa;

    void Update()
    {
        if(GetGrab())
        {
            pa=GameObject.Find("GameMaster").gameObject.GetComponent<Paused>();
            pa.Toggle();
        }
    }
    public bool GetGrab()
    {
        return click.GetState(handType);
    }
}
