using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadUI : MonoBehaviour
{
    [SerializeField] private Button reloadBtn;

    private void Awake()
    {
        reloadBtn.onClick.AddListener(ReloadGame);
    }

    private void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
