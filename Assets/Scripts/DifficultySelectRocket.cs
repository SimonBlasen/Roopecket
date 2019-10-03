using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultySelectRocket : MonoBehaviour
{
    public Transform[] difficultyRockets;
    public Transform[] texts;

    public Image image;
    public float fadeWait = 2f;
    public float fadeSpeed = 1f;

    private Transform hovered = null;

    private int selected = -1;

    private float startFade = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selected == -1)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            for (int i = 0; i < difficultyRockets.Length; i++)
            {
                if (hovered != null && difficultyRockets[i].GetInstanceID() == hovered.GetInstanceID())
                {
                    hovered.localScale = Vector3.Lerp(hovered.localScale, new Vector3(0.12f, 0.12f, 0.12f), 0.08f);
                }
                else
                {
                    difficultyRockets[i].localScale = Vector3.Lerp(difficultyRockets[i].localScale, new Vector3(0.1f, 0.1f, 0.1f), 0.08f);
                }
            }

            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < difficultyRockets.Length; i++)
                {
                    hovered = hit.transform.parent;
                }
            }
            else
            {
                hovered = null;
            }
        }

        if (selected != -1)
        {
            for (int i = 0; i < difficultyRockets.Length; i++)
            {
                if (i != selected)
                {
                    difficultyRockets[i].position += new Vector3(0f, 0f, 1f) * Time.deltaTime * 10f;
                    texts[i].position += new Vector3(0f, 0f, 1f) * Time.deltaTime * 10f;
                }
            }

            startFade += Time.deltaTime;

            if (startFade >= fadeWait)
            {
                float perc = (startFade - fadeWait) * fadeSpeed;

                if (perc >= 1.2f)
                {
                    SceneManager.LoadScene("Main_Menu_3");
                }

                if (perc > 1f)
                {
                    perc = 1f;
                }
                image.color = new Color(0f, 0f, 0f, perc);
            }
        }

        if (selected == -1 && Input.GetMouseButtonDown(0) && hovered != null)
        {
            int selectedDiff = 0;
            if (hovered.GetInstanceID() == difficultyRockets[1].GetInstanceID())
            {
                selectedDiff = 1;
            }
            else if (hovered.GetInstanceID() == difficultyRockets[2].GetInstanceID())
            {
                selectedDiff = 2;
            }

            selected = selectedDiff;

            int selRock = 0;
            if (selectedDiff == 0)
            {
                selRock = 2;
            }
            else if (selectedDiff == 1)
            {
                selRock = 1;
            }
            else
            {
                selRock = 0;
            }

            SavedGame.OwnedRockets[0] = selRock;
            SavedGame.LastPlayedRocket = 0;
            Statics.selectedRocket = SavedGame.OwnedRockets[SavedGame.LastPlayedRocket];
        }
    }
}
