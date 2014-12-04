using UnityEngine;
using System.Collections;

public class MidiDevicePlayer : MonoBehaviour, INoteHandler {

    public GameObject bassDrum;
    private Color originalColor;
	// Use this for initialization
	void Start () {
        originalColor = bassDrum.renderer.material.color;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (bassDrum.renderer.material.color != originalColor)
        {
            bassDrum.renderer.material.color = Color.Lerp(bassDrum.renderer.material.color, originalColor, Time.deltaTime);
        }
	
	}

    public void PlayNote(int noteNumber, int velocity, NoteInputType inputType)
    {
        if (inputType != NoteInputType.USER)
        {
            return;
        }
        bassDrum.transform.position = new Vector3(((noteNumber - 36f) / 15f * 40f) - 20f , bassDrum.transform.position.y, bassDrum.transform.position.z);
        bassDrum.renderer.material.color = Color.red;

    }
}
