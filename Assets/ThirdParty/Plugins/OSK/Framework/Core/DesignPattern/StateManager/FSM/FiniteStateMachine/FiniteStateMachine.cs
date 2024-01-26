using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the Finite State Machine class which stores all the states of AI
/// </summary>
public class FiniteStateMachine
{
    /// <summary>
    ///  This list stores all the states which were added to this finite state machine
    /// </summary>
    private List<FSMState> m_FSMStateList = new List<FSMState>();

    /// <summary>
    /// This state is the first state when the finite state machine system run,and you can not change it
    /// </summary>
    private FSMState m_Entry = new FSMEntry();

    /// <summary>
    /// This state is the exit state of the finite state machine.
    /// Once the system enter this state,the finite state machine system can not switch to other state,
    /// represent the finite state machine is stop running;
    /// </summary>
    private FSMState m_Exit = new FSMExit();

    /// <summary>
    /// The state which will execute after the entry state
    /// </summary>
    private FSMState m_DefaultFSMState;

    /// <summary>
    /// The end state,the state which will run before enter the exit state
    /// </summary>
    private FSMState m_EndFSMState;

    /// <summary>
    /// The current state which the Finite State Machine updates
    /// </summary>
    private FSMState m_CurrentFSMState;


    /// <summary>
    /// The Initialize function before the finite state machine system running 
    /// </summary>
    public void OnInitialize()
    {
        IFSMTransitionCondition[] entryFSMTransitionConditionArray = new IFSMTransitionCondition[1] { new FSMDefaultTransitionCondition() };
        m_Entry.AddTransition(m_DefaultFSMState, entryFSMTransitionConditionArray);

        AddState(m_Entry);
        AddState(m_Exit);

        this.m_CurrentFSMState = m_Entry;
    }

    public void OnRestart()
    {
        this.m_CurrentFSMState = m_Entry;
    }

    /// <summary>
    /// The Update Function of the finite state machine
    /// </summary>
    public void OnUpdate()
    {
        ExecuteCurrentFSMStateOnCheckTransition();
        ExecuteCurrentFSMStateOnUpdate();
    }

    /// <summary>
    /// Add a state to the finite state machine system 
    /// </summary>
    public void AddState(FSMState fsmState)
    {
        if (fsmState == null)
        {
            Debug.LogError("the fsmState can not be null");

            return;
        }

        if (m_FSMStateList.Contains(fsmState) == false)
        {
            m_FSMStateList.Add(fsmState);
        }
        else
        {
            Debug.LogErrorFormat("The finite state machine already have the state:{0}", fsmState.ToString());
        }

    }

    /// <summary>
    /// Delete state from the List
    /// </summary>
    public void DeleteState(FSMState fsmState)
    {
        if (fsmState == null)
        {
            Debug.LogError("the fsmState can not be null");

            return;
        }

        if (m_FSMStateList.Contains(fsmState) == true)
        {
            m_FSMStateList.Remove(fsmState);
        }
        else
        {
            Debug.LogErrorFormat("The finite state machine do not have the state:{0}", fsmState.ToString());
        }

    }

    /// <summary>
    /// Set the default state
    /// </summary>
    public void SetDefaultState(FSMState fsmState)
    {
        if (fsmState == null)
        {
            Debug.LogError("the fsmState can not be null");
            return;
        }

        bool exist = m_FSMStateList.Contains(fsmState);
        if (exist == true)
        {
            this.m_DefaultFSMState = fsmState;
        }
        else
        {
            Debug.LogErrorFormat("The {0} is not exist", fsmState.ToString());
        }

    }

    /// <summary>
    /// Set the end state
    /// </summary>
    public void SetEndState(FSMState fsmState, IFSMTransitionCondition[] endStateToExitStateFSMTransitionConditionArray)
    {
        if (fsmState == null)
        {
            Debug.LogError("the fsmState can not be null");
            return;
        }

        bool exist = m_FSMStateList.Contains(fsmState);

        if (exist == true)
        {
            this.m_EndFSMState = fsmState;
            this.m_EndFSMState.AddTransition(m_Exit, endStateToExitStateFSMTransitionConditionArray);
        }
        else
        {
            Debug.LogErrorFormat("The {0} is not exist", fsmState.ToString());
        }
    }

    /// <summary>
    /// Current state which the finite state machine updates
    /// </summary>
    /// <returns></returns>

    public FSMState CurrentState()
    {
        return m_CurrentFSMState;
    }

    /// <summary>
    /// Create a transition which represent the state switch to another state,and add it to the transition list.
    /// </summary>
    public FSMTransition CreateFSMStateToAnotherFSMStateTransition(FSMState fsmState, FSMState nextFSMState, IFSMTransitionCondition[] fsmTransitionConditionArray)
    {
        bool fsmStateExist = m_FSMStateList.Contains(fsmState);
        if (fsmStateExist == false)
        {
            Debug.LogErrorFormat("The fsmState:{0} is not exist", fsmState.ToString());

            return null;
        }

        bool nextFSMStateExist = m_FSMStateList.Contains(nextFSMState);
        if (nextFSMStateExist == false)
        {
            Debug.LogErrorFormat("The nextFSMState:{0} is not exist", nextFSMState.ToString());

            return null;
        }
        return fsmState.AddTransition(nextFSMState, fsmTransitionConditionArray);
    }

    /// <summary>
    /// Delete the specific transition in the state
    /// </summary>
    public void DeleteFSMStateToAnotherFSMStateTransition(FSMState fsmState, FSMTransition fsmTransition)
    {
        bool fsmStateExist = m_FSMStateList.Contains(fsmState);

        if (fsmStateExist == true)
        {
            fsmState.DeleteTransition(fsmTransition);
        }
        else
        {
            Debug.LogErrorFormat("The fsmState:{0} is not exist", fsmState.ToString());
        }

    }

    /// <summary>
    ///  Create each state switch to the given state's transition ,and add them to the transition list.
    /// </summary>
    public void CreateAnyFSMStateToFSMStateTransition(FSMState nextFSMState, IFSMTransitionCondition[] fsmTransitionConditionArray)
    {
        bool nextFSMStateExist = m_FSMStateList.Contains(nextFSMState);

        if (nextFSMStateExist == false)
        {
            Debug.LogErrorFormat("The nextFSMState:{0} is not exist", nextFSMState.ToString());

            return;
        }

        for (int i = 0; i < m_FSMStateList.Count; i++)
        {
            FSMState fsmState = m_FSMStateList[i];

            if (fsmState == m_Entry
                || fsmState == m_Exit
                || fsmState == m_EndFSMState
                || fsmState == nextFSMState)
            {
                continue;
            }

            fsmState.AddTransition(nextFSMState, fsmTransitionConditionArray);
        }

    }

    /// <summary>
    ///  Create each state switch to the given state's transition ,and add them to the transition list.
    /// </summary>
    public void CreateAnyFSMStateToFSMStateTransition(FSMState nextFSMState, FSMState[] excludeFSMStates, IFSMTransitionCondition[] fsmTransitionConditionArray)
    {
        bool nextFSMStateExist = m_FSMStateList.Contains(nextFSMState);

        if (nextFSMStateExist == false)
        {
            Debug.LogErrorFormat("The nextFSMState:{0} is not exist", nextFSMState.ToString());

            return;
        }

        for (int i = 0; i < m_FSMStateList.Count; i++)
        {
            FSMState fsmState = m_FSMStateList[i];

            bool haveExcludeFSMState = false;
            if (excludeFSMStates != null && excludeFSMStates.Length != 0)
            {
                for (int j = 0; j < excludeFSMStates.Length; j++)
                {
                    FSMState excludeFSMState = excludeFSMStates[j];

                    if (fsmState == excludeFSMState)
                    {
                        haveExcludeFSMState = true;
                        break;
                    }
                }
            }

            if (fsmState == m_Entry
            || fsmState == m_Exit
            || fsmState == m_EndFSMState
            || fsmState == nextFSMState
            || haveExcludeFSMState == true)
            {
                continue;
            }

            fsmState.AddTransition(nextFSMState, fsmTransitionConditionArray);

        }

    }
    /// <summary>
    /// check all the conditions of current state,if match the condition,then exit current state and switch to the next state. 
    /// </summary>
    public void ExecuteCurrentFSMStateOnCheckTransition()
    {
        if (m_CurrentFSMState.OnCheckTransition() == true)
        {
            //execute current state OnExit function
            m_CurrentFSMState.OnExit();

            //get the next state
            FSMState fsmState = m_CurrentFSMState.GetToNextFSMState();

            //switch to the next state
            m_CurrentFSMState = fsmState;

            //execute the next state OnEnter function
            m_CurrentFSMState.OnEnter();
        }
    }

    /// <summary>
    /// Update the current state's update function
    /// </summary>
    public void ExecuteCurrentFSMStateOnUpdate()
    {
        m_CurrentFSMState.OnUpdate();
    }

}
