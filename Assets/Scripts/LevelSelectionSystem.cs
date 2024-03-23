using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelectionSystem : MonoBehaviour
{
    [SerializeField] private int currentWorld;
    [Header("Objects")]
    [SerializeField] private GameObject worldText;
    [SerializeField] private List<GameObject> worldButtons = new List<GameObject>();
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;
    [Header("RectTransform Anchoring Settings")]
    [SerializeField] private Vector2 centerAnchorMin;
    [SerializeField] private Vector2 centerAnchorMax;
    [SerializeField] private Vector2 leftAnchorMin;
    [SerializeField] private Vector2 leftAnchorMax;
    [SerializeField] private Vector2 rightAnchorMin;
    [SerializeField] private Vector2 rightAnchorMax;
    [Header("Animation Settings")]
    [SerializeField] private float worldMoveDuration;
    [SerializeField] private float worldMoveOffsetTime;

    private void Awake()
    {
        foreach (GameObject button in worldButtons)
        {
            button.GetComponent<RectTransform>().anchorMax = centerAnchorMax;
            button.GetComponent<RectTransform>().anchorMin = centerAnchorMin;
            button.GetComponent<Button>().interactable = false;
            button.SetActive(false);
        }
        currentWorld = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        worldButtons[currentWorld].SetActive(true);
        worldText.GetComponent<TMP_Text>().text = worldButtons[currentWorld].GetComponent<WorldButton>().WorldName;
        if (currentWorld == 0)
        {
            leftButton.GetComponent<Button>().interactable = false;
        }
        if (currentWorld == worldButtons.Count-1)
        {
            rightButton.GetComponent<Button>().interactable = false;
        }
        worldButtons[currentWorld].GetComponent<Button>().interactable = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // changes to previous or next level
    public void ChangeLevel(bool rightward)
    {
        GameObject oldWorld = worldButtons[currentWorld];
        if (rightward)
        {
            currentWorld++;
        }
        else
        {
            currentWorld--;
        }
        leftButton.GetComponent<Button>().interactable = false;
        rightButton.GetComponent<Button>().interactable = false;
        oldWorld.GetComponent<Button>().interactable = false;
        worldButtons[currentWorld].GetComponent<Button>().interactable = false;
        worldButtons[currentWorld].SetActive(true);
        worldText.GetComponent<TMP_Text>().text = "";
        StartCoroutine(ShiftWorlds(oldWorld, worldButtons[currentWorld], rightward, worldMoveDuration, worldMoveOffsetTime));
    }

    // changes to the previous level
    public void Left()
    {
        if (currentWorld > 0)
        {
            if (currentWorld == worldButtons.Count - 1)
            {
                rightButton.GetComponent<Button>().interactable = true;
            }
            currentWorld--;
            worldButtons[currentWorld + 1].SetActive(false);
            worldButtons[currentWorld].SetActive(true);
            worldText.GetComponent<TMP_Text>().text = worldButtons[currentWorld].GetComponent<WorldButton>().WorldName;
            if (currentWorld == 0)
            {
                leftButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    // changes to the next level
    public void Right()
    {
        if (currentWorld < worldButtons.Count - 1)
        {
            if (currentWorld == 0)
            {
                leftButton.GetComponent<Button>().interactable = true;
            }
            currentWorld++;
            worldButtons[currentWorld - 1].SetActive(false);
            worldButtons[currentWorld].SetActive(true);
            worldText.GetComponent<TMP_Text>().text = worldButtons[currentWorld].GetComponent<WorldButton>().WorldName;
            if (currentWorld == worldButtons.Count - 1)
            {
                rightButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    // shifts the worlds
    private IEnumerator ShiftWorlds(GameObject oldWorldButton, GameObject newWorldButton, bool rightward, float moveDuration, float moveOffset)
    {
        float elapsedTime = 0f;
        // set new world's position to the right spot
        if (rightward)
        {
            newWorldButton.GetComponent<RectTransform>().anchorMax = rightAnchorMax;
            newWorldButton.GetComponent<RectTransform>().anchorMin = rightAnchorMin;
        }
        else
        {
            newWorldButton.GetComponent<RectTransform>().anchorMax = leftAnchorMax;
            newWorldButton.GetComponent<RectTransform>().anchorMin = leftAnchorMin;
        }

        // shift the worlds
        while (elapsedTime < moveDuration + moveOffset)
        {
            if (elapsedTime < moveDuration)
            {
                if (rightward)
                {
                    oldWorldButton.GetComponent<RectTransform>().anchorMax = Vector2.Lerp(centerAnchorMax, leftAnchorMax, elapsedTime / moveDuration);
                    oldWorldButton.GetComponent<RectTransform>().anchorMin = Vector2.Lerp(centerAnchorMin, leftAnchorMin, elapsedTime / moveDuration);
                }
                else
                {
                    oldWorldButton.GetComponent<RectTransform>().anchorMax = Vector2.Lerp(centerAnchorMax, rightAnchorMax, elapsedTime / moveDuration);
                    oldWorldButton.GetComponent<RectTransform>().anchorMin = Vector2.Lerp(centerAnchorMin, rightAnchorMin, elapsedTime / moveDuration);
                }
            }
            else
            {
                oldWorldButton.SetActive(false);
            }
            if (elapsedTime >= moveOffset)
            {
                if (rightward)
                {
                    newWorldButton.GetComponent<RectTransform>().anchorMax = Vector2.Lerp(rightAnchorMax, centerAnchorMax, (elapsedTime - moveOffset) / moveDuration);
                    newWorldButton.GetComponent<RectTransform>().anchorMin = Vector2.Lerp(rightAnchorMin, centerAnchorMin, (elapsedTime - moveOffset) / moveDuration);
                }
                else
                {
                    newWorldButton.GetComponent<RectTransform>().anchorMax = Vector2.Lerp(leftAnchorMax, centerAnchorMax, (elapsedTime - moveOffset) / moveDuration);
                    newWorldButton.GetComponent<RectTransform>().anchorMin = Vector2.Lerp(leftAnchorMin, centerAnchorMin, (elapsedTime - moveOffset) / moveDuration);
                }
            }
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }
        newWorldButton.GetComponent<RectTransform>().anchorMax = centerAnchorMax;
        newWorldButton.GetComponent<RectTransform>().anchorMin = centerAnchorMin;
        oldWorldButton.SetActive(false);
        newWorldButton.GetComponent<Button>().interactable = true;
        if (currentWorld > 0)
        {
            leftButton.GetComponent<Button>().interactable = true;
        }
        if (currentWorld < worldButtons.Count - 1)
        {
            rightButton.GetComponent<Button>().interactable = true;
        }
        worldText.GetComponent<TMP_Text>().text = newWorldButton.GetComponent<WorldButton>().WorldName;
    }
}