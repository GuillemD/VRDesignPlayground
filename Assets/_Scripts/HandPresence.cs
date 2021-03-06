using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHand;
    private Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("Error. Did not find corresponding controller model");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHand = Instantiate(handPrefab, transform);
            handAnimator = spawnedHand.GetComponent<Animator>();
        }
    }
    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                spawnedHand.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHand.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
        
        //Debug
        //if(targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
        //    Debug.Log("pressing primary button");
        
        //if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && (triggerValue > 0.1f))
        //    Debug.Log("trigger pressed" +  triggerValue);
       
        //if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primaryAxisValue) && (primaryAxisValue != Vector2.zero))
        //    Debug.Log("Primary touchpad" + primaryAxisValue);
    }

}
