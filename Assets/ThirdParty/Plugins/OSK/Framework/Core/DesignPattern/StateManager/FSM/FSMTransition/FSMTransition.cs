using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class of Transition in the finite state machine system
/// </summary>
public class FSMTransition
{
    /// <summary>
    /// The state this transition will switch to
    /// </summary>
    private FSMState m_NextFSMState;

    /// <summary>
    /// The list of transition
    /// </summary>
    private List<IFSMTransitionCondition> m_FSMTransitionConditionList = new List<IFSMTransitionCondition>();

    /// <summary>
    /// The constructor of the transition class
    /// </summary>
    public FSMTransition(FSMState nextFSMState, IFSMTransitionCondition[] fsmTransitionConditionArray)
    {
        this.m_NextFSMState = nextFSMState;

        for (int i = 0; i < fsmTransitionConditionArray.Length; i++)
        {
            IFSMTransitionCondition fsmTransitionCondition = fsmTransitionConditionArray[i];

            this.AddFSMTransitionCondition(fsmTransitionCondition);
        }
    }

    /// <summary>
    /// Add the specific transtion condition to the list
    /// </summary>
    public void AddFSMTransitionCondition(IFSMTransitionCondition fSMTransitionCondition)
    {
        if (m_FSMTransitionConditionList.Contains(fSMTransitionCondition) == false)
        {
            m_FSMTransitionConditionList.Add(fSMTransitionCondition);
        }
        else
        {
            Debug.LogErrorFormat("The fsmTransition : {0} aleady have the fSMTransitionCondition : {1}", this.ToString(), fSMTransitionCondition.ToString());
        }
    }

    /// <summary>
    /// Remove the specific transtion condition from the list
    /// </summary>
    public void RemoveFSMTransitionCondition(IFSMTransitionCondition fSMTransitionCondition)
    {
        if (m_FSMTransitionConditionList.Contains(fSMTransitionCondition) == true)
        {
            m_FSMTransitionConditionList.Remove(fSMTransitionCondition);
        }
        else
        {
            Debug.LogErrorFormat("The fsmTransition : {0} do not have the fSMTransitionCondition : {1}", this.ToString(), fSMTransitionCondition.ToString());
        }
    }

    /// <summary>
    /// Check every condition in the list
    /// </summary>
    public bool CheckTransitionList()
    {
        bool ifAllMatch = true;

        for (int i = 0; i < this.m_FSMTransitionConditionList.Count; i++)
        {
            IFSMTransitionCondition fsmTransitionConditionList = this.m_FSMTransitionConditionList[i];

            if (fsmTransitionConditionList.CheckCondition() == false)
            {
                ifAllMatch = false;

                break;
            }
        }

        return ifAllMatch;
    }

    /// <summary>
    /// The state this transition will switch to
    /// </summary>
    /// <returns></returns>
    public FSMState GetNextFSMState()
    {
        return m_NextFSMState;
    }
}
