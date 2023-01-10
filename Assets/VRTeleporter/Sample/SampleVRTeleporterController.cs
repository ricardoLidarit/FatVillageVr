using UnityEngine;
using WebXR;

public class SampleVRTeleporterController : MonoBehaviour {

    public enum UpdateTypes
    {
        Flat,
        Controller
    }

    [SerializeField]
    private VRTeleporter teleporter = null;
    [SerializeField]
    private UpdateTypes updateType = UpdateTypes.Flat;
    [SerializeField]
    private WebXRController controller = null;

    public void SetUpdateType(UpdateTypes updateType)
    {
        this.updateType = updateType;
    }

    public void SetWebXRConrtoller(WebXRController controller)
    {
        this.controller = controller;
    }

    void Start()
    {
        if (teleporter == null)
        {
            teleporter = GetComponent<VRTeleporter>();
        }
    }

    void Update()
    {
        switch (updateType)
        {
            case UpdateTypes.Flat:
                UpdateFlat();
                break;
            case UpdateTypes.Controller:
                UpdateController();
                break;
        }
    }

    void UpdateFlat()
    {
        if (Input.GetMouseButtonDown(0))
        {
            teleporter.ToggleDisplay(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            teleporter.Teleport();
            teleporter.ToggleDisplay(false);
        }
    }

    void UpdateController()
    {
        Vector2 thumbstick = controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick);
        Vector2 touchpad = controller.GetAxis2D(WebXRController.Axis2DTypes.Touchpad);

        teleporter.ToggleDisplay(thumbstick.y > 0 || touchpad.y > 0);

        if (controller.GetButtonUp(WebXRController.ButtonTypes.Thumbstick) && thumbstick.y > 0)
        {
            teleporter.Teleport();
        }

        if (controller.GetButtonUp(WebXRController.ButtonTypes.Touchpad) && touchpad.y > 0)
        {
            teleporter.Teleport();
        }
    }
}
