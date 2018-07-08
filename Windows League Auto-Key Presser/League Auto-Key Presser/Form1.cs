using System;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using League_Auto_Key_Presser.Ultimate_Caster;
using Newtonsoft.Json;

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
            profileComboBox.SelectedIndex = selectedProfileIndex;
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
            rightClickMillisecondsText.Text = profile.RightClickMillisecondDelay.ToString();
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
            qOnCheckbox.Checked = profile.QSpellData.On;
            wOnCheckbox.Checked = profile.WSpellData.On;
            eOnCheckbox.Checked = profile.ESpellData.On;
            rOnCheckbox.Checked = profile.RSpellData.On;
            activesOnCheckbox.Checked = profile.ActivesOn;
            bindActivesToQCheckbox.Checked = profile.ActivesBoundToQ;
            bindActivesToWCheckbox.Checked = profile.ActivesBoundToW;
            bindActivesToECheckbox.Checked = profile.ActivesBoundToE;
            bindActivesToRCheckbox.Checked = profile.ActivesBoundToR;
            wardHopQCheckbox.Checked = profile.WardHopUsingQ;
            wardHopWCheckbox.Checked = profile.WardHopUsingW;
            wardHopECheckbox.Checked = profile.WardHopUsingE;
            wardHopRCheckbox.Checked = profile.WardHopUsingR;
            rightClickOnCheckbox.Checked = profile.RightClickSpamOn;
            rightClickPreactivateQCheckbox.Checked = profile.RightClickPreactivateQ;
            rightClickPreactivateWCheckbox.Checked = profile.RightClickPreactivateW;
            rightClickPreactivateECheckbox.Checked = profile.RightClickPreactivateE;
            rightClickPreactivateRCheckbox.Checked = profile.RightClickPreactivateR;
            rightClickPreactivateActive1Checkbox.Checked = profile.RightClickPreactivate1;
            rightClickPreactivateActive2Checkbox.Checked = profile.RightClickPreactivate2;
            rightClickPreactivateActive3Checkbox.Checked = profile.RightClickPreactivate3;
            rightClickPreactivateActive5Checkbox.Checked = profile.RightClickPreactivate5;
            rightClickPreactivateActive6Checkbox.Checked = profile.RightClickPreactivate6;
            rightClickPreactivateActive7Checkbox.Checked = profile.RightClickPreactivate7;
        }

        private void active1On_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ActivesDo1 = active1On.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void active2On_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ActivesDo2 = active2On.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void active3On_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ActivesDo3 = active3On.Checked;
            casterController.GetProfileController().SaveProfiles();
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
            int selectedProfile = casterController.GetProfileController().Profiles.IndexOf(casterController.SelectedProfile);
            settings.Add("SelectedProfile", selectedProfile.ToString());
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

            casterController.StopTimer();

            casterController.GetProfileController().SaveProfiles();
            Task.Delay(500).ContinueWith(t => Application.Exit());
        }

        private void qValueText_TextChanged(object sender, EventArgs e)
        {
            int milliseconds = 10;
            try
            {
                milliseconds = Convert.ToInt32(qMillisecondsText.Text);
            } catch (Exception) { }
            casterController.SelectedProfile.QSpellData.MillisecondDelay = milliseconds;
            casterController.GetProfileController().SaveProfiles();
        }

        private void wValueText_TextChanged(object sender, EventArgs e)
        {
            int milliseconds = 10;
            try
            {
                milliseconds = Convert.ToInt32(wMillisecondsText.Text);
            }
            catch (Exception) { }
            casterController.SelectedProfile.WSpellData.MillisecondDelay = milliseconds;
            casterController.GetProfileController().SaveProfiles();
        }

        private void eValueText_TextChanged(object sender, EventArgs e)
        {
            int milliseconds = 10;
            try
            {
                milliseconds = Convert.ToInt32(eMillisecondsText.Text);
            }
            catch (Exception) { }
            casterController.SelectedProfile.ESpellData.MillisecondDelay = milliseconds;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rValueText_TextChanged(object sender, EventArgs e)
        {
            int milliseconds = 10;
            try
            {
                milliseconds = Convert.ToInt32(rMillisecondsText.Text);
            }
            catch (Exception) { }
            casterController.SelectedProfile.RSpellData.MillisecondDelay = milliseconds;
            casterController.GetProfileController().SaveProfiles();
        }

        private void activeValueText_TextChanged(object sender, EventArgs e)
        {
            int milliseconds = 10;
            try
            {
                milliseconds = Convert.ToInt32(activeMillisecondsText.Text);
            }
            catch (Exception) { }
            casterController.SelectedProfile.ActivesMillisecondDelay = milliseconds;
            casterController.GetProfileController().SaveProfiles();
        }

        private void active5On_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ActivesDo5 = active5On.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void active6On_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ActivesDo6 = active6On.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void active7On_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ActivesDo7 = active7On.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void wardCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.AutoWardOn = wardCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void activeKeyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           // activeKey = activeKeyComboBox.Text.ToCharArray()[0];
        }

        private void qActivateWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (qActivateWCheckBox.Checked)
            {
                casterController.SelectedProfile.QSpellData.Preactivate.Add('W');
            } else
            {
                casterController.SelectedProfile.QSpellData.Preactivate.Remove('W');
            }
            casterController.GetProfileController().SaveProfiles();
        }

        private void qActivateECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (qActivateECheckBox.Checked)
            {
                casterController.SelectedProfile.QSpellData.Preactivate.Add('E');
            }
            else
            {
                casterController.SelectedProfile.QSpellData.Preactivate.Remove('E');
            }
            casterController.GetProfileController().SaveProfiles();
        }

        private void qActivateRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (qActivateRCheckBox.Checked)
            {
                casterController.SelectedProfile.QSpellData.Preactivate.Add('R');
            }
            else
            {
                casterController.SelectedProfile.QSpellData.Preactivate.Remove('R');
            }
            casterController.GetProfileController().SaveProfiles();
        }

        private void wActivateQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (wActivateQCheckBox.Checked)
            {
                casterController.SelectedProfile.WSpellData.Preactivate.Add('Q');
            }
            else
            {
                casterController.SelectedProfile.WSpellData.Preactivate.Remove('Q');
            }
            casterController.GetProfileController().SaveProfiles();
        }

        private void wActivateECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (wActivateECheckBox.Checked)
            {
                casterController.SelectedProfile.WSpellData.Preactivate.Add('E');
            }
            else
            {
                casterController.SelectedProfile.WSpellData.Preactivate.Remove('E');
            }
            casterController.GetProfileController().SaveProfiles();
        }

        private void wActivateRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (wActivateRCheckBox.Checked)
            {
                casterController.SelectedProfile.WSpellData.Preactivate.Add('R');
            }
            else
            {
                casterController.SelectedProfile.WSpellData.Preactivate.Remove('R');
            }
            casterController.GetProfileController().SaveProfiles();
        }

        private void eActivateQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (eActivateQCheckBox.Checked)
            {
                casterController.SelectedProfile.ESpellData.Preactivate.Add('Q');
            }
            else
            {
                casterController.SelectedProfile.ESpellData.Preactivate.Remove('Q');
            }
            casterController.GetProfileController().SaveProfiles();
        }

        private void eActivateWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (eActivateWCheckBox.Checked)
            {
                casterController.SelectedProfile.ESpellData.Preactivate.Add('W');
            }
            else
            {
                casterController.SelectedProfile.ESpellData.Preactivate.Remove('W');
            }
            casterController.GetProfileController().SaveProfiles();
        }

        private void eActivateRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (eActivateRCheckBox.Checked)
            {
                casterController.SelectedProfile.ESpellData.Preactivate.Add('R');
            }
            else
            {
                casterController.SelectedProfile.ESpellData.Preactivate.Remove('R');
            }
            casterController.GetProfileController().SaveProfiles();
        }

        private void rActivateQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rActivateQCheckBox.Checked)
            {
                casterController.SelectedProfile.RSpellData.Preactivate.Add('Q');
            }
            else
            {
                casterController.SelectedProfile.RSpellData.Preactivate.Remove('Q');
            }
            casterController.GetProfileController().SaveProfiles();
        }

        private void rActivateWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rActivateWCheckBox.Checked)
            {
                casterController.SelectedProfile.RSpellData.Preactivate.Add('W');
            }
            else
            {
                casterController.SelectedProfile.RSpellData.Preactivate.Remove('W');
            }
            casterController.GetProfileController().SaveProfiles();
        }

        private void rActivateECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rActivateECheckBox.Checked)
            {
                casterController.SelectedProfile.RSpellData.Preactivate.Add('E');
            }
            else
            {
                casterController.SelectedProfile.RSpellData.Preactivate.Remove('E');
            }
            casterController.GetProfileController().SaveProfiles();
        }

        private void wardHopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.WardHopOn = wardHopCheckBox.Checked;
            casterController.GetProfileController().SaveProfiles();
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
            casterController.SelectedProfile.QSpellData.On = qOnCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
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
            casterController.SetSelectedProfileAtIndex(profileComboBox.SelectedIndex);
        }

        private void profileNameText_TextChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ProfileName = profileNameText.Text;
            UpdateProfileNamesInComboBox();
        }

        private void newProfileButton_Click(object sender, EventArgs e)
        {
            casterController.GetProfileController().Profiles.Add(new ProfileData());
            UpdateProfileNamesInComboBox();
            profileComboBox.SelectedIndex = casterController.GetProfileController().Profiles.Count - 1;
            casterController.SetSelectedProfileAtIndex(profileComboBox.SelectedIndex);
            casterController.GetProfileController().SaveProfiles();
        }

        private void duplicateProfileButton_Click(object sender, EventArgs e)
        {
            var newProfile = JsonConvert.DeserializeObject<ProfileData>(JsonConvert.SerializeObject(casterController.SelectedProfile));
            newProfile.ProfileName += " Copy";
            casterController.GetProfileController().Profiles.Add(newProfile);
            UpdateProfileNamesInComboBox();
            profileComboBox.SelectedIndex = casterController.GetProfileController().Profiles.Count - 1;
            casterController.SetSelectedProfileAtIndex(profileComboBox.SelectedIndex);
            casterController.GetProfileController().SaveProfiles();
        }

        private void deleteProfileButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = casterController.GetProfileController().Profiles.IndexOf(casterController.SelectedProfile);
            casterController.GetProfileController().Profiles.Remove(casterController.SelectedProfile);
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = 0;
            }
            if (casterController.GetProfileController().Profiles.Count == 0)
            {
                casterController.GetProfileController().Profiles.Add(new ProfileData());
            }
            profileComboBox.SelectedIndex = selectedIndex;
            UpdateProfileNamesInComboBox();
            casterController.SetSelectedProfileAtIndex(selectedIndex);
            casterController.GetProfileController().SaveProfiles();
        }

        private void wOnCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.WSpellData.On = wOnCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void eOnCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ESpellData.On = eOnCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rOnCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.RSpellData.On = rOnCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void activesOnCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ActivesOn = activesOnCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rightClickOnCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.RightClickSpamOn = rightClickOnCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void bindActivesToQCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ActivesBoundToQ = bindActivesToQCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void bindActivesToWCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ActivesBoundToW = bindActivesToWCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void bindActivesToECheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ActivesBoundToE = bindActivesToECheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void bindActivesToRCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.ActivesBoundToR = bindActivesToRCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void wardHopQCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.WardHopUsingQ = wardHopQCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void wardHopWCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.WardHopUsingW = wardHopWCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void wardHopECheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.WardHopUsingE = wardHopECheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void wardHopRCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.WardHopUsingR = wardHopRCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rightClickPreactivateQCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.RightClickPreactivateQ = rightClickPreactivateQCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rightClickPreactivateWCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.RightClickPreactivateW = rightClickPreactivateWCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rightClickPreactivateECheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.RightClickPreactivateE = rightClickPreactivateECheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rightClickPreactivateRCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.RightClickPreactivateR = rightClickPreactivateRCheckbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rightClickPreactivateActive1Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.RightClickPreactivate1 = rightClickPreactivateActive1Checkbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rightClickPreactivateActive2Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.RightClickPreactivate2 = rightClickPreactivateActive2Checkbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rightClickPreactivateActive3Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.RightClickPreactivate3 = rightClickPreactivateActive3Checkbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rightClickPreactivateActive5Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.RightClickPreactivate5 = rightClickPreactivateActive5Checkbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rightClickPreactivateActive6Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.RightClickPreactivate6 = rightClickPreactivateActive6Checkbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rightClickPreactivateActive7Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            casterController.SelectedProfile.RightClickPreactivate7 = rightClickPreactivateActive7Checkbox.Checked;
            casterController.GetProfileController().SaveProfiles();
        }

        private void rightClickMillisecondsText_TextChanged(object sender, EventArgs e)
        {
            int milliseconds = 10;
            try
            {
                milliseconds = Convert.ToInt32(rightClickMillisecondsText.Text);
            }
            catch (Exception) { }
            casterController.SelectedProfile.RightClickMillisecondDelay = milliseconds;
            casterController.GetProfileController().SaveProfiles();
        }
    }
}
