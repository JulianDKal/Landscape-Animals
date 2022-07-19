using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{
    private static PointerEventData eventDataCurrentPosition;
    private static List<RaycastResult> results;

    //checks wether you are hovering over UI
    public static bool isOverUI()
    {
        eventDataCurrentPosition = new PointerEventData(EventSystem.current){ position = Input.mousePosition };
        results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    //Gets the position of a UI element in world position
    public static Vector2 GetWorldPosofCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera.main, out var result);
        return result;
    }
    //overload
    public static Vector2 GetWorldPosofCanvasElement(RectTransform element, Camera camera)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, camera, out var result);
        return result;
    }
    //Shuffling a list
    public static void Shuffle<T>(this IList<T> list) {
        System.Random rnd = new System.Random();
        for (var i = 0; i < list.Count; i++)
            list.Swap(i, rnd.Next(i, list.Count));
    }

    public static void Swap<T>(this IList<T> list, int i, int j) {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
