using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;

namespace HRVZZMapControl_ZZGA
{
    public class MapViewModel:UserControl
    {
        public int RegionCount = 0;
        //public int RegionID = 0;

        public int LowLevel = 20;
        public int MiddleLevel = 40;
        public int HighLevel = 60;

        public List<Region> RegionList { get; set; }
        public DataTable RegionTable { get; set; }
        public string[] RegionNames { get; set; }

        public MapViewModel(DataTable regionTable, string[] regionNames)
        {
            RegionTable = regionTable;
            RegionNames = regionNames;
            RegionCount = RegionNames.Length;

            InitRegionList();
        }

        private void InitRegionList()
        {
            RegionList = new List<Region>();
            for (int i = 0; i < RegionCount; i++)
            {
                int reNo = -1;  // 如果DataTable中不包含查找的区域名称，那么区域ID设置为-1
                string rn = RegionNames[i];
                for (int j = 0; j < RegionTable.Rows.Count; j++)
                {
                    Region ron;
                    string rt = RegionTable.Rows[j][5].ToString().Trim();
                    if (rn == rt)
                    {
                        reNo = i;
                        ron = new Region
                        {
                            RegionID = reNo,
                            PoliceCase = Convert.ToInt32(RegionTable.Rows[j][0]),
                            SpecialCase = Convert.ToInt32(RegionTable.Rows[j][1]),
                            PolicePower = Convert.ToInt32(RegionTable.Rows[j][2]),
                            CriminalCase = Convert.ToInt32(RegionTable.Rows[j][3]),
                            TrafficCase = Convert.ToInt32(RegionTable.Rows[j][4]),
                            RegionName = rt
                        };

                        RegionList.Add(ron);
                    }
                }
            }
        }

        private DataTable InitDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable = RegionTable.Clone();

            for (int i = 0; i < RegionCount; i++)
            {
                string rn = RegionNames[i];

                for (int j = 0; j < RegionTable.Rows.Count; j++)
                {

                    string rt = RegionTable.Rows[j][5].ToString().Trim();
                    if (rn == rt)
                    {
                        dataTable.ImportRow(RegionTable.Rows[j]);
                    }
                }
            }

            return dataTable;
        }
    }

    public class Region
    {
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public int PoliceCase { get; set; }
        public int SpecialCase { get; set; }
        public int PolicePower { get; set; }
        public int CriminalCase { get; set; }
        public int TrafficCase { get; set; }

        public Region() { }
    }

}
