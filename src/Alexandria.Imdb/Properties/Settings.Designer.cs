﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alexandria.Imdb.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Here are the (?<Matches>\\d{1,}) matching titles")]
        public string PowerSearch_Matches {
            get {
                return ((string)(this["PowerSearch_Matches"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<OL>(?<OLRegion>.*?)</OL>")]
        public string PowerSearch_OLRegion {
            get {
                return ((string)(this["PowerSearch_OLRegion"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("(?<LIRegion><LI>.*?</LI>)")]
        public string PowerSearch_LIRegion {
            get {
                return ((string)(this["PowerSearch_LIRegion"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<li>.*?/title/tt(?<Code>\\d{7})/\">(?<Title>.*?)[(](?<Year>(?:[?]{4}|\\d{4}))[)/](?<" +
            "Flags>.*?)(?<Rating>\\d{1,2}[.]\\d).*?(?<Votes>\\d*?)[ ]votes[)](?<Akas>.*?)</li>")]
        public string PowerSearch_MainValues {
            get {
                return ((string)(this["PowerSearch_MainValues"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("[.]{3}aka(?<Title>.*?)[(](?:[?]|\\d){4}[)/]")]
        public string PowerSearch_AKA {
            get {
                return ((string)(this["PowerSearch_AKA"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("(?<LI><li>.*?/title/tt.*?</li>)")]
        public string MovieSearch_LI {
            get {
                return ((string)(this["MovieSearch_LI"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<li>.*?/title/tt(?<Code>\\d{7}).*?[\"]>(?<Title>.*?)</a>.*?[(](?<Year>(?:[?]{4}|\\d{" +
            "4}))[)/](?<Akas>.*?)</li>")]
        public string MovieSearch_MainValues {
            get {
                return ((string)(this["MovieSearch_MainValues"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("aka <em>(?<Title>.*?)</em>")]
        public string MovieSearch_AKA {
            get {
                return ((string)(this["MovieSearch_AKA"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<strong class=\"title\">(?:\\s){0,}(?<Title>.*?)(?:\\s){0,}<")]
        public string MovieDetails_Title {
            get {
                return ((string)(this["MovieDetails_Title"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("/Sections/Years/(?<Year>\\d{4})")]
        public string MovieDetails_Year {
            get {
                return ((string)(this["MovieDetails_Year"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<a name=\"poster\".*?src=\"(?<Poster>.*?)\"")]
        public string MovieDetails_Poster {
            get {
                return ((string)(this["MovieDetails_Poster"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("/Sections/Genres/(?<Genre>.*?)/")]
        public string MovieDetails_Genre {
            get {
                return ((string)(this["MovieDetails_Genre"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("User Rating:.*?(?<Rating>(?:\\d){1,2}[.]\\d).*?[(](?<Votes>\\d(?:\\d|,){0,}) votes")]
        public string MovieDetails_RatingVotes {
            get {
                return ((string)(this["MovieDetails_RatingVotes"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Also Known As:(?<Akas>.*?)<b class")]
        public string MovieDetails_AKAS {
            get {
                return ((string)(this["MovieDetails_AKAS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<br>(?<Aka>.*?)<br>")]
        public string MovieDetails_AKA {
            get {
                return ((string)(this["MovieDetails_AKA"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<b class=\"ch\"><a href=\"/mpaa\">MPAA</a>[:]</b>(?<MPAA>.*?)\\s*?<br>")]
        public string MovieDetails_MPAA {
            get {
                return ((string)(this["MovieDetails_MPAA"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<p class=\"plotpar\">(?<Plot>.*?)</p>")]
        public string MovieDetails_Plot {
            get {
                return ((string)(this["MovieDetails_Plot"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("class=\"blackcatheader\">Directed by(?<Directors>.*?)</table>")]
        public string MovieDetails_Directors {
            get {
                return ((string)(this["MovieDetails_Directors"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("/name/nm(?<Code>\\d{7})/[\"]>(?<Name>.*?)<")]
        public string MovieDetails_Director {
            get {
                return ((string)(this["MovieDetails_Director"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("class=\"blackcatheader\">Writing credits(?<Writers>.*?)</table>")]
        public string MovieDetails_Writers {
            get {
                return ((string)(this["MovieDetails_Writers"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("/name/nm(?<Code>\\d{7})/[\"]>(?<Name>.*?)<")]
        public string MovieDetails_Writer {
            get {
                return ((string)(this["MovieDetails_Writer"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("class=\"blackcatheader\">Cast<(?<Actors>.*?)</table>")]
        public string MovieDetails_Actors {
            get {
                return ((string)(this["MovieDetails_Actors"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("href=\\\"/name/nm(?<Code>\\d{7})/[\"]>(?<Name>.*?)<.*?</td>.*?</td>.*?<td.*?>(?:<a hr" +
            "ef=\"quotes\">){0,1}(?<Role>.*?)<")]
        public string MovieDetails_Actor {
            get {
                return ((string)(this["MovieDetails_Actor"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<strong class=\"title\">(?<Name>.*?)<")]
        public string PersonDetails_Name {
            get {
                return ((string)(this["PersonDetails_Name"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<a name=\"headshot\".*?src=\"(?<Headshot>.*?)\"")]
        public string PersonDetails_Headshot {
            get {
                return ((string)(this["PersonDetails_Headshot"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<div class=\"ch\">Date of birth.*?day=(?<Day>\\d{1,2})&month=(?<Month>.*?)\".*?BornIn" +
            "Year[?](?<Year>\\d{4})\"")]
        public string PersonDetails_DateOfBirth {
            get {
                return ((string)(this["PersonDetails_DateOfBirth"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.2) Gecko/20060308 Firefo" +
            "x/1.5.0.2\r\n")]
        public string UserAgent {
            get {
                return ((string)(this["UserAgent"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<form action=\"/Vote\" method=\"post\"><select name=\"(?<Code>.*?)\"")]
        public string MovieDetails_Code {
            get {
                return ((string)(this["MovieDetails_Code"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<a name=\"director\">Director.*?<ol>(?<Directed>.*?)</ol>")]
        public string PersonDetails_Directed {
            get {
                return ((string)(this["PersonDetails_Directed"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("(?<LIRegion><LI>.*?</LI>)")]
        public string PersonDetails_LIRegion {
            get {
                return ((string)(this["PersonDetails_LIRegion"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("href=\"/title/tt(?<Code>.*?)/\".*?>(?<Title>.*?)<.*?[(](?<Year>\\d{4}).*?[)](?<AKAs>" +
            ".*?</li>)")]
        public string PersonDetails_MainValues {
            get {
                return ((string)(this["PersonDetails_MainValues"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("aka (?<AKA>.*?)(?:<br>|</li>)")]
        public string PersonDetails_AKA {
            get {
                return ((string)(this["PersonDetails_AKA"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<a name=\"writer\">Writer.*?<ol>(?<Writed>.*?)</ol>")]
        public string PersonDetails_Writed {
            get {
                return ((string)(this["PersonDetails_Writed"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<a name=\"act.*?\">Act.*?<ol>(?<Acted>.*?)</ol>")]
        public string PersonDetails_Acted {
            get {
                return ((string)(this["PersonDetails_Acted"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<dt class=\"ch\">Mini biography</dt>(?:.*?)<dd><p class=\"biopar\">(?<Bio>.*?)</p>")]
        public string PersonDetails_Bio {
            get {
                return ((string)(this["PersonDetails_Bio"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<a name=\"act.*?\">Act(?:.*?)</a>(?:.*?)<table(?<MostVoted>.*?)</table>")]
        public string PersonDetails_MostVoted {
            get {
                return ((string)(this["PersonDetails_MostVoted"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("(?<TRRegion><tr>.*?</tr>)")]
        public string PersonDetails_TRRegion {
            get {
                return ((string)(this["PersonDetails_TRRegion"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<td align=\"right\">(?<Votes>.*?)</td>(?:.*?)href=\"/title/tt(?<Code>.*?)/\">")]
        public string PersonDetails_MostVotedMain {
            get {
                return ((string)(this["PersonDetails_MostVotedMain"]));
            }
        }
    }
}
