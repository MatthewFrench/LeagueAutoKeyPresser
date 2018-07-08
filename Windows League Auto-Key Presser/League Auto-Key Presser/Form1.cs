using System;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using League_Auto_Key_Presser.Ultimate_Caster;

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
            int selectedProfileIndex = 0;
            if (appSettings.Count != 0)
            {
                casterController.ElevateProcesses = Convert.ToBoolean(appSettings["ElevateProcesses"]);
                casterController.LeagueProcessName = Convert.ToString(appSettings["LeagueProcessName"]);
                if (casterController.LeagueProcessName.Length == 0)
                {
                    casterController.LeagueProcessName = "League of Legends.exe";
                }
                casterController.OnlyRunCasterWhenProcessIsOpen = Convert.ToBoolean(appSettings["OnlyRunCasterWhenProcessIsOpen"]);
                casterController.UltimateCasterOn = Convert.ToBoolean(appSettings["UltimateCasterOn"]);
                selectedProfileIndex = Convert.ToInt32(appSettings["SelectedProfile"]);
                if (selectedProfileIndex < 0)
                {
                    selectedProfileIndex = 0;
                }
            }

            //Now update the UI with the selected options
            elevateApplicationsCheckbox.Checked = casterController.ElevateProcesses;
            turnOnWhenAvailableCheckbox.Checked = casterController.OnlyRunCasterWhenProcessIsOpen;
            autoKeyOnCheckbox.Checked = casterController.UltimateCasterOn;
            processNameTextbox.Text = casterController.LeagueProcessName;
            UpdateProfileNamesInComboBox();
            casterController.SetSelectedProfileAtIndex(selectedProfileIndex);
        }

        public void UpdateProfileNamesInComboBox()
        {
            //Loop through profiles and update the combo box with names
            var profiles = casterController.GetProfileController().Profiles;
            int selectedItemIndex = profileComboBox.SelectedIndex;
            if (selectedItemIndex < 0)
            {
                selectedItemIndex = 0;
            }
            profileComboBox.Items.Clear();
            foreach (ProfileData profile in profiles)
            {
                profileComboBox.Items.Add(profile.ProfileName);
            }
            profileComboBox.SelectedIndex = selectedItemIndex;
        }

        public void UpdateProfileInterface()
        {
            //Update all the interface elements with profile information
            ProfileData profile = casterController.SelectedProfile;
            profileNameText.Text = profile.ProfileName;
            qMillisecondsText.Text = profile.QSpellData.MillisecondDelay.ToString();
            wMillisecondsText.Text = profile.WSpellData.MillisecondDelay.ToString();
            eMillisecondsText.Text = profile.ESpellData.MillisecondDelay.ToString();
            rMillisecondsText.Text = profile.RSpellData.MillisecondDelay.ToString();
            activeMillisecondsText.Text = profile.ActivesMillisecondDelay.ToString();
            active1On.Checked = profile.ActivesDo1;
            active2On.Checked = profile.ActivesDo2;
            active3On.Checked = profile.ActivesDo3;
            active5On.Checked = profile.ActivesDo5;
            active6On.Checked = profile.ActivesDo6;
            active7On.Checked = profile.ActivesDo7;
            wardCheckbox.Checked = profile.AutoWardOn;
            wardHopCheckBox.Checked = profile.WardHopOn;
            qActivateWCheckBox.Checked = profile.QSpellData.Preactivate.Contains('W');
            qActivateECheckBox.Checked = profile.QSpellData.Preactivate.Contains('E');
            qActivateRCheckBox.Checked = profile.QSpellData.Preactivate.Contains('R');
            wActivateQCheckBox.Checked = profile.WSpellData.Preactivate.Contains('Q');
            wActivateECheckBox.Checked = profile.WSpellData.Preactivate.Contains('E');
            wActivateRCheckBox.Checked = profile.WSpellData.Preactivate.Contains('R');
            eActivateQCheckBox.Checked = profile.ESpellData.Preactivate.Contains('Q');
            eActivateWCheckBox.Checked = profile.ESpellData.Preactivate.Contains('W');
            eActivateRCheckBox.Checked = profile.ESpellData.Preactivate.Contains('R');
            rActivateQCheckBox.Checked = profile.RSpellData.Preactivate.Contains('Q');
            rActivateWCheckBox.Checked = profile.RSpellData.Preactivate.Contains('W');
            rActivateECheckBox.Checked = profile.RSpellData.Preactivate.Contains('E');
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
            casterController.SelectedProfile.ProfileName = profileNameText.Text;
            UpdateProfileNamesInComboBox();
        }
    }
    
}
