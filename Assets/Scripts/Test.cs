using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;
using System.Xml;

public class Test : MonoBehaviour
{
    void Start()
    {
        var path = Path.Combine(Application.streamingAssetsPath, "save.txt");
        Save(path, "test");
    }

    private void Save(string path, string data) {
        // При использовании using, после работы с файлом, автоматически почистится кэш и будет вызван Dispose.
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
}
