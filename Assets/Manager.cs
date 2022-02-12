using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Manager : MonoBehaviour
{
    private const string MS = "m/s";
    private const string SPACE = " ";
    private Move _square1;
    private Move _square2;
    private TextField _square1Speed;
    private TextField _square2Speed;
    private TextField _fixedUpdate;

    public void Start()
    {
        this._square1 = GameObject.Find("Block1").GetComponent<Move>();
        this._square2 = GameObject.Find("Block2").GetComponent<Move>();

        Debug.Log("GM Start");

        var uiRoot = GameObject.Find("SimControls").GetComponent<UIDocument>().rootVisualElement;
        this._square1Speed = uiRoot.Q<TextField>("Square1SpeedTxt");
        this._square2Speed = uiRoot.Q<TextField>("Square2SpeedTxt");
        this._fixedUpdate = uiRoot.Q<TextField>("FixedUpdateTxt");

        this._square1Speed.value = $"{this._square1.Speed} {MS}";
        this._square2Speed.value = $"{this._square2.Speed} {MS}";
        this._fixedUpdate.value = $"{Time.fixedDeltaTime:F5}";
    }

    public void Update()
    {
        this._square1.Speed = ParseSpeed(this._square1Speed.value);
        this._square2.Speed = ParseSpeed(this._square2Speed.value);
    }

    public void FixedUpdate()
    {
        var newFixedTime = ParseTime(this._fixedUpdate.value);
        if (Math.Abs(Time.fixedDeltaTime - newFixedTime) > float.Epsilon)
        {
            Time.fixedDeltaTime = newFixedTime;
        }
    }

    private static float ParseTime(string time)
    {
        const float MIN_TIME = 0.001F;
        const float MAX_TIME = 0.3333333F;
        const float DEFAULT = 0.02F;

        time = time.Replace(SPACE, string.Empty);
        if (float.TryParse(time, out var res) && res is >= MIN_TIME and <= MAX_TIME)
        {
            return res;
        }

        Debug.Log("Invalid fixed time");
        return DEFAULT;
    }

    private static float ParseSpeed(string speed)
    {
        speed = speed.Replace(MS, string.Empty);
        speed = speed.Replace(SPACE, string.Empty);

        if (float.TryParse(speed, out var res))
        {
            return res;
        }

        Debug.Log("Failed to parse speed");
        return 1F;
    }
}
