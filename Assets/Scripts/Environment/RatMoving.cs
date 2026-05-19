using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]

public class Squirrel : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 3f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Границы движения на террейне")]
    [SerializeField] private float minX = 350f;
    [SerializeField] private float maxX = 600f;
    [SerializeField] private float minZ = 350f;
    [SerializeField] private float maxZ = 600f;

    [Header("Клики")]
    [SerializeField] private AudioClip squeakSound;

    [Header("Маски NavMesh")]
    [SerializeField] private int walkableAreaMask = -1;

    private NavMeshAgent agent;
    private bool isWaiting = false;
    private AudioSource audioSource;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        // Исключить воду
        if (walkableAreaMask != -1)
        {
            agent.areaMask = walkableAreaMask;
        }

        audioSource = GetComponent<AudioSource>();

        MoveToRandomPoint();
    }

    private void Update()
    {
        if (isWaiting) return;

        if (agent != null && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            StartWaiting();
        }

        // Поворот в направлении движения
        if (agent != null && agent.velocity.magnitude > 0.1f)
        {
            Vector3 direction = agent.velocity.normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void MoveToRandomPoint()
    {
        if (agent == null) return;
        if (!agent.isOnNavMesh || !agent.enabled) return;

        Vector3 randomPoint = GetRandomPoint();

        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(randomPoint, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(randomPoint);
        }
        else
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, walkableAreaMask))
            {
                agent.SetDestination(hit.position);
            }
            else
            {
                Invoke(nameof(MoveToRandomPoint), 1f);
            }
        }
    }

    private Vector3 GetRandomPoint()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        float y = GetTerrainHeight(x, z);
        return new Vector3(x, y, z);
    }

    private float GetTerrainHeight(float x, float z)
    {
        Terrain terrain = Terrain.activeTerrain;
        if (terrain != null)
        {
            return terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.transform.position.y;
        }
        return transform.position.y;
    }

    private void StartWaiting()
    {
        if (agent == null) return;
        if (!agent.isOnNavMesh || !agent.enabled) return;

        isWaiting = true;
        agent.isStopped = true;
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        Invoke(nameof(EndWaiting), waitTime);
    }

    private void EndWaiting()
    {
        if (agent == null) return;
        if (!agent.isOnNavMesh || !agent.enabled) return;

        isWaiting = false;
        agent.isStopped = false;
        MoveToRandomPoint();
    }
}