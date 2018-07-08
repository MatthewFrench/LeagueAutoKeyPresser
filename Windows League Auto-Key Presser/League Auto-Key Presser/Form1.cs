using System;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace League_Auto_Key_Presser
{
    public partial class LeagueUltimateCasterForm : Form
    {
        Ultimate_Caster.UltimateCasterController casterController;

        public LeagueUltimateCasterForm()
        {
            InitializeComponent();

            casterController = new Ultimate_Caster.UltimateCasterController(this);

            //Load state
            var appSettings = ConfigurationManager.AppSettings;
            if (appSettings.Count != 0)
            {
                //settings.Add("ElevateProcesses", casterController.ElevateProcesses.ToString());
                //settings.Add("LeagueProcessName", casterController.LeagueProcessName.ToString());
                //settings.Add("OnlyRunCasterWhenProcessIsOpen", casterController.OnlyRunCasterWhenProcessIsOpen.ToString());
                //settings.Add("UltimateCasterOn", casterController.UltimateCasterOn.ToString());
                //settings.Add("SelectedProfile", casterController.GetProfileController().Profiles.IndexOf(casterController.SelectedProfile).ToString());
                casterController.ElevateProcesses = Convert.ToBoolean(appSettings["ElevateProcesses"]);
                casterController.LeagueProcessName = Convert.ToString(appSettings["LeagueProcessName"]);
                if (casterController.LeagueProcessName.Length == 0)
                {
                    casterController.LeagueProcessName = "League of Legends.exe";
                }
                casterController.OnlyRunCasterWhenProcessIsOpen = Convert.ToBoolean(appSettings["OnlyRunCasterWhenProcessIsOpen"]);
                casterController.UltimateCasterOn = Convert.ToBoolean(appSettings["UltimateCasterOn"]);
                int selectedProfileIndex = Convert.ToInt32(appSettings["SelectedProfile"]);
                casterController.SelectedProfileAtIndex(selectedProfileIndex);
            }

            //Now update the UI with the selected options
            elevateApplicationsCheckbox.Checked = casterController.ElevateProcesses;
            turnOnWhenAvailableCheckbox.Checked = casterController.OnlyRunCasterWhenProcessIsOpen;
            autoKeyOnCheckbox.Checked = casterController.UltimateCasterOn;
            processNameTextbox.Text = casterController.LeagueProcessName;
            //Update the names of profiles in the profile combo box
            //Update the selected profile in the combo box
            //Update the profile specific data

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
            casterController.UltimateCasterOn = autoKeyOnCheckbox.Checked;
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
            //Save state
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            settings.Clear();
            settings.Add("ElevateProcesses", casterController.ElevateProcesses.ToString());
            settings.Add("LeagueProcessName", casterController.LeagueProcessName.ToString());
            settings.Add("OnlyRunCasterWhenProcessIsOpen", casterController.OnlyRunCasterWhenProcessIsOpen.ToString());
            settings.Add("UltimateCasterOn", casterController.UltimateCasterOn.ToString());
            settings.Add("SelectedProfile", casterController.GetProfileController().Profiles.IndexOf(casterController.SelectedProfile).ToString());
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

            casterController.StopTimer();

            casterController.GetProfileController().SaveProfiles();
            Task.Delay(500).ContinueWith(t => Application.Exit());
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

        private void turnOnWhenAvailableCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.OnlyRunCasterWhenProcessIsOpen = turnOnWhenAvailableCheckbox.Checked;
        }

        private void processNameTextbox_TextChanged(object sender, EventArgs e)
        {
            casterController.LeagueProcessName = processNameTextbox.Text;
        }

        private void elevateApplicationsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.ElevateProcesses = elevateApplicationsCheckbox.Checked;
        }

        private void profileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Change selected profile
        }

        private void profileNameText_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
