using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public struct AsteroidShard {
	public Mesh mesh;
	public Material material;
	public Vector3 position;
	public Quaternion rotation;
	public Vector3 scale;
	public Vector3 velocity;
    public Quaternion rotationVelocity;

	public void update()
	{
		position += velocity * Time.deltaTime;
        rotation *= rotationVelocity;
        NormalizeQuaternion(ref rotation);
	}

    /// <summary>
    /// Normalizes the quaternion.
    /// Source: https://forum.unity.com/threads/quaternion-to-matrix-conversion-failed.36159/
    /// </summary>
    /// <param name="q">Q.</param>
    void NormalizeQuaternion (ref Quaternion q)
    {
        float sum = 0;
        for (int i = 0; i < 4; ++i)
            sum += q[i] * q[i];
        float magnitudeInverse = 1 / Mathf.Sqrt(sum);
        for (int i = 0; i < 4; ++i)
            q[i] *= magnitudeInverse;
    }
}

public class Asteroid : MonoBehaviour {

    [SerializeField] bool useUnityTriggers = true;
    [SerializeField] string[] triggeringTags; // Which tags will trigger the explosion
    [SerializeField] string[] triggeringNames; // Which names will trigger the explosion

    private delegate void drawReaction_delegate();
    drawReaction_delegate drawReaction;

    //private Rigidbody

	[SerializeField] Vector3 initialVelocity = Vector3.zero;
	[SerializeField] Vector3 initialRotationSpeed = Vector3.zero;
	
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotationSpeed = Vector3.zero;
	
    [SerializeField] GameObject[] crackedBitsPrefabs;
	private AsteroidShard[] asteroidShards;
	[SerializeField] int minimumNumberOfBits = 4;
	[SerializeField] int maximumNumberOfBits = 8;
    private float shards_time = 5f; // shards lifetime after the asteroid exploded

	[SerializeField] DustCloud[] dustPrefabs;
	
	[SerializeField] int startingLife = 100;
	private int life;

	private AudioSource au;

	void Start () {
		life = startingLife;
		velocity = initialVelocity;
		rotationSpeed = initialRotationSpeed;
		if(maximumNumberOfBits < minimumNumberOfBits)
		{
			Debug.LogWarning(name+": maximum number of bits is LESS than minimum number of bits, which makes no sense. Now both set to "+minimumNumberOfBits);
			maximumNumberOfBits = minimumNumberOfBits;
		}

        if (useUnityTriggers)
        {
            if (!GetComponent<Rigidbody>())
                gameObject.AddComponent<Rigidbody>();
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            if (!GetComponent<Collider>())
                gameObject.AddComponent<BoxCollider>();
            GetComponent<Collider>().isTrigger = true;
        }

		au = GetComponent<AudioSource>();
        shards_time = (au != null )? Mathf.Min(au.clip.length + 0.1f, 5f) : 5f;
	}
	
	void Update () {
		
		Vector3 pos = transform.position;
		pos += velocity * Time.deltaTime;
		transform.position = pos;
		
		transform.Rotate (rotationSpeed);
//
        if (drawReaction != null)
            drawReaction();
	}
	
	public void explode()
	{
        drawReaction += () => {UpdateAndRenderShards();};

        Destroy (GetComponent<Rigidbody>()); // Don't collide anymore

        GetComponent<Renderer>().enabled = false; // hide the asteroid

		// prevent calling explode if already marked for destruction

		// Hide asteroid's renderer

		if(crackedBitsPrefabs.Length == 0)
		{
			Debug.LogWarning(name+": Cracked Bits Prefab is not set.");
			return;
		}
		else
		{
			// Create bits
			int numberOfBits = Random.Range(minimumNumberOfBits, maximumNumberOfBits);
			asteroidShards = new AsteroidShard[numberOfBits];
			for(int i=0; i<numberOfBits; i++)
			{
				// AsteroidCrackedBit a = (AsteroidCrackedBit) Instantiate(
				// 	crackedBitsPrefabs[Random.Range (0,crackedBitsPrefabs.Length)],
				// 	transform.position,
				// 	transform.rotation);
				// a.setVelocity ((new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)) * 0.1f));
				// a.transform.localScale *= Random.Range(0.5f,1f);
				int randomPrefabIndex = Random.Range (0,crackedBitsPrefabs.Length);
				asteroidShards[i] = new AsteroidShard();
				asteroidShards[i].mesh = crackedBitsPrefabs[randomPrefabIndex].GetComponent<MeshFilter>().sharedMesh;
				asteroidShards[i].material = crackedBitsPrefabs[randomPrefabIndex].GetComponent<Renderer>().sharedMaterial;
                asteroidShards[i].position = transform.position + 0.5f * new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), Random.Range(-1f,1f));
                asteroidShards[i].rotation = Quaternion.Euler(new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), Random.Range(-1f,1f)));
                asteroidShards[i].scale = transform.localScale * Random.Range(0.2f,0.5f);
				asteroidShards[i].velocity = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), Random.Range(-1f,1f)) + velocity;
                asteroidShards[i].rotationVelocity = Quaternion.Euler(new Vector3(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f)));
			}
			
			// Create Dust Clouds
			for(int i=0; i<dustPrefabs.Length; i++)
			{
				DustCloud d = (DustCloud) Instantiate(
					dustPrefabs[i],
					transform.position,
					transform.rotation);
			}

			// If audiosource present, destroy this object later
			if(au != null)
			{
				au.Play ();
				GetComponent<BoxCollider>().enabled = false;
			}
		}
	}
	
	public void hit(int damage)
	{
		life -= damage;
		if(life<=0)
		{
			explode();
		}
	}

    void OnTriggerEnter(Collider c)
    {
        bool tagCheck = false, maneCheck = false;

        if (triggeringTags.Length > 0)
        {
            for (int i = 0; i < triggeringTags.Length; i++)
            {
                if (triggeringTags[i] == c.tag)
                    tagCheck = true;
            }
        }
        else
        {
            tagCheck = true;
        }

        if (triggeringNames.Length > 0)
        {
            for (int i = 0; i < triggeringNames.Length; i++)
            {
                if (triggeringNames[i] == c.name)
                    maneCheck = true;
            }
        }
        else
        {
            maneCheck = true;
        }
        
        if(tagCheck && maneCheck)
            explode();
    }

	void UpdateAndRenderShards()
	{
        float scaleTweening = (shards_time > 1f) ? 1f : shards_time;
		for(int i=0; i<asteroidShards.Length; i++)
		{
			Graphics.DrawMesh(
				asteroidShards[i].mesh, 
                Matrix4x4.TRS(asteroidShards[i].position, asteroidShards[i].rotation, asteroidShards[i].scale * scaleTweening),
				asteroidShards[i].material,
				0,
				null
				);
			asteroidShards[i].update();
		}

        shards_time -= Time.deltaTime;
        if (shards_time <= 0f)
        {
             Destroy(this.gameObject);
             Destroy(this);
        }
	}

}
