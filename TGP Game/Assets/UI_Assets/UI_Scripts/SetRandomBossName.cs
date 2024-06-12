using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetRandomBossName : MonoBehaviour
{
    private string[] m_FirstNames = {"Jeff","Bob","Bartholemew","Ryan","Garfield","Gronk","David","Augustus","Gregory","Ratthew","Matt","Kane","Michael","Davin","Tom","James","Luke","Craig","Jake","Kaede","Sam","Dylan","Cornelius","Basil","Waltah","Karen", "Boblin", "Grog", "Jester", "Meatball", "Death"};
    private string[] m_SecondNames = { "The Wise", "The Cruel","The Greedy","The Taxed","The Ratty","The Impressed","The Beast","The Beyblade","The Tired","The Innocent","The Feeble","The Visible","The Untaxed","The Wealthy","The Breaking","The Bad", "The Greasy", "The Dark Lord", "Von Zarovich", "Attorney at Law", "2 (Electric Boogaloo)", "- Master of spinjistu", "The First", "The All Seeing", "The Dislecsick", "The Lazy Developer", "The Meatball", "In Boots", "The Unwavering", "The Fearless", "Incarnate", "In A Hat", "Of Whitestone", "(Not A Robot)"};
    private string m_FullName;
    void Start()
    {
        m_FullName = m_FirstNames[Random.Range(0, m_FirstNames.Length)] + " " + m_SecondNames[Random.Range(0, m_SecondNames.Length)];
        GetComponent<TextMeshProUGUI>().text = m_FullName;
    }


}
