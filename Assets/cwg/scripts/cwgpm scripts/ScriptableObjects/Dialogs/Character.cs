using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string fullName;
    public Sprite portrait;
   // public Animator animator;
   // public RuntimeAnimatorController controller;
   // AnimatorOverrideController overrideController;
   // Animator.runtimeAnimationController = someNewController;
}
