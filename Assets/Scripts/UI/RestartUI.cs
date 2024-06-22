using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartUI : MonoBehaviour
{
    [SerializeField] private GameObject restartUIObject;
    [SerializeField] private string scene;

    [SerializeField] private HealthSO playerHealthSO;
    // Start is called before the first frame update
    void Start()
    {
        PlayerReceiveHit.PlayerHealth.DeathEvent += ShowUI;
    }

    private void ShowUI()
    {
        StartCoroutine(RestartScreen());
    }

    private IEnumerator RestartScreen()
    {
        yield return new WaitForSeconds(2);
        restartUIObject.SetActive(true);
    }

    public void Restart()
    {
        playerHealthSO.health = 9f;
        SceneManager.LoadSceneAsync(scene);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
