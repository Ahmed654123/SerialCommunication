using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SerialCommunication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] portNames = SerialPort.GetPortNames().Distinct().ToArray();
                comboBoxPoort.Items.Clear();
                comboBoxPoort.Items.AddRange(portNames);
                if (comboBoxPoort.Items.Count > 0) comboBoxPoort.SelectedIndex = 0;

                comboBoxBaudrate.SelectedIndex = comboBoxBaudrate.Items.IndexOf("115200");
            }
            catch (Exception)
            { }
        }

        private void cboPoort_DropDown(object sender, EventArgs e)
        {
            try
            {
                string selected = (string)comboBoxPoort.SelectedItem;
                string[] portNames = SerialPort.GetPortNames().Distinct().ToArray();

                comboBoxPoort.Items.Clear();
                comboBoxPoort.Items.AddRange(portNames);

                comboBoxPoort.SelectedIndex = comboBoxPoort.Items.IndexOf(selected);
            }
            catch (Exception)
            {
                if (comboBoxPoort.Items.Count > 0) comboBoxPoort.SelectedIndex = 0;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    // Ik heb verbinding -> gebruiker wil verbreken
                    serialPortArduino.WriteLine("set d3 0"); // led UIT
                    serialPortArduino.WriteLine("set d4 0"); // buzzer UIT
                    serialPortArduino.Close();
                    radioButtonVerbonden.Checked = false;
                    buttonConnect.Text = "Connect";
                    labelStatus.Text = "Status: Disconnected";
                }
                else
                {
                    // Ik heb geen verbinding -> gebruiker wil verbinden
                    serialPortArduino.PortName = (string) comboBoxPoort.SelectedItem;
                    serialPortArduino.BaudRate = Int32.Parse((string) comboBoxBaudrate.SelectedItem);
                    serialPortArduino.DataBits = (int)numericUpDownDatabits.Value;

                    if (radioButtonParityEven.Checked) serialPortArduino.Parity = Parity.Even;
                    else if (radioButtonParityOdd.Checked) serialPortArduino.Parity = Parity.Odd;
                    else if (radioButtonParityNone.Checked) serialPortArduino.Parity = Parity.None;
                    else if (radioButtonParityMark.Checked) serialPortArduino.Parity = Parity.Mark;
                    else if (radioButtonParitySpace.Checked) serialPortArduino.Parity = Parity.Space;

                    if (radioButtonStopbitsNone.Checked) serialPortArduino.StopBits = StopBits.None;
                    else if (radioButtonStopbitsOne.Checked) serialPortArduino.StopBits = StopBits.One;
                    else if (radioButtonStopbitsOnePointFive.Checked) serialPortArduino.StopBits = StopBits.OnePointFive;
                    else if (radioButtonStopbitsTwo.Checked) serialPortArduino.StopBits = StopBits.Two;

                    if (radioButtonHandshakeNone.Checked) serialPortArduino.Handshake = Handshake.None;
                    else if (radioButtonHandshakeRTS.Checked) serialPortArduino.Handshake = Handshake.RequestToSend;
                    else if (radioButtonHandshakeRTSXonXoff.Checked) serialPortArduino.Handshake = Handshake.RequestToSendXOnXOff;
                    else if (radioButtonHandshakeXonXoff.Checked) serialPortArduino.Handshake = Handshake.XOnXOff;

                    serialPortArduino.RtsEnable = checkBoxRtsEnable.Checked;
                    serialPortArduino.DtrEnable = checkBoxDtrEnable.Checked;

                    serialPortArduino.Open();
                    string commando = "ping";
                    serialPortArduino.WriteLine(commando);
                    string antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    if (antwoord == "pong")
                    {
                        radioButtonVerbonden.Checked = true;
                        buttonConnect.Text = "Disconnect";
                        labelStatus.Text = "Status: Connected";
                    }
                    else
                    {
                        serialPortArduino.Close();
                        labelStatus.Text = "Error: Verkeerd antwoord";
                    }

                    {

                        }

                }

            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }

        private void comboBoxPoort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void checkBoxDigital2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Controleer of de seriële poort open is
                if (serialPortArduino.IsOpen)
                {
                    string command;

                    // Controleer of checkbox aangevinkt is
                    if (checkBoxDigital2.Checked)
                    {
                        command = "set d2 high";
                    }
                    else
                    {
                        command = "set d2 low";
                    }

                    // Verstuur command naar Arduino
                    serialPortArduino.WriteLine(command);
                }
                else
                {
                    MessageBox.Show("Seriële verbinding is niet open.");
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }

        }

        private void checkBoxDigital3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Controleer of de seriële poort open is
                if (serialPortArduino.IsOpen)
                {
                    string command;

                    // Controleer of checkbox aangevinkt is
                    if (checkBoxDigital3.Checked)
                    {
                        command = "set d3 high";
                    }
                    else
                    {
                        command = "set d3 low";
                    }

                    // Verstuur command naar Arduino
                    serialPortArduino.WriteLine(command);
                }
                else
                {
                    MessageBox.Show("Seriële verbinding is niet open.");
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }


        }

        private void checkBoxDigital4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Controleer of de seriële poort open is
                if (serialPortArduino.IsOpen)
                {
                    string command;

                    // Controleer of checkbox aangevinkt is
                    if (checkBoxDigital4.Checked)
                    {
                        command = "set d4 high";
                    }
                    else
                    {
                        command = "set d4 low";
                    }

                    // Verstuur command naar Arduino
                    serialPortArduino.WriteLine(command);
                }
                else
                {
                    MessageBox.Show("Seriële verbinding is niet open.");
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }


        }

        private void trackBarPWM9_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string command = "set pwm9 0" + trackBarPWM9.Value;
                    serialPortArduino.WriteLine(command);
                }
                else
                {
                    MessageBox.Show("Geen seriële connectie.");
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }



        }

        private void trackBarPWM10_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string command = "set pwm10 0" + trackBarPWM10.Value;
                    serialPortArduino.WriteLine(command);
                }
                else
                {
                    MessageBox.Show("Geen seriële connectie.");
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }



        }

        private void trackBarPWM11_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string command = "set pwm11 0" + trackBarPWM11.Value;
                    serialPortArduino.WriteLine(command);
                }
                else
                {
                    MessageBox.Show("Geen seriële connectie.");
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }



        }

        private void radioButtonDigital5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPageOefening3_Click(object sender, EventArgs e)
        {

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            timerOefening3.Enabled = tabControl.SelectedIndex == 3;
            timerOefening4.Enabled = tabControl.SelectedIndex == 4;
            timerOefening5.Enabled = tabControl.SelectedIndex == 5;
            timerTemperatuurAlarm.Enabled = tabControl.SelectedIndex == 6;

        }

        private void timerOefening3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string commando;
                    string antwoord;

                    // D5
                    serialPortArduino.ReadExisting();
                    commando = "get d5";
                    serialPortArduino.WriteLine(commando);
                    antwoord = serialPortArduino.ReadLine().TrimEnd();
                    antwoord = antwoord.Substring(4);
                    radioButtonDigital5.Checked = (antwoord == "1");

                    // D6
                    
                    commando = "get d6";
                    serialPortArduino.WriteLine(commando);
                    antwoord = serialPortArduino.ReadLine().TrimEnd();
                    antwoord = antwoord.Substring(4);
                    radioButtonDigital6.Checked = (antwoord == "1");

                    // D7
                    
                    commando = "get d7";
                    serialPortArduino.WriteLine(commando);
                    antwoord = serialPortArduino.ReadLine().TrimEnd();
                    antwoord = antwoord.Substring(4);
                    radioButtonDigital7.Checked = (antwoord == "1");
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }

        private void timerOefening4_Tick(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string commando;
                    string antwoord;

                    serialPortArduino.ReadExisting();
                    commando = "get a0";
                    serialPortArduino.WriteLine(commando);
                    antwoord = serialPortArduino.ReadLine().TrimEnd();
                    antwoord= antwoord.Substring(4);
                    labelAnalog0.Text = antwoord;


                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }

        private void timerOefening5_Tick(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string commando;
                    string antwoord;

                    
                    serialPortArduino.ReadExisting();
                    commando = "get a0";
                    serialPortArduino.WriteLine(commando);

                    antwoord = serialPortArduino.ReadLine().TrimEnd();
                    antwoord = antwoord.Substring(4);

                    int A0 = Convert.ToInt32(antwoord);

                    double gewensteTemp = A0 * (40.0 / 1023.0) + 5.0;
                    labelGewensteTemp.Text = gewensteTemp.ToString("0.0") + " °C";


                    serialPortArduino.ReadExisting();
                    commando = "get a5";
                    serialPortArduino.WriteLine(commando);

                    antwoord = serialPortArduino.ReadLine().TrimEnd();
                    antwoord = antwoord.Substring(4);

                    int A1 = Convert.ToInt32(antwoord);

                    double huidigeTemp = A1 * (500.0 / 1023.0);
                    labelHuidigeTemp.Text = huidigeTemp.ToString("0.0") + " °C";


                    
                    if (huidigeTemp < gewensteTemp)
                    {
                        serialPortArduino.WriteLine("set d2 1");
                    }
                    else
                    {
                        serialPortArduino.WriteLine("set d2 0");
                    }
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }

        }

        int toestand = 0;

        private void timerTemperatuurAlarm_Tick(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string commando;
                    string antwoord;

                    // Analoge pin 0 uitlezen (alarmtemperatuur)
                    serialPortArduino.ReadExisting();
                    serialPortArduino.WriteLine("get a0");
                    serialPortArduino.ReadLine();

                    // Tweede meting gebruiken
                    serialPortArduino.ReadExisting();
                    serialPortArduino.WriteLine("get a0");
                    antwoord = serialPortArduino.ReadLine().TrimEnd();
                    antwoord = antwoord.Substring(4);
                    int A0 = Convert.ToInt32(antwoord);
                    double huidigeTemp2 = A0 * (5.0 / 1023.0) * 100.0;
                    labelHuidigeTemp2.Text = huidigeTemp2.ToString("0.0") + " °C";

                    // Analoge pin 1 uitlezen (huidige temperatuur)
                    serialPortArduino.ReadExisting();
                    commando = "get a1";
                    serialPortArduino.WriteLine(commando);

                    antwoord = serialPortArduino.ReadLine().TrimEnd();
                    antwoord = antwoord.Substring(4);

                    int A1 = Convert.ToInt32(antwoord);

                    double alarmTemp = A1 * (70.0 / 1023.0) - 10.0;
                    labelAlarmTemp.Text = alarmTemp.ToString("0.0") + " °C";

                    // Digitale pin 5 uitlezen (buttonBevestig)
                    serialPortArduino.ReadExisting();
                    commando = "get d5";
                    serialPortArduino.WriteLine(commando);

                    antwoord = serialPortArduino.ReadLine().TrimEnd();
                    antwoord = antwoord.Substring(4);

                    bool buttonBevestig = antwoord == "1";

                    // OK → ALARM
                    if (toestand == 0 && huidigeTemp2 >= alarmTemp)
                    {
                        toestand = 1;
                    }
                    // ALARM → BEVESTIGD of OK
                    else if (toestand == 1 && buttonBevestig)
                    {
                        if (huidigeTemp2 < alarmTemp)
                            toestand = 0;
                        else
                            toestand = 2;
                    }
                    // BEVESTIGD → OK
                    else if (toestand == 2 && huidigeTemp2 < alarmTemp)
                    {
                        toestand = 0;
                    }

                    // ── Visualiseer status ────────────────────────────────────────

                    if (toestand == 0)
                    {
                        labelStatus2.Text = "OK";
                    }
                    else if (toestand == 1)
                    {
                        labelStatus2.Text = "ALARM";
                    }
                    else if (toestand == 2)
                    {
                        labelStatus2.Text = "BEVESTIGD";
                    }

                    // ── Stuur led (d3) en buzzer (d4) aan ────────────────────────

                    if (toestand == 0)
                    {
                        serialPortArduino.WriteLine("set d2 0"); // led UIT
                        serialPortArduino.WriteLine("set d3 0"); // buzzer UIT
                    }
                    else if (toestand == 1)
                    {
                        serialPortArduino.WriteLine("set d2 1"); // led AAN
                        serialPortArduino.WriteLine("set d3 1"); // buzzer AAN
                    }
                    else if (toestand == 2)
                    {
                        serialPortArduino.WriteLine("set d2 1"); // led AAN
                        serialPortArduino.WriteLine("set d3 0"); // buzzer UIT
                    }
                


            }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "Error: " + exception.Message;
                serialPortArduino.Close();
                radioButtonVerbonden.Checked = false;
                buttonConnect.Text = "Connect";
            }
        }
    }
    
    
    

}
