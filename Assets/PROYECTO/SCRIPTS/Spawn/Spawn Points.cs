using UnityEngine;

[System.Serializable]
public class SpawnPoints
{
    public Transform[] round1;
    public Transform[] round2;
    public Transform[] round3;
    public Transform[] round4;
    public Transform[] round5;
    public Transform[] round6;
    public Transform[] round7;
    public Transform[] round8;
    public Transform[] round9;
    public Transform[] round10; 
    
    public Transform[] SpawnPointRounds(int round)
    {
        switch (round)
        {
            case 1: return round1;
            case 2: return round2;
            case 3: return round3;
            case 4: return round4;
            case 5: return round5;
            case 6: return round6;
            case 7: return round7;
            case 8: return round8;
            case 9: return round9;
            case 10: return round10;
            default:
                return null;
        }
    }
}
