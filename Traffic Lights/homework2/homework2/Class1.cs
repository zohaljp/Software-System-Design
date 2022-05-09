using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// COP 4365, Spring 2022
// Homework 4: traffic study
// This assignment uses the instances of the the traffic light object that communicate with each other to switch states, there are 2 classes, 1 for the light traffic object and 1 for car objects, both in the same file which is this one
//Ahmed El Maliki

namespace homework2
{
    internal class Class1
    {
        string stoplightname;
        string currentstatus;

        public void setname(string name) { stoplightname = name; }
        public void setstatus(string status) { currentstatus = status; }
        public string getstoplightname() { return stoplightname; }
        public string getCurrentstatus() { return currentstatus; }

        

    }

    internal class Class2

    {

        string sequence;
        int arrivalTime;
        int leavingTime;
        char direction;
        int waittime;

       public void setsequence(string seq) { sequence = seq; }
       public void setarrivaltime(int artime) { arrivalTime = artime; }
       public void setleavinfTime(int leavinfTime) { leavingTime = leavinfTime; }

        public void setdirection(char dir) { direction = dir; }
        public char getdirection() { return direction; }


       public string getsequence() { return sequence; }
       public int getarrivaltime() { return arrivalTime; }
       public int getleavingtime() { return leavingTime; }

       public void setwaittime(int wat ) { waittime = wat; }
       public int getwaittime() { return waittime; }


       


  



    }
}
