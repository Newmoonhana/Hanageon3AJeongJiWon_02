using UnityEngine;
//출처: https://mrhook.co.kr/266 [리프(리뷰하는 프로그래머) TV]
public class SingletonPattern_IsA_Mono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T inst
    {
        get
        {
            instance = FindObjectOfType(typeof(T)) as T;
            if (instance == null)
            {
                instance = new GameObject(typeof(T).ToString(), typeof(T)).AddComponent<T>();
            }
            return instance;
        }
    }
}
