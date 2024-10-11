using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrystalSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject CrystalPrefub;
    [SerializeField] private int CrystalNum;
    [SerializeField] private Vector2 SpawnDelay = new Vector2(0.5f, 1f);

    private List<GameObject> CrystalList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < CrystalNum; i++)
        {
            var crystal = Instantiate(CrystalPrefub);
            crystal.transform.parent = transform;
            CrystalList.Add(crystal);
            crystal.SetActive(false);
        }

        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(SpawnDelay.x, SpawnDelay.y));
            SpawnCrystal();
            yield return null;
        }
    }

    void SpawnCrystal()
    {
        if (CrystalList.Where(x => !x.gameObject.activeSelf).Count() <= 0)
        {
            return;
        }

        var crystal = CrystalList.FirstOrDefault(x => !x.gameObject.activeSelf);
        crystal.SetActive(true);
        crystal.GetComponent<CrystalBehaviour>().Spawn(new Vector3(Random.Range(-transform.localScale.x / 2f, transform.localScale.x / 2f), Random.Range(-transform.localScale.y / 2f, transform.localScale.y / 2f), 0) + transform.position);
	}
}
