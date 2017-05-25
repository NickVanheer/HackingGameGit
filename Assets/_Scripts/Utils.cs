using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;


/// <summary>
/// Simple State system, not used in HackingGame project
/// </summary>
public abstract class GameState
{
    protected GameManager gameManager;
    public bool IsInitialized;

    public GameState(GameManager gameMng)
    {
        gameManager = gameMng;
        IsInitialized = false;
    }

    public virtual void StateInitialize()
    {
        this.IsInitialized = true;
    }
    public abstract void StateEntry();
    public abstract void StateUpdate();
    public abstract void StateExit();
}


public static class TransformDeepChildExtension
    {
        public static Transform FindDeepChild(this Transform aParent, string aName)
        {
            var result = aParent.Find(aName);
            if (result != null)
                return result;
            foreach (Transform child in aParent)
            {
                result = child.FindDeepChild(aName);
                if (result != null)
                    return result;
            }
            return null;
        }

    public static T FindChildComponent<T>(this Transform aParent)
    {
        var result = aParent.GetComponent<T>();

        if(result != null)
            return result;

        foreach (Transform child in aParent)
        {
            result = child.FindChildComponent<T>();
            if (result != null)
                return result;
        }
        return default(T);
    }

    public static List<T> FindChildComponents<T>(this Transform aParent)
    {
        List<T> toReturn = new List<T>();
        var result = aParent.GetComponent<T>();
        if (result != null)
            toReturn.Add(result);

        foreach (Transform child in aParent)
        {
            List<T> res = child.FindChildComponents<T>();
            if (res.Count > 0)
                toReturn.Add(res);
        }
        return toReturn;
    }
}

public static class ListExtensions
    {
        public static void Add<T>(this List<T> list, params T[] item)
        {
            foreach (var listItem in item)
            {
                list.Add(listItem);
            }
        }

    public static void Add<T>(this List<T> list, List<T> item)
    {
        foreach (var listItem in item)
        {
            list.Add(listItem);
        }
    }
}

    public static class TransformExtensions
    {

        public static void SetX(this Transform transform, float x)
        {
            Vector3 newPosition =
               new Vector3(x, transform.position.y, transform.position.z);

            transform.position = newPosition;
        }

        public static void SetY(this Transform transform, float y)
        {
            Vector3 newPosition =
               new Vector3(transform.position.x, y, transform.position.z);

            transform.position = newPosition;
        }

        public static void SetZ(this Transform transform, float z)
        {
            Vector3 newPosition =
               new Vector3(transform.position.x, transform.position.y, z);

            transform.position = newPosition;
        }

        public static void SetPosition(this Transform transform, float x, float y, float z)
        {
            Vector3 newPosition =
               new Vector3(x, y, z);

            transform.position = newPosition;
        }
    }

class Utils
{
    public static GameObject InstantiateSafe(GameObject gO, Vector3 position)
    {
        if(gO == null)
        {
            StackFrame frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method.DeclaringType;
            var name = method.Name;

            UnityEngine.Debug.LogError(string.Format("Can't load prefab at {0}.{1}", type, name));
            UnityEngine.Debug.Break();
        }

        GameObject result = (GameObject)GameObject.Instantiate(gO, position, Quaternion.identity);
        return result;
    }

    public static GameObject LoadGameObjectSafe(string filenameFromResources)
    {
        GameObject GO = Resources.Load<GameObject>(filenameFromResources.Trim());

        if (GO == null)
        {
            //report error better
            UnityEngine.Debug.LogError("Can't load GameObject " + filenameFromResources);
            UnityEngine.Debug.Break();
        }

        return GO;
    }
}

