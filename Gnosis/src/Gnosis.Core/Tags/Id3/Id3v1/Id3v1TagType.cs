﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Tags.Id3.Id3v1
{
    public class Id3v1TagType
        : TagType
    {
        private Id3v1TagType(int id, string name, ITagDomain domain)
            : base(id, name, TagSchema.Id3v1, domain)
        {
        }

        static Id3v1TagType()
        {
            all.Add(Title);
            all.Add(Artist);
            all.Add(Album);
            all.Add(Year);
            all.Add(Comment);
            all.Add(Track);
            all.Add(Genre);
            //all.Add(TitleExtension);
            //all.Add(ArtistExtension);
            //all.Add(AlbumExtension);
            //all.Add(Speed);
            //all.Add(GenreExtension);
            //all.Add(StartTime);
            //all.Add(EndTime);
        }

        private static readonly IList<ITagType> all = new List<ITagType>();

        public static readonly ITagType Title = new Id3v1TagType(2, "Title", TagDomain.String);
        public static readonly ITagType Artist = new Id3v1TagType(3, "Artist", TagDomain.String);
        public static readonly ITagType Album = new Id3v1TagType(4, "Album", TagDomain.String);
        public static readonly ITagType Year = new Id3v1TagType(5, "Year", TagDomain.PositiveInteger);
        public static readonly ITagType Comment = new Id3v1TagType(6, "Comment", TagDomain.String);
        public static readonly ITagType Track = new Id3v1TagType(7, "Track", TagDomain.PositiveInteger);
        public static readonly ITagType Genre = new Id3v1TagType(8, "Genre", TagDomain.String);
        //public static readonly ITagType TitleExtension = new Id3v1TagType(9, "Title Extension", TagDomain.String);
        //public static readonly ITagType ArtistExtension = new Id3v1TagType(10, "Artist Extension", TagDomain.String);
        //public static readonly ITagType AlbumExtension = new Id3v1TagType(11, "Album Extension", TagDomain.String);
        //public static readonly ITagType Speed = new Id3v1TagType(12, "Speed", TagDomain.PositiveInteger);
        //public static readonly ITagType GenreExtension = new Id3v1TagType(13, "Genre Extension");
        //public static readonly ITagType StartTime = new Id3v1TagType(14, "Start Time");
        //public static readonly ITagType EndTime = new Id3v1TagType(15, "End Time");

        public static IEnumerable<ITagType> GetAll()
        {
            return all;
        }
    }
}
