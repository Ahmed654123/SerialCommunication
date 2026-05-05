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
            catch (Exception ex)
            {
                MessageBox.Show("Fout: " + ex.Message);
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
            catch (Exception ex)
            {
                MessageBox.Show("Fout: " + ex.Message);
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
            catch (Exception ex)
            {
                MessageBox.Show("Fout: " + ex.Message);
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
            catch (Exception ex) 
            {
                MessageBox.Show("Fout" + ex.Message);
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
            catch (Exception ex)
            {
                MessageBox.Show("Fout" + ex.Message);
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
            catch (Exception ex)
            {
                MessageBox.Show("Fout" + ex.Message);
            }



        }
    }
    
    
    

}
