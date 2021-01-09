using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public JackMove move;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        move.GroundSet(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        move.GroundSet(false);
    }
}
