using UnityEngine;

public class You : MonoBehaviour
{
    private Bw bw;

    private readonly string[] tags = {"bw", "bullet"};

    // Start is called before the first frame update
    private void Start()
    {
        bw = GameObject.FindWithTag("bw")?.GetComponent<Bw>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckCollide(other))
            if (bw != null)
                bw.DoCollide();
    }

    private bool CheckCollide(Collider other)
    {
        var collide = false;
        foreach (var tag in tags)
            if (tag.Equals(other.gameObject.tag))
                collide = true;
        return collide;
    }
}