using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float delay;

    void OnEnable()
    {
        CancelInvoke();

        Invoke("HideObject", delay);
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}