using System.Collections;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Life vida = other.GetComponent<Life>();

        if (vida != null)
        {
            StartCoroutine(Die(vida));
        }
    }

    private IEnumerator Die(Life vida)
    {
        yield return new WaitForSeconds(1.0f);

        vida.Amount = 0f;
    }
}