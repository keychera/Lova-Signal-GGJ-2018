using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BehaviourMessagePasser))]
public class ChangeScene : MonoBehaviour
{
    BehaviourMessagePasser passer;
    Animator animator;

    void Awake()
    {
        passer = GetComponent<BehaviourMessagePasser>();
        animator = GetComponent<Animator>();
    }

    public void LoadStage(string sceneName)
    {
        passer.message = sceneName;
        animator.SetBool("isDone",true);
        //SceneManager.LoadScene(sceneName);
    }

    public void RestartStage()
    {
        passer.message = SceneManager.GetActiveScene().name;
        animator.SetBool("isDone",true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(int level)
    {
        passer.message = "Level" + level;
        animator.SetBool("isDone",true);
        //SceneManager.LoadScene("Level"+level);
    }


}
