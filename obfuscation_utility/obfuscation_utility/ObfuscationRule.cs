using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obfuscation_utility
{
    public enum IntRuleState
    { more = 0, less = 1, equal = 2, not_equeal = 3, less_or_equal = 4, more_or_equal = 5 }
    public enum StringRuleState
    { equal = 0, not_equeal = 1, is_not_substr = 2, is_substr = 3 }
    public enum BoolRuleState
    {
        equal = 0, not_equeal = 1
    }
    public enum DateRuleState
    { equal = 0, earlier = 1, later = 2 }
    public enum RuleMoificators
    { none = 0, is_geographical_coords = 1 }

    public class ObfuscationRule
    {
        int RuleId;
        string RuleTextForm;
        string RuleAttrib;
        string atribType;
        string RuleTable;
        object RuleOldValue;
        object RuleNewValue;
        object RuleState;
        List<RuleMoificators> activeModificators;

        string bindedAtrib = null;
        double bindedAtribValue;
         public class GeographiclObj
        {
            string OldGeographicalLocation;
            string NewGeographicalLocation;
            double oldLat = -1, oldLon = -1;
            double newLat = -1, newLon = -1;
            double LatBound = -1, LonBound = -1;
            static Dictionary<string, List<KeyValuePair<double, double>>> worldParts_endPoints = new Dictionary<string, List<KeyValuePair<double, double>>>()
            {
                //S,N,W,E
                {"North America", new List<KeyValuePair<double, double>>()
                    {
                    new KeyValuePair<double, double>(71.833333,-94.75),
                    new KeyValuePair<double, double>(7.2,-80.866667),
                    new KeyValuePair<double, double>(65.583333,-168.083333),
                    new KeyValuePair<double, double>(52.4,-55.666667)
                    }
                },
                 {"South America", new List<KeyValuePair<double, double>>()
                    {
                    new KeyValuePair<double, double>(12.45,-71.65),
                    new KeyValuePair<double, double>(-53.9,-71.3),
                    new KeyValuePair<double, double>(-56.5,-68.716667),
                    new KeyValuePair<double, double>(-7.166667,-34.783333)
                    }
                },
                {"Asia", new List<KeyValuePair<double, double>>()
                    {
                    new KeyValuePair<double, double>(77.716667,104.3),
                    new KeyValuePair<double, double>(1.266667,103.05),
                    new KeyValuePair<double, double>(39.483333,26.166667),
                    new KeyValuePair<double, double>(66.066667,169.65)
                    }
                },
                 {"Africa", new List<KeyValuePair<double, double>>()
                    {
                    new KeyValuePair<double, double>(37.34,9.74),
                    new KeyValuePair<double, double>(-34.82,20),
                    new KeyValuePair<double, double>(14.74,-17.53),
                    new KeyValuePair<double, double>(10.45,51.4)
                    }
                },
                {"Europe", new List<KeyValuePair<double, double>>()
                    {
                    new KeyValuePair<double, double>(71.133333,27.65),
                    new KeyValuePair<double, double>(36,-5.6),
                    new KeyValuePair<double, double>(38.766667,9.483333),
                    new KeyValuePair<double, double>(67.75,66.216667)
                    }
                },                     
                {"Australia", new List<KeyValuePair<double, double>>()
                    {
                    new KeyValuePair<double, double>(-10.683333,142.533333),
                    new KeyValuePair<double, double>(-39.183333,146.416667),
                    new KeyValuePair<double, double>(-26.15,113.15),
                    new KeyValuePair<double, double>(-28.666667,153.566667)
                    }
                }
            };

            public GeographiclObj(double oldLat, double oldLon, double LatBound, double LonBound,
                string NewGeographicalLocation = "", string OldGeographicalLocation = "")
            {

                
                this.OldGeographicalLocation = OldGeographicalLocation;
                this.NewGeographicalLocation = NewGeographicalLocation;
                this.oldLat = oldLat;
                this.oldLon = oldLon;
                this.LatBound = LatBound;
                this.LonBound = LonBound;
                // need calculus
                if (NewGeographicalLocation== "America")
                {
                    Random r = new Random();
                    List<string> l = new List<string>() { "North America", "South America" };
                    NewGeographicalLocation = l[r.Next(0, l.Count - 1)];
                }
                if (NewGeographicalLocation == "Indian")
                {
                    NewGeographicalLocation = "Africa";
                }
                if (NewGeographicalLocation == "Pacific" || NewGeographicalLocation == "Atlantic")
                {
                    Random r = new Random();
                    List<string> l = new List<string>() { "Asia", "Africa","Europe" };
                    NewGeographicalLocation = l[r.Next(0, l.Count - 1)];
                }    
                if (NewGeographicalLocation == "Arctic" || NewGeographicalLocation == "Antarctica")
                {
                    Random r = new Random();
                    List<string> l = new List<string>() { "Asia", "Africa", "Europe" };
                    NewGeographicalLocation = l[r.Next(0, l.Count - 1)];
                }
                var points = worldParts_endPoints[NewGeographicalLocation];
                double LatCoeff = CalculateСarryoverСoefficient(points[0].Key, points[1].Key, points[2].Key, points[3].Key);
                double LonCoeff = CalculateСarryoverСoefficient(points[0].Value, points[1].Value, points[2].Value, points[3].Value);
                //if (LatCoeff * oldLat - LatBound<)
                //{

                //}
                newLat = LatCoeff * oldLat - LatBound;
                newLon = LonCoeff * oldLon - LonBound;
            }
            public double getNewLat()
            { return this.newLat; }

            public double getNewLon()
            { return this.newLon; }



            double CalculateСarryoverСoefficient(double WestLat, double EastLat, double NorthLat, double SouthLat)
            {
                WestLat = Math.Abs(WestLat);
                EastLat = Math.Abs(EastLat);
                NorthLat = Math.Abs(NorthLat);
                SouthLat = Math.Abs(SouthLat);

                double Min = Math.Min(WestLat, EastLat);
                double Min1 = Math.Min(NorthLat, SouthLat);
                Min = Math.Min(Min, Min1);

                WestLat = WestLat / Min;
                EastLat = EastLat / Min;
                NorthLat = NorthLat / Min;
                SouthLat = SouthLat / Min;

                Min = Math.Min(WestLat, EastLat);
                Min1 = Math.Min(NorthLat, SouthLat);
                Min = Math.Min(Min, Min1);

                double Max = Math.Min(WestLat, EastLat);
                double Max1 = Math.Min(NorthLat, SouthLat);
                Max = Math.Min(Max, Max1);

                List<double> p = new List<double>() { WestLat, EastLat, NorthLat, SouthLat };
                for (int i = 0; i < p.Count; i++)
                {
                    if (p[i] - Min == 0 || p[i] - Max == 0)
                    {
                        p.RemoveAt(1);
                    }
                }


                return 1 / (Min * Max - p[0] * p[1]);
            }

        }
        GeographiclObj geographiclObj;
        string replacePattern;

        int GetRuleId()
        {
            return this.RuleId;
        }
       public string GetRuleTextForm() { return this.RuleTextForm; }
        public string GetRuleAttrib() { return this.RuleAttrib; }
        public string GetAtribType() { return this.atribType; }
        public string GetRuleTable() { return this.RuleTable; }
        public object GetRuleOldValue() { return this.RuleOldValue; }
        public object GetRuleNewValue() { return this.RuleNewValue; }
        public object GetRuleState() { return this.RuleState; ; }

        public List<RuleMoificators> ActiveModificators { get => activeModificators; }
        public string GetBindedAtrib { get => bindedAtrib; }
        public double GetBindedAtribValue { get => bindedAtribValue;}

        public ObfuscationRule(int RuleId, string RuleTextForm, string RuleAttrib, string atribType, string RuleTable,
        object RuleOldValue, object RuleNewValue, object RuleState, string replacePattern, List<RuleMoificators> mofocators,GeographiclObj GeoObj,
        string bindedAtrib = null, double bindedAtribValue = -100000)
        {
            this.RuleId = RuleId;
            this.RuleTextForm = RuleTextForm;
            this.RuleAttrib = RuleAttrib;
            this.RuleTable = RuleTable;
            this.RuleOldValue = RuleOldValue;
            this.RuleNewValue = RuleNewValue;
            this.RuleState = RuleState;
            this.replacePattern = replacePattern;
            this.activeModificators = mofocators;
            this.bindedAtrib = bindedAtrib;
            this.bindedAtribValue = bindedAtribValue;
            this.geographiclObj = GeoObj;
        }
        //public void AddModificators
    }

}
