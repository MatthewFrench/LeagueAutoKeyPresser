using System;
using System.Windows.Forms;
using System.Configuration;

namespace League_Auto_Key_Presser
{
    public partial class LeagueUltimateCasterForm : Form
    {
        Ultimate_Caster.UltimateCasterController casterController;

        public LeagueUltimateCasterForm()
        {
            InitializeComponent();

            casterController = new Ultimate_Caster.UltimateCasterController();

            //Load state
            var appSettings = ConfigurationManager.AppSettings;
            if (appSettings.Count != 0)
            {
                /*
                pressSpell1Interval = Convert.ToInt32(appSettings["pressSpell1Interval"]);
                pressSpell2Interval = Convert.ToInt32(appSettings["pressSpell2Interval"]);
                pressSpell3Interval = Convert.ToInt32(appSettings["pressSpell3Interval"]);
                pressSpell4Interval = Convert.ToInt32(appSettings["pressSpell4Interval"]);
                pressActiveInterval = Convert.ToInt32(appSettings["pressActiveInterval"]);
                autoKeyOnBool = Convert.ToBoolean(appSettings["autoKeyOnBool"]);
                active1OnBool = Convert.ToBoolean(appSettings["active1OnBool"]);
                active2OnBool = Convert.ToBoolean(appSettings["active2OnBool"]);
                active3OnBool = Convert.ToBoolean(appSettings["active3OnBool"]);
                active5OnBool = Convert.ToBoolean(appSettings["active5OnBool"]);
                active6OnBool = Convert.ToBoolean(appSettings["active6OnBool"]);
                active7OnBool = Convert.ToBoolean(appSettings["active7OnBool"]);
                wardOnBool = Convert.ToBoolean(appSettings["wardOnBool"]);
                wardHopOn = Convert.ToBoolean(appSettings["wardHopOn"]);
                qPreactivateW = Convert.ToBoolean(appSettings["qPreactivateW"]);
                qPreactivateE = Convert.ToBoolean(appSettings["qPreactivateE"]);
                qPreactivateR = Convert.ToBoolean(appSettings["qPreactivateR"]);
                wPreactivateQ = Convert.ToBoolean(appSettings["wPreactivateQ"]);
                wPreactivateE = Convert.ToBoolean(appSettings["wPreactivateE"]);
                wPreactivateR = Convert.ToBoolean(appSettings["wPreactivateR"]);
                ePreactivateQ = Convert.ToBoolean(appSettings["ePreactivateQ"]);
                ePreactivateW = Convert.ToBoolean(appSettings["ePreactivateW"]);
                ePreactivateR = Convert.ToBoolean(appSettings["ePreactivateR"]);
                rPreactivateQ = Convert.ToBoolean(appSettings["rPreactivateQ"]);
                rPreactivateW = Convert.ToBoolean(appSettings["rPreactivateW"]);
                rPreactivateE = Convert.ToBoolean(appSettings["rPreactivateE"]);
                wardHopKey = Convert.ToChar(appSettings["wardHopKey"]);
                activeKey = Convert.ToChar(appSettings["activeKey"]);
                */
            }
            /*
            qMillisecondsText.Text = pressSpell1Interval.ToString();
            wMillisecondsText.Text = pressSpell2Interval.ToString();
            eMillisecondsText.Text = pressSpell3Interval.ToString();
            rMillisecondsText.Text = pressSpell4Interval.ToString();
            activeMillisecondsText.Text = pressActiveInterval.ToString();
            autoKeyOnCheckbox.Checked = autoKeyOnBool;
            active1On.Checked = active1OnBool;
            active2On.Checked = active2OnBool;
            active3On.Checked = active3OnBool;
            active5On.Checked = active5OnBool;
            active6On.Checked = active6OnBool;
            active7On.Checked = active7OnBool;
            wardCheckbox.Checked = wardOnBool;
            wardHopCheckBox.Checked = wardHopOn;
            qActivateWCheckBox.Checked = qPreactivateW;
            qActivateECheckBox.Checked = qPreactivateE;
            qActivateRCheckBox.Checked = qPreactivateR;
            wActivateQCheckBox.Checked = wPreactivateQ;
            wActivateECheckBox.Checked = wPreactivateE;
            wActivateRCheckBox.Checked = wPreactivateR;
            eActivateQCheckBox.Checked = ePreactivateQ;
            eActivateWCheckBox.Checked = ePreactivateW;
            eActivateRCheckBox.Checked = ePreactivateR;
            rActivateQCheckBox.Checked = rPreactivateQ;
            rActivateWCheckBox.Checked = rPreactivateW;
            rActivateECheckBox.Checked = rPreactivateE;
            wardHopKeyComboBox.Text = wardHopKey.ToString();
            activeKeyComboBox.Text = activeKey.ToString();
            */
            //End load state

            //activeKeyComboBox.Text = "E";
            //wardHopKeyComboBox.Text = "Q";
        }

        private void active1On_CheckedChanged(object sender, EventArgs e)
        {
            //active1OnBool = ((CheckBox)sender).Checked;
        }

        private void active2On_CheckedChanged(object sender, EventArgs e)
        {
            //active2OnBool = ((CheckBox)sender).Checked;
        }

        private void active3On_CheckedChanged(object sender, EventArgs e)
        {
            //active3OnBool = ((CheckBox)sender).Checked;
        }

        private void autoKeyOn_CheckedChanged(object sender, EventArgs e)
        {
            /*
            autoKeyOnBool = ((CheckBox)sender).Checked;

            pressingSpell1 = false;
            pressingSpell2 = false;
            pressingSpell3 = false;
            pressingSpell4 = false;
            */
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            casterController.stopTimer();
            //Save state'
        /*
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            settings.Clear();
            settings.Add("pressSpell1Interval", pressSpell1Interval.ToString());
            settings.Add("pressSpell2Interval", pressSpell2Interval.ToString());
            settings.Add("pressSpell3Interval", pressSpell3Interval.ToString());
            settings.Add("pressSpell4Interval", pressSpell4Interval.ToString());
            settings.Add("pressActiveInterval", pressActiveInterval.ToString());
            settings.Add("autoKeyOnBool", autoKeyOnBool.ToString());
            settings.Add("active1OnBool", active1OnBool.ToString());
            settings.Add("active2OnBool", active2OnBool.ToString());
            settings.Add("active3OnBool", active3OnBool.ToString());
            settings.Add("active5OnBool", active5OnBool.ToString());
            settings.Add("active6OnBool", active6OnBool.ToString());
            settings.Add("active7OnBool", active7OnBool.ToString());
            settings.Add("wardHopOn", wardHopOn.ToString());
            settings.Add("wardOnBool", wardOnBool.ToString());
            settings.Add("qPreactivateW", qPreactivateW.ToString());
            settings.Add("qPreactivateE", qPreactivateE.ToString());
            settings.Add("qPreactivateR", qPreactivateR.ToString());
            settings.Add("wPreactivateQ", wPreactivateQ.ToString());
            settings.Add("wPreactivateE", wPreactivateE.ToString());
            settings.Add("wPreactivateR", wPreactivateR.ToString());
            settings.Add("ePreactivateQ", ePreactivateQ.ToString());
            settings.Add("ePreactivateW", ePreactivateW.ToString());
            settings.Add("ePreactivateR", ePreactivateR.ToString());
            settings.Add("rPreactivateQ", rPreactivateQ.ToString());
            settings.Add("rPreactivateW", rPreactivateW.ToString());
            settings.Add("rPreactivateE", rPreactivateE.ToString());
            settings.Add("wardHopKey", wardHopKey.ToString());
            settings.Add("activeKey", activeKey.ToString());
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            */
            Application.Exit();
            Environment.Exit(0);
        }

        private void qValueText_TextChanged(object sender, EventArgs e)
        {
            //pressSpell1Interval = Convert.ToInt32(qMillisecondsText.Text);
        }

        private void wValueText_TextChanged(object sender, EventArgs e)
        {
            //pressSpell2Interval = Convert.ToInt32(wMillisecondsText.Text);
        }

        private void eValueText_TextChanged(object sender, EventArgs e)
        {
            //pressSpell3Interval = Convert.ToInt32(eMillisecondsText.Text);
        }

        private void rValueText_TextChanged(object sender, EventArgs e)
        {
            //pressSpell4Interval = Convert.ToInt32(rMillisecondsText.Text);
        }

        private void activeValueText_TextChanged(object sender, EventArgs e)
        {
            //pressActiveInterval = Convert.ToInt32(activeMillisecondsText.Text);
        }

        private void active5On_CheckedChanged(object sender, EventArgs e)
        {
            //active5OnBool = ((CheckBox)sender).Checked;
        }

        private void active6On_CheckedChanged(object sender, EventArgs e)
        {
            //active6OnBool = ((CheckBox)sender).Checked;
        }

        private void active7On_CheckedChanged(object sender, EventArgs e)
        {
            //active7OnBool = ((CheckBox)sender).Checked;
        }

        private void wardCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //wardOnBool = ((CheckBox)sender).Checked;
        }

        private void activeKeyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           // activeKey = activeKeyComboBox.Text.ToCharArray()[0];
        }

        private void qActivateWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //qPreactivateW = ((CheckBox)sender).Checked;
        }

        private void qActivateECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //qPreactivateE = ((CheckBox)sender).Checked;
        }

        private void qActivateRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //qPreactivateR = ((CheckBox)sender).Checked;
        }

        private void wActivateQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //wPreactivateQ = ((CheckBox)sender).Checked;
        }

        private void wActivateECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //wPreactivateE = ((CheckBox)sender).Checked;
        }

        private void wActivateRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //wPreactivateR = ((CheckBox)sender).Checked;
        }

        private void eActivateQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //ePreactivateQ = ((CheckBox)sender).Checked;
        }

        private void eActivateWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //ePreactivateW = ((CheckBox)sender).Checked;
        }

        private void eActivateRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //ePreactivateR = ((CheckBox)sender).Checked;
        }

        private void rActivateQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //rPreactivateQ = ((CheckBox)sender).Checked;
        }

        private void rActivateWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //rPreactivateW = ((CheckBox)sender).Checked;
        }

        private void rActivateECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //rPreactivateE = ((CheckBox)sender).Checked;
        }

        private void wardHopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //wardHopOn = ((CheckBox)sender).Checked;
        }

        private void wardHopKeyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //wardHopKey = wardHopKeyComboBox.Text.ToCharArray()[0];
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
    
}
