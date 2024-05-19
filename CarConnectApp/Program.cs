using CarConnect.CarConnectApp;
using CarConnect.Util;
using System.Data.SqlClient;
using System;
using CarConnect.Exceptions;
using CarConnect.Model;
using CarConnect.Service;

namespace CarConnect
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("------------Welcome to CarConnect-------------");
            CarConnectApplication.MainMenu();
        }
    }
}


