using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This abstract class represents the state concept in the Finite State Machine.
/// </summary>
public abstract class FSMState
{
    /// <summary>
    /// The list of transition
    /// </summary>
    private List<FSMTransition> m_FSMTransitionList = new List<FSMTransition>();

    public List<FSMTransition> FSMTransitionList
    {
        get { return m_FSMTransitionList; }
        set { m_FSMTransitionList = value; }
    }

    /// <summary>
    /// The next state which the Finite State Machine will switch to
    /// </summary>
    private FSMState m_ToNextFSMState;

    /// <summary>
    /// Create a transition an add it to the list
    /// </summary>
    /// <param name="nextFSMState">From this state,the next state the Finite State Machine will switch to</param>
    /// <param name="fsmTransitionConditionArray">The transition conditions of switching to the given nextFSMState,
    /// they determine whether transfer to the next state or not </param>
    public FSMTransition AddTransition(FSMState nextFSMState, IFSMTransitionCondition[] fsmTransitionConditionArray)
    {
        if (nextFSMState == null)
        {
            Debug.LogError("nextFSMState can not be null");

            return null;
        }

        if (fsmTransitionConditionArray == null || fsmTransitionConditionArray.Length == 0)
        {
            Debug.LogError("FSMTransitionConditionArray can not be null or Empty");

            return null;
        }

        FSMTransition fsmTransition = new FSMTransition(nextFSMState, fsmTransitionConditionArray);

        m_FSMTransitionList.Add(fsmTransition);

        return fsmTransition;
    }

    /// <summary>
    /// Remove the specific transition in the list
    /// </summary>
    public void DeleteTransition(FSMTransition fSMTransition)
    {
        if (fSMTransition == null)
        {
            Debug.LogError("FSMTransition can not be null");

            return;
        }

        if (m_FSMTransitionList.Contains(fSMTransition) == true)
        {
            m_FSMTransitionList.Remove(fSMTransition);
        }
        else
        {
            Debug.LogErrorFormat("{0} do not have the fSMTransition : {1}", this.ToString(), fSMTransition.ToString());
        }
    }

    /// <summary>
    /// Return the next state which the Finite State Machine will switch to
    /// </summary>
    public FSMState GetToNextFSMState()
    {
        return m_ToNextFSMState;
    }

    /// <summary>
    /// This method is used to initialize variables or something when the Finite State Machine switch to this state 
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// Every action, movement or communication the AI does should be placed here
    /// </summary>
    public virtual void OnUpdate() { }

    /// <summary>
    ///  This method decides if the state should transition to another
    /// </summary>
    /// <returns></returns>
    public virtual bool OnCheckTransition()
    {
        bool isTransition = false;

        for (int i = 0; i < m_FSMTransitionList.Count; i++)
        {
            FSMTransition fsmTransition = m_FSMTransitionList[i];

            if (fsmTransition.CheckTransitionList() == true)
            {
                //Get the next state which the Finite State Machine will switch to
                this.m_ToNextFSMState = fsmTransition.GetNextFSMState();

                isTransition = true;

                break;
            }
        }

        return isTransition;
    }

    /// <summary>
    /// This method is used to make anything necessary, as reseting variables
    /// before the Finite State Machine changes to another one. 
    /// </summary>
    public virtual void OnExit() { }
}
