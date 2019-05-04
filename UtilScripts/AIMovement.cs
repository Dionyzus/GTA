using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] int xPos;
    [SerializeField] int zPos;
    [SerializeField] GameObject NPCDestination;
    [SerializeField] int xMaxRange;
    [SerializeField] int xMinRange;
    [SerializeField] int zMaxRange;
    [SerializeField] int zMinRange;
    [SerializeField] float moveSpeed;
    [SerializeField] Actions actions;

    void Start()
    {
        xPos = Random.Range(xMinRange, xMaxRange);
        zPos = Random.Range(zMinRange, zMaxRange);
        NPCDestination.transform.position = new Vector3(xPos, 0, zPos);
        StartCoroutine(RandomMovement());
    }

    void Update()
    {
        transform.LookAt(NPCDestination.transform);
        actions.Run();
        transform.position = Vector3.MoveTowards(transform.position, NPCDestination.transform.position, moveSpeed * Time.deltaTime);
        if (transform.position == NPCDestination.transform.position)
        {
            actions.Stay();
        }
    }

    IEnumerator RandomMovement()
    {
        yield return new WaitForSeconds(10);
        actions.Stay();
        xPos = Random.Range(xMinRange, xMaxRange);
        zPos = Random.Range(zMinRange, zMaxRange);
        NPCDestination.transform.position = new Vector3(xPos, 0, zPos);
        StartCoroutine(RandomMovement());
    }
}
