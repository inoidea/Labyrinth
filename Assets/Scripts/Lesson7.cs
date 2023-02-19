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
            Debug.Log($"Ключ \"{element.Key}\" встречается {element.Value} раз(а).");
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
 * Массив, связанный список, хэш таблица и очередь.
Структуры:
List
+ быстрый доступ к элементам по индексу
+ включает методы для вставки и удаления значения по индексу
+ методы для проверки на содержание некоторого элемента в списке
+ динамическое выделение памяти

- выделение сразу большого объема памяти

ArrayList
+ (List) + может содержать данные любого типа

ObservableCollection
+ (List) + наличие ивентов

LinkedList
+ каждый элемент может хранить ссылку на последующий и предыдущий элементы
+ можно быстро вставить элемент в любую позицию и удалить его

- нет обращения через индекс

Dictionary
+ быстрый доступ до значений, т.к. ключ преобразуется в 16-ричное значение, которое соответствует какому-то участку памяти 

- сразу выделят для себя большой объем памяти
- может произойти пересечение, если 2 ключа будут похожи

Queue и Stack
+ быстрый доступ к элементу

- добавление и удаление элементов с начала/конца 
 */