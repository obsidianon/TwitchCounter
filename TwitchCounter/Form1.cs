using System.IO;
namespace TwitchCounter
{
    public partial class Form1 : Form
    {
        public int intCount;
        public int intSet;
        public int intSetMax = 2147483647;

        public bool boolParse;

        public int intTextFileCount; 

        string ExeLoc = Application.ExecutablePath; //Path to the exe, INCLUDING the .exe in the path
        string ExeDir = AppDomain.CurrentDomain.BaseDirectory;

        public Form1()
        {
            InitializeComponent();
        }

        private void checkTextFile()
        {
            if (File.Exists("count.txt"))
            {
                if (File.ReadAllText(ExeDir + "\\count.txt") == "")
                {
                    File.WriteAllText(ExeDir + "\\count.txt", "0");
                }
            }
            else
            {
                MessageBox.Show("Thanks for using Twitch Counter! A file named count.txt was created and placed in the same directory as the exe file for this application. This .txt file contains the current number counted and can be used within StreamLabs or any streaming software that allows you to display the contents of a text file on-stream.", "First Launch");
                File.WriteAllText(ExeDir + "\\count.txt", "0");
            }

            bool parse = int.TryParse(File.ReadAllText(ExeDir + "\\count.txt"), out intTextFileCount);
            if (parse == true)
            {
                intCount = intTextFileCount;
                File.WriteAllText(ExeDir + "\\count.txt", intCount.ToString());
            }
            else
            {
                MessageBox.Show("Not able to parse the contents of count.txt to an integer, has the file been modified?", "Error");

            }
        }

        private void updateCount()
        {
            textBoxCount.Text = intCount.ToString("N0");

            File.WriteAllText(ExeDir + "\\count.txt", intCount.ToString());

            EasterEggs();
        }

        private void EasterEggs()
        {
            labelEasterEgg.Visible = true;

            if (intCount == 69)
            {
                labelEasterEgg.Text = "Nice";
            }
            else if (intCount == 420)
            {
                labelEasterEgg.Text = "Blaze it dude!";
            }
            else if (intCount == 666)
            {
                labelEasterEgg.Text = "\\m/";
            }
            else
            {
                labelEasterEgg.Text = "";
                labelEasterEgg.Visible = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            checkTextFile();

            textBoxCount.Text = File.ReadAllText(ExeDir + "\\count.txt");

            updateCount();
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            if (intCount == intSetMax)
            {
                MessageBox.Show("Can't count higher than " + intSetMax.ToString("N0"), "Error");
            }
            else
            {
                intCount += 1;
                updateCount();
            }
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            intCount += -1;
            updateCount();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Reset counter to zero?", "Reset?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                intCount = 0;
                updateCount();
            }
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            boolParse = int.TryParse(textBoxSet.Text, out intSet);
            if (boolParse == true)
            {
                intCount = intSet;
                updateCount();
                textBoxSet.Text = "";
            }
            else
            {
                MessageBox.Show("Please ensure the value in the text box next to the set button is a valid integer no greater than " + intSetMax.ToString("N0"), "Error");
                textBoxSet.Text = "";
            }
        }
    }
}