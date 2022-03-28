using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn_Trap : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private LayerMask triggerTarget;
    [SerializeField] private float rayRange;
    private Enemy behaviour;
    private float randomZ, randomX;
    private Transform tf;

    private void Start()
    {
        tf = GetComponent<Transform>();
    }

    private void Update()
    {
        Raycasting();
    }

    private void Raycasting()
    {
        RaycastHit hit;
        Ray ray = new Ray(tf.position, tf.TransformDirection(Vector3.up));
        Vector3 tfOffset = new Vector3(randomX, -0.293312f, randomZ);
        Vector3 lineRange = new Vector3(ray.origin.x, ray.origin.y + rayRange, ray.origin.z);
        Debug.DrawLine(ray.origin, lineRange, Color.green);
        if (Physics.Raycast(ray, out hit, rayRange, triggerTarget))
        {
            randomZ = Random.Range(-12, 5);
            randomX = Random.Range(-19, 22);
            behaviour = prefab.GetComponent<Enemy>();
            behaviour.objective = hit.transform;
            behaviour.originPosition = hit.transform;
            Instantiate(prefab, tf.position + tfOffset, prefab.transform.rotation);

            this.GetComponent<Enemy_Spawn_Trap>().enabled = false;
        }
    }
}