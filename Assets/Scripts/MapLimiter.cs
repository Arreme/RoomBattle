using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLimiter : MonoBehaviour
{
    public Transform Target;
    //private Rigidbody2D _rigidbody;
    [SerializeField] private float speed;
    private int count = 0;
    private float _minDistanceToTarget = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        //_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DistanceToTarget() > _minDistanceToTarget)
            ApplyAtracctionForce();
    }

 

    private void ApplyAtracctionForce()
    {
        //var force = TargetDir() * ConstForce * DistanceToTarget();
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Target.position, step);
    }

    private Vector2 TargetDir()
    {
        var dir = Target.position - transform.position;
        return dir.normalized;
    }

    private float DistanceToTarget()
    {
        return Vector2.Distance(Target.position, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponentInParent<WeaponDetect>().destroyBallon();
        }
    }
}
