using UnityEngine;

public class CrystalBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
	[SerializeField] private AudioClip _sound;

	private SpriteRenderer _sr;
    private Vector3 targetMovePos;

    // Start is called before the first frame update
    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        CheckAlive();
        Move();
	}

	private void Move()
	{
        transform.position = Vector3.MoveTowards(transform.position, targetMovePos, speed * Time.deltaTime);
	}

	private void CheckAlive()
    {
		if (_sr.bounds.max.y < Camera.main.ScreenToWorldPoint(Vector3.down).y)
        {
            Kill();
        }
    }

    public void Spawn(Vector3 pos)
    {
        transform.position = pos;
		targetMovePos = new Vector3(transform.position.x, Camera.main.ScreenToWorldPoint(Vector3.down).y - _sr.bounds.extents.y - 1f, transform.position.z);
	}

	public void Kill()
    {
        if (_sound != null)
        {
            SoundBehaviour.onPlayAudioClipSound.Invoke(_sound);
        }
		gameObject.SetActive(false);
    }
}
