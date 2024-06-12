using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnEnterJump : MonoBehaviour
{
    [SerializeField] private float m_JumpPower = 10;
    [SerializeField] private float m_JumpDuration = 1;
    public void ButtonJump()
    {

        //Ideally this would be set to 0 however unity sometimes likes to turn 0 into an extremely small number when it should be set to 0.
        if (gameObject.transform.localPosition.y<=0.001)
        {
            this.gameObject.transform.DOJump(this.transform.position, m_JumpPower, 1, m_JumpDuration);
        }
        //if (Mathf.Round(gameObject.transform.localPosition.y) == 0)
        //{
        //    this.gameObject.transform.DOJump(this.transform.position, m_JumpPower, 1, m_JumpDuration);
        //}
    }
    //Unused?? if yes delete
    public void ButtonReset()
    {
        gameObject.transform.localPosition = Vector3.zero;
    }

}
