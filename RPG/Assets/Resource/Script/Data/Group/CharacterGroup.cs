using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterGroup : MonoBehaviour
{
    public const int TEAM_MEMBER_NUM = 4;
    public Dictionary<string, Character> list;
    public void init()
    {
        list = new Dictionary<string, Character>();

        int seq = 0;
        foreach (Transform child in transform)
        {
            var data = child.gameObject.GetComponent<Character>();
            data.seq = seq++;
            if (list.ContainsKey(data.characterName))
                Debug.Log("Character duplicate:" + data.characterName);

            list.Add(data.characterName, data);
        }
    }

    public Character getCharacterBySeq(int seq)
    {
        foreach (Character character in list.Values)
        {
            if (character.seq == seq)
                return character;
        }

        return null;
    }

    public List<int> getCharacterSeqs()
    {
        List<int> seqs = new List<int>();
        foreach (Character character in list.Values)
        {
            seqs.Add(character.seq);
        }

        return seqs;
    }

    public int getLvExp(int lv)
    {
        int[] lvExp = new int[]{
            0, 20, 30, 40, 50, 60, 70, 80, 90, 100,
            150, 200, 250, 300, 350, 400, 450, 500, 600, 700,
            800, 900, 1000, 1200, 1400, 1600, 1800, 2000, 2300, 2600,
            2900, 3200, 3500, 3800, 4200, 4600, 5000, 5400, 5800, 6200,
            6600, 7000, 7500, 8000, 8500, 9000, 9500, 10000, 11000, 12000
        };
        return lvExp[lv];
    }
}
