using UnityEngine;

public class CanvasFacePlayer : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.LookAt(target);
    }
}
