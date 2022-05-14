using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAdapter : MonoBehaviour
{
    public GameObject startingPaperObject, lastPaperObject , finalResultObject;

    [Header("Paper Type")]
    [Tooltip("Types : 1-DuzKagit /  2-KareKagit1  /  3-KareKagit2")]
    public int paperType;
    //" Type lar oyun isleyisi icin son derece onemli" +
    //"DuzKagit icin 1,  KareKagit1 icin 2, KareKagit2 icin 3 girilmeli"
    // Duzkagit1 kadin ve orumcek modellerinde
    // KareKagit1 anakare ve karekare modellerinde
    // KareKagit2 kartane modellerinde

    public bool kizMi, orumcekMi, kare1Mi, kare2Mi, kartanesi1Mi, kartanesi2Mi;
}
