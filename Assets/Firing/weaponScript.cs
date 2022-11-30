using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    public float TTL;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KMS());
    }
    public IEnumerator KMS()
    {
        yield return new WaitForSeconds(TTL);
        GameObject.Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "laser")
        {
            GameObject.Destroy(this.gameObject);

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
