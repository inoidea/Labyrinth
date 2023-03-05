using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadUI : MonoBehaviour
{
    [SerializeField] private Button reloadBtn;

    private void Start()
    {
        reloadBtn.onClick.AddListener(ReloadGame);
        Debug.Log("reload start");
    }

    private void ReloadGame()
    {
        Debug.Log("reload");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
