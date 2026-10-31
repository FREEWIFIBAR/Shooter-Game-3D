using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Sight))]
public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState
    {
        GoToBase,
        AttackBase,
        ChasePlayer,
        AttackPlayer
    }

    public EnemyState currentState;

    private Sight _sight;

    private Transform _baseTransform;

    public float baseAttackDistance, playerAttackDistance;

    private NavMeshAgent _agent;

    private Animator _animator;

    private float _lastShootTime;

    public float shootRate;

    public GameObject shootingPoint;

    public ParticleSystem fireEffect;

    public AudioSource shootSound;

    private void Awake()
    {
        _sight = GetComponent<Sight>();
        _baseTransform = GameObject.FindWithTag("Base").transform;
        _agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.GoToBase:
                GoToBase();
                break;

            case EnemyState.AttackBase:
                AttackBase();
                break;

            case EnemyState.ChasePlayer:
                ChasePlayer();
                break;

            case EnemyState.AttackPlayer:
                AttackPlayer();
                break;
        }
    }

    void GoToBase()
    {
        _animator.SetBool("Shot Bullet Bool", false);

        _agent.isStopped = false;
        _agent.SetDestination(_baseTransform.position);

        if (_sight.detectedTarget != null)
        {
            currentState = EnemyState.ChasePlayer;
        }

        float distanceToBase = Vector3.Distance(transform.position, _baseTransform.position);
        if (distanceToBase < baseAttackDistance)
        {
            currentState = EnemyState.AttackBase;
        }
    }

    void AttackBase()
    {
        _agent.isStopped = true;
        LookAt(_baseTransform.position);
        ShootTarget();
    }

    void ChasePlayer()
    {
        _animator.SetBool("Shot Bullet Bool", false);

        if (_sight.detectedTarget == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        _agent.isStopped = false;
        _agent.SetDestination(_sight.detectedTarget.transform.position);

        float distanceToPlayer = Vector3.Distance(transform.position,
            _sight.detectedTarget.transform.position);

        if (distanceToPlayer < playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }
    }

    void AttackPlayer()
    {
        _agent.isStopped = true;

        if (_sight.detectedTarget == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        LookAt(_sight.detectedTarget.transform.position);
        ShootTarget();

        float distanceToPlayer = Vector3.Distance(transform.position,
            _sight.detectedTarget.transform.position);

        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    void ShootTarget()
    {
        if (Time.timeScale > 0)
        {
            _animator.SetBool("Shot Bullet Bool", true);

            var timeSinceLastShoot = Time.time - _lastShootTime;
            if (timeSinceLastShoot < shootRate)
            {
                return;
            }

            _lastShootTime = Time.time;

            Invoke("FireBullet", 0.3f);
        }
    }

    void FireBullet()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetFirstPooledObject();
        bullet.layer = LayerMask.NameToLayer("Enemy Bullet");
        bullet.transform.position = shootingPoint.transform.position;
        bullet.transform.rotation = shootingPoint.transform.rotation;
        bullet.SetActive(true);

        if (fireEffect != null)
        {
            fireEffect.Play();
        }

        Instantiate(shootSound, transform.position, transform.rotation).GetComponent<AudioSource>().Play();
    }

    private void LookAt(Vector3 targetPos)
    {
        var directionToLook = Vector3.Normalize(targetPos - transform.position);
        directionToLook.y = 0;
        transform.parent.forward = directionToLook;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }
}