using UnityEngine;
using System.Collections;

public interface INoteHandler {
    void PlayNote(int noteNumber, int velocity, NoteInputType inputType);
}
