using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private PlayerMovementComponent mover;

    [Header("Elements")]
    [SerializeField] private ElementData[] elements;   // sırası: Fire, Water, Earth, Air

    private Dictionary<ElementType, IAbility> abilityMap;
    private IAbility currentAbility;
    private int currentIndex;

    private void Awake()
    {
        mover = mover ?? GetComponent<PlayerMovementComponent>();

        // Sahnedeki tüm IAbility komponentlerini al, ElementType'a göre map'le
        abilityMap = new Dictionary<ElementType, IAbility>();
        foreach (var ability in GetComponents<IAbility>())
            abilityMap[ability.ElementType] = ability;
    }

    private void Start() => ApplyElement(0);

    private void Update()
    {
        // Hareket & zıplama
        float dirX = Input.GetAxisRaw("Horizontal");
        mover.Move(dirX);
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
             && mover.IsGrounded())
            mover.Jump();

        // Element değiştir (Q tuşu)
        if (Input.GetKeyDown(KeyCode.Q))
            ApplyElement((currentIndex + 1) % elements.Length);

        // Yeteneği kullan (sol click)
        if (Input.GetMouseButtonDown(0))
            currentAbility?.Use();
    }

    private void ApplyElement(int newIndex)
    {
        currentIndex = newIndex;
        var data = elements[newIndex];

        // 1) Renk atama (alpha=1)
        var sr = GetComponent<SpriteRenderer>();
        var c = data.tintColor;
        c.a = 1f;
        sr.color = c;

        // // 2) AnimController
        // var anim = GetComponent<Animator>();
        // if (data.animCtrl != null)
        //     anim.runtimeAnimatorController = data.animCtrl;

        // 3) Doğru ability'i seç
        if (abilityMap.TryGetValue(data.elementType, out var ab))
            currentAbility = ab;
        else
            currentAbility = null;

        Debug.Log($"[PlayerController] Mode → {data.elementType}, Ability → {currentAbility?.GetType().Name}");
    }
}
