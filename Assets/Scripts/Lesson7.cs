using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MyExtensions
{
    public static int GetCharCount(this string self)
    {
        return self.Length;
    }

    public static void GetKeyCount<T>(this List<T> list) {
        var charCountList = new Dictionary<T, int>();

        for(int i = 0; i < list.Count; i++)
        {
            if (!charCountList.ContainsKey(list[i]))
                charCountList.Add(list[i], 1);
            else
                charCountList[list[i]]++;
        }

        foreach(var element in charCountList)
        {
            Debug.Log($"���� \"{element.Key}\" ����������� {element.Value} ���(�).");
        }
    }
}

public class Lesson7 : MonoBehaviour
{
    public void Start()
    {
        string s = "qweqweqwe";
        Debug.Log(s.GetCharCount());

        List<int> list = new List<int>() { 1, 5, 1, 4};
        list.GetKeyCount();
    }
}

/*
 * ������, ��������� ������, ��� ������� � �������.
���������:
List
+ ������� ������ � ��������� �� �������
+ �������� ������ ��� ������� � �������� �������� �� �������
+ ������ ��� �������� �� ���������� ���������� �������� � ������
+ ������������ ��������� ������

- ��������� ����� �������� ������ ������

ArrayList
+ (List) + ����� ��������� ������ ������ ����

ObservableCollection
+ (List) + ������� �������

LinkedList
+ ������ ������� ����� ������� ������ �� ����������� � ���������� ��������
+ ����� ������ �������� ������� � ����� ������� � ������� ���

- ��� ��������� ����� ������

Dictionary
+ ������� ������ �� ��������, �.�. ���� ������������� � 16-������ ��������, ������� ������������� ������-�� ������� ������ 

- ����� ������� ��� ���� ������� ����� ������
- ����� ��������� �����������, ���� 2 ����� ����� ������

Queue � Stack
+ ������� ������ � ��������

- ���������� � �������� ��������� � ������/����� 
 */