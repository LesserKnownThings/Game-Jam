using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    private bool enter = false;
    // Start is called before the first frame update
    void Start()
    {
        if (enter == false)
            StartCoroutine(your_timer());

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator your_timer()
    {
        enter = true;
        Debug.Log("Your enter Coroutine at" + Time.time);
        yield return new WaitForSeconds(10.0f);
        enter = false;
        Destroy(gameObject);
    }

}
