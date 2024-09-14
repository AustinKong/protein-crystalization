using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toaster : Interactable
{
    private int breadCount = 0;
    private bool isToasting = false;
    [SerializeField]
    private GameObject toastPrefab;

    public override Interactable Interact(Interactable other)
    {
        if (other != null) {
            // Put bread into toaster
            if (other.Is("Bread") && breadCount < 2)
            {
                breadCount++;
                Destroy(other.gameObject);
                return null;
            }
        } else {
            // Turn on toaster
            if (breadCount == 2 && !isToasting)
            {
                StartCoroutine(Toast());
                return null;
            } else {
                return this;
            }
        }

        return other;
    }

    public override InteractionCheck CanInteract(Interactable other)
    {
        if (other == null && breadCount < 2) return new InteractionCheck(true, "Pick up toaster");

        if (other != null && other.Is("Bread") && breadCount < 2) return new InteractionCheck(true, "Put bread into toaster");
        if (breadCount == 2 && !isToasting && other == null) return new InteractionCheck(true, "Turn on toaster");

        return new InteractionCheck(false, "");
    }

    private IEnumerator Toast() {
        isToasting = true;
        float time = 0;
        while (time < 3) {
            time += 0.4f;
            yield return new WaitForSeconds(0.1f);
            rb.AddTorque(Random.Range(-1f, 1f) * 0.3f, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * Random.Range(0.5f, 1f), ForceMode2D.Impulse);
        }
        isToasting = false;
        Instantiate(toastPrefab, transform.position + Vector3.up, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        Instantiate(toastPrefab, transform.position + Vector3.up, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4, ForceMode2D.Impulse);
        breadCount = 0;
    }
}
