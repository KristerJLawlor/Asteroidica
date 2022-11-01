using UnityEngine;
using System.Collections;

public class MouseBullets : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("Mouse hits "+hit.collider.name);
                Asteroid a = hit.collider.GetComponent<Asteroid>();
                if (a)
                    a.explode();
            }
        }

	}
}
