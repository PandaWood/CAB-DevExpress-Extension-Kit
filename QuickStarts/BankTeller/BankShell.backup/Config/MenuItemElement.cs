//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Configuration;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace BankShell.Config
{
	class MenuItemElement : ConfigurationElement
	{
		[ConfigurationProperty("commandname", IsRequired = false)]
		public string CommandName
		{
			get { return (string) this["commandname"]; }
			set { this["commandname"] = value; }
		}

		[ConfigurationProperty("key", IsRequired = false)]
		public string Key
		{
			get { return (string) this["key"]; }
			set { this["key"] = value; }
		}

		[ConfigurationProperty("id", IsRequired = false, IsKey = true)]
		public int ID
		{
			get { return (int) this["id"]; }
			set { this["id"] = value; }
		}

		[ConfigurationProperty("label", IsRequired = true)]
		public string Label
		{
			get { return (string) this["label"]; }
			set { this["label"] = value; }
		}

		[ConfigurationProperty("site", IsRequired = true)]
		public string Site
		{
			get { return (string) this["site"]; }
			set { this["site"] = value; }
		}

		[ConfigurationProperty("register", IsRequired = false)]
		public bool Register
		{
			get { return (bool) this["register"]; }
			set { this["register"] = value; }
		}

		[ConfigurationProperty("registrationsite", IsRequired = false)]
		public string RegistrationSite
		{
			get { return (string) this["registrationsite"]; }
			set { this["registrationsite"] = value; }
		}

        [ConfigurationProperty("glyph", IsRequired = false)]
        public string Glyph
        {
            get { return (string)this["glyph"]; }
            set { this["glyph"] = value; }
        }

        [ConfigurationProperty("largeglyph", IsRequired = false)]
        public string LargeGlyph
        {
            get { return (string)this["largeglyph"]; }
            set { this["largeglyph"] = value; }
        }

        public BarItem ToMenuItem()
		{
			BarItem barItem;
			if (Register)
				barItem = new BarSubItem();
			else
				barItem = new BarButtonItem();

			barItem.Caption = Label;
            barItem.Hint = Label;

            if (!String.IsNullOrEmpty(Glyph))
                barItem.Glyph = GetGlyph(Glyph);
            if (!String.IsNullOrEmpty(LargeGlyph))
                barItem.LargeGlyph = GetGlyph(LargeGlyph);

			if (!String.IsNullOrEmpty(Key))
				barItem.ItemShortcut = new BarShortcut((Keys) Enum.Parse(typeof (Keys), Key));

			return barItem;
		}

        public static System.Drawing.Image GetGlyph(string glyphName)
        {
            // Since we are reading which resource to use from a config file, we
            // need to access the type-safe Properties.Resources in a non 
            // type-safe way. So create a resource manager pointing to 
            // Properties.Resources. Then grab the image from the glyphName.
            ResourceManager resourceMan = new ResourceManager(typeof(Properties.Resources));
            object obj = resourceMan.GetObject(glyphName);
            return (Bitmap)obj;
        }
	}
}