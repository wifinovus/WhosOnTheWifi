using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetIPsMain
{
    class Scanner
    {
        private DBConnect db;
        private TimeSelection t;
        private String currentHour;
        private String currentMinutes;

        public Scanner()
        {
            db = new DBConnect();
            t = new TimeSelection();
        }

        public void go()
        {
            for (int i = 0; i < t.scanTimes[0].Length; i++)
            {

                for (int j = 0; j < t.scanTimes[t.scanMinutes].Length; j++)
                {

                    int arrayHour = t.scanTimes[0][i];
                    int arrayMinute = t.scanTimes[t.scanMinutes][j];

                    currentMinutes = "" + t.scanTimes[t.scanMinutes][(j - 1 + t.scanTimes[t.scanMinutes].Length) % t.scanTimes[t.scanMinutes].Length];
                    if (Convert.ToInt32(t.realHour) == arrayHour && Convert.ToInt32(t.realMin) == arrayMinute)
                    {
                        if (j == 0)
                        {
                            currentHour = "" + t.scanTimes[0][(i - 1 + t.scanTimes[0].Length) % t.scanTimes[0].Length];
                        }
                        else
                        {
                            currentHour = "" + t.scanTimes[0][(i)];
                        }
                        Console.WriteLine("work");

                        arrayMinute = Convert.ToInt32(t.realMin);
                        arrayHour = Convert.ToInt32(t.realHour);


                        //do this
                        Dictionary<string, string> list = GetIPs.getConnectedDevices();

                        List<string> listy = new List<string>();


                        foreach (string s in list.Keys)
                        {
                            Console.WriteLine("Initial Scan: " + s);

                            listy.Add(s);

                        }

                        //DBConnect db = new DBConnect();
                        List<String> listed = db.getMacs();

                        foreach (String a in listed)
                        {
                            Console.WriteLine("MAC stored in DB: " + a);
                        }

                        List<String> regList = new List<string>();

                        foreach (String s1 in listy)
                        {
                            foreach (String s2 in listed)
                            {
                                if (s1 == s2)
                                {
                                    regList.Add(s1);
                                }
                            }
                        }
                        foreach (String q in regList)
                        {
                            Console.WriteLine("Only registered MACs from scan: " + q);
                        }

                        DateTime lastTime = new DateTime(Convert.ToInt32(t.today.ToString("yyyy")), Convert.ToInt32(t.today.ToString("MM")), Convert.ToInt32(t.today.ToString("dd")), Convert.ToInt32(currentHour), Convert.ToInt32(currentMinutes), 0);//p.SelectLastTime();
                        String dayOfLastScan = lastTime.ToString("dd");
                        String stringLastTime = lastTime.ToString("yyyy-MM-dd HH:mm:ss");

                        List<String> logMacList = db.SelectLastScanTime(stringLastTime);
                        Boolean notFound = true;
                        foreach (String s1 in regList)
                        {
                            foreach (String s2 in logMacList)
                            {
                                if (s1 == s2 && t.realDay != dayOfLastScan)
                                {
                                    db.Insert(s1, t.currentDateTime);
                                    notFound = false;
                                }
                                else if (s1 == s2)
                                {
                                    db.Update(t.currentDateTime, s1, stringLastTime);
                                    notFound = false;
                                }
                                else
                                {
                                    notFound = true;
                                    //    p.Insert(s1, current);
                                }
                            }
                            if (notFound == true)
                            {
                                db.Insert(s1, t.currentDateTime);
                            }
                        }



                        //happen later
                        t.today = DateTime.Now;

                        t.realHour = t.today.ToString("HH");
                        t.realMin = t.today.ToString("mm");

                        i = 0;
                        j = 0;

                    }
                    else if (i == t.scanTimes[0].Length - 1 && j == t.scanTimes[t.scanMinutes].Length - 1)
                    {
                        t.today = DateTime.Now;
                        t.currentDateTime = t.today.ToString("yyyy-MM-dd HH:mm" + ":00");
                        t.realDay = t.today.ToString("dd");
                        t.realHour = t.today.ToString("HH");
                        t.realMin = t.today.ToString("mm");
                        t.hMin = t.realHour + t.realMin;

                        i = 0;
                        j = 0;
                    }
                }
            }
        }
    }
}
