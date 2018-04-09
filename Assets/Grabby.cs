using UnityEngine;
using VRTK;

public class Grabby : MonoBehaviour {

    public AudioClip mcClippySound;
    private VRTK_InteractableObject io;
    private VRTK_ControllerReference controllerReference;

    void OnEnable()
    {
        io = GetComponent<VRTK_InteractableObject>();
        io.InteractableObjectGrabbed += Io_InteractableObjectGrabbed;
        io.InteractableObjectUngrabbed += Io_InteractableObjectUngrabbed;
    }

    private void DoHaptics() {
        if (controllerReference != null)
        {
            VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, mcClippySound);
        }
    }


    private void Io_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
       controllerReference = VRTK_ControllerReference.GetControllerReference(e.interactingObject);
        InvokeRepeating("DoHaptics", 0f, mcClippySound.length);
    }

    private void Io_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    { 
        CancelInvoke("DoHaptics");
        if (controllerReference != null) {
           Debug.Log(VRTK_DeviceFinder.GetControllerVelocity(controllerReference));
           VRTK_ControllerHaptics.CancelHapticPulse(controllerReference);
        }
       controllerReference = null;
    }
} 
