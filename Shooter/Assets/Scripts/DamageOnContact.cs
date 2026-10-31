using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        Life life = other.GetComponent<Life>();

        if (life != null)
        {
            life.Amount -= damage;
        }

        gameObject.SetActive(false);
    }
}