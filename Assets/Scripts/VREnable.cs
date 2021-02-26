using UnityEngine;
using UnityEngine.XR;

public class VREnable : MonoBehaviour
{
    [SerializeField] private bool enableVR;
    
    private void Start()
    {
        XRSettings.enabled = enableVR;
    }
}
