//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace Localizer_Extension
{
    // a simple class representing a string resource (key - default value - (optional) translation)
    public class StringResource : IComparable<string>
    {
        string _Key, _DefaultValue, _TranslationValue;

        [DisplayName("Key")]
        public string Key
        {
            get { return _Key; }
            set
            {
                Resourcer.UpdateStringKey(_Key, value);
                _Key = value;
            }
        }
        [DisplayName("Default value")]
        public string DefaultValue
        {
            get { return _DefaultValue; }
            set
            {
                Resourcer.UpdateString(_Key,
              _DefaultValue = value, null);
            }
        }

        [Browsable(false)]
        public string TranslationLocale { get; set; }
        [DisplayName("Translated value")]
        public string TranslationValue
        {
            get { return _TranslationValue; }
            set
            {
                Resourcer.UpdateString(_Key,
              _TranslationValue = value, TranslationLocale);
            }
        }

        public StringResource() { }

        public StringResource(string key, string defaultValue)
        {
            _Key = key;
            _DefaultValue = defaultValue;
        }

        public StringResource(string key, string defaultValue,
            string translationLocale, string translationValue)
        {
            _Key = key;
            _DefaultValue = defaultValue;

            TranslationLocale = translationLocale;
            _TranslationValue = translationValue;
        }

        public int CompareTo(string other)
        {
            if (other.Length == 4)
                return 1;

            var searchType = other.Substring(0, 3);
            var searchValue = other.Substring(4);

            switch (searchType)
            {
                case "key":
                    return (Key == null ? false :
                        Key.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) > -1)
                        ? 1 : 0;

                default:
                case "val":
                    return (DefaultValue == null ? false :
                        DefaultValue.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) > -1)
                        ? 1 : 0;

                case "trs":
                    return (TranslationValue == null ? false :
                        TranslationValue.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) > -1)
                        ? 1 : 0;
            }
        }
    }
}
