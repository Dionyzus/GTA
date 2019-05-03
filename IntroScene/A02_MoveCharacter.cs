using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A02_MoveCharacter : MonoBehaviour {

    public AudioSource leftFoot;
    public AudioSource rightFoot;
    public bool steppingLeft = true;
    public GameObject mainCharacter;
    public int numberOfStepsTaken;
    
	// Use this for initialization
	void Start () {
        StartCoroutine(WalkSequence());
	}
	
	// Update is called once per frame
	void Update () {
        mainCharacter.transform.Translate(0, 0, 0.35f * Time.deltaTime);
	}

    IEnumerator WalkSequence()
    {
        yield return new WaitForSeconds(0.4f);
        while (numberOfStepsTaken < 21)
        {
            yield return new WaitForSeconds(0.5f);
            if (steppingLeft == true)
            {
                leftFoot.Play();
                steppingLeft = false;
            }
            else
            {
                rightFoot.Play();
                steppingLeft = true;
            }
            numberOfStepsTaken += 1;
        }
     
        mainCharacter.SetActive(false);
    }
}
