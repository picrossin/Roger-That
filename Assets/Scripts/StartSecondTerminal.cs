using UnityEngine;

[RequireComponent(typeof(Terminal))]
public class StartSecondTerminal : MonoBehaviour
{
    [SerializeField] private GameObject secondRobot;
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
        secondRobot.SetActive(true);
        Instantiate(sound, transform.position, Quaternion.identity);
    }
}
