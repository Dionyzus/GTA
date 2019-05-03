using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroVoiceOver : MonoBehaviour
{ 
    public GameObject introSub;
    public AudioSource introLine1;
    public AudioSource introLine2;
    public AudioSource introLine3;
    public AudioSource introLine4;
    public AudioSource introLine5;
    public AudioSource pistolShot;
    public AudioSource walkSound;
    public GameObject fullBlack;
    public GameObject leonLucic;
    public GameObject target;
    public GameObject bagSack;
    public GameObject chair;
    public GameObject leon;
    public GameObject deadTarget;
    public GameObject gun;
    public GameObject fadeToBlack;
    public GameObject loadingScreen;
    public GameObject muzzleFlash;
    public GameObject ambienceSounds;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroSubtitles());
    }

    IEnumerator IntroSubtitles()
    {
        yield return new WaitForSeconds(22.5f);
        introSub.GetComponent<TextMeshProUGUI>().text = "I bet you didn't see this coming.";
        introLine1.Play();
        introSub.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        introSub.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(0.25f);
        introSub.GetComponent<TextMeshProUGUI>().text = "Leon no! I can explain!";
        introLine2.Play();
        yield return new WaitForSeconds(2.0f);
        introSub.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(0.5f);
        introSub.GetComponent<TextMeshProUGUI>().text = "Nothing to be explained here.";
        introLine3.Play();
        yield return new WaitForSeconds(1.75f);
        introSub.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(0.5f);
        introSub.GetComponent<TextMeshProUGUI>().text = "Listen! Please!";
        introLine4.Play();
        yield return new WaitForSeconds(1.25f);
        introSub.GetComponent<TextMeshProUGUI>().text = "";
        pistolShot.Play();
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.10f);
        muzzleFlash.SetActive(false);
        fullBlack.SetActive(true);
        chair.SetActive(false);
        target.SetActive(false);
        bagSack.SetActive(false);
        leon.SetActive(false);
        gun.SetActive(false);
        leonLucic.SetActive(true);
        deadTarget.SetActive(true);
        yield return new WaitForSeconds(1);

      
        introSub.GetComponent<TextMeshProUGUI>().text = "Moje ime je Leon Lucic.";
        introLine5.Play();
        yield return new WaitForSeconds(2);
        introSub.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(0.5f);
        introSub.GetComponent<TextMeshProUGUI>().text = "Vladislav Roslyakov me izdao";
        yield return new WaitForSeconds(2.5f);
        introSub.GetComponent<TextMeshProUGUI>().text = "pokusao ubiti. Sada je vrijeme za moju osvetu.";
        yield return new WaitForSeconds(3.3f);
        introSub.GetComponent<TextMeshProUGUI>().text = "";

        fadeToBlack.SetActive(true);
        yield return new WaitForSeconds(2.1f);
        walkSound.Play();
        yield return new WaitForSeconds(3);
        ambienceSounds.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(3);
    }
}
