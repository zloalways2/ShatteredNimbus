using UnityEngine;
using UnityEngine.EventSystems;

public class PlayingTouchAreaBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 offset;

	private PlayerBehaviour player;
	private Camera cam;
	private bool isMouseDown = false;

	// Start is called before the first frame update
	void Start()
    {
		player = FindObjectOfType<PlayerBehaviour>();
		cam = Camera.main;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if (Input.touchCount > 0)
		{
			player.Move(cam.ScreenToWorldPoint(Input.touches[0].position) + offset);
		}

		if (isMouseDown)
		{
			player.Move(cam.ScreenToWorldPoint(Input.mousePosition) + offset);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		offset = player.gameObject.transform.position - cam.ScreenToWorldPoint(Input.mousePosition);

		if (Input.touchCount > 0)
		{
			offset = player.gameObject.transform.position - cam.ScreenToWorldPoint(Input.touches[0].position);
		}

		isMouseDown = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isMouseDown = false;
	}
}
