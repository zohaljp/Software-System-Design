using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace homework2
{
    public partial class Form1 : Form
    {
        Class1 North = new Class1();
        Class1 South = new Class1();
        Class1 East = new Class1();
        Class1 West = new Class1();
        List<Class2> scannedsCars = new List<Class2>(); // scan file and store 
        

        Queue<Class2> northlight = new Queue<Class2>(); // all these queues mimic a line in a light
        Queue<Class2> southlight = new Queue<Class2>();
        Queue<Class2> westlight = new Queue<Class2>();
        Queue<Class2> eastlight = new Queue<Class2>();




        int counter;
        int northcars = 0;
        int southcars = 0;
        int westcars = 0;
        int eastcars = 0;
        int maxnorth = 0;
        int maxsouth = 0;
        int maxeast = 0;
        int maxwest = 0;
       
        



        void updateevent(Class1 North, Class1 South, Class1 East, Class1 West, int counter)
        {
            int emergency = 0;

            // let's start with North and South conditions-- if east or west is either yellow or green, north and south are red

            if (East.getCurrentstatus() == "Green" || West.getCurrentstatus() == "Green" || East.getCurrentstatus() == "Yellow" || West.getCurrentstatus() == "Yellow" && emergency == 0) { North.setstatus("Red"); South.setstatus("Red");  }

            // let's set up conditions for east and west, if either north or south are green or yellow, east and west are red
            if (North.getCurrentstatus() == "Green" || South.getCurrentstatus() == "Green" || North.getCurrentstatus() == "Yellow" || South.getCurrentstatus() == "Yellow" && emergency == 0) { East.setstatus("Red"); West.setstatus("Red");   }


            //North going green
            if (East.getCurrentstatus() == "Red" && West.getCurrentstatus() == "Red" && counter % 6 == 0 && South.getCurrentstatus() == "Red" && emergency == 0) { North.setstatus("Green"); }

            // North going yellow
            if (North.getCurrentstatus() == "Green" && East.getCurrentstatus() == "Red" && West.getCurrentstatus() == "Red" && counter % 3 == 0 && South.getCurrentstatus() == "Green" && emergency == 0) { North.setstatus("Yellow");  }
            //North going Red
            if (North.getCurrentstatus() == "Yellow" && East.getCurrentstatus() == "Red" && West.getCurrentstatus() == "Red" && counter % 6 == 0 && South.getCurrentstatus() == "Green" && emergency == 0) { North.setstatus("Red"); }


            // South turning green
            if (South.getCurrentstatus() == "Red" && East.getCurrentstatus() == "Red" && West.getCurrentstatus() == "Red" && counter % 6 == 0 && North.getCurrentstatus() == "Green" && emergency == 0) { South.setstatus("Green");  }

            // South going yellow
            if (South.getCurrentstatus() == "Green" && East.getCurrentstatus() == "Red" && West.getCurrentstatus() == "Red" && North.getCurrentstatus() == "Red" && counter % 5 == 0 && emergency == 0) { South.setstatus("Yellow");  }

            // setting South to Red and giving control to East

            if (South.getCurrentstatus() == "Yellow" && East.getCurrentstatus() == "Red" && West.getCurrentstatus() == "Red" && North.getCurrentstatus() == "Red" && counter % 6 == 0 && emergency == 0) { South.setstatus("Red"); East.setstatus("Green");  }

            // East going yellow 


            if (East.getCurrentstatus() == "Green" && West.getCurrentstatus() == "Green" && (counter % 9 == 0 || counter % 59 == 0) && emergency == 0) { East.setstatus("Yellow"); }
            // East going Red 

            if (East.getCurrentstatus() == "Yellow" && counter % 10 == 0 && emergency == 0) { East.setstatus("Red"); }




            // Transfer Control to West

            if (North.getCurrentstatus() == "Red" && East.getCurrentstatus() == "Green" && counter % 8 == 0 && South.getCurrentstatus() == "Red" && emergency == 0) { West.setstatus("Green");  }

            if (North.getCurrentstatus() == "Red" && East.getCurrentstatus() == "Red" && counter % 11 == 0 && South.getCurrentstatus() == "Red" && emergency == 0) { West.setstatus("Yellow"); }

            // Transfer control back to North and start over 
            if (West.getCurrentstatus() == "Yellow" && East.getCurrentstatus() == "Red" && counter % 12 == 0 && South.getCurrentstatus() == "Red" && emergency == 0) { West.setstatus("Red"); North.setstatus("Green"); }


            // Emergency vehicule simulation, this part can be deleted and code would still normally function 

        }






        public Form1()
        {
            InitializeComponent();


            counter = 0;
            North.setstatus("Green");
            South.setstatus("Red");
            East.setstatus("Red");
            West.setstatus("Red");

            pictureBox1.BackColor = Color.Green;
            pictureBox2.BackColor = Color.Gray;
            pictureBox3.BackColor = Color.Gray;

            pictureBox4.BackColor = Color.Red;
            pictureBox5.BackColor = Color.Gray;
            pictureBox6.BackColor = Color.Gray;



            pictureBox7.BackColor = Color.Red;
            pictureBox8.BackColor = Color.Gray;
            pictureBox9.BackColor = Color.Gray;




            pictureBox10.BackColor = Color.Red;
            pictureBox11.BackColor = Color.Gray;
            pictureBox12.BackColor = Color.Gray;

        

            string outp;
            outp = String.Format("Time: {0}     North: {1:15}         South: {2:15}        East: {3:15}        West: {4:15}", counter, North.getCurrentstatus(), South.getCurrentstatus(), East.getCurrentstatus(), West.getCurrentstatus()); // print initial conditions
            Console.WriteLine(outp);


            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\elmal\Desktop\homework2\homework2\HW #4 Data.txt"); // scan file as an array
            int i = 0;

            foreach (string line in lines)

            {
                scannedsCars.Insert(i, new Class2()); // create a car object for each line

                scannedsCars[i].setsequence(line);
                int length = line.Length - 1;

                String time = line.Substring(1, length);
                int timeint = Int32.Parse(time);
                scannedsCars[i].setarrivaltime(timeint);
                scannedsCars[i].setdirection(line[0]);

                i++;


            }





        }

        private void Form1_Load(object sender, EventArgs e)
        {



        }

        private void timer1_Tick(object sender, EventArgs e) // if you want to run this infinitly delete the conditional statment for the timer being over 60 seconds
        {

            counter++;
            if (counter > 240)
            {
                timer1.Dispose();

                return;
            }


           

            updateevent(North, South, East, West, counter);




            // function to enqueue car objects 

            for (int i = 0; i < scannedsCars.Count; i++)
            {
                if (scannedsCars[i].getarrivaltime() == counter)
                {
                    if (scannedsCars[i].getdirection() == 'N') { northlight.Enqueue(scannedsCars[i]); northcars++; if (North.getCurrentstatus() == "Red" || North.getCurrentstatus()=="Yellow") maxnorth++; }
                    if (scannedsCars[i].getdirection() == 'S') { southlight.Enqueue(scannedsCars[i]); southcars++; if (South.getCurrentstatus() == "Red" || South.getCurrentstatus() == "Yellow") maxsouth++; }
                    if (scannedsCars[i].getdirection() == 'E') { eastlight.Enqueue(scannedsCars[i]); eastcars++; if (East.getCurrentstatus() == "Red" || East.getCurrentstatus() == "Yellow") maxeast++; }
                    if (scannedsCars[i].getdirection() == 'W') { westlight.Enqueue(scannedsCars[i]); westcars++; if (West.getCurrentstatus() == "Red" || West.getCurrentstatus() == "Yellow") maxwest++; }
                }

                // functions to dequeue car objects 

            }

            if (northlight.Count != 0 && northlight.Peek().getdirection() == 'N' && North.getCurrentstatus() == "Green") { northlight.Peek().setleavinfTime(counter); northlight.Peek().setwaittime(counter - northlight.Peek().getarrivaltime()); Console.WriteLine("Sequence: {0}----- Arrival time: {1}----- Wait time: {2}--- Leaving time: {3}", northlight.Peek().getsequence(), northlight.Peek().getarrivaltime(), northlight.Peek().getwaittime(), northlight.Peek().getleavingtime()); northlight.Dequeue(); }
            if (southlight.Count != 0 && southlight.Peek().getdirection() == 'S' && South.getCurrentstatus() == "Green") { southlight.Peek().setleavinfTime(counter); southlight.Peek().setwaittime(counter - southlight.Peek().getarrivaltime()); Console.WriteLine("Sequence: {0}----- Arrival time: {1}----- Wait time: {2}--- Leaving time: {3}", southlight.Peek().getsequence(), southlight.Peek().getarrivaltime(), southlight.Peek().getwaittime(), southlight.Peek().getleavingtime()); southlight.Dequeue(); }
            if (eastlight.Count != 0 && eastlight.Peek().getdirection() == 'E' && East.getCurrentstatus() == "Green") { eastlight.Peek().setleavinfTime(counter); eastlight.Peek().setwaittime(counter - eastlight.Peek().getarrivaltime()); Console.WriteLine("Sequence: {0}----- Arrival time: {1}----- Wait time: {2}--- Leaving time: {3}", eastlight.Peek().getsequence(), eastlight.Peek().getarrivaltime(), eastlight.Peek().getwaittime(), eastlight.Peek().getleavingtime()); eastlight.Dequeue(); }
            if (westlight.Count != 0 && westlight.Peek().getdirection() == 'W' && West.getCurrentstatus() == "Green") { westlight.Peek().setleavinfTime(counter); westlight.Peek().setwaittime(counter - westlight.Peek().getarrivaltime()); Console.WriteLine("Sequence: {0}----- Arrival time: {1}----- Wait time: {2}--- Leaving time: {3}", westlight.Peek().getsequence(), westlight.Peek().getarrivaltime(), westlight.Peek().getwaittime(), westlight.Peek().getleavingtime()); westlight.Dequeue(); }





            // Logic to update picture box has nothing to do with the logic of the code, the code is in a seperate class in a seperate method as requested 
            if (North.getCurrentstatus() == "Red")
            {
                pictureBox1.BackColor = Color.Red;
                pictureBox2.BackColor = Color.Gray;
                pictureBox3.BackColor = Color.Gray;

            }
            else if (North.getCurrentstatus() == "Green")
            {
                pictureBox1.BackColor = Color.Gray;
                pictureBox2.BackColor = Color.Gray;
                pictureBox3.BackColor = Color.Green;

            }

            else if (North.getCurrentstatus() == "Yellow")
            {
                pictureBox1.BackColor = Color.Gray;
                pictureBox2.BackColor = Color.Yellow;
                pictureBox3.BackColor = Color.Gray;

            }




            if (South.getCurrentstatus() == "Red")
            {
                pictureBox4.BackColor = Color.Red;
                pictureBox5.BackColor = Color.Gray;
                pictureBox6.BackColor = Color.Gray;

            }
            else if (South.getCurrentstatus() == "Green")
            {
                pictureBox4.BackColor = Color.Gray;
                pictureBox5.BackColor = Color.Gray;
                pictureBox6.BackColor = Color.Green;

            }

            else if (South.getCurrentstatus() == "Yellow")
            {
                pictureBox4.BackColor = Color.Gray;
                pictureBox5.BackColor = Color.Yellow;
                pictureBox6.BackColor = Color.Gray;

            }

            if (East.getCurrentstatus() == "Red")
            {
                pictureBox7.BackColor = Color.Red;
                pictureBox8.BackColor = Color.Gray;
                pictureBox9.BackColor = Color.Gray;

            }
            else if (East.getCurrentstatus() == "Green")
            {
                pictureBox7.BackColor = Color.Gray;
                pictureBox8.BackColor = Color.Gray;
                pictureBox9.BackColor = Color.Green;

            }



            else if (East.getCurrentstatus() == "Yellow")
            {
                pictureBox7.BackColor = Color.Gray;
                pictureBox8.BackColor = Color.Yellow;
                pictureBox9.BackColor = Color.Gray;

            }

            if (West.getCurrentstatus() == "Red")
            {
                pictureBox10.BackColor = Color.Red;
                pictureBox11.BackColor = Color.Gray;
                pictureBox12.BackColor = Color.Gray;

            }
            else if (West.getCurrentstatus() == "Green")
            {
                pictureBox10.BackColor = Color.Gray;
                pictureBox11.BackColor = Color.Gray;
                pictureBox12.BackColor = Color.Green;

            }


            else if (West.getCurrentstatus() == "Yellow")
            {
                pictureBox10.BackColor = Color.Gray;
                pictureBox11.BackColor = Color.Yellow;
                pictureBox12.BackColor = Color.Gray;

            }





            if (counter%3==0 ) // case 1 set the 


            {

                
                

                string outp1;
                outp1 = String.Format("Time: {0}     North: {1:15}         South: {2:15}        East: {3:15}        West: {4:15}", counter, North.getCurrentstatus(), South.getCurrentstatus(), East.getCurrentstatus(), West.getCurrentstatus());
                Console.WriteLine(outp1);
               

            }

            if (counter==240)


            {
                double waitsouth = 0;
                int numsouth = 0;
                double waitnorth = 0;
                int numnorth = 0;
                double waiteast = 0;
                int numeast = 0;
                double waitwest = 0;
                int numwest = 0;

                for (int j = 0; j < scannedsCars.Count; j++)

                {
                    if (scannedsCars[j].getdirection() == 'S') { waitsouth += scannedsCars[j].getwaittime(); numsouth++; }

                    if (scannedsCars[j].getdirection() == 'N') { waitnorth += scannedsCars[j].getwaittime(); numnorth++; }
                  
                    if (scannedsCars[j].getdirection() == 'E') { waiteast += scannedsCars[j].getwaittime(); numeast++; }
                   
                    if (scannedsCars[j].getdirection() == 'W') { waitwest += scannedsCars[j].getwaittime(); numwest++; }



                }

                waitsouth = waitsouth / numsouth;
                waitnorth = waitnorth / numnorth;
                waiteast = waitwest / numeast;
                waitwest = waitwest / numwest;







                Console.WriteLine(" Total cars that passed by the North light: {0} --- Total cars that had to wait in this line: {1}---- Average wait time in this line: {2}", northcars, maxnorth, waitnorth);
                Console.WriteLine(" Total cars that passed by the South light: {0} --- Total cars that had to wait in this line: {1}---- Average wait time in this line: {2}", southcars, maxsouth, waitsouth);
                Console.WriteLine(" Total cars that passed by the East light:  {0} --- Total cars that had to wait in this line: {1}---- Average wait time in this line: {2}", eastcars, maxeast, waiteast);
                Console.WriteLine(" Total cars that passed by the West light:  {0} --- Total cars that had to wait in this line: {1}---- Average wait time in this line: {2}", westcars, maxwest, waitwest);


            }

        }

       


    }



}
