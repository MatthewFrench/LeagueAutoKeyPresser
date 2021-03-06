﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace League_Auto_Key_Presser.Ultimate_Caster
{
    public class ProfileController
    {
        const string profileFileName = "urf-caster-profiles.txt";
        string profilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, profileFileName);
        public List<ProfileData> Profiles { get; set; }

        public ProfileController()
        {
            LoadProfiles();
        }
        
        public void LoadProfiles()
        {
            // This text is added only once to the file.
            if (File.Exists(profilePath))
            {
                string text = File.ReadAllText(profilePath);
                Profiles = JsonConvert.DeserializeObject<List<ProfileData>>(text);
            } else
            {
                Profiles = new List<ProfileData>();
            }
            if (Profiles.Count == 0)
            {
                Profiles.Add(new ProfileData());
                SaveProfiles();
            }
        }

        public void SaveProfiles()
        {
            File.WriteAllText(profilePath, JsonConvert.SerializeObject(Profiles, Formatting.Indented));
        }
    }
}