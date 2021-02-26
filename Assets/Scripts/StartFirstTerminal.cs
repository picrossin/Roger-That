using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Terminal))]
public class StartFirstTerminal : MonoBehaviour
{
    [SerializeField] private GameObject lights;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject firstRobot;
    [SerializeField] private GameObject minimap;
    [SerializeField] private GameObject terminalOnAudio;
    [SerializeField] private GameObject instructionsAudio;
    
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
        lights.SetActive(true);
        door.SetActive(false);
        minimap.SetActive(true);
        StartCoroutine(EnableRobot());
    }

    private IEnumerator EnableRobot()
    {
        Instantiate(terminalOnAudio, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(6f);
        firstRobot.SetActive(true);
        yield return new WaitForSeconds(6f);
        Instantiate(instructionsAudio, transform.position, Quaternion.identity);
    }
}
