using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scoring
{
    public class BaseballGame
    {
        public string User { get; }
        public string Date { get; }
        public int Points { get; set; }
        public int PerfectCatches { get; set; }
        public int Thrown { get; set; }
        public int Caught { get; set; }
        public bool PerfectGame { get; set; }
       
        private BaseballGame(string user)
        {
            User = user;
            Date = DateTime.Now.ToShortDateString();
            Points = 0;
            PerfectCatches = 0;
            Thrown = 0;
            Caught = 0;
            PerfectGame = true;
        }

        public static BaseballGame New(string user)
        {
            if(!(String.IsNullOrEmpty(user)))
            {
                return new BaseballGame(user);
            }

            return null;
        }

        public void Throw(int points)
        {
            Thrown++;
            AdjustScore(points);
            
        }

        public void AdjustScore(int amt)
        {
            if (amt > 0)
            {
                Points += amt;
                Caught++;

                if (amt == 2)
                {
                    PerfectCatches += 1;
                }
            }
            if (Thrown != Caught)
            {
                PerfectGame = false;
            }
        }
    }
} 

