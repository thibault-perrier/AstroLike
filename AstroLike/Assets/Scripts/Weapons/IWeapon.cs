using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public int id { get; }
    public int name { get; set; }
    public int nbUtilisation { get; set; }
}
