using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private string animationName;

    private Animator animator;

    void Start()
    {
        animator = targetObject.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the target object!");
        }

        targetObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetObject.SetActive(false);
        targetObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator != null && animator.HasState(0, Animator.StringToHash(animationName)))
        {
            animator.Play(animationName);
        }
        else
        {
            Debug.LogError("Animation name is incorrect or does not exist!");
        }
    }
}
