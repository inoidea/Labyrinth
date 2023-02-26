using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class MyWindow : EditorWindow
{
    public static GameObject ObjectInstantiate;
    public string _nameObject = "Hello World";
    public bool _groupEnabled;
    public bool _randomColor = true;
    public int _countObject = 1;
    public float _radius = 10;

    private void OnGUI()
    {
        GUILayout.Label("Базовые настройки", EditorStyles.boldLabel);
        ObjectInstantiate = EditorGUILayout.ObjectField("Объект который хотим вставить", ObjectInstantiate, typeof(GameObject), true) as GameObject;
        _nameObject = EditorGUILayout.TextField("Имя объекта", _nameObject);
        _groupEnabled = EditorGUILayout.BeginToggleGroup("Дополнительные настройки", _groupEnabled);
        _randomColor = EditorGUILayout.Toggle("Случайный цвет", _randomColor);
        _countObject = EditorGUILayout.IntSlider("Количество объектов", _countObject, 1, 100);
        _radius = EditorGUILayout.Slider("Радиус окружности", _radius, 10, 50);
        EditorGUILayout.EndToggleGroup();

        var button = GUILayout.Button("Создать объекты");

        if (button)
        {
            if (ObjectInstantiate)
            {
                GameObject root = new GameObject("Root");

                for (int i = 0; i < _countObject; i++)
                {
                    float angle = i * Mathf.PI * 2 / _countObject;
                    Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _radius;
                    GameObject temp = Instantiate(ObjectInstantiate, pos, Quaternion.identity);
                    temp.name = _nameObject + "(" + i + ")";
                    temp.transform.parent = root.transform;
                    var tempRenderer = temp.GetComponent<Renderer>();

                    if (tempRenderer && _randomColor)
                    {
                        tempRenderer.material.color = UnityEngine.Random.ColorHSV();
                    }
                }
            }
        }
    }
}
