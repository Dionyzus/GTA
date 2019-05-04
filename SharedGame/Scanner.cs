using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharedGame.Extensions;

[RequireComponent(typeof(SphereCollider))]
public class Scanner : MonoBehaviour
{
    [SerializeField] float scanSpeed;

    [SerializeField] [Range(0, 360)] float fieldOfView;

    [SerializeField] public LayerMask layerMask;

    SphereCollider rangeTrigger;

    public float ScanRange
    {
        get
        {
            if (rangeTrigger == null)
            {
                rangeTrigger = GetComponent<SphereCollider>();
            }
            return rangeTrigger.radius;
        }
    }
    public event System.Action OnScanReady;

    void PrepareScan()
    {
        GameManager.Instance.Timer.Add(() =>
        {
            if (OnScanReady != null)
            {
                OnScanReady();
            }
        }, scanSpeed);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(fieldOfView / 2) * GetComponent<SphereCollider>().radius);
        Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(-fieldOfView / 2) * GetComponent<SphereCollider>().radius);
    }
    Vector3 GetViewAngle(float angle)
    {
        float radian = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
    public List<T> ScanForTargets<T>()
    {
        print("Scanning");
        List<T> targets = new List<T>();
        Collider[] results = Physics.OverlapSphere(transform.position, ScanRange);

        for (int i = 0; i < results.Length; i++)
        {
            var player = results[i].transform.GetComponent<T>();

            if (player == null)
            {
                continue;
            }
            if (!transform.IsInLineOfSight(results[i].transform.position, fieldOfView, layerMask, Vector3.up))
            {
                continue;
            }
            else
            {
                targets.Add(player);
            }
        }
        PrepareScan();
        return targets;
    }
}
