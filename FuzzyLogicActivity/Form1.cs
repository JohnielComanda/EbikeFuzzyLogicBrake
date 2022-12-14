using DotFuzzy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuzzyLogicActivity
{
    public partial class Form1 : Form
    {
        FuzzyEngine fe;
        MembershipFunctionCollection initSpeed, initObstacleDistance, initBrake;
        LinguisticVariable speed, obstacleDistance, brake;
        FuzzyRuleCollection rules;


        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {         
            setFuzzyEngine();
            fe.Consequent = "BRAKE";
            textBox4.Text = "" + fe.Defuzzify();

            var crysptOutput = fe.Defuzzify();

            if (crysptOutput <= 2)
                label4.Text = "Brake is LOW";
            else if(crysptOutput > 2 && crysptOutput <= 4)
                label4.Text = "Brake is LOW MEDIUM";
            else if (crysptOutput > 4 && crysptOutput <= 6)
                label4.Text = "Brake is MEDIUM";
            else if (crysptOutput > 6 && crysptOutput <= 8)
                label4.Text = "Brake is HIGH MEDIUM";
            else if (crysptOutput > 8 && crysptOutput <= 10)
                label4.Text = "Brake is HIGH";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setMembers();
            setRules();
        }


        public void setMembers()
        {
            initSpeed = new MembershipFunctionCollection();
            initSpeed.Add(new MembershipFunction("LOW", 0.0, 0.0, 25.0, 30.0));
            initSpeed.Add(new MembershipFunction("OK", 25.0, 30.0, 30.0, 35.0));
            initSpeed.Add(new MembershipFunction("HIGH", 30.0, 35.0, 80.0, 80.0));
            speed = new LinguisticVariable("SPEED", initSpeed);


            initObstacleDistance = new MembershipFunctionCollection();
            initObstacleDistance.Add(new MembershipFunction("NEAR", 100.0, 100.0, 200.0, 350.0));
            initObstacleDistance.Add(new MembershipFunction("MED", 200.0, 400.0, 400.0, 500.0));
            initObstacleDistance.Add(new MembershipFunction("FAR", 350.0, 500.0, 1000.0, 1000.0));
            obstacleDistance = new LinguisticVariable("DISTANCE", initObstacleDistance);

            initBrake = new MembershipFunctionCollection();
            initBrake.Add(new MembershipFunction("LOW", 0.0, 0.0, 2.0, 4.0));
            initBrake.Add(new MembershipFunction("LM", 2.0, 4.0, 4.0, 6.0));
            initBrake.Add(new MembershipFunction("MED", 4.0, 6.0, 6.0, 8.0));
            initBrake.Add(new MembershipFunction("HM", 6.0, 8.0, 8.0, 10.0));
            initBrake.Add(new MembershipFunction("HIGH", 8.0, 10.0, 10.0, 10.0));
            brake = new LinguisticVariable("BRAKE", initBrake);
        }

        public void setRules()
        {
            rules = new FuzzyRuleCollection();
            rules.Add(new FuzzyRule("IF (SPEED IS HIGH) AND (DISTANCE IS NEAR) THEN BRAKE IS HIGH"));
            rules.Add(new FuzzyRule("IF (SPEED IS HIGH) AND (DISTANCE IS MED) THEN BRAKE IS HM"));
            rules.Add(new FuzzyRule("IF (SPEED IS HIGH) AND (DISTANCE IS FAR) THEN BRAKE IS LM"));
            rules.Add(new FuzzyRule("IF (SPEED IS OK) AND (DISTANCE IS NEAR) THEN BRAKE IS HIGH"));
            rules.Add(new FuzzyRule("IF (SPEED IS OK) AND (DISTANCE IS MED) THEN BRAKE IS MED"));
            rules.Add(new FuzzyRule("IF (SPEED IS OK) AND (DISTANCE IS FAR) THEN BRAKE IS LOW"));
            rules.Add(new FuzzyRule("IF (SPEED IS LOW) AND (DISTANCE IS NEAR) THEN BRAKE IS LM"));
            rules.Add(new FuzzyRule("IF (SPEED IS LOW) AND (DISTANCE IS MED) THEN BRAKE IS LOW"));
            rules.Add(new FuzzyRule("IF (SPEED IS LOW) AND (DISTANCE IS FAR) THEN BRAKE IS LOW"));
        }

        public void setFuzzyEngine()
        {
            fe = new FuzzyEngine();

            fe.LinguisticVariableCollection.Add(speed);
            fe.LinguisticVariableCollection.Add(obstacleDistance);
            fe.LinguisticVariableCollection.Add(brake);

            fe.FuzzyRuleCollection = rules;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            speed.InputValue = (Convert.ToDouble(textBox3.Text));
            speed.Fuzzify("OK");

            obstacleDistance.InputValue = (Convert.ToDouble(textBox2.Text));
            obstacleDistance.Fuzzify("MED");
        }
    }
}
