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
    }

    // Update is called once per frame
    void Update()
    {

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

    // moves from initialPosition to targetPosition over moveDuration
    private IEnumerator ShiftWorlds(GameObject oldWorldButton, GameObject newWorldButton, bool rightward, float moveDuration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            //transform.position = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            //fix
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        //set active and interactable
    }
}