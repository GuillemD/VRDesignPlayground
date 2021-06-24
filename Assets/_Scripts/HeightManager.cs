using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HeightManager : MonoBehaviour
{
    public GameObject player;
    public XRController controller;

    private float minY = 1.0f;
    private float maxY = 3.0f;
    private float step = 0.1f;

    
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if(isButtonPressed(controller, InputHelpers.Button.PrimaryButton))
        {
            HeightDecrease();
        }

        if (isButtonPressed(controller, InputHelpers.Button.SecondaryButton))
        {
            HeightIncrease();
        }

    }

    void HeightIncrease()
    {
        if (controller)
        {
            if (player.transform.position.y < maxY)
            {
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + step, player.transform.position.z);
            }
        }
    }

    void HeightDecrease()
    {
        if(controller)
        {
            if(player.transform.position.y > minY)
            {
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - step, player.transform.position.z);
            }
        }
    }

    public bool isButtonPressed(XRController con, InputHelpers.Button button)
    {
        InputHelpers.IsPressed(con.inputDevice, button, out bool pressed, 0.3f);
        return pressed;
    }
}
