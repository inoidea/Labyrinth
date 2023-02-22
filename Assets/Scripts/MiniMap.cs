using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private Transform _player;

    void Start()
    {
        transform.parent = null;
        transform.rotation= Quaternion.Euler(90.0f, 0, 0);
        transform.position = _player.position + new Vector3(0, 10.0f, 0);

        var rt = Resources.Load<RenderTexture>("MiniMap/MiniMapTexture");

        GetComponent<Camera>().targetTexture = rt;
    }

    private void LateUpdate()
    {
        var newPosition = _player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90, _player.eulerAngles.y, 0);
    }
}
