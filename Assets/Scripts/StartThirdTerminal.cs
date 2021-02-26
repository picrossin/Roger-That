using UnityEngine;

public class StartThirdTerminal : MonoBehaviour
{
    [SerializeField] private GameObject sound;

    private void Start()
    {
        Instantiate(sound, transform.position, Quaternion.identity);
    }
}
