using UnityEngine;

namespace RubyAdventure 
{ 
public class RubyController : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {
            Vector2 position = transform.position;

            position.x = position.x + 3f * Input.GetAxis("Horizontal") * Time.deltaTime;
            position.y = position.y + 3f * Input.GetAxis("Vertical") * Time.deltaTime;

            transform.position = position;
    }
}
}
