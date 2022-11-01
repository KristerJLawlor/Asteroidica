using UnityEngine;
using System.Collections;

public class DustCloud : MonoBehaviour {
	
	private Vector3 cameraPosition;
	public float lifeTimeSeconds = 5f;
	public float spin = 0.1f;
	Color c;

	float rotationPhase = 0f;

	// Use this for initialization
	void Start () {
		transform.localScale = Vector3.zero;
		c = transform.GetComponent<Renderer>().material.GetColor("_Color");

		rotationPhase = 1000f * (Random.Range(-100f, 100f)
		                         + transform.position.x
		                         + transform.position.y); // purpose of phase is to make two near dust clouds not to look the very same.
	}
	
	// Update is called once per frame
	void Update () {
		cameraPosition = Camera.main.transform.position;

		transform.up = -(transform.position - cameraPosition).normalized;
		transform.localScale += (new Vector3(0.2f,0.2f,0.2f))*Time.deltaTime;

		transform.Rotate (transform.up, rotationPhase + 10f * Time.time * spin, Space.World);
		//transform.localRotation *= Quaternion.Euler (0f, 10f * Time.time * spin, 0f); // Alternative
		
		c.a -= 1f/lifeTimeSeconds * Time.deltaTime;
		transform.GetComponent<Renderer>().material.SetColor("_Color", c);
		
		if(lifeTimeSeconds<=0)
		{
			Destroy(gameObject);
			Destroy(this);
		}
		else
		{
			lifeTimeSeconds-=Time.deltaTime;
		}
	}
}