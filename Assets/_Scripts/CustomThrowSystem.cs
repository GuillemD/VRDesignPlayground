using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomThrowSystem : MonoBehaviour
{
    private List<GameObject> objects = new List<GameObject>(1);
    public XRController controller;
    public float ThrowThreshold = 0.1f;
    public float ThrowForce = 3.0f;

    private bool objectGrabbed = false;

    // Update is called once per frame
    void Update()
    {
        if(controller)
        {
            if (GripPressed(controller) && objects.Count > 0)
            {
                if(!objectGrabbed)
                {
                    foreach (GameObject go in objects)
                    {
                        go.transform.parent = transform;

                        if (go.GetComponent<Rigidbody>())
                        {
                            Rigidbody rb = go.GetComponent<Rigidbody>();
                            rb.isKinematic = true;
                            rb.useGravity = false;
                        }
                        objectGrabbed = true;
                    }
                }
                else
                {
                    foreach (GameObject go in objects)
                    {
                        if (go.GetComponent<Rigidbody>())
                        {
                            Rigidbody rb = go.GetComponent<Rigidbody>();
                            rb.isKinematic = false;
                            rb.useGravity = true;
                        }
                        go.transform.parent = null;
                        objectGrabbed = false;
                    }
                    objects.Clear();
                }
            }

            if (TriggerPressed(controller) && objectGrabbed && objects.Count > 0)
            {
                
                foreach(GameObject go in objects)
                {
                    if(go.GetComponent<Rigidbody>())
                    {
                        Rigidbody rb = go.GetComponent<Rigidbody>();
                        rb.isKinematic = false;
                        rb.useGravity = true;
                        
                        rb.AddForce(controller.transform.forward * ThrowForce, ForceMode.Impulse);
                    }
                    go.transform.parent = null;
                    objectGrabbed = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        objects.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        objects.Remove(other.gameObject);
    }

    private bool GripPressed(XRController con)
    {
        InputHelpers.IsPressed(con.inputDevice, InputHelpers.Button.Grip, out bool gripPressed);
        return gripPressed;
    }

    private bool TriggerPressed(XRController con)
    {
        InputHelpers.IsPressed(con.inputDevice, InputHelpers.Button.Trigger, out bool triggerPressed, ThrowThreshold);
        return triggerPressed;
    }
}
