using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A01_CameraSwitch : MonoBehaviour {

    public GameObject firstCamera;
    public GameObject secondCamera;
    public GameObject thirdCamera;
    public GameObject credLeadDesigner;
    public GameObject credStory;
    public GameObject credEngine;
    public GameObject credGameName;
    public GameObject fourthCamera;
    public GameObject fifthCamera;

    public int waitTimeForCredits = 3;
    public float waitTimeForCameraSwitch = 5f;
    public float waitForThirdCamera = 3f;

	// Use this for initialization
	void Start () {
        StartCoroutine(CameraSwitcher());
	}

    IEnumerator CameraSwitcher()
    {
        yield return new WaitForSeconds(waitTimeForCredits);
        credLeadDesigner.SetActive(true);

        yield return new WaitForSeconds(waitTimeForCredits);
        credStory.SetActive(true);

        yield return new WaitForSeconds(waitForThirdCamera);
        thirdCamera.SetActive(true);
        firstCamera.SetActive(false);

        yield return new WaitForSeconds(2);
        credEngine.SetActive(true);


        yield return new WaitForSeconds(waitTimeForCameraSwitch);
        secondCamera.SetActive(true);
        thirdCamera.SetActive(false);
        credGameName.SetActive(true);

        yield return new WaitForSeconds(6);
        secondCamera.SetActive(false);
        fourthCamera.SetActive(true);

        yield return new WaitForSeconds(9.5f);
        fourthCamera.SetActive(false);
        fifthCamera.SetActive(true);
    }
	
}
