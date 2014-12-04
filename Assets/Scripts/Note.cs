using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {

	// Use this for initialization
    bool wasVisible = false;
    public float startY = 7f;
    public float endY = -5f;
    public float onScreenTime = 3f;
    private float speed = 0f;
    private int note = 80;
    private int velocity = 100;
    private NoteManager parentManager;
	void Start () {
        speed = (Mathf.Abs(startY) + Mathf.Abs(endY)) / onScreenTime;
        parentManager = transform.parent.gameObject.GetComponent<NoteManager>();
	}

    public void OnEnable()
    {
        transform.position = new Vector3(((note - 36f) / 15f * 40f) - 20f , startY, transform.position.z);
    }

    public void OnBecameVisible()
    {
            wasVisible = true;
    }
	// Update is called once per frame
	void Update () {
        if (!renderer.isVisible && wasVisible)
        {
            gameObject.SetActive(false);
        }
        transform.Translate(new Vector3(0f, -speed * Time.deltaTime));
	}

    public void OnDisable()
    {
        if (parentManager == null)
        {
            return;
        }
        parentManager.removeNote(note, gameObject);
    }

    public void SetNoteProperties(int noteNumber, int velocity)
    {
        this.note = noteNumber;
        this.velocity = velocity;
    }

    public void SetPlayed()
    {
        gameObject.SetActive(false);
        Debug.Log("Played: " + note);
    }
}
