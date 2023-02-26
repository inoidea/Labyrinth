using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class MenuItems
{
    [MenuItem("Test GB/Пункт меню №1 %q")]
    private static void MenuOption()
    {
        EditorWindow.GetWindow(typeof(MyWindow), true, "Test GB");
    }
}
