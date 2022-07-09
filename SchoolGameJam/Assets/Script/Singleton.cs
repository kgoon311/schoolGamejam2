using UnityEngine;

/// <summary>
/// �̱��� ��ä ���� ���� (���� �������ѵѰ�) / ����ȯ�� �����ȵ�
/// </summary>
public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    protected static T _instance;
    public static T In { get { return _instance; } }
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        OnAwake();
    }
    protected virtual void OnAwake() { }

}


/// <summary>
/// �̱��水ä ���� ���� (���� �������ѵѰ�) / ����ȯ�� ����
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected static T _instance;
    public static T In { get { return _instance; } }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = (T)this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        OnAwake();
    }
    protected virtual void OnAwake() { }
    protected virtual void OnDestroy()
    {
        _instance = null;
    }
    public static bool HasInstance
    {
        get { return In != null; }
    }
}


/// <summary>
/// �̱��水ä�� ������ �ڵ����� / ����ȯ�� �����ȵ�
/// </summary>
public abstract class SingletonMonoCreate<T> : MonoBehaviour where T : SingletonMonoCreate<T>
{
    private static T instance;
    public static T In
    {
        get
        {
            if (instance == null)
            {
                if (applicationIsQuitting)
                {
                    return null;
                }
                instance = FindObjectOfType<T>();

#if UNITY_EDITOR
                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    Debug.LogError("[SingletonMono] There should never be more than 1 singleton! Reopen the scene.");
                    return null;
                }
#endif

                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).ToString());
                    instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }
    public static bool HasInstance
    {
        get { return instance != null; }
    }
    public static bool IsDestroyed
    {
        get { return instance == null; }
    }

    protected static bool applicationIsQuitting = false;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            GameObject.Destroy(gameObject);
            return;
        }
        OnAwake();
    }
    protected virtual void OnAwake() { }
    protected virtual void OnApplicationQuit() { applicationIsQuitting = true; }
}

/// <summary>
/// �̱��水ä �ڵ� ����  / ����ȯ�� ����
/// </summary>
public abstract class SingletonMonoCreateDestroy<T> : MonoBehaviour where T : SingletonMonoCreateDestroy<T>
{
    protected static T _instance = null;
    public static T In
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    _instance = new GameObject("Singleton of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                }
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    _instance.gameObject.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector |
                                                     HideFlags.HideInHierarchy | HideFlags.DontSave;
                }
#endif
            }
            return _instance;
        }
    }

    public static bool IsValid()
    {
        return null != _instance;
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        OnAwake();
    }
    protected virtual void OnAwake() { }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }
}