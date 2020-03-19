using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean click;
    shop shop,shop1,shop2;
    RaycastHit hit;
    private LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        shop = GameObject.Find("standardTurretItem").GetComponent<shop>();
        shop1 = GameObject.Find("laserTurretItem").GetComponent<shop>();
        shop2 = GameObject.Find("IceTower").GetComponent<shop>();

    }
    private void Update()
    {
        UpdateLine();
        if (GetGrab())
        {
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag == "standardTurretItem")
                {
                    shop.SelectStandardTurret();
                }
                else if (hit.collider.gameObject.tag == "laserTurretItem")
                {
                    shop1.SelectlaserTurret();
                }
                
            }
        }
    }
    private void UpdateLine()
    {
        float targetLength = m_DefaultLength;

        hit = CreateRaycast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * targetLength);
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }
        m_Dot.transform.position = endPosition;
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }
    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);

        return hit;
    }
    public bool GetGrab()
    {
        return click.GetState(handType);
    }
}
