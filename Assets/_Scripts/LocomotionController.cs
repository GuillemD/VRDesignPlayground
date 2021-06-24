using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRController rightTeleportRay;
    public XRController leftTeleportRay;
 
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    public XRRayInteractor rightInteractorRay;
    public XRRayInteractor leftInteractorRay;

    public bool enableRightTeleport { get; set; } = true;
    public bool enableLeftTeleport { get; set; } = true;

    private Vector3 pos;
    private Vector3 norm;
    private int index = 0;
    private bool validTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3();
        norm = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {

        if (leftTeleportRay)
        {
            bool leftRayHovering = leftInteractorRay.TryGetHitInfo(out pos, out norm, out index, out validTarget); //see if left ray is hovering UI
            leftTeleportRay.gameObject.SetActive(enableLeftTeleport && IsActivated(leftTeleportRay) && !leftRayHovering);
        }
        if (rightTeleportRay)
        {
            bool rightRayHovering = rightInteractorRay.TryGetHitInfo(out pos, out norm, out index, out validTarget); //check if right ray is hovering ui
            rightTeleportRay.gameObject.SetActive(enableRightTeleport && IsActivated(rightTeleportRay) && !rightRayHovering);
        }
    }

    public bool IsActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool activated, activationThreshold);
        return activated;
    }
}
