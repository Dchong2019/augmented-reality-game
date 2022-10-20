using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database
{
    public class ScoresClass
    {
        public string _date;
        public string _userName;
        public int _points;
        public int _perfectCatches;
        public int _thrown;
        public int _caught;
        public int _perfectGame; 

        public ScoresClass(string userName, string date, string points, string perfectCatches, string thrown, string caught, string perfectGame)
        {
            _userName = userName;
            _date = date;
            _points = Int32.Parse(points);
            _perfectCatches = Int32.Parse(perfectCatches);
            _thrown = Int32.Parse(thrown);
            _caught = Int32.Parse(caught);
            _perfectGame = Int32.Parse(perfectGame);
        }

        public ScoresClass(string userName, string date, string points, string perfectCatches, string thrown, string caught, bool perfectGame)
        {
            _userName = userName;
            _date = date;
            _points = Int32.Parse(points);
            _perfectCatches = Int32.Parse(perfectCatches);
            _thrown = Int32.Parse(thrown);
            _caught = Int32.Parse(caught);
            _perfectGame = Convert.ToInt32(perfectGame);
        }

        public static ScoresClass getFakeScore()
        {
            return new ScoresClass("katie", "2/15/22", "80", "90", "50", "95", "0");
        }
    }


}