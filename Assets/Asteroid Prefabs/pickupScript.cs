using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 FBN;

    void Start()
    {
     FBN=new Vector3(this.transform.position.x, 6, this.transform.position.z); 
     StartCoroutine(KYS());
    }
    public IEnumerator KYS()
    {
        yield return new WaitForSeconds(15);
        GameObject.Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, FBN, .05f);
    }
}
