using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    void Update()
    {
#if UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            GameManager.Instance.ChangeScene("MainMenu");
        }
#endif

#if UNITY_ANDROID
        if (Input.anyKey)
        {
            GameManager.Instance.ChangeScene("MainMenu");
        }
#endif
    }
}
