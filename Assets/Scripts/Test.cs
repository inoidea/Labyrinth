using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;
using System.Xml;

public class Test : MonoBehaviour
{
    [SerializeField] private float ActiveDis;

    void Start()
    {
        var path = Path.Combine(Application.streamingAssetsPath, "save.txt");
        Save(path, "test");
    }

    private void Save(string path, string data) {
        // ��� ������������� using, ����� ������ � ������, ������������� ���������� ��� � ����� ������ Dispose.
        using (var sw = new StreamWriter(path))
        {
            sw.WriteLine(data);
        }
    }

    public string Load(string path = null)
    {
        var result = "";

        using (var sr = new StreamReader(path))
        {
            while (!sr.EndOfStream)
            {
                result += sr.ReadLine();
            }
        }

        return result;
    }

    public void CreateXML(string path) { 
        var xmlDoc = new XmlDocument();

        XmlNode rootNode = xmlDoc.CreateElement("Player");
        xmlDoc.AppendChild(rootNode);

        XmlNode userNode = xmlDoc.CreateElement("Info");

        var attribute = xmlDoc.CreateAttribute("Unity");
        attribute.Value = Application.unityVersion;

        userNode.Attributes.Append(attribute);
        userNode.InnerText = "System Language" + Application.systemLanguage;

        rootNode.AppendChild(userNode);

        xmlDoc.Save(path);
    }

    private void OnDrawGizmos()
    {
        // ����������� ������ �� �������. �� ��������� ������ �������� � ����� "Gizmos".
        //Gizmos.DrawIcon(transform.position, "image.jpg", true);
    }

    private void OnDrawGizmosSelected()
    {
        #if UNITY_EDITOR
        // ����������� ������� ����������� �������.
        Gizmos.DrawWireSphere(Vector3.zero, 5);

        // ����������� �������� � 3� ���������� (��� ���������).
        Transform t = transform;
        var flat = new Vector3(ActiveDis, 0, ActiveDis);
        Gizmos.matrix = Matrix4x4.TRS(t.position, t.rotation, flat);
        #endif
    }
}
