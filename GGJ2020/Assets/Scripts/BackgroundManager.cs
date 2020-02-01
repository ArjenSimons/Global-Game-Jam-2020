using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> smallMountains = new List<GameObject>();
    [SerializeField]
    private List<GameObject> bigMountains = new List<GameObject>();
    [SerializeField]
    private List<GameObject> sea = new List<GameObject>();

    [SerializeField]
    private float smallMountainSpeed = 0.5f, bigMountainSpeed = 0.5f, seaSpeed = 0.5f;

    [SerializeField]
    private BoatMovement player;

    [SerializeField]
    private GameObject bg;

    private SpriteRenderer bgRender;

    // Start is called before the first frame update
    void Start()
    {
        bgRender = bg.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < smallMountains.Count; i++)
        {
            smallMountains[i].transform.Translate(player.speed * smallMountainSpeed * Time.deltaTime, 0, 0);

            var mountainPos = smallMountains[i].transform.position;

            if (mountainPos.x < bg.transform.position.x - (bgRender.size.x * bg.transform.localScale.x))
            {
                mountainPos.x = bg.transform.position.x + bgRender.size.x * bg.transform.localScale.x;
                smallMountains[i].transform.position = mountainPos;
            }
        }

        for (int i = 0; i < bigMountains.Count; i++)
        {
            bigMountains[i].transform.Translate(player.speed * bigMountainSpeed * Time.deltaTime, 0, 0);

            var mountainPos = bigMountains[i].transform.position;

            if (mountainPos.x < bg.transform.position.x - (bgRender.size.x * bg.transform.localScale.x))
            {
                mountainPos.x = bg.transform.position.x + bgRender.size.x * bg.transform.localScale.x;
                bigMountains[i].transform.position = mountainPos;
            }
        }

        for (int i = 0; i < sea.Count; i++)
        {
            sea[i].transform.Translate(player.speed * seaSpeed * Time.deltaTime, 0, 0);

            var waterPos = sea[i].transform.position;

            if (waterPos.x < bg.transform.position.x - (bgRender.size.x * bg.transform.localScale.x))
            {
                waterPos.x = bg.transform.position.x + bgRender.size.x * bg.transform.localScale.x;
                sea[i].transform.position = waterPos;
            }
        }
    }
}
