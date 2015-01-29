using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetIPsMain
{
    class TimeSelection
    {
        public DateTime today { get; set; }
        public String currentDateTime { get; set; }

        public String realDay { get; set; }
        public String realHour { get; set; }
        public String realMin { get; set; }
        public String hMin { get; set; }
        public int scanMinutes { get; set; }

        public int currentHourMinutes { get; set; }

        public int[][] scanTimes { get; set; }

        public TimeSelection()
        {
            today = DateTime.Now;
            currentDateTime = today.ToString("yyyy-MM-dd HH:mm" + ":00");

            realDay = today.ToString("dd");
            realHour = today.ToString("HH");
            realMin = today.ToString("mm");
            hMin = realHour + realMin;
            scanMinutes = chooseMinutes();

            currentHourMinutes = Convert.ToInt32(hMin);
            Console.WriteLine(currentHourMinutes);

            scanTimes = new int[][] 
            {
                new int[] {8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18},
                new int[] {0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55},
                new int[] {0, 15, 30, 47}
            };
        }

        

        private int chooseMinutes()
        {
            if ((currentHourMinutes > 830 && currentHourMinutes < 930) ||
                    (currentHourMinutes > 1200 && currentHourMinutes < 1400) ||
                    (currentHourMinutes > 1630 && currentHourMinutes < 1730))
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}
