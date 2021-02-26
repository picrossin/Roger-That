using System.Collections;
using UnityEngine;

public class DeleteAfterSeconds : MonoBehaviour
{
    [SerializeField] private float secondsDestroy = 60.0f;

    private void Start()
    {
        StartCoroutine(Delete());
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(60.0f);
        Destroy(gameObject);
    }
}
