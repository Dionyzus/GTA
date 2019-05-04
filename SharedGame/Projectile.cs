using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    [SerializeField] float damage;
    [SerializeField] Transform bulletHole;
    [SerializeField] Transform bloodImpact;
    bool flag;

    Vector3 destination;

    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    void Update()
    {
        if(IsDestinationReached())
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (destination != Vector3.zero)
        {
            return;
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit,5f))
        {
            CheckDestructable(hit);
        }
    }

    void CheckDestructable(RaycastHit hitInfo)
    {
        var destructable = hitInfo.transform.GetComponent<Destructable>();
        destination = hitInfo.point + hitInfo.normal * .01f;

        if (hitInfo.transform.tag == "Enemy" || hitInfo.transform.tag == "Player")
        {
            Transform blood = Instantiate(bloodImpact, destination, Quaternion.LookRotation(hitInfo.normal) * Quaternion.Euler(0,180f,0));
            flag = true;
        }
        else if (flag == false)
        {
            Transform hole = Instantiate(bulletHole, destination, Quaternion.LookRotation(hitInfo.normal) * Quaternion.Euler(0, 180f, 0));
            hole.SetParent(hitInfo.transform);
        }
        if (destructable == null)
        {
            return;
        }
        destructable.TakeDamage(damage);
        flag = false;
    }
    bool IsDestinationReached()
    {
        if (destination == Vector3.zero)
        {
            return false;
        }

        Vector3 directionToDestination = destination - transform.position;
        float dot = Vector3.Dot(directionToDestination, transform.forward);

        if (dot < 0)
        {
            return true;
        }
        return false;
    }
}
