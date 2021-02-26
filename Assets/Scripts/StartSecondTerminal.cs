using UnityEngine;

[RequireComponent(typeof(Terminal))]
public class StartSecondTerminal : MonoBehaviour
{
    [SerializeField] private GameObject secondRobot;
    
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
        secondRobot.SetActive(true);
    }
}
