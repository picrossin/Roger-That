using System.Collections;
using UnityEngine;

public class InstructionsAudio : MonoBehaviour
{
    [SerializeField] private GameObject instructionsAudio;

    private void Start()
    {
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(instructionsAudio, transform.position, Quaternion.identity);
    }
}
