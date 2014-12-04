using UnityEngine;
using System.Collections.Generic;

public class NoteManager : MonoBehaviour, INoteHandler
{
    public int NotePoolSize;
    public GameObject NotePrefab;
    private List<GameObject> noteObjects;
    public List<int> validNoteNumbers;
    private Dictionary<int, List<GameObject>> noteMap = new Dictionary<int,List<GameObject>>();

    // Use this for initialization
    void Start()
    {
        noteObjects = new List<GameObject>();
        for (int i = 0; i < NotePoolSize; i++)
        {
            GameObject go = (GameObject)GameObject.Instantiate(NotePrefab);
            go.transform.parent = transform;
            noteObjects.Add(go);
            go.SetActive(false);
        }
        foreach (int note in validNoteNumbers)
        {
            noteMap.Add(note, new List<GameObject>());
        }
    }

    public void PlayNote(int noteNumber, int velocity, NoteInputType inputType)
    {
        if (velocity == 0)
        {
            return;
        }
        if (!validNoteNumbers.Contains(noteNumber))
        {
            return;
        }
        if (inputType == NoteInputType.TEMPLATE)
        {
            foreach (GameObject note in noteObjects)
            {
                if (!note.activeInHierarchy)
                {
                    note.GetComponent<Note>().SetNoteProperties(noteNumber, velocity);
                    note.SetActive(true);
                    addNoteToCollection(noteNumber, note);

                    return;
                }
            }
        }
        else if (inputType == NoteInputType.USER)
        {
            Debug.Log(noteNumber);
            handleUserNoteInput(noteNumber);
        }
    }

    private void addNoteToCollection(int note, GameObject go)
    {
        List<GameObject> currObjects = null;
        noteMap.TryGetValue(note, out currObjects);
        currObjects.Add(go);
    }

    public void removeNote(int noteNumber, GameObject go)
    {
        List<GameObject> currObjects = null;
        noteMap.TryGetValue(noteNumber, out currObjects);
        if (currObjects.Count == 0)
        {
            return;
        }
        currObjects.Remove(go);
        Debug.Log("Notes of number " + noteNumber + " left: " + currObjects.Count);
    }

    private void handleUserNoteInput(int noteNumber)
    {
        List<GameObject> currObjects = null;
        noteMap.TryGetValue(noteNumber, out currObjects);
        if (currObjects == null)
        {
            return;
        }
        else if (currObjects.Count == 0)
        {
            return;
        }
        currObjects[0].GetComponent<Note>().SetPlayed();
    }

}
