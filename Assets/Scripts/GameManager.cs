using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState gameState;
    public PlayerController player;
    public Deadushka deadushka;
    public float[] stateHeight = {5, 7, 9, 10, 65536};
    public enum State
    {
        Signal0 = 0,
        Signal1,
        Signal2,
        Signal3,
        Endgame,
        TranslateState
    }
    public State state = State.Signal0;

    void Start()
    {
    }

    void Update()
    {
        if (state == State.TranslateState)
            return;

        if (player.transform.position.y < stateHeight[(int)state])
            return;

        State fromState = state;
        state = State.TranslateState;
        StartCoroutine(InTranslateState(fromState));
    }

    IEnumerator InTranslateState(State fromState)
    {
        yield return new WaitForSeconds(1.0f);

        if (player.transform.position.y < stateHeight[(int)fromState])
        {
            state = fromState;
            yield break;
        }

        switch (fromState)
        {
            case State.Signal0: yield return ToState1(); break;
            case State.Signal1: yield return ToState2(); break;
            case State.Signal2: yield return ToState3(); break;
            case State.Signal3: yield return ToEndgame(); break;
        }
    }

    IEnumerator ToState1()
    {
        player.CallPhone(Phone.State.Signal1, 1);
        yield return new WaitForSeconds(0.5f);
        state = State.Signal1;
    }

    IEnumerator ToState2()
    {
        player.CallPhone(Phone.State.Signal2, 2);
        yield return new WaitForSeconds(0.5f);
        state = State.Signal2;
    }

    IEnumerator ToState3()
    {
        player.CallPhone(Phone.State.Signal3, 3);
        yield return new WaitForSeconds(0.5f);
        state = State.Signal3;
    }

    IEnumerator ToEndgame()
    {
        player.CallPhone(Phone.State.Rickroll, 4, true);
        yield return new WaitForSeconds(0.5f);
        state = State.Endgame;
    }
}
