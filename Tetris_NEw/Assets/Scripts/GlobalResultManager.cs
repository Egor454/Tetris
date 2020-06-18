using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class GlobalResultManager : MonoBehaviour
//{

//    public static GlobalResultManager instance = null;

//    [SerializeField] private int scoreEnd;
//    public int ScoreEnd => scoreEnd;

//    [SerializeField] private float lineEnd;
//    public float LineEnd => lineEnd;
//    void Awake()
//    {

//        if (instance != null)
//            Destroy(gameObject);
//        instance = this;

 
//        DontDestroyOnLoad(gameObject);

//    }
//    public void InsertScore(int scoreend,float lineend)
//    {
//        scoreEnd = scoreend;
//        lineEnd = lineend;
//    }
//}
public class GlobalResultManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static T instance;

    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed. Returning null.");
                return null;
            }

            lock (m_Lock)
            {
                if (instance == null)
                {

                    instance = (T)FindObjectOfType(typeof(T));


                    if (instance == null)
                    {

                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

 
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return instance;
            }
        }
    }


    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }
}