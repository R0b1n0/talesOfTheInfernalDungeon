using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScHighlightPerso : MonoBehaviour{
    [SerializeField] private InputActionReference Changing;
    [SerializeField] GameObject reeve, elayne, peneloppe;
    private enum Turn { Reeve, Elayne, Peneloppe }
    [SerializeField] private Turn persoTurn = Turn.Reeve;

    private void Awake() {
        elayne.SetActive(false);
        peneloppe.SetActive(false);
        reeve.SetActive(true);
    }

    public void ChoosePerso(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            switch (persoTurn) { 
            case Turn.Reeve:
                persoTurn = Turn.Elayne;
                break;
            case Turn.Elayne:
                persoTurn = Turn.Peneloppe;
                break;
            case Turn.Peneloppe:
                persoTurn = Turn.Reeve;
                break;
            }
        }
    }

    private void FixedUpdate() {
        switch (persoTurn) {
            case Turn.Reeve:
                elayne.SetActive(false);
                peneloppe.SetActive(false);
                reeve.SetActive(true);
                break;
            case Turn.Elayne:
                elayne.SetActive(true);
                peneloppe.SetActive(false);
                reeve.SetActive(false);
                break;
            case Turn.Peneloppe:
                elayne.SetActive(false);
                peneloppe.SetActive(true);
                reeve.SetActive(false);
                break;
        }
    }
}
