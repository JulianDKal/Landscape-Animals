using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hex
{

    public Hex(int q, int r, int s)
    {
        if (q + r + s == 0)
        {
            this.q = q;
            this.r = r;
            this.s = s;
        }
        else Debug.LogWarning("q + r +s does not equal zero!");
    }

    public int q;
    public int r;
    public int s;
    }

