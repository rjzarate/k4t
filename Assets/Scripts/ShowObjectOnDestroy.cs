using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowObjectOnDestroy : MonoBehaviour
{
    [SerializeField] private GameObject hiddenGameObject;
    
    private void OnDestroy()
    {
        hiddenGameObject.SetActive(true);
    }

}