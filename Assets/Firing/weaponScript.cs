using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    public float TTL = 2;
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

        GameObject.Destroy(this.gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
