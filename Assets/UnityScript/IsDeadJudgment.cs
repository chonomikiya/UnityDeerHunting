using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDeadJudgment : MonoBehaviour
{

    static bool deer_dead = false;
    public static void DeerIsAlive(){
        deer_dead = false;
    }
    public void DeerIsDead(){
        deer_dead = true;
    }
    public static bool GetDeerState(){
        return deer_dead;
    }

}