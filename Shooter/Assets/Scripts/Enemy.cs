using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int pointsAmount;

    private void Awake()
    {
        var life = GetComponent<Life>();
        life.onDeath.AddListener(DestroyEnemy);
    }

    private void Start()
    {
        EnemyManager.SharedInstance.AddEnemy(this);
    }

    private void DestroyEnemy()
    {
        EnemyFSM fsm = GetComponentInChildren<EnemyFSM>();
        if (fsm != null)
        {
            fsm.enabled = false;
        }

        Animator anim = GetComponent<Animator>();

        anim.SetBool("Shot Bullet Bool", false);

        anim.SetTrigger("Play Die");

        Invoke("PlayDestruction", 0.2f);

        var life = GetComponent<Life>();
        life.onDeath.RemoveListener(DestroyEnemy);

        Destroy(gameObject, 1);

        ScoreManager.SharedInstance.Amount += pointsAmount;
        EnemyManager.SharedInstance.RemoveEnemy(this);
    }

    void PlayDestruction()
    {
        ParticleSystem explosion = GetComponentInChildren<ParticleSystem>();
        explosion.Play();
    }
}