﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;

namespace Valve.VR.Extras
{
    public class SteamVR_LaserPointer : MonoBehaviour
    {
        public SteamVR_Behaviour_Pose pose;

        //public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.__actions_default_in_InteractUI;
        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");
        public SteamVR_Input_Sources handType;
        public SteamVR_Action_Boolean click;
        shop shop;
        BuildManager buildManager;
        Node node;
        MainMenu mu;
        GameManager gm;
        Paused pa;

        public GameObject gamemanager;
        public TurretBlueprint standardTurret;
        public TurretBlueprint laserTurret;
        public TurretBlueprint iceTurret;
        public bool active = true;
        public Color color;
        public float thickness = 0.002f;
        public Color clickColor = Color.green;
        public GameObject holder;
        public GameObject pointer;
        bool isActive = false;
        public bool addRigidBody = false;
        public Transform reference;

        public event PointerEventHandler PointerIn;
        public event PointerEventHandler PointerOut;
        public event PointerEventHandler PointerClick;

        Transform previousContact = null;


        private void Start()
        {
            buildManager = BuildManager.instance;
            if (pose == null)
                pose = this.GetComponent<SteamVR_Behaviour_Pose>();
            if (pose == null)
                Debug.LogError("No SteamVR_Behaviour_Pose component found on this object");

            if (interactWithUI == null)
                Debug.LogError("No ui interaction action has been set on this component.");


            holder = new GameObject();
            holder.transform.parent = this.transform;
            holder.transform.localPosition = Vector3.zero;
            holder.transform.localRotation = Quaternion.identity;

            pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pointer.transform.parent = holder.transform;
            pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
            pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
            pointer.transform.localRotation = Quaternion.identity;
            BoxCollider collider = pointer.GetComponent<BoxCollider>();
            if (addRigidBody)
            {
                if (collider)
                {
                    collider.isTrigger = true;
                }
                Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
                rigidBody.isKinematic = true;
            }
            else
            {
                if (collider)
                {
                    Object.Destroy(collider);
                }
            }
            Material newMaterial = new Material(Shader.Find("Unlit/Color"));
            newMaterial.SetColor("_Color", color);
            pointer.GetComponent<MeshRenderer>().material = newMaterial;
        }

        public virtual void OnPointerIn(PointerEventArgs e)
        {
            if (PointerIn != null)
                PointerIn(this, e);
        }

        public virtual void OnPointerClick(PointerEventArgs e)
        {
            if (PointerClick != null)
                PointerClick(this, e);
        }

        public virtual void OnPointerOut(PointerEventArgs e)
        {
            if (PointerOut != null)
                PointerOut(this, e);
        }


        private void Update()
        {
            if (!isActive)
            {
                isActive = true;
                this.transform.GetChild(0).gameObject.SetActive(true);
            }

            float dist = 100f;

            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);

            if (previousContact && previousContact != hit.transform)
            {
                PointerEventArgs args = new PointerEventArgs();
                args.fromInputSource = pose.inputSource;
                args.distance = 0f;
                args.flags = 0;
                args.target = previousContact;
                OnPointerOut(args);
                previousContact = null;
            }
            if (bHit && previousContact != hit.transform)
            {
                PointerEventArgs argsIn = new PointerEventArgs();
                argsIn.fromInputSource = pose.inputSource;
                argsIn.distance = hit.distance;
                argsIn.flags = 0;
                argsIn.target = hit.transform;
                OnPointerIn(argsIn);
                previousContact = hit.transform;
            }
            if (!bHit)
            {
                previousContact = null;
            }
            if (bHit && hit.distance < 100f)
            {
                dist = hit.distance;
            }

            if (bHit && interactWithUI.GetStateUp(pose.inputSource))
            {
                PointerEventArgs argsClick = new PointerEventArgs();
                argsClick.fromInputSource = pose.inputSource;
                argsClick.distance = hit.distance;
                argsClick.flags = 0;
                argsClick.target = hit.transform;
                OnPointerClick(argsClick);
            }

            if (interactWithUI != null && interactWithUI.GetState(pose.inputSource))
            {
                pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
                pointer.GetComponent<MeshRenderer>().material.color = clickColor;
            }
            else
            {
                pointer.transform.localScale = new Vector3(thickness, thickness, dist);
                pointer.GetComponent<MeshRenderer>().material.color = color;
            }
            pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
            if (GetGrab())
            {
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.tag);
                    
                    //shop2 = GameObject.Find("IceTower").GetComponent<shop>();
                    if (hit.collider.gameObject.tag == "standardTurretItem")
                    {
                        //shop = hit.collider.gameObject.GetComponent<shop>();
                        SelectStandardTurret();
                    }
                    else if (hit.collider.gameObject.tag == "laserTurretItem")
                    {
                      //  shop1 = GameObject.Find("laserTurretItem").GetComponent<shop>();
                        SelectlaserTurret();
                    }
                    else if (hit.collider.gameObject.tag == "IceTower")
                    {
                        //  shop1 = GameObject.Find("laserTurretItem").GetComponent<shop>();
                        SelectIceTurret();
                    }
                    else if (hit.collider.gameObject.tag == "Node")
                    {
                        node = hit.collider.gameObject.GetComponent<Node>();
                        node.OnMouseDown();
                    }
                    else if (hit.collider.gameObject.tag == "play")
                    {
                        mu = GameObject.Find("Main").gameObject.GetComponent<MainMenu>();
                        mu.play();
                    }
                    else if (hit.collider.gameObject.tag == "exit")
                    {
                        mu = GameObject.Find("Main").gameObject.GetComponent<MainMenu>();
                        mu.quit();
                    }
                    else if (hit.collider.gameObject.tag == "Nomal")
                    {
                        mu = GameObject.Find("Main").gameObject.GetComponent<MainMenu>();
                        mu.Nomal();
                    }
                    else if (hit.collider.gameObject.tag == "Snow")
                    {
                        mu = GameObject.Find("Main").gameObject.GetComponent<MainMenu>();
                        mu.Winter();
                    }
                    else if (hit.collider.gameObject.tag == "Forest")
                    {
                        mu = GameObject.Find("Main").gameObject.GetComponent<MainMenu>();
                        mu.Forest();
                    }
                    else if (hit.collider.gameObject.tag == "back")
                    {
                        mu = GameObject.Find("Main").gameObject.GetComponent<MainMenu>();
                        mu.back();
                    }
                    else if (hit.collider.gameObject.tag == "Menu")
                    {
                        gm = GameObject.Find("GameMaster").gameObject.GetComponent<GameManager>();
                        gm.Menu();
                    }
                    else if (hit.collider.gameObject.tag == "Continue")
                    {
                        pa = GameObject.Find("GameMaster").gameObject.GetComponent<Paused>();
                        pa.Toggle();
                    }

                }
            }

        }
        public bool GetGrab()
        {
            return click.GetState(handType);
        }
        public void SelectStandardTurret()
        {
            Debug.Log(standardTurret.prefab);
            buildManager.SelectTurretToBuild(standardTurret);
        }
        public void SelectlaserTurret()
        {
            Debug.Log(laserTurret.prefab);
            buildManager.SelectTurretToBuild(laserTurret);


        }
        public void SelectIceTurret()
        {
            Debug.Log(iceTurret.prefab);
            buildManager.SelectTurretToBuild(iceTurret);
        }
    }

    public struct PointerEventArgs
    {
        public SteamVR_Input_Sources fromInputSource;
        public uint flags;
        public float distance;
        public Transform target;
    }

    public delegate void PointerEventHandler(object sender, PointerEventArgs e);

}