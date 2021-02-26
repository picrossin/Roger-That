using UnityEngine;

public class StartThirdTerminal : MonoBehaviour
{
    [SerializeField] private GameObject sound;
    
    private Terminal _terminal;
    
    private void Start()
    {
        _terminal = GetComponent<Terminal>();
        _terminal.OnActivated += ActivateRoom;
    }
    
    private void OnDisable()
    {
        _terminal.OnActivated -= ActivateRoom;
    }

    private void ActivateRoom()
    {
        Instantiate(sound, transform.position, Quaternion.identity);
    }
}
