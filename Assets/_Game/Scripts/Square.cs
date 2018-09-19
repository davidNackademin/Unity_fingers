using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

public class Square : MonoBehaviour {

    private TapGestureRecognizer tapGesture;
    private PanGestureRecognizer panGesture;

    private bool dragging = false;
    private Vector3 offset;



	// Use this for initialization
	void Start () {
        tapGesture = new TapGestureRecognizer();
        //tapGesture.ThresholdSeconds = 3.0f;
        tapGesture.StateUpdated += TapGesture_StateUpdated;
        FingersScript.Instance.AddGesture(tapGesture);

        panGesture = new PanGestureRecognizer();
        panGesture.ThresholdUnits = 0.0f;
        //panGesture.AllowSimultaneousExecutionWithAllGestures();
        panGesture.PlatformSpecificView = gameObject;
        panGesture.StateUpdated += PanGesture_StateUpdated;
        FingersScript.Instance.AddGesture(panGesture);


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PanGesture_StateUpdated(GestureRecognizer gesture)
    {
        switch (gesture.State)
        {
            case GestureRecognizerState.Began:
                Vector3 pos = new Vector3(gesture.FocusX, gesture.FocusY, 0);
                pos = Camera.main.ScreenToWorldPoint(pos);

                offset = transform.position - pos;

                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    dragging = true;
                }
                break;
            case GestureRecognizerState.Executing:
                if ( dragging) 
                {
                    Vector3 newPos = new Vector3(gesture.FocusX, gesture.FocusY);
                    newPos = Camera.main.ScreenToWorldPoint(newPos);
                   // newPos.z = 0f;

                    transform.position = newPos + offset;
                }
                break;
            case GestureRecognizerState.Ended:
                if(dragging) 
                {
                    dragging = false;
                }
                break;
        }
    }


    void TapGesture_StateUpdated(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended) {
            Vector2 pos = new Vector2(gesture.FocusX, gesture.FocusY);
            pos = Camera.main.ScreenToWorldPoint(pos);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if(hit.collider != null && hit.collider.gameObject == gameObject) {
                Debug.Log("Tap recognized");

            }

        }

    }

}
