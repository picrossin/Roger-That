using UnityEngine;

public class Terminal : MonoBehaviour
{
    [SerializeField] private KeyLightManager keyLightManager;
    
    public delegate void ActivateAction();
    public event ActivateAction OnActivated;
    
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Texture2D onEmissionMap;

    private bool _activated;

    private void OnTriggerEnter(Collider other)
    {
        if (!_activated && other.transform.CompareTag(playerTag))
        {
            ActivateTerminal();
        }   
    }

    private void ActivateTerminal()
    {
        _activated = true;
        OnActivated?.Invoke();

        MeshRenderer renderer = GetComponent<MeshRenderer>();

        renderer.material.EnableKeyword("_EMISSION");
        renderer.material.SetTexture("_EmissionMap", onEmissionMap);
        
        keyLightManager.AddTerminal();
    }
}
