using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private float _maxTimer = 90f;

    private Image _timerBar;
    private EventBus _eventBus;
    private bool _isTimeStopped = false;
    private bool _isTimerFulfilled = false;
    private Coroutine _removeTimeCoroutine;

    public static float SecondsLeft;

    private void Start()
    {
        _timerBar = GetComponent<Image>();

        EventBus eventBus = FindObjectOfType<EventBus>();
        _eventBus = eventBus;

        if (_eventBus != null)
        {
            _eventBus.Subscribe<TimerChangedSignal>(GetTime);
        }
        else
        {
            Debug.LogError("EventBus component is missing!");
        }
    }

    private void Update()
    {
        ShowTime();
        TimeStop();
    }

    private void GetTime(TimerChangedSignal signal)
    {
        SecondsLeft = signal.SecondsLeft;

        if (SecondsLeft >= _maxTimer)
        {
            _isTimerFulfilled = true;
        }
        else
        {
            _isTimerFulfilled = false;
        }
    }

    private void ShowTime()
    {
        int minutes = Mathf.FloorToInt(SecondsLeft / 60);
        int seconds = Mathf.FloorToInt(SecondsLeft % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        _timerBar.fillAmount = SecondsLeft / _maxTimer;
    }

    private void ToggleTimeStopped()
    {
        _isTimeStopped = !_isTimeStopped;
        TimeManager.IsTimeGoing = !TimeManager.IsTimeGoing;

        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcs)
        {
            var navAgent = npc.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                navAgent.isStopped = _isTimeStopped;
            }

            var animator = npc.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = !_isTimeStopped;
            }
        }

        if (_isTimeStopped)
        {
            Debug.Log("Время остановлено");
            if (_removeTimeCoroutine == null)
            {
                _removeTimeCoroutine = StartCoroutine(RemoveTimeCoroutine());
            }
        }
        else
        {
            Debug.Log("Время возобновлено");
            if (_removeTimeCoroutine != null)
            {
                StopCoroutine(_removeTimeCoroutine);
                _removeTimeCoroutine = null;
            }
        }
    }

    private void TimeStop()
    {
        if ((Input.GetKeyDown(KeyCode.Tab) && (_isTimerFulfilled || _isTimeStopped)) || (SecondsLeft == 0 && _isTimeStopped))
        {
            ToggleTimeStopped();
        }
    }

    private System.Collections.IEnumerator RemoveTimeCoroutine()
    {
        while (_isTimeStopped)
        {
            RemoveTime();
            yield return new WaitForSeconds(2f);
        }
    }

    private void RemoveTime()
    {
        SecondsLeft = Mathf.Clamp(SecondsLeft - 10, 0, _maxTimer);
        _eventBus.Invoke(new TimerChangedSignal(SecondsLeft));
    }
}

