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
            animator.Play(animationName);
    }
    public void ChangeMenu()
    {
        animator.Play(animationName);
    }
}
