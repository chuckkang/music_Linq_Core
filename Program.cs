using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
			IEnumerable<Artist> artVern = Artists.Where( str => str.Hometown == "Mount Vernon");
			foreach(var art in artVern){
				Console.WriteLine(art.ArtistName + "--art");
			}
			
            //Who is the youngest artist in our collection of artists?
			Artist youngest = new Artist();
			Console.WriteLine(youngest.Age + "--youngest");
			foreach(var young in Artists){
				if (youngest.Age == 0){
					youngest = young;
				} else {
					youngest = (youngest.Age < young.Age ? youngest : young);
				}
			}
			Console.WriteLine(youngest.ArtistName + ": " + youngest.Age);

            //Display all artists with 'William' somewhere in their real name
			IEnumerable<Artist> will = Artists.Where(str => str.RealName.IndexOf("William")>0);
			foreach (var william in will){
				Console.WriteLine(william.RealName + "--" + william.ArtistName);
			}

            // Display the 3 oldest artist from Atlanta
			// select top 3 artists from atlanta orderby artist age desc.
			IEnumerable<Artist> oldest = Artists.Where(old => old.Hometown == "Atlanta").OrderByDescending(old => old.Age ).Take(3);
			foreach(var oldArtist in oldest){
				Console.WriteLine(oldArtist.ArtistName + ":" + oldArtist.Hometown + ":" + oldArtist.Age);
			}

            //(Optional) Display the Group Name of all groups that have members that are not from New York City
			var nonyc = Groups.Join(Artists.Where(notny => notny.Hometown != "New York City"), group => group.Id, artist => artist.GroupId, (group, artist)=>{
				return group;
			}).Distinct();
			PrintLine(nonyc);
            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
			// select wutang from groups then select all members from 

			var wutang = Artists.Join(Groups.Where(grp => grp.GroupName == "Wu-Tang Clan"),  art => art.GroupId, group => group.Id, (art, grp) => { return art; });

        }

		static void PrintLine(IEnumerable<Artist> artists){
			//separate sections
			const String separator = "*****************************";
			Console.WriteLine(separator);
			for (int i = 0; i < artists.Count(); i++)
			{
				Console.WriteLine(artists.ElementAt(i).ArtistName);
			}
			Console.WriteLine(separator);

		}
		
		//overloaded method
		static void PrintLine(IEnumerable<Group> groups){
			const String seperator = "*****************************";
			Console.WriteLine(seperator);
			foreach (var group in groups)
			{
				Console.WriteLine(group.GroupName);
			}
			Console.WriteLine(seperator);
		}
    }
}
