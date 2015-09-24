using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public GameObject MovementControl;
    private int count = 0;
    private bool canSwipe = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (MovementControl.GetComponent<SwipeDetector>().SwipeState == enum_SwipeState.UP && canSwipe)
        {
            MovementControl.GetComponent<SwipeDetector>().SwipeState = enum_SwipeState.NONE;
            canSwipe = false;
            StartCoroutine("Jump");
            //gameObject.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Mathf.PingPong(Time.time, 1));
        }

        //
        Debug.Log(MovementControl.GetComponent<SwipeDetector>().SwipeState);
	}

    IEnumerator Jump()
    {
        float elapsedTime = 0;
        while(elapsedTime < 0.5f)
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Mathf.PingPong((elapsedTime / 1.4f), 1));
            elapsedTime += Time.deltaTime;
           
            yield return new WaitForEndOfFrame();
        }
        canSwipe = true;
    }
}
