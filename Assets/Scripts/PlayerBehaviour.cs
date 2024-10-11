using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private SpriteRenderer _sr;

    // Start is called before the first frame update
    void Start()
    {
		_sr = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Move(Vector3 pos)
	{
		Vector3 newPos = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);

		if (newPos.x > Camera.main.ScreenToWorldPoint(Screen.width * Vector3.one).x - _sr.bounds.extents.x)
			newPos = new Vector3(Camera.main.ScreenToWorldPoint(Screen.width * Vector3.one).x - _sr.bounds.extents.x, newPos.y, newPos.z);

		if (newPos.x < -Camera.main.ScreenToWorldPoint(Screen.width * Vector3.one).x + _sr.bounds.extents.x)
			newPos = new Vector3(-Camera.main.ScreenToWorldPoint(Screen.width * Vector3.one).x + _sr.bounds.extents.x, newPos.y, newPos.z);

		transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision == null)
			return;

		if (collision.CompareTag("Crystal"))
		{
			collision.GetComponent<CrystalBehaviour>().Kill();
			GameControllerBehaviour.onScoreUpEvent.Invoke();

		}
	}
}
