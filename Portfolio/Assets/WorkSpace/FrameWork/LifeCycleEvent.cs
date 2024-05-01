using UnityEngine;
using UnityEngine.Events;

public class LifeCycleEvent : MonoBehaviour
{
    public UnityEvent onAwakeEvent;
    public UnityEvent onEnableEvent;
    public UnityEvent onStartEvent;
    public UnityEvent onApplicationQuitEvent;
    public UnityEvent onDisableEvent;
    public UnityEvent onDestroyEvent;
    private void Awake()
    {
        onAwakeEvent.Invoke();
    }
    private void OnEnable()
    {
        onEnableEvent.Invoke();
    }
    private void Start()
    {
        onStartEvent.Invoke();
    }
    private void OnApplicationQuit()
    {
        onApplicationQuitEvent.Invoke();
    }
    private void OnDisable()
    {
        onDisableEvent.Invoke();
    }
    private void OnDestroy()
    {
        onDestroyEvent.Invoke();
    }
}
