﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;


namespace GetIPsMain
{
    class Program
    {
        static void Main(string[] args)
        {
            new Scanner().go();
        }
    }
}

