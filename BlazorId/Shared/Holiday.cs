﻿
using System.Collections.Generic;

namespace BlazorId.Shared
{
    public class Holiday
    {
        public int Day {  get; set; }
        public int Month { get; set; }
        public Holiday(int day,int month)
        {
            Day = day;
            Month = month;
        }
       
    }
}
